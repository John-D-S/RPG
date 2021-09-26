using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Saving
{
    [System.Serializable]
    public class SkillData
    {
        public int freeSkillPoints;
        
        public int health;
        public int stamina;
        public int speed;
    }

    [System.Serializable]
    public class AppearanceData
    {
        public int characterHair;
        public int characterHead;
        public int characterClothes;
        public int characterGloves;
        public int characterShoes;
    }

    [System.Serializable]
    public class ProgressData
    {
        public int currentCheckpoint;
        public List<string> questsCompleted = new List<string>();
    }
    
    /// <summary>
    /// contains all the data needed to save and load levels;
    /// </summary>
    [System.Serializable]
    public class GameSave
    {
        public string name;

        public SkillData skillData;
        public AppearanceData appearanceData;
        public ProgressData progressData;
        
        public GameSave(string _name, SkillData _skillData, AppearanceData _appearanceData, ProgressData _progressData)
        {
            name = _name;
            skillData = _skillData;
            appearanceData = _appearanceData;
            progressData = _progressData;
        }
    }

    /// <summary>
    /// used for saving;
    /// </summary>
    [System.Serializable]
    public class GameData
    {
        //these are where the high scores are stored
        public List<GameSave> gameSaves = new List<GameSave>();
        
        /// <summary>
        /// returns the index of gameSaves at which the name, "_name" appears;
        /// </summary>
        public int IndexOfName(string _name)
        {
            for (int i = 0; i < gameSaves.Count; i++)
            {
                if (gameSaves[i].name == _name)
                    return i;
            }
            return -1;
        }

        public GameSave GameSaveWithName(string _name)
        {
            foreach(GameSave gameSave in gameSaves)
            {
                if(gameSave.name == _name)
                {
                    return gameSave;
                }
            }
            return null;
        }

        public bool RemoveGameSaveWithName(string _name)
        {
            GameSave saveToRemove = GameSaveWithName(_name);
            if(saveToRemove != null)
                return gameSaves.Remove(saveToRemove);
            return false;
        }
        
        /// <summary>
        /// Adds a GameSave with name "name" and score "score" to the list of highscores and sorts it and trims it if the number of highscores excedes 8
        /// </summary>
        public void AddGameSave(GameSave _gameSave)
        {
            int indexOfName = IndexOfName(_gameSave.name);
            if (indexOfName > 0)
            {
                gameSaves.Add(_gameSave);
            }
            else
            {
                gameSaves[indexOfName] = _gameSave;
            }
        }
    }
}