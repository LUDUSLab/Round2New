using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimePool : MonoBehaviour {
    GameObject Dino;
    Rigidbody2D rbDino;
    float velDinoMax, jumpDino;
    public int waitTime;
    float velY, accel;
    bool slow, back;
    DinoBehaviour db;
	// Use this for initialization
	void Start () {
        Dino = GameObject.FindGameObjectWithTag("Player");
        db = Dino.GetComponent<DinoBehaviour>();
        rbDino = Dino.GetComponent<Rigidbody2D>();
        jumpDino = Dino.GetComponent<DinoBehaviour>().jumpForce;
        velDinoMax = Dino.GetComponent<DinoBehaviour>().maxSpeed;
        velY = rbDino.velocity.y;
        accel = db.maxAccel;
        //slow = true;
       // back = true;
    }
	
	// Update is called once per frame
	void Update () {
      
	}

    void Slow()
    {
       // if (slow)
        //{
            jumpDino = 350;
            velDinoMax = 1.85f;
            accel = 1;
            Dino.GetComponent<DinoBehaviour>().jumpForce = jumpDino;
            Dino.GetComponent<DinoBehaviour>().maxSpeed = velDinoMax;
        //}
    }
        

    void BackToNormal()
    {
        //if (back)
        //{
        accel = 2;
            jumpDino = 650;
            velDinoMax = 4.5f;
            Dino.GetComponent<DinoBehaviour>().jumpForce = jumpDino;
            Dino.GetComponent<DinoBehaviour>().maxSpeed = velDinoMax;
        //}
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            //back = true;
            //Debug.Log("entrou");
            rbDino.gravityScale = 0.05f;
            Slow();
            //slow = false;
            //rbDino.velocity = new Vector2(rbDino.velocity.x, -3);
        }
    }

    private IEnumerator OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            //slow = true;
            //Debug.Log("sai");
            Dino.GetComponent<DinoBehaviour>().jumpForce = 400;
            rbDino.gravityScale = 1.5f;
            Invoke("setGravity", 1);
            //back = false;
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
