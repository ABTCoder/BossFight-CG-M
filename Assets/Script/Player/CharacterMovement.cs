using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class CharacterMovement : MonoBehaviour
{
    private MovementController movement;
    private float speed = 5.5f;
    private Vector3 characterCameraOffset;
    Vector3 followOffset;
    private float cameraDeg = 0.3f;
    private float rollSpeed = 10f;
    private float speedRollToDecrease;
    private float rollStartTime;
    private float duration = 1f;

    private Vector3 nextPosition;
    private Vector3 startRollPos;
    private Vector3 endRollPos;
    private Vector3 lastPosition;
    private Vector3 localDirection;
    private Vector2 move;


    private Quaternion nextRotation;
    private AnimationController combatController;

    public bool lockOnInput;
    public bool right_Stick_Right_Input;
    public bool right_Stick_Left_Input;

    public bool lockOnFlag;
    [SerializeField] private GameObject lockOnTargetIcon;
    private GameObject activeTargetIcon;

    [SerializeField] private GameObject followTransform;
    [SerializeField] private Cinemachine.CinemachineVirtualCamera mainCamera;
    

    private Transform currentLockOnTarget;
    List<Transform> availableTargets = new List<Transform>();
    private Transform nearestLockOnTarget;
    private Transform leftLockTarget;
    private Transform rightLockTarget;

    // Start is called before the first frame update
    void Start()
    {
        combatController = GetComponentInChildren<AnimationController>();
    }

    private void Awake()
    {
        lastPosition = Vector3.zero;
        movement = new MovementController();
        characterCameraOffset = new Vector3()
        {
            x = 0,
            y = 2,
            z = -4
        };
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
        Vector2 charMove = movement.Main.Move.ReadValue<Vector2>();
        availableTargets.Clear();
        nearestLockOnTarget = null; 

        //Debug.Log(combatController.getIsAttacking());
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
        if (!(combatController.getIsAttacking()) && !(combatController.getIsBlocking()) && !(combatController.getIsRolling()) && (charMove.x != 0 || charMove.y != 0))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, followTransform.transform.rotation.eulerAngles.y, 0), Time.deltaTime * 10);

            //reset the y rotation of the look transform (relative to the parent,the player)
            //followTransform.transform.localEulerAngles = new Vector3(angles.x, 0, 0);
        }

        HandleLockOn();

        if (lockOnFlag)
        {
            followTransform.transform.LookAt(currentLockOnTarget);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, followTransform.transform.rotation.eulerAngles.y, 0), Time.deltaTime * 10);

        }


        if (!(combatController.getIsAttacking()) && !(combatController.getIsBlocking()))
        {
            if (combatController.getIsRolling())
            {
                // Manage the roll system
                float t = (Time.time - rollStartTime) / duration;
                speedRollToDecrease = Mathf.SmoothStep(rollSpeed, 0, t);
                transform.Translate(localDirection * speedRollToDecrease * Time.deltaTime);
                followTransform.transform.position = transform.position + followOffset;

            }
            else
            {
                move = Vector2.Lerp(move, charMove, Time.deltaTime * 10f);
                transform.Translate(move.x * Time.deltaTime * speed, 0, move.y * Time.deltaTime * speed);
                followTransform.transform.position = transform.position + followOffset;
            }
        }
        else
        {
            move = Vector2.Lerp(move, Vector2.zero, Time.deltaTime * 10f);
        }

    }

    private void LateUpdate()
    {
        
    }

    private void LockOn(InputAction.CallbackContext ctx)
    {   
        if (lockOnFlag == false)
        {
            if (nearestLockOnTarget != null)
            {
                currentLockOnTarget = nearestLockOnTarget;
                activeTargetIcon = Instantiate(lockOnTargetIcon, currentLockOnTarget);
                mainCamera.LookAt = currentLockOnTarget;
                lockOnFlag = true;
            }
        }
        else
        {
            ClearLockOnTargets();
            mainCamera.LookAt = followTransform.transform;
        }

    }


    private void SwitchLeftLockOnTarget(InputAction.CallbackContext ctx)
    {
        if (leftLockTarget != null)
        {
            Destroy(activeTargetIcon);
            currentLockOnTarget = leftLockTarget;
            activeTargetIcon = Instantiate(lockOnTargetIcon, currentLockOnTarget);
            mainCamera.LookAt = currentLockOnTarget;
        }
    }

    private void SwitchRightLockOnTarget(InputAction.CallbackContext ctx)
    {
        if (rightLockTarget != null)
        {
            Destroy(activeTargetIcon);
            currentLockOnTarget = rightLockTarget;
            activeTargetIcon = Instantiate(lockOnTargetIcon, currentLockOnTarget);
            mainCamera.LookAt = currentLockOnTarget;
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
            if (colliders[i].tag == "Enemy")
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

            if (lockOnFlag)
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
    }

    private void ClearLockOnTargets()
    {
        lockOnFlag = false;
        availableTargets.Clear();
        nearestLockOnTarget = null;
        currentLockOnTarget = null;
        Destroy(activeTargetIcon);
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
        speedRollToDecrease = rollSpeed; // Reset roll speed
        rollStartTime = Time.time;
        localDirection = new Vector3()
        {
            x = charMove.x,
            y = 0,
            z = charMove.y
        };
    }

    public MovementController getMovement() 
    {
        return movement;
    }
}
