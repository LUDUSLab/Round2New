using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBallBehaviour : MonoBehaviour
{
    Rigidbody2D rb;
    public Vector2 force;
    float posInicialY;
    GameObject Dino;
    float jumpDino, velDinoMax;
    DinoBehaviour db;
    [Tooltip("Tempo para repetição do pulo da bola")]
    public float time;
    // Use this for initialization
    void Start()
    {
        posInicialY = transform.position.y;
        Dino = GameObject.FindGameObjectWithTag("Player");
        db = GameObject.FindGameObjectWithTag("Player").GetComponent<DinoBehaviour>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        Attack();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y <= posInicialY)
        {
            if (!rb.isKinematic)
            {
                Stop();
                Invoke("Attack", time);
            }

        }
    }

    void Stop()
    {
        rb.isKinematic = true;
        rb.velocity = Vector2.zero;
    }
    void Attack()
    {
        rb.isKinematic = false;
        rb.AddForce(force);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Slime Puddle")
        {
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
        if (collision.tag == "Player")
        {
            Slow();
            Invoke("BackToNormal", 7);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Slime Puddle")
        {
            gameObject.GetComponent<CircleCollider2D>().enabled = true;
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    void Slow()
    {
        jumpDino = 350;
        velDinoMax = 1.85f;
        Dino.GetComponent<DinoBehaviour>().jumpForce = jumpDino;
        Dino.GetComponent<DinoBehaviour>().maxSpeed = velDinoMax;
    }

    void BackToNormal()
    {
        jumpDino = 650;
        velDinoMax = 4.5f;
        Dino.GetComponent<DinoBehaviour>().jumpForce = jumpDino;
        Dino.GetComponent<DinoBehaviour>().maxSpeed = velDinoMax;
    }
}
