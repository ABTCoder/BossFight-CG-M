using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "A.I./Enemy Actions/Attack Action")]
public class EnemyAttackAction : EnemyAction
{
    public int weaponAttackDamage;
    public float recoveryTime = 2;
    public float moveRecoveryTime = 0f;
    public bool canDoMove = true;
    public bool canBeInterrupted = true;

    public float maximumAttackAngle = 35;

    public float minimumDistanceNeededToAttack = 0;
    public float maximumDistanceNeededToAttack = 3;

    public string colliderName;

    public bool hasExtraEffect;
    public GameObject extraEffect;

    public float weaponSoundDelay = 0;
    public float gruntSoundDelay = 0;
    public float effectDelay = 0;

    public AudioClip[] weaponAudioClips;
    public AudioClip[] gruntsAudioClips;

    public IEnumerator ExtraEffectCoroutine(Vector3 position)
    {
        yield return new WaitForSeconds(effectDelay);
        Instantiate(extraEffect, position, Quaternion.identity);
    }

    public IEnumerator DecreaseRecoveryTimeCoroutine()
    {
        yield return new WaitForSeconds(moveRecoveryTime);
        canDoMove = true;
    }
}
