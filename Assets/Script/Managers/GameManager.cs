using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private AudioClip musicMain;
    [SerializeField] private AudioClip musicBoss;
    [SerializeField] private AudioClip musicBoss2Phase;
    [SerializeField] private AudioSource musicAudioSource;
    [SerializeField] private GameObject gameOverCutscene;
    [SerializeField] private GameObject introCutscene;
    [SerializeField] private GameObject Boss2PhaseCutscene;
    [SerializeField] private GameObject TransitionCutscene;
    [SerializeField] private GameObject BossDeathCutscene;
    [SerializeField] private Canvas ui;
    [SerializeField] private Canvas tutorialCanvas;

    public static bool playingCutscene;
    private static UiControls uiControls;
    public static bool gameOver = false;
    public static Queue<string> tutorialsToPlay;
    public static GameObject currentTutorial;

    private PlayableDirector gameOverCutsceneDirector;
    private PlayableDirector introCutsceneDirector;
    private PlayableDirector Boss2PhaseCutsceneDirector;
    private PlayableDirector TransitionCutsceneDirector;
    private PlayableDirector BossDeathCutsceneDirector;
    // Start is called before the first frame update

    public static bool gameStarted = false;
    public static bool boss2PhaseCutsceneEnded = false;

    private void Awake()
    {
        uiControls = new UiControls();
        tutorialsToPlay = new Queue<string>();
        Cursor.visible = false;
    }
    void Start()
    {
        ui.enabled = false;
        gameOver = false;
        gameStarted = false;
        boss2PhaseCutsceneEnded = false;
        playingCutscene = true;
        currentTutorial = null;

        gameOverCutsceneDirector = gameOverCutscene.GetComponent<PlayableDirector>();
        gameOverCutsceneDirector.stopped += GameOverCutsceneStopped;

        introCutsceneDirector = introCutscene.GetComponent<PlayableDirector>();
        introCutsceneDirector.stopped += IntroCutsceneStopped;

        Boss2PhaseCutsceneDirector = Boss2PhaseCutscene.GetComponent<PlayableDirector>();
        Boss2PhaseCutsceneDirector.stopped += Boss2PhaseCutsceneStopped;

        TransitionCutsceneDirector = TransitionCutscene.GetComponent<PlayableDirector>();
        TransitionCutsceneDirector.stopped += BossDeathTransition;

        BossDeathCutsceneDirector = BossDeathCutscene.GetComponent<PlayableDirector>();
        BossDeathCutsceneDirector.stopped += BossDeathCutsceneStopped;
        CharacterMovement.LockControls();

        uiControls.Enable();
        uiControls.UI.SkipTutorial.performed += PressEnterToContinue;
    }
    private void GameOverCutsceneStopped(PlayableDirector d)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        playingCutscene = false;
    }

    private void Update()
    {
        ShowNextTutorial();
    }

    public static UiControls getUiController()
    {
        return uiControls;
    }


    private void IntroCutsceneStopped(PlayableDirector d)
    {
        musicAudioSource.clip = musicMain;
        musicAudioSource.Play();
        ui.enabled = true;
        gameStarted = true;
        CharacterMovement.UnlockControls();
        playingCutscene = false;
    }

    private void BossDeathTransition(PlayableDirector d)
    {
        BossDeathCutsceneDirector.Play();
    }


    private void Boss2PhaseCutsceneStopped(PlayableDirector d)
    {
        musicAudioSource.clip = musicBoss2Phase;
        musicAudioSource.Play();
        ui.enabled = true;
        gameStarted = true;
        CharacterMovement.UnlockControls();
        playingCutscene = false;
    }

    private void BossDeathCutsceneStopped(PlayableDirector d)
    {
        SceneManager.LoadScene(0);
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
        playingCutscene = true;
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
            Time.timeScale = 0f;
            CharacterMovement.LockControls();
            //StartCoroutine(WaitForTutorialCompletion());
        }
        if(currentTutorial != null)
        {
            Time.timeScale = 0f;
            CharacterMovement.LockControls();
        }
    } 

    IEnumerator WaitForTutorialCompletion()
    {
        yield return new WaitForSecondsRealtime(5);
        Time.timeScale = 1f;
        CharacterMovement.UnlockControls();
        currentTutorial.SetActive(false);
        currentTutorial = null;
    }



    private void PressEnterToContinue(InputAction.CallbackContext ctx)
    {
        if (currentTutorial != null)
        {
            Debug.Log("Sono qui");
            Time.timeScale = 1f;
            CharacterMovement.UnlockControls();
            currentTutorial.SetActive(false);
            currentTutorial = null;
        }
    }
}
