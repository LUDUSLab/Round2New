using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaspDistanceToAttack : MonoBehaviour {
    GameObject Dino;
    public float distance;
    Transform posDino;
    // Use this for initialization
    void Start()
    {
        Dino = GameObject.FindGameObjectWithTag("Player");
        posDino = Dino.GetComponent<Transform>();
    }

    void Update()
    {
        if (posDino.position.x < gameObject.transform.position.x)
        {
            if (-posDino.position.x + gameObject.transform.position.x < distance)
            {
                gameObject.GetComponent<WaspIA>().enabled = true;
            }
            else
            {
                gameObject.GetComponent<WaspIA>().enabled = false;
            }

        }
        else
        {
            if (+posDino.position.x - gameObject.transform.position.x < distance)
            {
                gameObject.GetComponent<WaspIA>().enabled = true;
            }
            else
            {
                gameObject.GetComponent< WaspIA>().enabled = false;
            }
        }
    }
}
