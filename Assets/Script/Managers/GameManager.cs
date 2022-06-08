using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private AudioSource musicMain;
    [SerializeField] private AudioSource musicBoss;
    // Start is called before the first frame update
    void Start()
    {
        musicMain.Play();
    }

    public void PlayBossMusic()
    {
        musicMain.Stop();
        musicBoss.Play();
    }

    public void GameOver()
    {
        // Game over script
        // Timeline
        Debug.Log("Game Over!");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
