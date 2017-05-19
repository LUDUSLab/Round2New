using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaspIA : MonoBehaviour {
    public int hp;
    public float thrust, speed;
    GameObject Dino;
    //Transform posInicial;
    bool canAttack;
    Rigidbody2D rb;
    Vector2 direction, posInicial;
    
	// Use this for initialization
	void Start () {
        hp = 3;
        Dino = GameObject.FindGameObjectWithTag("Player");
        rb = gameObject.GetComponent<Rigidbody2D>();
        posInicial.x = gameObject.transform.position.x;
        posInicial.y = gameObject.transform.position.y;
        canAttack = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (!canAttack)
        {
            gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position,posInicial, speed * Time.deltaTime);
            if (gameObject.transform.position.x == posInicial.x)
            {
                canAttack = true;
            }
        }
        else{
            Attack();
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Debug.Log("acertou mizeravi");
            canAttack = false;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            rb.velocity = new Vector2(0, 0);
            Invoke("setVelocityZero", 1f);
        }
    }

    void Attack()
    {
        direction = new Vector2(Dino.transform.position.x - gameObject.transform.position.x, Dino.transform.position.y - gameObject.transform.position.y);
        rb.AddForce(direction * thrust);
    }

    void setVelocityZero()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }

}
