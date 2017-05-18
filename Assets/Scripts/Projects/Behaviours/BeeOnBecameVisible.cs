using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeOnBecameVisible : MonoBehaviour {
    GameObject Dino;
    public float distance;
    Transform posDino;
	// Use this for initialization
	void Start () {
        Dino = GameObject.FindGameObjectWithTag("Player");
        posDino = Dino.GetComponent<Transform>();
	}

    void Update()
    {
        if (posDino.position.x<gameObject.transform.position.x)
        {
            if(-posDino.position.x + gameObject.transform.position.x < distance)
            {
                gameObject.GetComponent<BeeIA>().enabled = true;
            }
            else
            {
                gameObject.GetComponent<BeeIA>().enabled = false;
            }
            
        }
        else
        {
            if (+posDino.position.x - gameObject.transform.position.x < distance)
            {
                gameObject.GetComponent<BeeIA>().enabled = true;
            }
            else
            {
                gameObject.GetComponent<BeeIA>().enabled = false;
            }
        }
    }
  
}
