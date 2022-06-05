using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chandelier : MonoBehaviour
{

    private Rigidbody physics;
    // Start is called before the first frame update
    private float lifeTime = 5f;
    private bool broken = false;
    void Start()
    {
        physics = GetComponent<Rigidbody>();
        physics.isKinematic = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Fireball")
        {
            physics.isKinematic = false;
            broken = true;
        }
        Debug.Log(other.gameObject.name);
        if (other.gameObject.name == "Boss")
        {
            gameObject.SetActive(false);
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (broken) lifeTime -= Time.deltaTime;

        if (lifeTime < 0)
        {
            gameObject.SetActive(false);
        }
    }
}
