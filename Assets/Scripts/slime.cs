using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slime : MonoBehaviour {
    private Animator animator;
    Rigidbody2D rb;
    private GameObject Dino;
    public float vel, tempoalt;
    Vector2 posInicial, velocidade;
    private bool CanMove, isRight;
    public float stay;

    void Awake()
    {
        transform.tag = "Enemy"; //isto irá por a tag Enemy
    }

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        posInicial = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
        animator = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Animator>();
        CanMove = true;
        isRight = true;
        StartCoroutine(turn());
        Dino = GameObject.FindGameObjectWithTag("Player");

    }
	
	// Update is called once per frame
	void Update () {

        if (CanMove)
        {
            if (!isRight)
            {
                //Debug.Log("esquerda");
                MovimentoEsq();
            }
            else
                //Debug.Log("direita");
                MovimentoDir();
        }
        //movimentação do slime
        

       

    }
     IEnumerator turn()
    {
        if (isRight == true)
        {
            isRight = false;

            yield return new WaitForSeconds(tempoalt);
        
        }
        else
        {
            isRight = true;
            yield return new WaitForSeconds(tempoalt);
        }
        CanMove = false;
        animator.SetTrigger("stay");
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(stay);
        CanMove = true;
        StartCoroutine(turn());

    }


    void MovimentoDir()
    {
        animator.SetTrigger("walk");
        rb.velocity = Vector2.right * vel;
    }
    void MovimentoEsq()
    {
        animator.SetTrigger("walk");
        rb.velocity = Vector2.left *(- vel);
       
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Dino.GetComponent<DinoBehaviour>().Damage(gameObject);
        }

        if (collision.gameObject.tag == "Bite")
        {
                Destroy(gameObject);

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "FireBall")
        {
            Destroy(gameObject);
        }
    }
}



