using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColliderPlayerAttack : MonoBehaviour
{
    public Text textObj;
    private string textHit;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Hitbox_Boss")
        {
            textHit = "Ah! Hit!";
            textObj.text = textHit;
            textObj.gameObject.SetActive(true);
        }
    }
}
