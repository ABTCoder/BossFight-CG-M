using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimatorManager : AnimatorManager
{
    EnemyManager enemyManager;

    [SerializeField] public ColliderAttack[] enemyColliders;
    [SerializeField] public AudioClip[] footstepAudioClips;
    private float x;
    private float y;
    private bool isPlayingFootstepLeft = false;
    private bool isPlayingFootstepRight = false;
    private Vector3 leftFootIKPos;
    private Vector3 rightFootIKPos;
    private CharacterSoundManager soundManager;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        enemyManager = GetComponentInParent<EnemyManager>();
        soundManager = GetComponentInParent<CharacterSoundManager>();
    }

    private void OnAnimatorMove()
    {
        try
        {
            float delta = Time.deltaTime;
            Vector3 deltaPosition = anim.deltaPosition;
            deltaPosition.y = 0;
            Vector3 velocity = deltaPosition / delta;
            if (float.IsNaN(velocity.x) || float.IsNaN(velocity.y) || float.IsNaN(velocity.z) || float.IsInfinity(velocity.x) || float.IsInfinity(velocity.y) || float.IsInfinity(velocity.z))
                velocity = Vector3.zero;
            enemyManager.enemyRigidBody.velocity = velocity;

            if (enemyManager.isRotatingWithRootMotion)
            {
                enemyManager.transform.rotation *= anim.deltaRotation;
            }
        }
        catch
        {
            Debug.Log("OnAnimatorMove() Excpetion");
            return;
        }
    }

    void OnAnimatorIK()
    {
        // Take the feet position using the IK of the animator
        leftFootIKPos = anim.GetIKPosition(AvatarIKGoal.LeftFoot);
        rightFootIKPos = anim.GetIKPosition(AvatarIKGoal.RightFoot);

    }

    private void Update()
    {
        FootStepSound();
    }

    private void FootStepSound()
    {
        x = anim.GetFloat("Horizontal");
        y = anim.GetFloat("Vertical");

        if ((x != 0 || y != 0))
        {
            // Find the height of the foot relative to the player
            Vector3 position = transform.root.position;
            float leftFootDistanceFromGround = leftFootIKPos.y - position.y;
            float rightFootDistanceFromGround = rightFootIKPos.y - position.y;
            float threshold = 0.15f * transform.root.localScale.x;
            if (leftFootDistanceFromGround <= threshold)
            {
                if (isPlayingFootstepLeft == false)
                {
                    soundManager.PlayAudioEffect(footstepAudioClips);
                    isPlayingFootstepLeft = true;
                }

            }
            else
                isPlayingFootstepLeft = false;

            if (rightFootDistanceFromGround <= threshold)
            {
                if (isPlayingFootstepRight == false)
                {
                    soundManager.PlayAudioEffect(footstepAudioClips);
                    isPlayingFootstepRight = true;
                }
            }
            else
                isPlayingFootstepRight = false;
        }
    }



    public void SetCanDoDamageTrue()
    {
        for (int i = 0; i < enemyColliders.Length; i++)
        {
            enemyColliders[i].GetComponent<Collider>().enabled = true;
        }
    }

    public void ResetCanDoDamage()
    {
        for (int i = 0; i < enemyColliders.Length; i++)
        {
            enemyColliders[i].GetComponent<Collider>().enabled = false;
        }
    }
}
