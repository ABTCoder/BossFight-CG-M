using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillsUI : MonoBehaviour
{
    public static SkillsUI Instance;

    [SerializeField] private Image hpUp;
    [SerializeField] private Image fireball;
    [SerializeField] private Sprite hpUpUsed;
    [SerializeField] private Sprite hpUpAvailable;
    [SerializeField] private Sprite fireballUsed;
    [SerializeField] private Sprite fireballAvailable;
    // Start is called before the first frame update


    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void HpUpReady()
    {
        hpUp.sprite = hpUpAvailable;
    }

    public  void HpUpUsed()
    {
        hpUp.sprite = hpUpUsed;
    }

    public  void FireballReady()
    {
        fireball.sprite = fireballAvailable;
    }

    public  void FireballUsed()
    {
        fireball.sprite = fireballUsed;
    }

}
