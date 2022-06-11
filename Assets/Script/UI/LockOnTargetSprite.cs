using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockOnTargetSprite : MonoBehaviour
{
    private GameObject uiCamera;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        uiCamera = GameObject.Find("UI Camera");
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(uiCamera.transform);
    }
}
