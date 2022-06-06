using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SirArginald : MonoBehaviour
{

    [SerializeField] private GameObject player;
    [SerializeField] private Vector3 offset;
    [SerializeField] private Transform fireballObj;
    [SerializeField] private Transform splash;

    private AudioSource fireballFX;
    private MovementController controller;
    private Vector3 pos;
    // Start is called before the first frame update
    void Awake()
    {
        controller = player.GetComponent<CharacterMovement>().getMovement();
        fireballFX = GetComponent<AudioSource>();
        offset = new Vector3()
        {
            x = -0.4f,
            y = 1.69f,
            z = 0,
        };
        pos = player.transform.position;

        controller.Main.Fireball.Enable();
        controller.Main.Fireball.performed += OnShoot;
    }

    void OnShoot(InputAction.CallbackContext ctx)
    {
        Vector3 pos = transform.position;
        pos.y += 0.2f;
        Transform fireball = Instantiate(fireballObj, pos, transform.rotation);
        Vector3 direction = player.transform.Find("Target").transform.forward.normalized;
        fireball.GetComponent<Fireball>().Setup(direction, splash);
        fireballFX.Play();
    }


    // Update is called once per frame
    void Update()
    {
        transform.Find("ghost").Translate(0, Mathf.Sin(Time.time*1.5f) * Time.deltaTime * 0.2f, 0);
        Vector3 rotation = new Vector3()
        {
            x = Mathf.Sin(Time.time * 3f) * Time.deltaTime * 25f,
            y = 0,
            z = 0
        };
        transform.Find("ghost").Rotate(rotation);

        pos = Vector3.Lerp(pos, player.transform.position, 0.2f);
        transform.position = pos;
        transform.Translate(offset);
        transform.rotation = player.transform.rotation;

    }
}
