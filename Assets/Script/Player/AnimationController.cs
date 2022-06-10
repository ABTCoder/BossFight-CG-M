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
    private bool blockingTransitionPlayed = false;
    private bool isRolling = false;
    private bool isSliding = false; // It prevents the interruption of the roll's animation
    private Vector2 move = Vector2.zero;
    private float x = 0;
    private float y = 0;

    // Sound effetcs
    [SerializeField] private AudioClip[] footstepAudioClips;
    [SerializeField] private AudioClip[] rollAudioClips;
    private AudioSource audioSource;

    void Start()
    {
        playerAnimator = GetComponent<Animator>();

        
        componentCharacterMovement = GetComponentInParent<CharacterMovement>();
        playerInput = componentCharacterMovement.getMovement();

        EventCombatAddListners();
        colliderScript = weaponCollider.GetComponent<ColliderAttack>();

        audioSource = GetComponent<AudioSource>();
    }


    private void FixedUpdate()
    {
        MovementAnimationHandler();
    }


    private void MovementAnimationHandler()
    {
        if (!isAttacking)
            weaponTrail.SetActive(false);
        if (isRolling && !playerAnimator.IsInTransition(0))
        {
            playerAnimator.Play("RollBlend");
            //Debug.Log("FORCE FIX");
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

    public bool AnimatorIsPlaying()
    {
        return playerAnimator.GetCurrentAnimatorStateInfo(0).length >
               playerAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime;
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

    private void NextAttack()
    {
        if (comboStep == 2)
        {
            weaponTrail.SetActive(true);
            playerAnimator.CrossFade("PlayerAttack02", 0.2f);
            colliderScript.SetDamage(damage2);
            colliderScript.resetCollided();
        }
        else if (comboStep == 3)
        {
            weaponTrail.SetActive(true);
            playerAnimator.CrossFade("PlayerAttack03", 0.2f);
            colliderScript.SetDamage(damage3);
            colliderScript.resetCollided();
        }
    }

    private void ResetCombo()
    {
        isAttacking = false;
        comboPossible = false;
        comboStep = 0;
    }

    public void NormalAttack(InputAction.CallbackContext ctx)
    {
        if (!isBlocking && !isRolling)
        {
            if (comboStep == 0)
            {
                weaponTrail.SetActive(true);
                playerAnimator.CrossFade("PlayerAttack01", 0.2f);
                isAttacking = true;
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
    }

    public void DodgeRoll(InputAction.CallbackContext ctx)
    {
        Vector2 charMove = playerInput.Main.Move.ReadValue<Vector2>();

        if (!isRolling && !isAttacking && !isBlocking && (charMove != Vector2.zero))
        {
            playerAnimator.SetFloat("X", charMove.x);
            playerAnimator.SetFloat("Y", charMove.y);
            playerAnimator.CrossFade("RollBlend", 0.2f);
            componentCharacterMovement.setStartRollPos();
            isRolling = true;
        }

    }

    public void ResetAll()
    {
        isRolling = false;
        isBlocking = false;
        isAttacking = false;
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

    private void FootstepAudioEffetct()
    {
        if ((move.x < -0.05f || move.x > 0.05f) || (move.y < -0.5f || move.y > 0.5f))
        {
            PlayAudioEffect(footstepAudioClips);
        }
    }

    private void RollAudioEffect()
    {
        
        if ((move.x < -0.05 || move.x > 0.05) || (move.y < -0.5 || move.y > 0.5))
        {
            PlayAudioEffect(rollAudioClips);
        }
    }

    private void PlayAudioEffect(AudioClip[] audioClips) 
    {
        AudioClip audioClip = audioClips[UnityEngine.Random.Range(0, audioClips.Length)];
        audioSource.PlayOneShot(audioClip);
    }

    #endregion
}
