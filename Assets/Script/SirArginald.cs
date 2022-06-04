using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SirArginald : MonoBehaviour
{

    [SerializeField] private GameObject player;
    [SerializeField] private Vector3 offset;
    private MovementController controller;
    private Vector3 pos;
    // Start is called before the first frame update
    void Start()
    {
        controller = new MovementController();
        offset = new Vector3()
        {
            x = -0.4f,
            y = 1.69f,
            z = 0,
        };
        pos = player.transform.position;
    }

    private void OnEnable()
    {
        controller.Enable();
    }

    private void OnDisable()
    {
        controller.Disable();
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
