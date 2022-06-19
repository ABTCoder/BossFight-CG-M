using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceFieldCollider : ColliderAttack
{
    private float lifeTime = 4f;

    private void Awake()
    {
        SetDamage(10);
        characterTarget = "Player";
    }

    // Start is called before the first frame update
    protected override bool CanDoDamage()
    {
        return true;
    }

    private void Update()
    {
        if (lifeTime > 0)
        {
            transform.localScale += Vector3.one * Time.deltaTime*2f;
            lifeTime -= Time.deltaTime;
        }
        else
            Destroy(gameObject);
        
    }


}
