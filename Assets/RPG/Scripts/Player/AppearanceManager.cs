using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ApperancePart
{
    Hair,
    Head,
    Clothes,
    Glove,
    Shoe
}

public class AppearanceManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> hairs;
    public int NumberOfHairs => hairs.Count;
    [SerializeField] private List<GameObject> heads;
    public int NumberOfHeads => heads.Count;
    [SerializeField] private List<GameObject> clothes;
    public int NumberOfClothes => clothes.Count;
    [SerializeField] private List<GameObject> gloves;
    public int NumberOfGloves => gloves.Count;
    [SerializeField] private List<GameObject> shoes;
    public int NumberOfShoes => shoes.Count;

    public void ChangeAppearance(ApperancePart _part, int index)
    {
        List<GameObject> objectList;
        switch(_part)
        {
            case ApperancePart.Hair:
                objectList = hairs;
                break;
            case ApperancePart.Head:
                objectList = heads;
                break;
            case ApperancePart.Clothes:
                objectList = clothes;
                break;
            case ApperancePart.Glove:
                objectList = gloves;
                break;
            case ApperancePart.Shoe:
                objectList = shoes;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(_part), _part, null);
        }
        foreach(GameObject go in objectList)
        {
            go.SetActive(false);
        }
        objectList[index % objectList.Count].SetActive(true);
    }
}
