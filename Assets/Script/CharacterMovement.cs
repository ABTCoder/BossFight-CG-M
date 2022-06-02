using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private MovementController movement;
    private float speed = 7;
    private Vector3 characterCameraOffset;
    private float cameraDeg = 6;
    private float maxCameraTilt = 34;
    private float minCameraTilt = 336;
    private Vector3 nextPosition;
    private Quaternion nextRotation;
    private float rotationLerp = 0.2f;

    [SerializeField] private GameObject followTransform;
    [SerializeField] private Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        
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
    }

    private void OnEnable()
    {
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

       
        // nextRotation = Quaternion.Lerp(followTransform.transform.rotation, nextRotation, Time.deltaTime * rotationLerp);
        // nextRotation = Quaternion.Lerp(Quaternion.Euler(0, followTransform.transform.rotation.eulerAngles.y, 0), nextRotation, Time.deltaTime * rotationLerp);

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
        transform.rotation = Quaternion.Euler(0, followTransform.transform.rotation.eulerAngles.y, 0);
        //transform.rotation = nextRotation;
        //reset the y rotation of the look transform (relative to the parent,the player)
        followTransform.transform.localEulerAngles = new Vector3(angles.x, 0, 0);
    }

    
    private void FixedUpdate()
    {
        Vector2 charMove = movement.Main.Move.ReadValue<Vector2>();
        Vector2 mouseDelta = movement.Main.MoveCamera.ReadValue<Vector2>();


        transform.Translate(charMove.x * Time.deltaTime * speed, 0, charMove.y * Time.deltaTime * speed);
        //Debug.Log(charMove.x + " x, " + charMove.y + " y");
        //transform.Rotate(Vector3.up, mouseDelta.x * Time.deltaTime * cameraDeg);
        Debug.Log(charMove);
    }
    /*
    private void LateUpdate()
    {
        
        Vector2 mouseDelta = movement.Main.MoveCamera.ReadValue<Vector2>();
        

        float horizontal = mouseDelta.x * Time.deltaTime * cameraDeg;
        float vertical = mouseDelta.y * Time.deltaTime * cameraDeg;
        mainCamera.transform.position = transform.position;
        if (horizontal != 0 || vertical != 0)
        {
            mainCamera.transform.Rotate(Vector3.right, -vertical);
            mainCamera.transform.Rotate(Vector3.up, horizontal, Space.World);

        }


        float cameraRotationX = mainCamera.transform.rotation.eulerAngles.x;
        float cameraRotationY = mainCamera.transform.rotation.eulerAngles.y;
        float cameraRotationZ = mainCamera.transform.rotation.eulerAngles.z;

        
        if (cameraRotationX < minCameraTilt && cameraRotationX > minCameraTilt - 40)
        {
            mainCamera.transform.rotation = Quaternion.Euler(minCameraTilt, cameraRotationY, cameraRotationZ);
        }

        if (cameraRotationX > maxCameraTilt && cameraRotationX < maxCameraTilt + 40)
        {
            mainCamera.transform.rotation = Quaternion.Euler(maxCameraTilt, cameraRotationY, cameraRotationZ);
        }

        Debug.Log(cameraRotationX);
        mainCamera.transform.Translate(characterCameraOffset);
        
    }*/

    public MovementController getMovement() 
    {
        return movement;
    }
}
