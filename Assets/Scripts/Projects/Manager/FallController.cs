using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallController : MonoBehaviour {
    LoadManager lm;
    // Use this for initialization
    void Start () {
        lm = FindObjectOfType<LoadManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            lm.LoadLevel("MenuRound2");
        }
    }
}
