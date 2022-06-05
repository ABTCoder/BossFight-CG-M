using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;


public class BossRoomCutscene : MonoBehaviour
{
    private PlayableDirector cutsceneDirector;



    // Start is called before the first frame update
    void Start()
    {
        cutsceneDirector = GetComponentInChildren<PlayableDirector>();
        cutsceneDirector.stopped += Stopped;
    }

    private void Stopped(PlayableDirector d)
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent.name == "Player")
        {
            cutsceneDirector.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
