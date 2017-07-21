using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radar : MonoBehaviour {

	private EnemyAI script;

	void Start () {

		script = (EnemyAI)GetComponentInParent (typeof(EnemyAI));

	}

	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D col){

		if (col.tag == "Player") {
			script.lostPlayer = false;
			script.canMove = true;
		}

	} 
    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            script.ChasePlayer();
            Debug.Log("colliding");

        }
    }
    
    void OnTriggerExit2D(Collider2D col){
		
		if (col.tag == "Player") {
			script.lostPlayer = true;
			script.canMove = false;
		}

	}

}
