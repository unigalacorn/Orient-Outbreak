using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsManager : MonoBehaviour
{
    public TMP_Dropdown resolutionDropdown;
    Resolution[] resolutions;


    public Slider bgmVolumeSlider;
    public Slider sfxVolumeSlider;
    public Button bgmVolumeMuter;
    public Button sfxVolumeMuter;

    [SerializeField] private Sprite muteSprite;
    [SerializeField] private Sprite unmuteSprite;

    // Start is called before the first frame update
    void Start()
    {
        // Resolutions
        resolutions =  Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0; 
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if(resolutions[i].width == Screen.currentResolution.width &&
               resolutions[i].height == Screen.currentResolution.height)
            {

                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    // Slider UI sets bgm volume
    public void SetBGMVolume(float volumeValue)
    {
        AudioManager.instance.bgmGroup.audioMixer.SetFloat("bgmVol", Mathf.Log10(volumeValue) * 20);
    }

    public void SetSFXVolume(float volumeValue)
    {
        AudioManager.instance.sfxGroup.audioMixer.SetFloat("sfxVol", Mathf.Log10(volumeValue) * 20);
    }

    public void SetBGMMute()
    {
        if (bgmVolumeSlider.value != 0.0001f)
        {
            bgmVolumeSlider.value = 0.0001f;
            bgmVolumeMuter.image.sprite = muteSprite;
        }

        else
        {
            bgmVolumeSlider.value = 1f;
            bgmVolumeMuter.image.sprite = unmuteSprite;
        }
    }

    public void SetSFXMute()
    {
        if (sfxVolumeSlider.value != 0.0001f)
        {
            sfxVolumeSlider.value = 0.0001f;
            sfxVolumeMuter.image.sprite = muteSprite;
        }

        else
        {
            sfxVolumeSlider.value = 1f;
            sfxVolumeMuter.image.sprite = unmuteSprite;
        }
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void HideSettings()
    {
        transform.gameObject.SetActive(false);
    }
}