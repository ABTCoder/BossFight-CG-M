using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponAttackCollider : ColliderAttack
{
    

    private AnimationController animationController;
   


    private void Start()
    {
        animationController = GetComponentInParent<AnimationController>();
    }

    protected override bool CanDoDamage()
    {
        return animationController.CanDoDamage();
    }

}
