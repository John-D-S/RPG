using Menu;

using Saving;

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
	[Header("-- Main Menu Relevant Fields --")]
	[SerializeField] private CharacterCustomizer characterCustomizer;
	private static GameSave gameSaveToLoad;

	[Header("-- Game Scene Relevant Fields --")] 
	[SerializeField] private AppearanceManager playerAppearanceManager;
	[SerializeField] private SkillManager playerSkillManager;
	[SerializeField] private ProgressionManager progressionManager;

	public void LoadNewLevel()
	{
		if(SceneManager.GetActiveScene().name == "Menu")
		{
			if(characterCustomizer.CharacterName != string.Empty)
			{
				gameSaveToLoad = SaveLoadSystem.theSaveLoadSystem.gameData.GameSaveWithName(characterCustomizer.CharacterName);
				SceneManager.LoadScene(1);
			}
		}
	}

	public void LoadLevel(int _levelIndex)
	{
		if(SceneManager.GetActiveScene().name == "Menu")
		{
			gameSaveToLoad = SaveLoadSystem.theSaveLoadSystem.gameData.gameSaves[_levelIndex];
			SceneManager.LoadScene(1);
		}
	}

	private void Start()
	{
		if(SceneManager.GetActiveScene().name != "Menu")
		{
			GameSave gameSave;
			if(gameSaveToLoad != null)
			{
				gameSave = gameSaveToLoad;
			}
			else if(SaveLoadSystem.theSaveLoadSystem.gameData.gameSaves.Count > 0)
			{
				gameSave = SaveLoadSystem.theSaveLoadSystem.gameData.gameSaves[0];
			}
			else
			{
				gameSave = new GameSave("default", new SkillData(), new AppearanceData(), new ProgressData());
			}
			playerAppearanceManager.ApplyAppearenceData(gameSave.appearanceData);
			playerSkillManager.ApplySkillData(gameSave.skillData);
			progressionManager.ApplyProgressionData(gameSave.progressData);
		}
	}
}
