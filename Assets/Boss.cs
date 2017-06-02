using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {
    private Transform target;
    private Animator animator;
    public int vidaBoss;
    private bool podeAtacar;
    public float vel;
    Rigidbody2D rb;
    private bool podeReceber;
    

    void Awake()
    {
        transform.tag = "Enemy"; //isto irá por a tag Enemy
    }

    // Use this for initialization
    void Start () {
        target = GameObject.FindGameObjectWithTag("Player").transform; //player
        rb = gameObject.GetComponent<Rigidbody2D>();
        podeAtacar = true;
        podeReceber = true;
        animator = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Animator>();


    }
	
	// Update is called once per frame
	void Update () {
        rb.velocity = Vector2.left * vel;


    }
}
