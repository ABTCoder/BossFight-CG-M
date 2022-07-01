using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;


public class BossRoomCutscene : MonoBehaviour
{
    private PlayableDirector cutsceneDirector;
    private GameManager gameManager;
    private Canvas ui;
    private bool ended = false;

    [SerializeField] private CharacterMovement characterMovement;

    // Start is called before the first frame update
    void Start()
    {
        cutsceneDirector = GetComponentInChildren<PlayableDirector>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        cutsceneDirector.stopped += Stopped;
        ui = GameObject.Find("UI").GetComponent<Canvas>();
    }

    private void Stopped(PlayableDirector d)
    {
        ended = true;
        CharacterMovement.UnlockControls();
        ui.enabled = true;
        ui.transform.Find("HealthBar Boss").gameObject.SetActive(true);
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
            characterMovement.ClearLockOnTargets();
            ui.enabled = false;
            gameManager.PlayBossMusic();
            CharacterMovement.LockControls();
            cutsceneDirector.Play();
            GameManager.playingCutscene = true;
        }
    }


}
