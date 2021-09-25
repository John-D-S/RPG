using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Dropdown))]
public class GraphicsDropdown : MonoBehaviour
{
    private TMP_Dropdown graphicsDropdown;
    
    private void InitializeGraphics()
    {
        // get the list of grapics tier names (low, high, ultra, etc..) 
        List<string> graphicalNames = QualitySettings.names.ToList();
        // makes sure the drop down is cleaned first
        graphicsDropdown.ClearOptions();
        // assigns the names to the dropDown
        graphicsDropdown.AddOptions(graphicalNames);

        if(PlayerPrefs.HasKey("QualityLevel"))
        {
            graphicsDropdown.value = PlayerPrefs.GetInt("QualityLevel");
            SetGraphics(graphicsDropdown.value);
        }
        graphicsDropdown.value = QualitySettings.GetQualityLevel();
        graphicsDropdown.RefreshShownValue();
    }
    
    /// <summary>
    /// Sets the graphics settings of the game
    /// </summary>
    private void SetGraphics(int i)
    {
        QualitySettings.SetQualityLevel(i, true);
        PlayerPrefs.SetInt("QualityLevel", i);
    }

    private void Awake()
    {
        graphicsDropdown = GetComponent<TMP_Dropdown>();
        InitializeGraphics();
        graphicsDropdown.onValueChanged.AddListener(delegate { SetGraphics(graphicsDropdown.value); });
    }
}
