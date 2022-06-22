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
    [SerializeField] private GameObject Boss2PhaseCutscene;
    [SerializeField] private Canvas ui;
    [SerializeField] private Canvas tutorialCanvas;


    public static bool gameOver = false;
    public static Queue<string> tutorialsToPlay;
    private GameObject currentTutorial;

    private PlayableDirector gameOverCutsceneDirector;
    private PlayableDirector introCutsceneDirector;
    private PlayableDirector Boss2PhaseCutsceneDirector;
    // Start is called before the first frame update

    public static bool gameStarted = false;
    public static bool boss2PhaseCutsceneEnded = false;

    private void Awake()
    {
        tutorialsToPlay = new Queue<string>();
    }
    void Start()
    {
        ui.enabled = false;
        gameOver = false;
        gameStarted = false;
        boss2PhaseCutsceneEnded = false;

        gameOverCutsceneDirector = gameOverCutscene.GetComponent<PlayableDirector>();
        gameOverCutsceneDirector.stopped += GameOverCutsceneStopped;

        introCutsceneDirector = introCutscene.GetComponent<PlayableDirector>();
        introCutsceneDirector.stopped += IntroCutsceneStopped;

        Boss2PhaseCutsceneDirector = Boss2PhaseCutscene.GetComponent<PlayableDirector>();
        Boss2PhaseCutsceneDirector.stopped += Boss2PhaseCutsceneStopped;
        CharacterMovement.LockControls();
    }
    private void GameOverCutsceneStopped(PlayableDirector d)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void Update()
    {
        ShowNextTutorial();
    }

    private void IntroCutsceneStopped(PlayableDirector d)
    {
        musicAudioSource.clip = musicMain;
        musicAudioSource.Play();
        ui.enabled = true;
        gameStarted = true;
        CharacterMovement.UnlockControls();
    }


    private void Boss2PhaseCutsceneStopped(PlayableDirector d)
    {
        musicAudioSource.clip = musicBoss;
        musicAudioSource.Play();
        ui.enabled = true;
        gameStarted = true;
        CharacterMovement.UnlockControls();
    }


    public void PlayBossMusic()
    {
        musicAudioSource.Stop();
        musicAudioSource.clip = musicBoss;
        musicAudioSource.Play();
    }

    public void GameOver()
    {
        gameOver = true;
        musicAudioSource.Stop();
        gameOverCutsceneDirector.Play();
        ui.enabled = false;
    }

    private void ShowNextTutorial()
    {
        if(tutorialsToPlay.Count > 0 && currentTutorial == null && gameStarted)
        {
            string tutorialName = tutorialsToPlay.Dequeue();
            currentTutorial = tutorialCanvas.transform.Find(tutorialName).gameObject;
            currentTutorial.SetActive(true);
            StartCoroutine(WaitForTutorialCompletion());
        }
    } 

    IEnumerator WaitForTutorialCompletion()
    {
        yield return new WaitForSeconds(8);
        currentTutorial.SetActive(false);
        currentTutorial = null;
    }

}
