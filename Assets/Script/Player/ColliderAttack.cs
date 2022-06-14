using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

abstract public class ColliderAttack : MonoBehaviour
{
    protected int damage;
    [SerializeField] protected string characterTarget;
    protected bool collided = false;

    private void Start()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (isAttacking() && !collided)
        {
            if (other.tag == characterTarget)
            {
                Debug.Log("COLLIDED WITH " + other.name);
                other.gameObject.GetComponent<CharacterStats>().TakeDamage(damage);
                Debug.Log("Damage done");
                collided = true;
            }
        }
        this.doExtraStuff(other);
    }

    public void SetDamage(int damage)
    {
        this.damage = damage;
    }


    public void resetCollided()
    {
        collided = false;
    }

    protected virtual void doExtraStuff(Collider other) { }

    abstract protected bool isAttacking();

}
