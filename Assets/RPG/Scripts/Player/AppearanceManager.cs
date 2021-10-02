using Saving;

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

    private List<GameObject> selectedAppearance = new List<GameObject>();
    
    public void ApplyAppearenceData(AppearanceData _data)
    {
        ChangeAppearance(ApperancePart.Hair, _data.characterHair);
        ChangeAppearance(ApperancePart.Head, _data.characterHead);
        ChangeAppearance(ApperancePart.Clothes, _data.characterClothes);
        ChangeAppearance(ApperancePart.Glove, _data.characterGloves);
        ChangeAppearance(ApperancePart.Shoe, _data.characterShoes);
    }

    public void SetVisibility(bool _visibility)
    {
        foreach(GameObject go in selectedAppearance)
        {
            go.SetActive(_visibility);
        }
    }

    private void RemovePartFromSelectedAppearance(ApperancePart _part)
    {
        foreach(GameObject go in selectedAppearance)
        {
            switch(_part)
            {
                case ApperancePart.Hair:
                    if(hairs.Contains(go))
                    {
                        selectedAppearance.Remove(go);
                        return;
                    }
                    break;
                case ApperancePart.Head:
                    if(heads.Contains(go))
                    {
                        selectedAppearance.Remove(go);
                        return;
                    }
                    break;
                case ApperancePart.Clothes:
                    if(clothes.Contains(go))
                    {
                        selectedAppearance.Remove(go);
                        return;
                    }
                    break;
                case ApperancePart.Glove:
                    if(gloves.Contains(go))
                    {
                        selectedAppearance.Remove(go);
                        return;
                    }
                    break;
                case ApperancePart.Shoe:
                    if(shoes.Contains(go))
                    {
                        selectedAppearance.Remove(go);
                        return;
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(_part), _part, null);
            }
        }
    }
    
    public void ChangeAppearance(ApperancePart _part, int index)
    {   
        RemovePartFromSelectedAppearance(_part);
        
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
        selectedAppearance.Add(objectList[index % objectList.Count]);
        objectList[index % objectList.Count].SetActive(true);
    }
}
