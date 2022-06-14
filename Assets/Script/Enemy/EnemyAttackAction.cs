using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "A.I./Enemy Actions/Attack Action")]
public class EnemyAttackAction : EnemyAction
{
    public int attackDamage;
    public float recoveryTime = 2;

    public float maximumAttackAngle = 35;

    public float minimumDistanceNeededToAttack = 0;
    public float maximumDistanceNeededToAttack = 3;

    public string colliderName;
}
