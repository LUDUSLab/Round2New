using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeCollision : MonoBehaviour {
    Animator anim;
	// Use this for initialization
	void Start () {
        anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            yield return new WaitForSeconds(1f);
            if (gameObject != null)
            {
                Destroy(gameObject);
            }
        }

    }
}
