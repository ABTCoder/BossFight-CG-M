using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

abstract public class ColliderAttack : MonoBehaviour
{
    protected int damage;
    [SerializeField] protected string characterTarget;
    protected bool collided = false;
    protected bool triggerEnter = false;
    protected Collider other = null;
    [SerializeField] protected AudioClip[] hitSounds;
    protected CharacterSoundManager soundManager;

    private void Awake()
    {
        soundManager = GetComponentInParent<CharacterSoundManager>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (!collided)
        {
            if (other.tag == characterTarget)
            {
                var stats = other.gameObject.GetComponent<CharacterStats>();
                if (stats.canTakeDamage && damage > 0)
                {
                    if (soundManager != null)
                        soundManager.PlayAudioEffect(hitSounds);
                    stats.TakeDamage(damage, transform.root);
                    
                }
                collided = true;
            }
        }
        else
            triggerEnter = false;
        doExtraStuff(other);
    }

    protected void DamageOperations()
    {
        
    }

    public void SetDamage(int damage)
    {
        this.damage = damage;
    }


    public void resetCollided()
    {
        collided = false;
        triggerEnter = false;
    }

    protected virtual void doExtraStuff(Collider other) { }


}
