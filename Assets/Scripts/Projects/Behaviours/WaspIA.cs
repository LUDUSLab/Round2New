using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaspIA : MonoBehaviour {
    public int hp;
    public float thrust, speed,_waitTime;
    GameObject Dino;
    bool turnedLeft;
    Vector2 pos;
    bool canAttack, ehnois, initiattack=true;
    Rigidbody2D rb;
    Vector2 direction, posInicial, posFinal;
    
	// Use this for initialization
	void Start () {
        hp = 3;
        turnedLeft = true;
        Dino = GameObject.FindGameObjectWithTag("Player");
        posFinal = new Vector2(Dino.transform.position.x, Dino.transform.position.y);
        rb = gameObject.GetComponent<Rigidbody2D>();
        posInicial = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
        canAttack = true;
        ehnois = false;

	}
	
	// Update is called once per frame
	void Update () {


        if ((!canAttack) && (ehnois))
        {
            if (turnedLeft) {
                pos = new Vector2(posFinal.x + 6, posInicial.y);
                //gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, pos, speed * Time.deltaTime);
            }
            else
            {
                pos = new Vector2(posFinal.x, posInicial.y);
            }

            gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position,pos, speed * Time.deltaTime);
            if (initiattack)
            {
                if ((gameObject.transform.position.x == pos.x))
                    {
                        Invoke("InitiateAttack", _waitTime);
                        initiattack = false;
                    if (Dino.transform.position.x < gameObject.transform.position.x)
                    {
                        turnedLeft = true;
                        gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
                    }
                    else
                    {
                        turnedLeft = false;
                        gameObject.transform.rotation = new Quaternion(0, 180, 0, 0);
                    }
                }
            }
        }

        else{
            if (canAttack)
            {
                Attack();
                
            }
            
        }

        if(gameObject.transform.position.x <= posFinal.x+0.5)
        {
            ehnois = true;
            canAttack = false;
            rb.velocity = new Vector2(0, 0);
            if (Dino.transform.position.x < gameObject.transform.position.x)
            {
                turnedLeft = true;
                gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
            }
            else
            {
                turnedLeft = false;
                gameObject.transform.rotation = new Quaternion(0, 180, 0, 0);
            }
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            canAttack = false;
            ehnois = true;
            rb.velocity = new Vector2(0, 0);
        }
    }

    void Attack()
    {
        posFinal = new Vector2(Dino.transform.position.x, Dino.transform.position.y);
        if (Dino.transform.position.x < gameObject.transform.position.x)
        {
            Debug.Log("esquerda");
            direction = new Vector2(posFinal.x - gameObject.transform.position.x, posFinal.y - gameObject.transform.position.y);
        }
        else
        {
            Debug.Log("direita");
            direction = new Vector2(-posFinal.x + gameObject.transform.position.x, -posFinal.y + gameObject.transform.position.y);
        }
        rb.AddForce(direction * thrust);
        canAttack = false;
        ehnois = false;
        initiattack = true;
    }

    void setAttackTrue()
    {
        canAttack = true;
    }
    void InitiateAttack()
    {
        this.canAttack = true;
        if (Dino.transform.position.x < gameObject.transform.position.x)
        {
            turnedLeft = true;
            gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
        }
        else
        {
            turnedLeft = false;
            gameObject.transform.rotation = new Quaternion(0, 180, 0, 0);
        }
    }
}
