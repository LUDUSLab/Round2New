using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaspDistanceToAttack : MonoBehaviour {
    GameObject Dino;
    public float distance;
    Transform posDino;
    Rigidbody2D rb;
    // Use this for initialization
    void Start()
    {
        Dino = GameObject.FindGameObjectWithTag("Player");
        posDino = Dino.GetComponent<Transform>();
        rb = gameObject.GetComponent<Rigidbody2D>();
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
                rb.velocity = new Vector2(0, 0);
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
                rb.velocity = new Vector2(0, 0);
            }
        }
    }
}
