using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;


public class BossRoomCutscene : MonoBehaviour
{
    private PlayableDirector cutsceneDirector;

    [SerializeField] private GameObject player;
    private MovementController controller;

    // Start is called before the first frame update
    void Start()
    {
        cutsceneDirector = GetComponentInChildren<PlayableDirector>();
        cutsceneDirector.stopped += Stopped;
        controller = player.GetComponent<CharacterMovement>().getMovement();
    }

    private void Stopped(PlayableDirector d)
    {
        gameObject.SetActive(false);
        controller.Enable();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            controller.Disable();
            cutsceneDirector.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
