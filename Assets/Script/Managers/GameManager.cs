using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private AudioClip musicMain;
    [SerializeField] private AudioClip musicBoss;
    [SerializeField] private AudioSource musicAudioSource;
    [SerializeField] private GameObject gameOverCutscene;
    [SerializeField] private GameObject introCutscene;
    [SerializeField] private Canvas ui;
    [SerializeField] private Canvas tutorialCanvas;


    public static bool gameOver = false;
    public static Queue<string> tutorialsToPlay;
    private GameObject currentTutorial;

    private PlayableDirector gameOverCutsceneDirector;
    private PlayableDirector introCutsceneDirector;
    // Start is called before the first frame update

    private bool gameStarted = false;
    void Start()
    {
        ui.enabled = false;
        gameOverCutsceneDirector = gameOverCutscene.GetComponent<PlayableDirector>();
        gameOverCutsceneDirector.stopped += GameOverCutsceneStopped;
        introCutsceneDirector = introCutscene.GetComponent<PlayableDirector>();
        introCutsceneDirector.stopped += IntroCutsceneStopped;
        gameOver = false;
        tutorialsToPlay = new Queue<string>();
        CharacterMovement.LockControls();
    }
    private void GameOverCutsceneStopped(PlayableDirector d)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void IntroCutsceneStopped(PlayableDirector d)
    {
        musicAudioSource.clip = musicMain;
        musicAudioSource.Play();
        ui.enabled = true;
        CharacterMovement.UnlockControls();
    }
    public void PlayBossMusic()
    {
        musicAudioSource.Stop();
        musicAudioSource.clip = musicBoss;
        musicAudioSource.Play();
    }

    private void Update()
    {
        ShowNextTutorial();
    }

    public void GameOver()
    {
        gameOver = true;
        musicAudioSource.Stop();
        gameOverCutsceneDirector.Play();
        ui.enabled = false;
        // Game over script
        // Timeline
        Debug.Log("Game Over!");
    }

    private void ShowNextTutorial()
    {
        if(tutorialsToPlay.Count > 0 && currentTutorial == null)
        {
            string tutorialName = tutorialsToPlay.Dequeue();
            currentTutorial = tutorialCanvas.transform.Find(tutorialName).gameObject;
            currentTutorial.SetActive(true);
            StartCoroutine(WaitForTutorialCompletion());
        }
    } 

    IEnumerator WaitForTutorialCompletion()
    {
        yield return new WaitForSeconds(5);
        currentTutorial.SetActive(false);
        currentTutorial = null;
    }

}
