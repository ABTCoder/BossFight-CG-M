using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

abstract public class ColliderAttack : MonoBehaviour
{
    protected int damage;
    [SerializeField] protected string characterTarget;

    private void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isAttacking())
        {
            if (other.tag == characterTarget)
            {
                other.gameObject.GetComponent<CharacterStats>().TakeDamage(getDamageValue());
                Debug.Log("Damage done");
            }
        }
        this.doExtraStuff(other);
    }
    protected virtual void doExtraStuff(Collider other) { }

    abstract protected bool isAttacking();

    abstract protected int getDamageValue();
}
