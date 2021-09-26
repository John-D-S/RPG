using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UiIntCycler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI valueDisplay;
    private int currentValue = 1;
    public int CurrentValue => currentValue;
    [System.NonSerialized] public bool canGoHigher = true;
    
    //this is only used when cyclevalue is true
    [System.NonSerialized] public int maxExclusiveValue;
    [System.NonSerialized] public bool cycleValue = false;

    public void ChangeValue(int _amount)
    {
        int actualAmount = _amount >= 0 ? 1 : -1;
        if(cycleValue)
            currentValue = (maxExclusiveValue + currentValue + actualAmount) % maxExclusiveValue;
        else
        {
            if(actualAmount >= 0)
            {
                if(canGoHigher)
                    currentValue += actualAmount;
            }
            else if(currentValue + actualAmount > 0)
            {
                currentValue += actualAmount;
            }
        } 
    }
    
    private void Update()
    {
        if(valueDisplay)
        {
            valueDisplay.text = currentValue.ToString();
        }
    }
}
