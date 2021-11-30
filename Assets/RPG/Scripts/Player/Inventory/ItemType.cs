using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ItemType : ScriptableObject
{
	[SerializeField] private Sprite itemSprite;
	public Sprite ItemSprite => itemSprite;
	[SerializeField] private string itemName;
	public string ItemName => itemName;
	[SerializeField] GameObject itemGameObjectPrefab;

	private void OnValidate()
	{
		if(itemGameObjectPrefab)
		{
			ItemComponent itemGameObjectPrefabComponent = itemGameObjectPrefab.GetComponent<ItemComponent>();
			if(itemGameObjectPrefabComponent == null)
			{
				itemGameObjectPrefab = null;
			}
			else
			{
				itemGameObjectPrefabComponent.itemTypeType = this;
			}
		}
	}
}
