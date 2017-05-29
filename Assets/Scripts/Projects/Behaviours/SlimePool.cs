using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimePool : MonoBehaviour {
    GameObject Dino;
    Rigidbody2D rbDino;
    float velDinoMax, jumpDino;
    public int waitTime;
    float velY;
    DinoBehaviour db;
	// Use this for initialization
	void Start () {
        Dino = GameObject.FindGameObjectWithTag("Player");
        db = Dino.GetComponent<DinoBehaviour>();
        rbDino = Dino.GetComponent<Rigidbody2D>();
        jumpDino = Dino.GetComponent<DinoBehaviour>().jumpForce;
        velDinoMax = Dino.GetComponent<DinoBehaviour>().maxSpeed;
        velY = rbDino.velocity.y;

    }
	
	// Update is called once per frame
	void Update () {
      
	}

    void Slow()
    {
        jumpDino = jumpDino - 250;
        velDinoMax = velDinoMax / 2;
        Dino.GetComponent<DinoBehaviour>().jumpForce = jumpDino;
        Dino.GetComponent<DinoBehaviour>().maxSpeed = velDinoMax;
    }

    void BackToNormal()
    {
        jumpDino = jumpDino + 250;
        velDinoMax = velDinoMax * 2;
        Dino.GetComponent<DinoBehaviour>().jumpForce = jumpDino;
        Dino.GetComponent<DinoBehaviour>().maxSpeed = velDinoMax;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Debug.Log("entrou");
            rbDino.gravityScale = 0.05f;
            Slow();
            //rbDino.velocity = new Vector2(rbDino.velocity.x, -3);
        }
    }

    private IEnumerator OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Debug.Log("sai");
            Dino.GetComponent<DinoBehaviour>().jumpForce = jumpDino + 250;
            Invoke("setGravity", 0.5f);
            //rbDino.velocity = new Vector2(rbDino.velocity.x, velY);
            yield return new WaitForSeconds(waitTime);
            BackToNormal();
        }
    }

    void setGravity()
    {
        rbDino.gravityScale = 1;
    }
}
