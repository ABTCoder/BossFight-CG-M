using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update

    public AudioClip playSound;
    public AudioSource audioSource;

    [SerializeField] private AudioMixer gameMixer;

    private void Awake()
    {
        gameMixer.SetFloat("Master", -80f + PlayerPrefs.GetFloat("Master", 80f));
        gameMixer.SetFloat("Music", -80f + PlayerPrefs.GetFloat("Music", 80f));
        Cursor.visible = true;
    }
    public void PlayGame()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(playSound);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        gameObject.SetActive(false);
    }


    public void Quit()
    {
        Application.Quit();
    }

}
