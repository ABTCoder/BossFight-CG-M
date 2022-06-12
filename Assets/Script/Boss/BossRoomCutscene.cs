using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;


public class BossRoomCutscene : MonoBehaviour
{
    private PlayableDirector cutsceneDirector;
    private GameManager gameManager;
    private GameObject UI;
    private bool ended = false;

    // Start is called before the first frame update
    void Start()
    {
        cutsceneDirector = GetComponentInChildren<PlayableDirector>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        cutsceneDirector.stopped += Stopped;
        UI = GameObject.Find("UI");
    }

    private void Stopped(PlayableDirector d)
    {
        ended = true;
        CharacterMovement.UnlockControls();
        UI.SetActive(true);
        UI.transform.Find("HealthBar Boss").gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    public bool IsEnded()
    {
        return ended;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            UI.SetActive(false);
            gameManager.PlayBossMusic();
            CharacterMovement.LockControls();
            cutsceneDirector.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
