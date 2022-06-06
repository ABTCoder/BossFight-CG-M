using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{
    private MovementController movement;
    private float speed = 7;
    private Vector3 characterCameraOffset;
    private float cameraDeg = 6;
    private Vector3 nextPosition;
    private Quaternion nextRotation;
    private CombatAnimationcontroller combatController;
    
    public bool lockOnInput;
    public bool right_Stick_Right_Input;
    public bool right_Stick_Left_Input;
    
    public bool lockOnFlag;

    MovementController inputActions;
    CameraHandler cameraHandler;

    [SerializeField] private GameObject followTransform;
    [SerializeField] private Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        combatController = GetComponentInChildren<CombatAnimationcontroller>();
    }

    private void Awake()
    {   
        movement = new MovementController();
        characterCameraOffset = new Vector3()
        {
            x = 0,
            y = 2,
            z = -4
        };
        
        cameraHandler = FindObjectOfType<CameraHandler>();
    }


    private void OnEnable()
    {
        if (movement == null)
        {
            movement = new MovementController();
            movement.Main.LockOn.performed += i => lockOnInput = true;
            movement.Main.LockOnTargetRight.performed += i => right_Stick_Right_Input = true;
            movement.Main.LockOnTargetLeft.performed += i => right_Stick_Left_Input = true;
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

        #region Follow Transform Rotation

        //Rotate the Follow Target transform based on the input
        followTransform.transform.rotation *= Quaternion.AngleAxis(mouseDelta.x * cameraDeg * Time.deltaTime , Vector3.up);

        #endregion

        #region Vertical Rotation

        followTransform.transform.rotation *= Quaternion.AngleAxis(mouseDelta.y * cameraDeg * Time.deltaTime, Vector3.right);

        var angles = followTransform.transform.localEulerAngles;
        angles.z = 0;

        var angle = followTransform.transform.localEulerAngles.x;

        //Clamp the Up/Down rotation
        if (angle > 180 && angle < 340)
        {
            angles.x = 340;
        }
        else if (angle < 180 && angle > 40)
        {
            angles.x = 40;
        }


        followTransform.transform.localEulerAngles = angles;
        #endregion


        if (charMove.x == 0 && charMove.y == 0)
        {
            nextPosition = transform.position;

            
            /*
            if (aimValue == 1)
            {
                //Set the player rotation based on the look transform
                transform.rotation = Quaternion.Euler(0, followTransform.transform.rotation.eulerAngles.y, 0);
                //reset the y rotation of the look transform
                followTransform.transform.localEulerAngles = new Vector3(angles.x, 0, 0);
            }*/
            

            return;
        }

        float moveSpeed = speed / 100f;
        Vector3 position = (transform.forward * charMove.y * moveSpeed) + (transform.right * charMove.x * moveSpeed);
        nextPosition = transform.position + position;


        //Set the player rotation based on the look transform
        if (!(combatController.getIsAttacking()) && !(combatController.getIsBlocking()))
            transform.rotation = Quaternion.Euler(0, followTransform.transform.rotation.eulerAngles.y, 0);
        //transform.rotation = nextRotation;
        //reset the y rotation of the look transform (relative to the parent,the player)
        followTransform.transform.localEulerAngles = new Vector3(angles.x, 0, 0);
    }

    
    private void FixedUpdate()
    {
        Vector2 charMove = movement.Main.Move.ReadValue<Vector2>();
        Vector2 mouseDelta = movement.Main.MoveCamera.ReadValue<Vector2>();

        if (!(combatController.getIsAttacking()) && !(combatController.getIsBlocking()))
            transform.Translate(charMove.x * Time.deltaTime * speed, 0, charMove.y * Time.deltaTime * speed);
        //Debug.Log(charMove.x + " x, " + charMove.y + " y");
        //transform.Rotate(Vector3.up, mouseDelta.x * Time.deltaTime * cameraDeg);
        //Debug.Log(charMove);
    }

    public void TickInput(float delta)
    {
        HandleLockOnInput();
    }

    public MovementController getMovement() 
    {
        return movement;
    }
    
    private void HandleLockOnInput()
    {
        if (lockOnInput && lockOnFlag == false)
        {
            lockOnInput = false;
            cameraHandler.HandleLockOn();
            if (cameraHandler.nearestLockOnTarget != null)
            {
                cameraHandler.currentLockOnTarget = cameraHandler.nearestLockOnTarget;
                lockOnFlag = true;
            }
        } 
        else if (lockOnInput && lockOnFlag)
        {
            lockOnInput = false;
            lockOnFlag = false;
            cameraHandler.ClearLockOnTargets();
        }

        if (lockOnFlag && right_Stick_Left_Input)
        {
            right_Stick_Left_Input = false;
            cameraHandler.HandleLockOn();
            if (cameraHandler.leftLockTarget != null)
            {
                cameraHandler.currentLockOnTarget = cameraHandler.leftLockTarget;
            }
        }

        if (lockOnFlag && right_Stick_Right_Input)
        {
            right_Stick_Right_Input = false;
            cameraHandler.HandleLockOn();
            if (cameraHandler.rightLockTarget != null)
            {
                cameraHandler.currentLockOnTarget = cameraHandler.rightLockTarget;
            }
        }
        
        cameraHandler.SetCameraHeight();
    }
}