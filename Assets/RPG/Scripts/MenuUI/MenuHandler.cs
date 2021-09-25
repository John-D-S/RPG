using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.Audio;

namespace Menu
{
    /// <summary>
    /// holds the Static menuHandler
    /// </summary>
    public static class TheMenuHandler
    {
        public static MenuHandler theMenuHandler;
    }

    public class MenuHandler : MonoBehaviour
    {
        [Header("-- Scene Name Variables --")]
        [SerializeField, Tooltip("The name of the Menu Scene")]
        private string menuSceneName = "Menu";
        [SerializeField, Tooltip("The name of the Game Scene")]
        private string gameSceneName = "Main";

        [Header("-- Audio --")]
        [Tooltip("The master audio mixer")]
        public AudioMixer masterMixer;

        [Header("-- Menu Objects --")]
        [SerializeField, Tooltip("The pause/main menu")]
        private GameObject MenuPanel;

        #region Pausing
        private bool paused = false;
        public bool Paused
        {
            get
            {
                return paused;
            }
        }
        
        /// <summary>
        /// pauses the game
        /// </summary>
        public void Pause()
        {
            Time.timeScale = 0;
            if (MenuPanel)
                MenuPanel.SetActive(true);
            paused = true;
        }

        /// <summary>
        /// unpauses the gam
        /// </summary>
        public void Unpause()
        {
            paused = false;
            if (MenuPanel)
                MenuPanel.SetActive(false);
            Time.timeScale = 1;
        }
        #endregion
        
        #region Options

        #region Volume
        public void ChangeSFXVolume(float _volume)
        {
            if (masterMixer)
            {
                masterMixer.SetFloat("SFXVolume", _volume);
                PlayerPrefs.SetFloat("SFXVolume", _volume);
                PlayerPrefs.Save();
            }
        }

        public void ChangeMusicVolume(float _volume)
        {
            if (masterMixer)
            {
                masterMixer.SetFloat("MusicVolume", _volume);
                PlayerPrefs.SetFloat("MusicVolume", _volume);
                PlayerPrefs.Save();
            }
        }

        public void SetMute(bool isMuted)
        {
            if (masterMixer)
            {
                if (isMuted)
                    masterMixer.SetFloat("MasterVolume", -80f);
                else
                    masterMixer.SetFloat("MasterVolume", 0f);
                PlayerPrefs.SetInt("IsMuted", isMuted ? 1 : 0);
                PlayerPrefs.Save();
            }
        }
        #endregion

        #endregion

        #region SceneSwitching
        /// <summary>
        /// Loads the game scene
        /// </summary>
        public void StartGame() => SceneManager.LoadScene(gameSceneName, LoadSceneMode.Single);
        /// <summary>
        /// Loads the main menu scene
        /// </summary>
        public void ReturnToMainMenu() => SceneManager.LoadScene(menuSceneName, LoadSceneMode.Single);
        #endregion

        #region Initialization
            
        /// <summary>
        /// initializes all the volume variables from playerprefs
        /// </summary>
        private void InitializeVolume()
        {
            if (PlayerPrefs.HasKey("MusicVolume"))
                ChangeMusicVolume(PlayerPrefs.GetFloat("MusicVolume"));
            if (PlayerPrefs.HasKey("SFXVolume"))
                ChangeSFXVolume(PlayerPrefs.GetFloat("SFXVolume"));
            if (PlayerPrefs.HasKey("IsMuted"))
                SetMute(PlayerPrefs.GetInt("IsMuted") == 1);
        }
        #endregion

        #region Saving
        /// <summary>
        /// doesn't work
        /// </summary>
        public void Save()
        {
            Debug.Log("Not Yet Implemented");
        }

        /// <summary>
        /// doesn't work
        /// </summary>
        public void Load()
        {
            Debug.Log("Not Yet Implemented");
        }
        #endregion

        /// <summary>
        /// if in the game scene, set the timescale to 1 and go to the menu scene
        /// if in the menu scene, quit the game
        /// </summary>
        public void Quit()
        {
            Time.timeScale = 1;
            if (gameObject.scene.name == gameSceneName)
                ReturnToMainMenu();
            else
            {
                Application.Quit();
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#endif
            }
        }

        private void Update()
        {
            if(SceneManager.GetActiveScene().name == gameSceneName && Input.GetAxis("Cancel") > 0.5f)
            {
                if(paused)
                    Unpause();
                else
                    Pause();
            }
        }

        private void Start()
        {
            InitializeVolume();
        }

        private void Awake()
        {
            TheMenuHandler.theMenuHandler = this;
        }
    }
}
