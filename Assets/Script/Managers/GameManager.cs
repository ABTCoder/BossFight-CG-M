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

    // Update is called once per frame
    void Update()
    {
        
    }
}
