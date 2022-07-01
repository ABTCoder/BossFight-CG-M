using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] Slider masterSlider;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider mouseSens;

    [SerializeField] private AudioMixer gameMixer;


    private void Start()
    {
        masterSlider.value = PlayerPrefs.GetFloat("Master", 80f);
        musicSlider.value = PlayerPrefs.GetFloat("Music", 80f);
        mouseSens.value = PlayerPrefs.GetInt("Sensitivity", 8);
    }

    public void OnMasterVolumeChange()
    {
        gameMixer.SetFloat("Master", -80f + masterSlider.value);
        PlayerPrefs.SetFloat("Master", masterSlider.value);
    }

    public void OnMusicVolumeChange()
    {
        gameMixer.SetFloat("Music", -80f + musicSlider.value);
        PlayerPrefs.SetFloat("Music", musicSlider.value);
    }

    public void OnSensitivityChange()
    {
        PlayerPrefs.SetInt("Sensitivity", (int) mouseSens.value);
        CharacterMovement.ChangeSensitivity((int) mouseSens.value);
    }

    private void OnDisable()
    {
        PlayerPrefs.Save();
    }
}
