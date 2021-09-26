using Saving;

using System;
using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class LoadMenu : MonoBehaviour
{
    [SerializeField] private LevelLoader levelLoader;
    [SerializeField] private GameObject levelLoadButton;
    [SerializeField] private RectTransform levelLoadPanel;

    private void Start()
    {
        InitializeLevelButtons();
    }

    private void InitializeLevelButtons()
    {
        SaveLoadSystem.theSaveLoadSystem.Load();
        int i = 0;
        foreach(GameSave gameDataGameSave in SaveLoadSystem.theSaveLoadSystem.gameData.gameSaves)
        {
            GameObject newButton = Instantiate(levelLoadButton, levelLoadPanel);
            int i1 = i;
            newButton.GetComponent<Button>().onClick.AddListener(delegate { levelLoader.LoadLevel(i1); });
            newButton.GetComponentInChildren<TextMeshProUGUI>().text = SaveLoadSystem.theSaveLoadSystem.gameData.gameSaves[i1].name;
            i++;
        }
    }
    
    public void ClearAllSaves()
    {
        SaveLoadSystem.theSaveLoadSystem.ResetProgress();
        foreach (Transform child in levelLoadPanel) {
            Destroy(child.gameObject);
        }
    }
    
    private void OnValidate()
    {
        if(!levelLoadButton.GetComponent<Button>())
        {
            levelLoadButton = null;
        }
    }
}
