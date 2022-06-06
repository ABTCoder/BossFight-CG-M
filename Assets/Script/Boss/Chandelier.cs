using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chandelier : MonoBehaviour
{

    private Rigidbody physics;
    // Start is called before the first frame update
    private float lifeTime = 100f;
    private bool broken = false;
    [SerializeField] AudioSource chainBreaking;
    [SerializeField] AudioSource hitSound;
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
            chainBreaking.Play();
        }
        Debug.Log(other.gameObject.name);
        if (other.gameObject.name == "Boss")
        {
            gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {

        if(collision.collider.gameObject.name == "Boss")
        {
            hitSound.Play();
            //gameObject.SetActive(false);
            
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
