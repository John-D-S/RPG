using Saving;

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(AppearanceManager))]
public class PlayerController : MonoBehaviour
{
    [Header("-- Movement Settings --")] 
    [SerializeField, Tooltip("how fast the character walks in meters/second")] private float walkingSpeed;
    [SerializeField, Tooltip("how fast the character runs in Meters/second")] private float runningforce;
    [SerializeField] private int maxJumps = 2;
    [SerializeField, Tooltip("")] private float jumpForce;
    [SerializeField, Tooltip("")] private float dashForce;
    [SerializeField] private float turnLerpSpeed;

    [Header("-- Camera Settings --")] 
    [SerializeField] private Vector3 firstPersonOffset = new Vector3(0, 1.5f, 0);
    [SerializeField] private bool thirdPerson;
    [SerializeField, Tooltip("The camera that rotates around the player")] private GameObject cameraGameObject;
    [SerializeField, Tooltip("The maximum angle the camera can look up or down.")] private float maxVerticalCameraAngle = 85;
    [SerializeField, Tooltip("What should be the camera's position in relation to the player.")] private Vector3 cameraOffset = Vector3.back * 10;
    [SerializeField, Tooltip("what position in relation to the player should the camera be looking at.")] private Vector3 cameraLookPosition = Vector3.up;

    private AppearanceManager appearanceManager;
    private Rigidbody rigidBody;
    private float movementVelocity;
    private Vector3 movementDireciton;
    private bool dashThisFrame;
    private bool jumpThisFrame;
    
    private float currentCameraXRotation = 0;
    private float currentCameraYRotation = 0;

    private Vector3 lastFramePosition;
    private Vector3 jumpVelocity = Vector3.zero;
    private Vector3 characterVelocity = Vector3.zero;

    //
    private int jumpsLeft;
    private bool jumpHeldDown;
    private float touchingGround = 0;
    private Vector3 groundNormal;

    private float dashCooldown;
    private float timeSinceLeftGround;
    private float timeSinceJump;

    //reset the doublejump when making contact with an object
    private void OnCollisionEnter(Collision collision)
    {
        if (jumpsLeft != maxJumps)
        {
            //for each contact point in the collision
            for (int i = 0; i < collision.contactCount; i++)
            {
                if (collision.GetContact(i).normal.y >= -0.1)
                {
                    jumpsLeft = maxJumps;
                }
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        touchingGround = 1;
        timeSinceLeftGround = 0;

        //get the average normal of all contact points
        Vector3 contactNormalSum = new Vector3(0, 0, 0);
        int noOfContacts = collision.contactCount;
        for (int i = 0; i < noOfContacts; i++)
        {
            contactNormalSum += collision.GetContact(i).normal;
        }
        groundNormal = contactNormalSum / noOfContacts;
    }

    private void OnCollisionExit(Collision collision)
    {
        touchingGround = 0;
        groundNormal = Vector3.up;
    }
    
    
    void FixedUpdate()
    {
        if (jumpThisFrame && jumpsLeft > 0)
        {
            if (timeSinceLeftGround < 0.25f && jumpsLeft == maxJumps)
            {
                rigidBody.AddForce(groundNormal.normalized * jumpForce, ForceMode.Impulse);
            }
            if (jumpsLeft == 1)
            {
                rigidBody.velocity = new Vector3(rigidBody.velocity.x, rigidBody.velocity.y * 0.5f, rigidBody.velocity.z);
            }
            rigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpsLeft -= 1;
        }
        if (timeSinceLeftGround < 0.3)
        {
            timeSinceLeftGround += Time.fixedDeltaTime;
        }

        if (dashCooldown > 0)
        {
            dashCooldown -= Time.fixedDeltaTime;
        }
        if (dashThisFrame && dashCooldown <= 0)
        {
            rigidBody.AddForce(cameraGameObject.transform.rotation * (dashForce * Vector3.forward), ForceMode.Impulse);
            dashCooldown = 1f;
        }
        jumpThisFrame = false;
        dashThisFrame = false;

        //sprinting is always on when touching the ground
        float forceToAdd = (touchingGround * movementVelocity);

        Vector3 force = movementDireciton * forceToAdd;

        Vector3 travellingDir = rigidBody.velocity;
        float dumbFriction = touchingGround * ( travellingDir.magnitude);
        Vector3 dumbFrictionDir = new Vector3(-travellingDir.x, 0, -travellingDir.z).normalized;
        Vector3 dumbFrictionForce = dumbFriction * dumbFrictionDir;

        rigidBody.AddForce(dumbFrictionForce);
        rigidBody.AddForce(force);
    }
    
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            thirdPerson = !thirdPerson;
            appearanceManager.SetVisibility(thirdPerson);
        }
        if(Input.GetMouseButtonDown(0))
        {
            dashThisFrame = true;
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            jumpThisFrame = true;
        }
        
        movementDireciton = Quaternion.Euler(0, cameraGameObject.transform.eulerAngles.y, 0) * new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        transform.LookAt(Vector3.Lerp(transform.position + transform.forward, transform.position + movementDireciton, turnLerpSpeed * Time.deltaTime), Vector3.up);
        bool isSprinting = Input.GetKey(KeyCode.LeftShift);
        if(Mathf.Abs(Input.GetAxisRaw("Horizontal")) + Mathf.Abs(Input.GetAxisRaw("Vertical")) > 0.1f)
        {
            movementVelocity = isSprinting ? runningforce : walkingSpeed;
        }
        else
        {
            movementVelocity = 0;
        }
        
        if(cameraGameObject)
        {
            if(thirdPerson)
            {
                currentCameraXRotation += Input.GetAxisRaw("Mouse Y");
                currentCameraYRotation += Input.GetAxisRaw("Mouse X");
                //clamp the camera rotation to be less than the max and greater than the min
                currentCameraXRotation = Mathf.Clamp(currentCameraXRotation, -maxVerticalCameraAngle, maxVerticalCameraAngle);
                //set the position and rotation of the camera according to the current camera rotation variables.
                cameraGameObject.transform.position = gameObject.transform.position + Quaternion.Euler(-currentCameraXRotation, currentCameraYRotation, 0) * cameraOffset;
                cameraGameObject.transform.LookAt(transform.position + cameraLookPosition);
            }
            else
            {
                currentCameraXRotation -= Input.GetAxisRaw("Mouse Y");
                currentCameraYRotation += Input.GetAxisRaw("Mouse X");
                //clamp the camera rotation to be less than the max and greater than the min
                currentCameraXRotation = Mathf.Clamp(currentCameraXRotation, -maxVerticalCameraAngle, maxVerticalCameraAngle);
                //set the position and rotation of the camera according to the current camera rotation variables.
                cameraGameObject.transform.rotation = Quaternion.Euler(currentCameraXRotation, currentCameraYRotation, 0);
                Vector3 cameraPosition = Vector3.Lerp(cameraGameObject.transform.position, gameObject.transform.position + firstPersonOffset, 0.25f);
                cameraGameObject.transform.position = cameraPosition;
            }
        }
    }

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        appearanceManager = GetComponent<AppearanceManager>();
        rigidBody = GetComponent<Rigidbody>();
        lastFramePosition = transform.position;
    }
}
