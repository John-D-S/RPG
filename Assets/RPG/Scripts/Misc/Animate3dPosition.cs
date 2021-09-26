using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animate3dPosition : MonoBehaviour
{
    [SerializeField] private Vector3 Pos1;
    [SerializeField] private Vector3 Pos2;
    private Vector3 target;
    private void Start()
    {
        target = Pos1;
    }

    public void AnimateToPos1()
    {
        target = Pos1;
    }
    
    public void AnimateToPos2()
    {
        target = Pos2;
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, target, 0.05f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(Pos1, 0.5f);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(Pos2, 0.5f);
    }
}
