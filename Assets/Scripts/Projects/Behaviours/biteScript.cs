using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class biteScript : MonoBehaviour {

    public DinoBehaviour player;

    private void Start()
    {
    }


    public void SetCollider(bool isActive) {
        gameObject.GetComponent<Collider2D>().enabled = isActive;
    }

}
