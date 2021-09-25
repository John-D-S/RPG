using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FullScreenToggle : MonoBehaviour
{
    private Toggle toggle;

    private void Awake()
    {
        toggle = GetComponent<Toggle>();
        toggle.onValueChanged.AddListener(delegate { SetFullscreen(toggle.isOn); } );
        
        if (PlayerPrefs.HasKey("isFullScreen"))
            toggle.isOn = PlayerPrefs.GetInt("IsFullScreen") == 1;
        else
            toggle.isOn = false;
    }

    private void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        PlayerPrefs.SetInt("IsFullscreen", isFullscreen ? 1 : 0);
        PlayerPrefs.Save();
    }
}
