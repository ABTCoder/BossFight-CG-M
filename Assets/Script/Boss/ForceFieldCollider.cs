using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceFieldCollider : ColliderAttack
{
    private float lifeTime = 4f;
    private ParticleSystem ps;

    private void Awake()
    {
        SetDamage(10);
        characterTarget = "Player";
        soundManager = GetComponent<CharacterSoundManager>();
        ps = GetComponent<ParticleSystem>();
    }

    private void Start()
    {
        ps.Emit(1);
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
