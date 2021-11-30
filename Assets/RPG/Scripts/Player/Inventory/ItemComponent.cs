using System;
using System.Collections;
using System.Collections.Generic;

using UnityEditor;

using UnityEngine;
using UnityEngine.Serialization;

public class ItemComponent : MonoBehaviour
{
	[HideInInspector] public ItemType itemTypeType;
	[SerializeField] private GameObject highlightIndicator;
	public static Dictionary<GameObject, ItemComponent> allItemsByGameObject;

	private void Start()
	{
		highlightIndicator.SetActive(false);
	}

	public bool IsHighlighted
	{
		get
		{
			if(highlightIndicator)
			{
				return highlightIndicator.activeSelf;
			}
			return false;
		}
		set
		{
			if(highlightIndicator)
			{
				highlightIndicator.SetActive(value);
			}
		}
	}
}
