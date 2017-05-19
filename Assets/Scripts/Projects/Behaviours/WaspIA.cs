using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaspIA : MonoBehaviour {
    public int hp;
    public float thrust, speed,_waitTime;
    GameObject Dino;
   
    bool canAttack, ehnois,initiattack=true;
    Rigidbody2D rb;
    Vector2 direction, posInicial, posFinal;
    
	// Use this for initialization
	void Start () {
        hp = 3;
        
        Dino = GameObject.FindGameObjectWithTag("Player");
        posFinal = new Vector2(Dino.transform.position.x, Dino.transform.position.y);
        rb = gameObject.GetComponent<Rigidbody2D>();
        posInicial = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
        canAttack = true;
        ehnois = false;
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log(posFinal.x);
        if ((!canAttack)&&(ehnois))
        {
            Vector2 pos = new Vector2(posFinal.x + 6, posInicial.y);
            gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position,pos, speed * Time.deltaTime);
            if (initiattack)
            {
                if (gameObject.transform.position.x == pos.x)
                {
                    Invoke("InitiateAttack", _waitTime);
                    initiattack = false;
                }
            }
        }
        else{
            if (canAttack)
            {
                Attack();
                
            }
            
        }
        if(gameObject.transform.position.x == posFinal.x+0.5)
        {
            ehnois = true;
            canAttack = false;
            rb.velocity = new Vector2(0, 0);
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
            direction = new Vector2(posFinal.x - gameObject.transform.position.x, posFinal.y - gameObject.transform.position.y);
        }
        else
        {
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
    }
}
