using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager instance;

    public TMP_Dropdown resolutionDropdown;
    Resolution[] resolutions;

    public Slider bgmVolumeSlider;
    public Slider sfxVolumeSlider;
    public Button bgmVolumeMuter;
    public Button sfxVolumeMuter;

    [SerializeField] private Sprite muteSprite;
    [SerializeField] private Sprite unmuteSprite;

    //int[,] resolutionsArray = new int[,] { { 1920, 1080 }, { 1280, 720 } };

    private bool isShown;

    // Start is called before the first frame update
    void Start()
    {
        SetFullScreen(true);
        //Screen.SetResolution(1920, 1080, true);
        //transform.GetChild(0).gameObject.SetActive(false);
        //isShown = false;
        //resolutionDropdown.ClearOptions();

        //List<string> options = new List<string>();

        //int currentResolutionIndex = 0;
        //for (int i = 0; i < resolutionsArray.GetLength(i) ; i++)
        //{
        //    for (int j = 0; j < resolutionsArray.GetLength(j); j++)
        //    {
        //        string option = resolutionsArray[i,j] + " x " + resolutions[i].height;
        //        options.Add(option);

        //        if (resolutions[i].width == Screen.currentResolution.width &&
        //           resolutions[i].height == Screen.currentResolution.height)
        //        {

        //            currentResolutionIndex = i;
        //        }
        //    }



        //// Resolutions
        //resolutions =  Screen.resolutions;
        ////resolutions.ap
        //resolutionDropdown.ClearOptions();

        //List<string> options = new List<string>();

        //int currentResolutionIndex = 0; 
        //for (int i = 0; i < resolutions.Length; i++)
        //{
        //    string option = resolutions[i].width + " x " + resolutions[i].height;
        //    options.Add(option);

        //    if(resolutions[i].width == Screen.currentResolution.width &&
        //       resolutions[i].height == Screen.currentResolution.height)
        //    {

        //        currentResolutionIndex = i;
        //    }
        //}

        //resolutionDropdown.AddOptions(options);
        //resolutionDropdown.value = currentResolutionIndex;
        //resolutionDropdown.RefreshShownValue();
    }

    // Hide settings button during minigame
    void Update()
    {
        if (SceneManager.GetActiveScene().name != "MainMenuScene")
        {
            if (GameManager.instance.currentState == GameState.Minigame)
            {
                transform.GetChild(1).gameObject.SetActive(false);
            }
            else
                transform.GetChild(1).gameObject.SetActive(true);
        }
    }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
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
        if(resolutionIndex == 0)
        {
            Screen.SetResolution(1920, 1080, true);
        }
        else if(resolutionIndex == 1)
        {
            Screen.SetResolution(1280, 720, false);
        }

        //Resolution resolution = resolutions[resolutionIndex];
        //Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void HideSettings()
    {
        //transform.gameObject.SetActive(false);
        transform.GetChild(0).gameObject.SetActive(false);
        isShown = false;
    }

    public void ToggleSettings()
    {
        if (isShown)
        {
            transform.GetChild(0).gameObject.SetActive(false);
            isShown = false;
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(true);
            isShown = true;
        }
    }

    public void CloseGame()
    {
        Application.Quit();
        //SceneLoader.instance.ChangeScene("MainMenuScene");
    }
}
