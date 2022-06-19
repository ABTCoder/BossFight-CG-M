using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWeaponAttackCollider : ColliderAttack
{
    private BossManager bossManager;

    private void Start()
    {
        bossManager = GetComponentInParent<BossManager>();
    }

   
}
