using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponAttackCollider : ColliderAttack
{
    private EnemyManager enemyManager;

    private void Start()
    {
        enemyManager = GetComponentInParent<EnemyManager>();
    }

}
