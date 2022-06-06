using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;


public class BossRoomCutscene : MonoBehaviour
{
    private PlayableDirector cutsceneDirector;
    private GameManager gameManager;

    [SerializeField] private GameObject player;
    private MovementController controller;
    private bool ended = false;

    // Start is called before the first frame update
    void Start()
    {
        cutsceneDirector = GetComponentInChildren<PlayableDirector>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        cutsceneDirector.stopped += Stopped;
        controller = player.GetComponent<CharacterMovement>().getMovement();
    }

    private void Stopped(PlayableDirector d)
    {
        gameObject.SetActive(false);
        ended = true;
        controller.Enable();
    }

    public bool IsEnded()
    {
        return ended;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
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
