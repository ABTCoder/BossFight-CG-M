using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

abstract public class ColliderAttack : MonoBehaviour
{
    protected int damage;
    [SerializeField] protected string characterTarget;
    protected bool collided = false;
    [SerializeField] private AudioClip[] hitSounds;
    private AudioSource audioSource;

    private void OnTriggerEnter(Collider other)
    {
        if (CanDoDamage() && !collided)
        {
            if (other.tag == characterTarget)
            {
                other.gameObject.GetComponent<CharacterStats>().TakeDamage(damage);
                collided = true;
            }
        }
        doExtraStuff(other);
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

    abstract protected bool CanDoDamage();

}
