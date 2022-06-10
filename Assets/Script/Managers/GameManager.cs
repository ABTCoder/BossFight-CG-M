using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private AudioSource musicMain;
    [SerializeField] private AudioSource musicBoss;
    [SerializeField] private GameObject gameOverCutscene;

    private PlayableDirector gameOverCutsceneDirector;
    // Start is called before the first frame update
    void Start()
    {
        musicMain.Play();
        gameOverCutsceneDirector = gameOverCutscene.GetComponent<PlayableDirector>();
        gameOverCutsceneDirector.stopped += GameOverCutsceneStopped;
        
    }
    private void GameOverCutsceneStopped(PlayableDirector d)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void PlayBossMusic()
    {
        musicMain.Stop();
        musicBoss.Play();
    }

    public void GameOver()
    {
        gameOverCutsceneDirector.Play();
        // Game over script
        // Timeline
        Debug.Log("Game Over!");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
