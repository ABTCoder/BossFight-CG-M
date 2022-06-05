using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public float mouseX;
    public float mouseY;

    public bool lockOnInput;
    
    public bool lockOnFlag;
    
    CameraHandler cameraHandler;

    private void Awake()
    {
        cameraHandler = CameraHandler.singleton;
    }

    private void FixedUpdate()
    {
        float delta = Time.fixedDeltaTime;

        if (cameraHandler != null)
        {
            cameraHandler.FollowTarget(delta);
            cameraHandler.HandleCameraRotation(delta, mouseX, mouseY);
        }
    }
}
