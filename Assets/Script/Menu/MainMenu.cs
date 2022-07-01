using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update

    public AudioClip playSound;
    public AudioSource audioSource;

    private void Awake()
    {
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
