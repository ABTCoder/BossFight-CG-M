using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{

    private Vector3 shootDir;
    private Transform splash;
    private float shootSpeed = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Setup(Vector3 shootDir, Transform splashObj)
    {
        this.shootDir = shootDir;
        splash = splashObj;
        Destroy(gameObject, 5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Player")
        {
            Instantiate(splash, transform.position, transform.rotation);
            Destroy(gameObject);
        }

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += shootDir * Time.deltaTime * shootSpeed;
    }
}
