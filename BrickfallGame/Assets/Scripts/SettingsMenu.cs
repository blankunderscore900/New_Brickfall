using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{

    public AudioMixer audioMixer;
    public Slider musicSlider;
    public Slider sfxSlider;

    public Button[] levelButtons;

    public AudioSource mainMenuMusic;
    //public AudioSource 

    public Dropdown resolutionDropdown;
    Resolution[] resolutions;



    void Start()
    {
        int levelReached = PlayerPrefs.GetInt("levelReached", 1);
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "BrickfallStartMenus")
        {
            mainMenuMusic.Play();
            Cursor.visible = true;
        }
        //music.volume = PlayerPrefs.GetFloat("MusicVolume");
        //music.volume = PlayerPrefs.GetFloat("MusicVolume");
        //fxVolume.value = PlayerPrefs.GetFloat("FxVolume");

        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i <resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if(resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }


        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (i + 1 > levelReached)
            levelButtons[i].interactable = false;
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        musicSlider.value = PlayerPrefs.GetFloat("Music", 0);
        sfxSlider.value = PlayerPrefs.GetFloat("SFX", 0);
    }

    public void SetResolution (int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("Music", volume);
    }

    public void SetSfxVolume(float volume)
    {
        audioMixer.SetFloat("SFX", volume);
    }

    public void QuitGame()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }

    public void StartStage1()
    {
        mainMenuMusic.Stop();
        SceneManager.LoadScene("BrickfallStage1");
    }

    public void StartStage2()
    {
        mainMenuMusic.Stop();
        SceneManager.LoadScene("BrickfallStage2");
    }

    public void StartStage3()
    {
        mainMenuMusic.Stop();
        SceneManager.LoadScene("BrickfallStage3");
    }

    public void StartStage4()
    {
        mainMenuMusic.Stop();
        SceneManager.LoadScene("BrickfallStage4");
    }

    public void LoadMenu()
    {
        GM.instance.unpausingGame();
        SceneManager.LoadScene("BrickfallStartMenus");
    }

    private void OnDisable()
    {
        float musicVolume = 0;
        float sfxVolume = 0;

        audioMixer.GetFloat("Music", out musicVolume);
        audioMixer.GetFloat("SFX", out sfxVolume);

        PlayerPrefs.SetFloat("Music", musicVolume);
        PlayerPrefs.SetFloat("SFX", sfxVolume);
        PlayerPrefs.Save();
    }

    public void ResetGame()
    {
        PlayerPrefs.SetInt("levelReached", 1);
        musicSlider.value = 0;
        sfxSlider.value = 0;
        SceneManager.LoadScene("BrickfallStartMenus");

    }


    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
}
