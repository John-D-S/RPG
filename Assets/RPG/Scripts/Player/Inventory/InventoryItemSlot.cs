using System.Collections;
using System.Collections.Generic;

using UnityEditor;

using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class InventoryItemSlot : MonoBehaviour
{
    [SerializeField] private int maxItems;
    private int numberOfItems;
    private ItemType containedItem = ScriptableObject.CreateInstance<ItemType>();

    /// <summary>
    /// attempts to add _numberToAdd items of _itemType to 
    /// </summary>
    public int TryAddItemToSlot(ItemType _itemType, int _numberToAdd)
    {
        if(containedItem == null || containedItem.ItemName == _itemType.ItemName)
        {
            containedItem = _itemType;
            int totalItems = numberOfItems + _numberToAdd;
            if(totalItems > maxItems)
            {
                numberOfItems = maxItems;
                return maxItems - totalItems;
            }
            return totalItems;
        }
        return _numberToAdd;
    }
}
