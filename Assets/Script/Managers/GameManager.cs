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

    private PlayableDirector gameOverCutsceneDirector;
    // Start is called before the first frame update
    void Start()
    {
        musicAudioSource.clip = musicMain;
        musicAudioSource.Play();
        gameOverCutsceneDirector = gameOverCutscene.GetComponent<PlayableDirector>();
        gameOverCutsceneDirector.stopped += GameOverCutsceneStopped;
        
    }
    private void GameOverCutsceneStopped(PlayableDirector d)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void PlayBossMusic()
    {
        musicAudioSource.Stop();
        musicAudioSource.clip = musicBoss;
        musicAudioSource.Play();
    }

    public void GameOver()
    {
        musicAudioSource.Stop();
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
