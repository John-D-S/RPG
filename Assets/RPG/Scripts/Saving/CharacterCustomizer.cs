using Saving;

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterCustomizer : MonoBehaviour
{
    [Header("-- Name Input --")] 
    [SerializeField] private SaveLoadSystem saveLoadSystem;
    [SerializeField] private TMP_InputField nameInput;
    
    [Header("-- Skill Customisation --")]
    [SerializeField] private int startingPoints = 10;
    [SerializeField] private TextMeshProUGUI pointsRemainingText;
    public int RemainingPoints => startingPoints - (health.CurrentValue + stamina.CurrentValue + speed.CurrentValue);
    [SerializeField] private UiIntCycler health;
    [SerializeField] private UiIntCycler stamina;
    [SerializeField] private UiIntCycler speed;

    [Header("-- Appearance Customisation --")]
    [SerializeField] private AppearanceManager appearanceManager;
    [SerializeField] private UiIntCycler hairCycler;
    [SerializeField] private UiIntCycler headCycler;
    [SerializeField] private UiIntCycler clothesCycler;
    [SerializeField] private UiIntCycler gloveCycler;
    [SerializeField] private UiIntCycler shoeCycler;

    private void Start()
    {
        health.cycleValue = false;
        stamina.cycleValue = false;
        speed.cycleValue = false;
        
        hairCycler.cycleValue = true;
        hairCycler.maxExclusiveValue = appearanceManager.NumberOfHairs;
        headCycler.cycleValue = true;
        headCycler.maxExclusiveValue = appearanceManager.NumberOfHeads;
        clothesCycler.cycleValue = true;
        clothesCycler.maxExclusiveValue = appearanceManager.NumberOfClothes;
        gloveCycler.cycleValue = true;
        gloveCycler.maxExclusiveValue = appearanceManager.NumberOfGloves;
        shoeCycler.cycleValue = true;
        shoeCycler.maxExclusiveValue = appearanceManager.NumberOfShoes;
        UpdateAppearance();
    }

    public void UpdateAppearance()
    {
        appearanceManager.ChangeAppearance(ApperancePart.Hair, hairCycler.CurrentValue);
        appearanceManager.ChangeAppearance(ApperancePart.Head, headCycler.CurrentValue);
        appearanceManager.ChangeAppearance(ApperancePart.Clothes, clothesCycler.CurrentValue);
        appearanceManager.ChangeAppearance(ApperancePart.Glove, gloveCycler.CurrentValue);
        appearanceManager.ChangeAppearance(ApperancePart.Shoe, shoeCycler.CurrentValue);
    }

    public void UpdateSkills()
    {
        bool canGoHigher = RemainingPoints > 0;
        health.canGoHigher = canGoHigher;
        stamina.canGoHigher = canGoHigher;
        speed.canGoHigher = canGoHigher;
        pointsRemainingText.text = $"Remaining Points: {RemainingPoints}";
    }

    public void SaveNewCharacter()
    {
        string name = nameInput.text;
        SkillData newSkillData = new SkillData();
        newSkillData.health = health.CurrentValue;
        newSkillData.speed = speed.CurrentValue;
        newSkillData.stamina = stamina.CurrentValue;
        AppearanceData newAppearanceData = new AppearanceData();
        newAppearanceData.characterHair = hairCycler.CurrentValue;
        newAppearanceData.characterHead = headCycler.CurrentValue;
        newAppearanceData.characterClothes = clothesCycler.CurrentValue;
        newAppearanceData.characterGloves = gloveCycler.CurrentValue;
        newAppearanceData.characterShoes = gloveCycler.CurrentValue;
        ProgressData newProgressData = new ProgressData();
        saveLoadSystem.Load();
        saveLoadSystem.gameData.gameSaves.Add(new GameSave(name, newSkillData, newAppearanceData, newProgressData));
        saveLoadSystem.Save();
    }
}
