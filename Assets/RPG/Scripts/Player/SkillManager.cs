using Saving;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    private int maxHealth;
    public int MaxHealth => maxHealth;
    private int maxStamina;
    public int MaxStamina => maxStamina;
    private int speed;
    public int Speed => speed;
    
    public void ApplySkillData(SkillData _data)
    {
        maxHealth = _data.health;
        maxStamina = _data.stamina;
        speed = _data.speed;
    }
}
