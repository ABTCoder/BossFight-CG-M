using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] Slider masterSlider;
    [SerializeField] Slider musicSlider;

    [SerializeField] private AudioMixer gameMixer;

    public void OnMasterVolumeChange()
    {
        gameMixer.SetFloat("Master", -80f + masterSlider.value);
    }

    public void OnMusicVolumeChange()
    {
        gameMixer.SetFloat("Music", -80f + musicSlider.value);
    }
}
