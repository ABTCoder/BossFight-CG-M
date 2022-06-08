using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponAttackCollider : ColliderAttack
{
    private int damageAttack = 10;

    private AnimationController animationController;

   

    private void Start()
    {
        animationController = GetComponentInParent<AnimationController>();
    }

    protected override bool isAttacking()
    {
        return animationController.getIsAttacking();
    }

    protected override int getDamageValue()
    {
        return damageAttack;
    }
}
