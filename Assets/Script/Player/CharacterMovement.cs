using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class CharacterMovement : MonoBehaviour
{
    
    private static MovementController movement;
    private float speed = 5.5f;
    Vector3 followOffset;
    private float cameraDeg = 0.3f;
    private float rollSpeed = 10f;
    private float pushbackSpeed = 3f;
    private float rollSpeedToDecrease;
    private float pushbackSpeedToDecrease;
    private float rollStartTime;
    private float pushbackStartTime;
    private float rollDuration = 1f; 
    private float pushbackDuration = 1f;

    private Vector3 localDirection;
    private Vector2 move;


    private AnimationController animationController;

    public bool lockOnInput;

    public bool lockOnFlag;
    [SerializeField] private GameObject lockOnTargetIcon;
    private GameObject activeTargetIcon;

    [SerializeField] private GameObject followTransform;
    [SerializeField] private Cinemachine.CinemachineVirtualCamera mainCamera;
    [SerializeField] private Cinemachine.CinemachineVirtualCamera lockOnCamera;

    private Vector3 attackerDirection;

    private Transform currentLockOnTarget;
    List<Transform> availableTargets = new List<Transform>();
    private Transform nearestLockOnTarget;
    private Transform leftLockTarget;
    private Transform rightLockTarget;

    // Start is called before the first frame update
    void Start()
    {
        animationController = GetComponentInChildren<AnimationController>();
    }

    private void Awake()
    {
        movement = new MovementController();
        followOffset = new Vector3()
        {
            x = 0,
            y = 1,
            z = 0,
        };

        movement.Main.LockOn.performed += LockOn;
        movement.Main.LockOnTargetLeft.performed += SwitchLeftLockOnTarget;
        movement.Main.LockOnTargetRight.performed += SwitchRightLockOnTarget;
    }

    private void OnEnable()
    {
        if (movement == null)
        {
            movement = new MovementController();
        }

        movement.Enable();
    }

    private void OnDisable()
    {
        movement.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mouseDelta = movement.Main.MoveCamera.ReadValue<Vector2>();
        mouseDelta.y = -mouseDelta.y;
        Vector2 charMove = movement.Main.Move.ReadValue<Vector2>();
        availableTargets.Clear();
        nearestLockOnTarget = null; 

        if (!lockOnFlag)
        {
            #region Follow Transform Rotation

            //Rotate the Follow Target transform based on the input
            followTransform.transform.rotation *= Quaternion.AngleAxis(mouseDelta.x * cameraDeg, Vector3.up);

            #endregion

            #region Vertical Rotation

            followTransform.transform.rotation *= Quaternion.AngleAxis(mouseDelta.y * cameraDeg, Vector3.right);

            var angles = followTransform.transform.localEulerAngles;
            angles.z = 0;

            var angle = followTransform.transform.localEulerAngles.x;

            //Clamp the Up/Down rotation
            if (angle > 180 && angle < 300)
            {
                angles.x = 300;
            }
            else if (angle < 180 && angle > 40)
            {
                angles.x = 40;
            }


            followTransform.transform.localEulerAngles = angles;
            #endregion
        }
        float moveSpeed = speed / 100f;
        Vector3 position = (transform.forward * charMove.y * moveSpeed) + (transform.right * charMove.x * moveSpeed);


        //Set the player rotation based on the look transform
        if (!(animationController.GetIsAttacking()) && !(animationController.GetIsHealing()) && !(animationController.GetIsBlocking()) 
            && !(animationController.GetIsRolling()) && (charMove.x != 0 || charMove.y != 0))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, followTransform.transform.rotation.eulerAngles.y, 0), Time.deltaTime * 10);

            //reset the y rotation of the look transform (relative to the parent,the player)
            //followTransform.transform.localEulerAngles = new Vector3(angles.x, 0, 0);
        }

        HandleLockOn();

        if (!animationController.GetIsAttacking() && !animationController.GetIsBlocking() && !animationController.GetIsHealing())
        {
            if (animationController.GetIsRolling())
            {
                // Manage the roll system
                float t = (Time.time - rollStartTime) / rollDuration;
                rollSpeedToDecrease = Mathf.SmoothStep(rollSpeed, 0, t);
                transform.Translate(localDirection * rollSpeedToDecrease * Time.deltaTime);
                if (rollSpeedToDecrease < 0.05f) animationController.ResetAll();
            }
            else if(animationController.GetIsTakingDamage())
            {
                float t = (Time.time - pushbackStartTime) / pushbackDuration;
                pushbackSpeedToDecrease = Mathf.SmoothStep(pushbackSpeed, 0, t);
                transform.Translate(-attackerDirection.normalized* pushbackSpeedToDecrease * Time.deltaTime, Space.World);
            }
            else
            {
                move = Vector2.Lerp(move, charMove, Time.deltaTime * 10f);
                transform.Translate(move.x * Time.deltaTime * speed, 0, move.y * Time.deltaTime * speed);
            }
        }
        else
        {
            move = Vector2.Lerp(move, Vector2.zero, Time.deltaTime * 10f);
        }
        followTransform.transform.position = transform.position + followOffset;

    }

    public static void LockControls()
    {
        movement.Disable();
    }

    public static void UnlockControls()
    {
        movement.Enable();
    }

    private void LockOn(InputAction.CallbackContext ctx)
    {   
        if (lockOnFlag == false)
        {
            if (nearestLockOnTarget != null)
            {
                currentLockOnTarget = nearestLockOnTarget;
                activeTargetIcon = Instantiate(lockOnTargetIcon, currentLockOnTarget);
                lockOnCamera.LookAt = currentLockOnTarget;
                lockOnFlag = true;
                lockOnCamera.Priority = 16;
            }
        }
        else
        {
            ClearLockOnTargets();
        }

    }


    private void SwitchLeftLockOnTarget(InputAction.CallbackContext ctx)
    {
        if (lockOnFlag && leftLockTarget != null && !movement.Main.LockOn.IsPressed())
        {
            Destroy(activeTargetIcon);
            currentLockOnTarget = leftLockTarget;
            activeTargetIcon = Instantiate(lockOnTargetIcon, currentLockOnTarget);
            lockOnCamera.LookAt = currentLockOnTarget;
            lockOnCamera.Priority = 16;
        }
    }

    private void SwitchRightLockOnTarget(InputAction.CallbackContext ctx)
    {
        if (lockOnFlag && rightLockTarget != null && !movement.Main.LockOn.IsPressed())
        {
            Destroy(activeTargetIcon);
            currentLockOnTarget = rightLockTarget;
            activeTargetIcon = Instantiate(lockOnTargetIcon, currentLockOnTarget);
            lockOnCamera.LookAt = currentLockOnTarget;
            lockOnCamera.Priority = 16;
        }
    }



    private void HandleLockOn()
    {
        float shortestDistance = Mathf.Infinity;
        float shortestDistanceOfLeftTarget = Mathf.Infinity;
        float shortestDistanceOfRightTarget = Mathf.Infinity;
        float maximumLockOnDistance = 30;

        Collider[] colliders = Physics.OverlapSphere(followTransform.transform.position, 26);
        for (int i = 0; i < colliders.Length; i++)
        {
            Transform character;
            if (colliders[i].tag == "LockOn")
            {
                character = colliders[i].transform;
                if (character != null)
                {
                    Vector3 lockTargetDirection = character.position - followTransform.transform.position;
                    float distanceFromTarget = Vector3.Distance(followTransform.transform.position, character.position);
                    float viewableAngle = Vector3.Angle(lockTargetDirection, followTransform.transform.forward);
                    if (character.transform.root != followTransform.transform.root && viewableAngle < 50 && distanceFromTarget <= maximumLockOnDistance)
                    {
                        availableTargets.Add(character);
                    }
                }
            }
        }

        for (int k = 0; k < availableTargets.Count; k++)
        {
            float distanceFromTarget = Vector3.Distance(followTransform.transform.position, availableTargets[k].transform.position);

            if (distanceFromTarget < shortestDistance)
            {
                shortestDistance = distanceFromTarget;
                nearestLockOnTarget = availableTargets[k];
   
            }

            if (lockOnFlag && currentLockOnTarget != null)
            {
                Vector3 availableTargetDirection = availableTargets[k].position - followTransform.transform.position;
                float angle = Vector3.SignedAngle(availableTargetDirection, mainCamera.transform.forward, Vector3.up);
                float distanceFromAvailableTarget = Vector3.Distance(currentLockOnTarget.transform.position, availableTargets[k].transform.position);
                if (angle > 0 && distanceFromAvailableTarget < shortestDistanceOfLeftTarget && currentLockOnTarget != availableTargets[k])
                {
                    shortestDistanceOfLeftTarget = distanceFromAvailableTarget;
                    leftLockTarget = availableTargets[k];
                }
                if (angle < 0 && distanceFromAvailableTarget < shortestDistanceOfRightTarget && currentLockOnTarget != availableTargets[k])
                {
                    shortestDistanceOfRightTarget = distanceFromAvailableTarget;
                    rightLockTarget = availableTargets[k];
                }
            }
        }
        Vector2 scroll = movement.Main.SwitchLockOnTarget.ReadValue<Vector2>();
        if (lockOnFlag && !movement.Main.LockOn.IsPressed() && scroll != Vector2.zero)
        {
            Destroy(activeTargetIcon);
            if(scroll.y < 0 && rightLockTarget!=null)
                currentLockOnTarget = rightLockTarget;
            else
                currentLockOnTarget = leftLockTarget;
            activeTargetIcon = Instantiate(lockOnTargetIcon, currentLockOnTarget);
            lockOnCamera.LookAt = currentLockOnTarget;
            lockOnCamera.Priority = 16;
        }
        if (lockOnFlag)
        {
            followTransform.transform.LookAt(currentLockOnTarget);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, followTransform.transform.rotation.eulerAngles.y, 0), Time.deltaTime * 10);
        }
        CharacterStats stats = null;
        if (currentLockOnTarget != null)
            stats = currentLockOnTarget.GetComponentInParent<CharacterStats>();
        if (lockOnFlag && (currentLockOnTarget == null || (stats != null && stats.currentHealth <= 0)))
        {
            ClearLockOnTargets();
        }
    }

    public void ClearLockOnTargets()
    {
        lockOnFlag = false;
        availableTargets.Clear();
        nearestLockOnTarget = null;
        currentLockOnTarget = null;
        Destroy(activeTargetIcon);
        lockOnCamera.Priority = 9;
    }


    public bool GetLockOnFlag()
    {
        return lockOnFlag;
    }

    public Transform GetLockedTarget()
    {
        return currentLockOnTarget;
    }

    public void setStartRollPos()
    {
        Vector2 charMove = movement.Main.Move.ReadValue<Vector2>();
        rollSpeedToDecrease = rollSpeed; // Reset roll speed
        rollStartTime = Time.time;
        localDirection = new Vector3()
        {
            x = charMove.x,
            y = 0,
            z = charMove.y
        };
    }

    public void SetAttacker(Vector3 attackerPosition)
    {
        pushbackSpeedToDecrease = pushbackSpeed;
        pushbackStartTime = Time.time;
        this.attackerDirection = attackerPosition - transform.position;
    }

    public static MovementController getMovement() 
    {
        return movement;
    }

    
}
