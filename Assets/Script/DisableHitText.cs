using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableHitText : MonoBehaviour
{
    public float dTime;

    private void OnEnable()
    {
        CancelInvoke();
        Invoke("Disable", dTime);
    }

    void Disable()
    {
        gameObject.SetActive(false);
    }

}
