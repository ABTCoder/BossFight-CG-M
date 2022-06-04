using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{

    private Vector3 shootDir;
    private float shootSpeed = 30;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Setup(Vector3 shootDir)
    {
        this.shootDir = shootDir;
        Destroy(gameObject, 5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += shootDir * Time.deltaTime * shootSpeed;
    }
}
