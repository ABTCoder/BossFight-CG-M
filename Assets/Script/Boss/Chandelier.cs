using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chandelier : MonoBehaviour
{

    private Rigidbody physics;
    [SerializeField] AudioSource chainBreaking;
    [SerializeField] AudioSource hitSound;
    private bool canDoDamage = true;
    private Renderer renderer;
    private ParticleSystem particleSystem;
    private Color materialColor;
    private Color particleColor;

    void Start()
    {
        renderer = GetComponent<Renderer>();
        particleSystem = GetComponent<ParticleSystem>();
        physics = GetComponent<Rigidbody>();
        physics.isKinematic = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Fireball")
        {
            physics.isKinematic = false;
            chainBreaking.Play();
        }
        
    }


    private void OnCollisionEnter(Collision collision)
    {
        hitSound.Play();
        if (collision.collider.gameObject.name == "Boss" && canDoDamage)
        {
            collision.collider.gameObject.GetComponent<CharacterStats>().TakeDamage(20);
            canDoDamage = false;
            //gameObject.SetActive(false);

        }
        else if (collision.collider.gameObject.tag != "Fireball")
            canDoDamage = false;
    }


    // Update is called once per frame
    void Update()
    {
        
        if (!canDoDamage)
        {
            particleColor = particleSystem.main.startColor.color;
            materialColor = renderer.material.color;
            float fadeAmount = (float)(materialColor.a - (0.3f * Time.deltaTime));
            materialColor = new Color(materialColor.r, materialColor.g, materialColor.b, fadeAmount);
            particleColor = new Color(particleColor.r, particleColor.g, particleColor.b, 0);
            renderer.material.color = materialColor;
            var main = particleSystem.main;
            main.startColor = new ParticleSystem.MinMaxGradient(particleColor);
            Debug.Log(materialColor.a);
            if (materialColor.a < 0)
                Destroy(gameObject);
        }
    }
}
