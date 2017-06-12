using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chefe : MonoBehaviour {
    private Transform target;
    private Animator animator;
    private float Distancia;
    public int vidaBoss;//vida do sapo
    private GameObject Dino;
    public int Damage;//dano do ataque do sapo
    private bool CanMove;
    public float vel;
    Rigidbody2D rb;
    private bool podeReceber;

    // Use this for initialization
    void Start () {
        rb = gameObject.GetComponent<Rigidbody2D>();
        Way = GameObject.FindGameObjectWithTag("waypoint").transform;//player
        CanMove = true;
        podeReceber = true;
        animator = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Animator>();

    }
	
	// Update is called once per frame
	void Update () {
        if (CanMove)
            gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, Way, 2 * Time.deltaTime);

    }
}
