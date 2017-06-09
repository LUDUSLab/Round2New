using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeIA : MonoBehaviour {
    public GameObject[] bees;
    private GameObject Dino;
    public int numBees;
    int nb;
    Vector2 direction;
    bool canAttack;
    Rigidbody2D rd;
    public float thrust;
    Animator anim;
   
	// Use this for initialization
	void Start () {
        anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        canAttack = true;
        nb = 0;
        Dino = GameObject.FindGameObjectWithTag("Player");
        InvokeRepeating("BeeAttack", 2, 2);
        
    }
	
	// Update is called once per frame
	void Update () {
        if ((nb > numBees-1)||(bees[numBees-1]==null))
        {
            CancelInvoke();

        }
    }

    public void BeeAttack()
    {
        if (nb < numBees)
        {
            rd = bees[nb].GetComponent<Rigidbody2D>();
            bees[nb].GetComponent<BoxCollider2D>().enabled = true;
            direction = new Vector3(Dino.transform.position.x - bees[nb].transform.position.x, Dino.transform.position.y - bees[nb].transform.position.y);
            rd.AddForce(direction * thrust);
            nb++;
        }
        else
        {
            canAttack = false;
        }  
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Player")
        {
            Dino.GetComponent<DinoBehaviour>().Damage(gameObject);
        }
        if (collision.tag == "Bite")
        {
            Destroy(gameObject);
        }
        if(collision.tag == "FireBall")
        {
            Destroy(gameObject);
        }
    }
    
}
