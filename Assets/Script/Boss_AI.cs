using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_AI : MonoBehaviour
{
    private ENEMY_STATE state;
    //private Transform transformHead;

    [SerializeField] private GameObject player;

    private void Awake()
    {
        state = ENEMY_STATE.IDLE;
        //transformHead = transform.Find("Head").transform;
    }
    // Start is called before the first frame update
    void Start()
    {
        // Start the Finite State Machine (BossFSM)
        StartCoroutine(BossFSM());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator BossFSM()
    {
        while(true) 
        {
            // Start the Coroutine relatives to the current state
            yield return StartCoroutine(state.ToString());
        }
    }


    IEnumerator IDLE()
    {
        float distance = 0;
        // ENTER THE IDLE STATE
        Debug.Log("Alright, seems no evil Player is around, I can chill!");

        // EXECUTE IDLE STATE
        while (state == ENEMY_STATE.IDLE)
        {
            distance = Vector3.Distance(player.transform.position, transform.position);
            if (distance <= 20)
            {
                state = ENEMY_STATE.CHASE;
            }
            yield return null;          

        }

        // EXIT THE IDLE STATE

        Debug.Log("Uh, I guess I smell a Player!");
    }


    IEnumerator CHASE()
    {
        float distance = 0;

        // ENTER THE CHASE STATE
        Debug.Log("AH! I'm coming for you, my darling!");

        // EXECUTE CHASE STATE
        while (state == ENEMY_STATE.CHASE)
        {
            distance = Vector3.Distance(player.transform.position, transform.position);
            if (distance <= 2)
            {
                state = ENEMY_STATE.ATTACK;
            }
            else
            {
                Vector3 lookTo = new Vector3()
                {
                    x = player.transform.position.x,
                    y = transform.position.y,
                    z = player.transform.position.z
                };
                transform.LookAt(lookTo);
                //transformHead.LookAt(player.transform.position);
                transform.Translate(Vector3.forward * Time.deltaTime * 6);
            }
            yield return null;
        }

        // EXIT THE IDLE STATE

        Debug.Log("We are SO close now, prepare yourself! <3");
    }


    IEnumerator ATTACK()
    {
        // ATTACK
        Debug.Log("Alright, begin! Take THIS!");

        // Change the state to CHASE and check again the distance
        state = ENEMY_STATE.CHASE;

        yield return null;
        
    }


    public enum ENEMY_STATE
    { 
        IDLE = 0,
        CHASE = 1,
        ATTACK = 2
    }
}
