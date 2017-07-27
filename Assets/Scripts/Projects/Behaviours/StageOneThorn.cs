using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageOneThorn : MonoBehaviour {
    GameObject Dino;
    // Use this for initialization
    void Start () {
        Dino = GameObject.FindGameObjectWithTag("Player");
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Dino.GetComponent<DinoBehaviour>().Damage(gameObject);
        }
        if (collision.gameObject.tag == "Ground")
        {
            yield return new WaitForSeconds(0.01f);
            Destroy(gameObject);
        }
    }
}
