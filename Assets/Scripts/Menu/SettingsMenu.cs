using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer mixer;
    public TMP_Dropdown resolutionDropdown;
    Resolution[] resolutions;
    private void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);
            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value=currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

    }
    public void setResolution(int resolutionIndex)
    {
        Resolution resouliton = resolutions[resolutionIndex];
        Screen.SetResolution(resouliton.width, resouliton.height, Screen.fullScreen);
    }
    public void SetVolume(float volume)
    {
        mixer.SetFloat("MusicVolume", volume);
    }
    public void SetMusic(float volume)
    {
        mixer.SetFloat("SoundVolume", volume);

    }
    public void SetFullscreen(bool isfullscreen)
    {
        Screen.fullScreen = isfullscreen;
    }
}