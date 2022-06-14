using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTriggers : MonoBehaviour
{
    [SerializeField] private string tutorialName;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        GameManager.tutorialsToPlay.Enqueue(tutorialName);
        Destroy(gameObject);
    }

}
