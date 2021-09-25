using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Dropdown))]
public class ResolutionDropdown : MonoBehaviour
{
    private TMP_Dropdown resolutionDropdown;
    private Resolution[] resolutions;
    
    private void SetResolution(int ResolutionIndex)
    {
        Resolution res = resolutions[ResolutionIndex];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);
    }
    
    /// <summary>
    /// Sets all the options in the resolutions Dropdown and sets te function.
    /// </summary>
    private void InitializeResolutions()
    {
        if(resolutionDropdown)
        {
            // set the list of resolutions to all the possible resolutions
            resolutions = Screen.resolutions;
            // clear the options in the dropdown
            resolutionDropdown.ClearOptions();
            // initialise the resolution options as a new list of strings
            List<string> resolutionOptions = new List<string>();
            int currentResolutionIndex = 0;
            for (int i = 0; i < resolutions.Length; i++)
            {
                string option = resolutions[i].width + "x" + resolutions[i].height;
                resolutionOptions.Add(option);
                if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
                {
                    currentResolutionIndex = i;
                }
            }
            // add all the resolutions in the resolution Options list
            resolutionDropdown.AddOptions(resolutionOptions);
            //set the currently sellected resolution to the current resolution of the screen
            resolutionDropdown.value = currentResolutionIndex;
            //refresh the shown value so that it displays correctly
            resolutionDropdown.RefreshShownValue();
        }
    }

    private void Awake()
    {
        resolutionDropdown = GetComponent<TMP_Dropdown>();
        InitializeResolutions();
        resolutionDropdown.onValueChanged.AddListener(delegate { SetResolution(resolutionDropdown.value); });
    }
}
