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
    private CharacterMovement cm;
    private Vector3 pos;
    private bool canShoot = true;
    // Start is called before the first frame update
    void Awake()
    {
        controller = CharacterMovement.getMovement();
        cm = player.GetComponent<CharacterMovement>();
        fireballFX = GetComponent<AudioSource>();
        offset = new Vector3()
        {
            x = -0.6f,
            y = 1.69f,
            z = 0,
        };
        pos = player.transform.position;

        controller.Main.Fireball.Enable();
        controller.Main.Fireball.performed += OnShoot;
    }

    IEnumerator CoolDown(float time)
    {
        Debug.Log("Waiting for cool down");
        yield return new WaitForSeconds(time);
        canShoot = true;
        SkillsUI.Instance.FireballReady();
    }

    void OnShoot(InputAction.CallbackContext ctx)
    {
        Debug.Log("OnShoot(), canShoot = " + canShoot);
        if (canShoot)
        {
            canShoot = false;
            SkillsUI.Instance.FireballUsed();
            Vector3 pos = transform.position;
            pos.y += 0.2f;
            Transform fireball = Instantiate(fireballObj, pos, transform.rotation);
            Vector3 direction;
            if (cm.GetLockOnFlag())
            {
                direction = transform.forward;
            }
            else direction = player.transform.forward.normalized;
            fireball.GetComponent<Fireball>().Setup(direction, splash);
            fireballFX.Play();
            Debug.Log("Fireball Shooted");
            StartCoroutine(CoolDown(2));
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (!GameManager.gameOver)
        {
            transform.Find("ghost").Translate(0, Mathf.Sin(Time.time * 1.5f) * Time.deltaTime * 0.2f, 0);
            Vector3 rotation = new Vector3()
            {
                x = Mathf.Sin(Time.time * 3f) * Time.deltaTime * 25f,
                y = 0,
                z = 0
            };
            transform.Find("ghost").Rotate(rotation);

            pos = Vector3.Lerp(pos, player.transform.position, 0.2f);
            transform.position = pos;
            transform.rotation = player.transform.rotation;
            transform.Translate(offset);
            if (cm.GetLockOnFlag())
            {
                transform.LookAt(cm.GetLockedTarget());
            } else
                transform.rotation = player.transform.rotation;
        } else
        {
            transform.Translate(Vector3.up * Time.deltaTime * 0.8f, Space.World);
        }
    }
}
