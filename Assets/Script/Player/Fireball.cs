using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : ColliderAttack
{

    private Vector3 shootDir;
    private Transform splash;
    private float shootSpeed = 1;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Setup(Vector3 shootDir, Transform splashObj)
    {
        this.shootDir = shootDir;
        splash = splashObj;
        Destroy(gameObject, 5f);
        SetDamage(50);
    }


    protected override void doExtraStuff(Collider other)
    {
        if (other.gameObject.tag != "Player2")
        {
            Debug.Log("I'm destroying the fireball");
            Instantiate(splash, transform.position, transform.rotation);
            Destroy(gameObject);
        }

    }

    protected override bool isAttacking()
    {
        return true;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += shootDir * Time.deltaTime * shootSpeed;
    }

}
