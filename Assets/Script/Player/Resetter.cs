using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resetter : StateMachineBehaviour
{
    [SerializeField] private AnimationController controller;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        controller = GameObject.Find("Player").GetComponentInChildren<AnimationController>();
        controller.ResetAll();
    }
}
