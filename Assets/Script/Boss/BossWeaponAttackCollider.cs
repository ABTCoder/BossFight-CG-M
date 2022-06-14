using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWeaponAttackCollider : ColliderAttack
{
    private BossManager enemyManager;

    private void Start()
    {
        enemyManager = GetComponentInParent<BossManager>();
    }

    protected override bool isAttacking()
    {
        return enemyManager.isAttacking;
    }
}
