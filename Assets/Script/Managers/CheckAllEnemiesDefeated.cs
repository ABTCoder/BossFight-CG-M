using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CheckAllEnemiesDefeated : MonoBehaviour
{

    [SerializeField] private GameObject[] enemies;
    [SerializeField] private PlayableDirector scriptedEvent;
    private bool notPlayed = true;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        bool allDead = true;
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i] != null)
            {
                allDead = false;
                break;
            }
        }

        if (allDead && notPlayed)
        {
            scriptedEvent.Play();
            notPlayed = false;
        }
            
    }
}
