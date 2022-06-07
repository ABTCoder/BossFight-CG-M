using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;


public class BossRoomCutscene : MonoBehaviour
{
    private PlayableDirector cutsceneDirector;
    private GameManager gameManager;

    [SerializeField] private GameObject player;
    private GameObject UI;
    private MovementController controller;
    private bool ended = false;

    // Start is called before the first frame update
    void Start()
    {
        cutsceneDirector = GetComponentInChildren<PlayableDirector>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        cutsceneDirector.stopped += Stopped;
        controller = player.GetComponent<CharacterMovement>().getMovement();
        UI = GameObject.Find("UI");
    }

    private void Stopped(PlayableDirector d)
    {
        gameObject.SetActive(false);
        ended = true;
        controller.Enable();
        UI.SetActive(true);
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
            controller.Disable();
            cutsceneDirector.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
