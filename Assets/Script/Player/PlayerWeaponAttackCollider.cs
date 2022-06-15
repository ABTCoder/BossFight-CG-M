using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponAttackCollider : ColliderAttack
{
    

    private AnimationController animationController;
    private PlayerSoundManager soundManager;
   


    private void Start()
    {
        soundManager = GetComponentInParent<PlayerSoundManager>();
        animationController = GetComponentInParent<AnimationController>();
    }

    protected override bool isAttacking()
    {
        return animationController.GetIsAttacking();
    }

}
