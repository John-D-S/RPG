using System;
using System.Collections;
using System.Collections.Generic;

using UnityEditor;

using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private int inventoryRowWidth;
    [SerializeField] private int inventoryRowHeight;
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private GameObject inventorySlotPrefab;
    [SerializeField] private GameObject inventoryRowPrefab;
    
    private List<ItemType> inventoryItems = new List<ItemType>();
    [SerializeField] private RectTransform itemPanel;

    public bool tryAddItemToInventory;
}
