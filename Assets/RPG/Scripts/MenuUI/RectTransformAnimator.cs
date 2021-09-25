using System;
using System.Collections;
using System.Collections.Generic;

using UnityEditor;

using UnityEngine;

public enum AnimDirection
{
    Left,
    Right
}

[RequireComponent(typeof(RectTransform))]
public class RectTransformAnimator : MonoBehaviour
{
    [SerializeField] private bool StartOffScreen = false;
    [SerializeField, HideInInspector] private float targetHorizontalOffset = 0;
    [SerializeField, HideInInspector] private Canvas canvas;
    [SerializeField, HideInInspector] private RectTransform rectTransform;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = FindObjectOfType<Canvas>();
       
    }

    public void AnimateIn(bool _moveLeft)
    {
        Vector2 startOffSet = new Vector2(_moveLeft ? - canvas.pixelRect.width : canvas.pixelRect.width, 0) * 2;
        rectTransform.offsetMax = startOffSet;
        rectTransform.offsetMin = startOffSet;
        targetHorizontalOffset = 0;
    }
    
    public void AnimateOut(bool _moveLeft)
    {
        rectTransform.offsetMax = Vector2.zero;
        rectTransform.offsetMin = Vector2.zero;
        targetHorizontalOffset = (_moveLeft ? canvas.pixelRect.width : - canvas.pixelRect.width) * 2;
    }

    private void Update()
    {
        rectTransform.offsetMax = Vector2.Lerp(rectTransform.offsetMax, new Vector2(targetHorizontalOffset, 0), 5f * Time.deltaTime);
        rectTransform.offsetMin = Vector2.Lerp(rectTransform.offsetMin, new Vector2(targetHorizontalOffset, 0), 5f * Time.deltaTime);
    }

    private void OnValidate()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = FindObjectOfType<Canvas>();
        if(StartOffScreen)
        {
            Vector2 startOffSet = new Vector2(canvas.pixelRect.width, 0) * 2;
        #if UNITY_EDITOR
            EditorApplication.delayCall = () =>
            {
                rectTransform.offsetMax = rectTransform.offsetMin = startOffSet;
                targetHorizontalOffset = canvas.pixelRect.width * 2;
            };
        #endif
        }
        else
        {
        #if UNITY_EDITOR
            EditorApplication.delayCall = () =>
            {
                rectTransform.offsetMax = rectTransform.offsetMin = Vector2.zero; 
                targetHorizontalOffset = 0;
            };
        #endif
        }
    }
}
