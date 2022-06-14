using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimationController : MonoBehaviour
{
    private Animator playerAnimator;
    private MovementController playerInput;
    private CharacterMovement componentCharacterMovement;
    [SerializeField] private GameObject weaponCollider;
    [SerializeField] private GameObject weaponTrail;
    private int damage1 = 10;
    private int damage2 = 15;
    private int damage3 = 20;
    private ColliderAttack colliderScript;
    private string[] damageAnimations = { "Damage1", "Damage2", "Damage3" };

    private int comboStep = 0;

    private bool comboPossible = false; // It determinates the possibility to concatenate the attacks
    private bool isAttacking = false;
    private bool isBlocking = false;
    private bool isRolling = false;
    private bool canInputAttack = true;
    private bool canInputRoll = true;
    private Vector2 move = Vector2.zero;
    private float x = 0;
    private float y = 0;

    // Sound effetcs
    [SerializeField] private AudioClip[] footstepAudioClips;
    [SerializeField] private AudioClip[] rollAudioClips;
    [SerializeField] private AudioClip[] attackAudioClips;
    [SerializeField] private AudioClip[] shieldHitAudioClips;
    private AudioSource audioSource;
    private bool isPlayingFootstepLeft = false;
    private bool isPlayingFootstepRight = false;
    private Vector3 leftFootIKPos;
    private Vector3 rightFootIKPos;

    void Start()
    {
        playerAnimator = GetComponent<Animator>();

        
        componentCharacterMovement = GetComponentInParent<CharacterMovement>();
        playerInput = CharacterMovement.getMovement();

        EventCombatAddListners();
        colliderScript = weaponCollider.GetComponent<ColliderAttack>();

        audioSource = GetComponent<AudioSource>();
    }


    private void Update()
    {
        //Debug.Log("Attacking" + isAttacking + " CanInput:"+canInputAttack + "Rolling:"+isRolling);
        MovementAnimationHandler();
        FootStepSound();
    }


    private void MovementAnimationHandler()
    {
        if (!isAttacking)
            weaponTrail.SetActive(false);
        if (isRolling && !playerAnimator.IsInTransition(0))
        {
            playerAnimator.Play("RollBlend");
        }
        

        Vector2 charMove = playerInput.Main.Move.ReadValue<Vector2>();
        if (playerInput.Main.ShieldBlock.IsPressed()) Block();
        if (!isAttacking && !isRolling && !isBlocking)
        {
            if (!playerAnimator.IsInTransition(0))
                playerAnimator.CrossFade("Movement Blend Tree", 0.2f);
            move = Vector2.Lerp(move, charMove, Time.deltaTime*8f);

            playerAnimator.SetFloat("X", move.x);
            playerAnimator.SetFloat("Y", move.y);
        }
    }


    private void EventCombatAddListners()
    {
        // Reads the input of the player and performs different combat actions due the input
        playerInput.Main.BaseAttack.performed += NormalAttack;
        playerInput.Main.ShieldBlock.canceled += StopBlocking;
        playerInput.Main.DodgeRoll.performed += DodgeRoll;
    }


    #region Handler's methods
    private void ComboPossible()
    {
        comboPossible = true;
    }

    IEnumerator ResetCanInputAttack()
    {
        yield return new WaitForSeconds(0.1f);
        canInputAttack = true;
        comboPossible = false;
        comboStep = 0;
    }
    private void ResetCombo()
    {  
        comboPossible = false;
    }

    private void SetIsAttackingFalse()
    {
        isAttacking = false;
        comboStep = 0;
        StartCoroutine(ResetCanInputAttack());
    }

    public void SetIsAttackingTrue()
    {
        isAttacking = true;
    }

    public void SetIsRollingTrue()
    {
        isRolling = true;
    }

    public void SetCanInputAttackFalse()
    {
        canInputAttack = false;
    }

    private void NextAttack()
    {
        isAttacking = true;
        if (comboStep == 2)
        {
            weaponTrail.SetActive(true);
            playerAnimator.CrossFade("PlayerAttack02", 0.1f);
            PlayAudioEffect(attackAudioClips);
            colliderScript.SetDamage(damage2);
            colliderScript.resetCollided();
        }
        else if (comboStep == 3)
        {
            weaponTrail.SetActive(true);
            playerAnimator.CrossFade("PlayerAttack03", 0.1f);
            PlayAudioEffect(attackAudioClips);
            colliderScript.SetDamage(damage3);
            colliderScript.resetCollided();
        }
    }


    private void NormalAttack(InputAction.CallbackContext ctx)
    {
        if (!isBlocking && !isRolling && canInputAttack)
        {
            if (comboStep == 0)
            {
                isAttacking = true;
                weaponTrail.SetActive(true);
                playerAnimator.CrossFade("PlayerAttack01", 0.1f);
                PlayAudioEffect(attackAudioClips, 0.2f);
                colliderScript.SetDamage(damage1);
                colliderScript.resetCollided();
                comboStep = 1;
            }
            else
            {
                if (comboPossible)
                {
                    comboPossible = false; //Evita di duplicare l'input
                    comboStep += 1;
                    NextAttack();
                }
            }
        }
    }

    public void Block()
    {
        if (!isAttacking && !isRolling && !isBlocking)
        {
            isBlocking = true;
            playerAnimator.CrossFade("ShieldBlock01_Loop", 0.075f);
        }
    }

    public void StopBlocking(InputAction.CallbackContext ctx)
    {

        isBlocking = false;
    }

    public void DamageHit()
    {
        int randomNumber = Mathf.RoundToInt(Random.Range(0, 2));
        playerAnimator.CrossFade(damageAnimations[randomNumber], 0.2f);
        ResetAll();
    }

    public void PlayShieldHit()
    {
        playerAnimator.CrossFade("ShieldHit", 0.2f);
        PlayAudioEffect(shieldHitAudioClips);
    }

    public void DodgeRoll(InputAction.CallbackContext ctx)
    {
        Vector2 charMove = playerInput.Main.Move.ReadValue<Vector2>();

        if (!isRolling && !isAttacking && !isBlocking && (charMove != Vector2.zero) && canInputRoll)
        {
            isRolling = true;
            canInputRoll = false;
            playerAnimator.SetFloat("X roll", charMove.x);
            playerAnimator.SetFloat("Y roll", charMove.y);
            RollAudioEffect();
            playerAnimator.CrossFade("RollBlend", 0.2f);
            componentCharacterMovement.setStartRollPos();
        }

    }
    private void RollAudioEffect()
    {
        PlayAudioEffect(rollAudioClips);
    }
    IEnumerator ResetCanInputRoll()
    {
        yield return new WaitForSeconds(0.1f);
        canInputRoll = true;
    }
    private void ResetRoll()
    {
        isRolling = false;
        StartCoroutine(ResetCanInputRoll());
    }

    public void ResetAll()
    {
        isRolling = false;
        isBlocking = false;
        isAttacking = false;
        comboStep = 0;
        comboPossible = false;
        canInputAttack = true;
        canInputRoll = true;
    }


    public bool getIsRolling()
    {
        return isRolling;
    }

    public bool getIsBlocking()
    {
        return isBlocking;
    }

    public bool getIsAttacking()
    {
        return isAttacking;
    }

    #endregion

    #region Animation's sounds


    #endregion


    private void FootStepSound()
    {
        Vector2 charMove = playerInput.Main.Move.ReadValue<Vector2>();

        if ((charMove.x != 0 || charMove.y != 0) && !isAttacking && !isBlocking && !isRolling)
        {

            // Find the height of the foot relative to the player
            Vector3 playerPosition = transform.root.position;
            float leftFootDistanceFromGround = leftFootIKPos.y - playerPosition.y;
            float rightFootDistanceFromGround = rightFootIKPos.y - playerPosition.y;

            if (leftFootDistanceFromGround <= 0.095f)
            {
                if (isPlayingFootstepLeft == false)
                {
                    PlayAudioEffect(footstepAudioClips);
                    isPlayingFootstepLeft = true;
                }

            }
            else
                isPlayingFootstepLeft = false;

            if (rightFootDistanceFromGround <= 0.095f)
            {
                if (isPlayingFootstepRight == false)
                {
                    PlayAudioEffect(footstepAudioClips);
                    isPlayingFootstepRight = true;
                }
            }
            else
                isPlayingFootstepRight = false;
        }
    }


    void OnAnimatorIK()
    {
        // Take the feet position using the IK of the animator
        leftFootIKPos = playerAnimator.GetIKPosition(AvatarIKGoal.LeftFoot);
        rightFootIKPos = playerAnimator.GetIKPosition(AvatarIKGoal.RightFoot);

    }


    IEnumerator CoPlayDelayedClip(AudioClip clip, float delay)
    {
        yield return new WaitForSeconds(delay);
        audioSource.PlayOneShot(clip);
    }

    private void PlayAudioEffect(AudioClip[] audioClips, float delay = 0.0f)
    {
        AudioClip audioClip = audioClips[UnityEngine.Random.Range(0, audioClips.Length)];
        StartCoroutine(CoPlayDelayedClip(audioClip, delay));
    }
}
