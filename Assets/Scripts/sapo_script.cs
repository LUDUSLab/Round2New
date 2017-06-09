using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class sapo_script : MonoBehaviour
{
    private Transform target;
    private Animator animator;
    private float Distancia;
    public float chaseRange;// distancia de onde o sapo começa a seguir o dino
    public double atqRange = 1.0;//range do ataque do sapo
    public int vidaSapo;//vida do sapo
    private GameObject Dino;
    public int Damage;//dano do ataque do sapo
    private bool podeAtacar;
    public float vel;
    Rigidbody2D rb;
    private bool podeReceber;



    void Awake()
    {
        transform.tag = "Enemy"; //isto irá por a tag Enemy
    }

    // Use this for initialization
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").transform;//player
        Dino = GameObject.FindWithTag("Player");
        podeAtacar = true;
        podeReceber = true;
        animator = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Distancia = Vector2.Distance(transform.position, target.transform.position); // o inimigo irá mover-se até a posição do player
        if (Distancia < chaseRange)
        {
            animator.SetTrigger("walk");
            Vector2 dino = new Vector2(Dino.GetComponent<Transform>().position.x, gameObject.transform.position.y);
            gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, dino, 2 * Time.deltaTime);
            //rb.AddForce(Vector2.left * vel, 0);
        }
        if (Distancia < atqRange)
        {
            ataque();
            Debug.Log("Ta atacando ou não?");


        }
        //aqui o enemy recebe dano do dino REVER
    /*    if (Distancia < 1.5)
        {
            if (Dino.GetComponent<DinoBehaviour>().isBitting == true)
            {
                Debug.Log("Sapo perdeu vida");
                LoseHP();
            }


        }

        if (vidaSapo <= 0)
        { // se a vida do inimigo for menor ou igual a 0 ele ira auto-destruir-se 
            Destroy(gameObject);
            Debug.Log("pq morreu, morreu pq?");
        }

    */
    }

    void LoseHP()
    {
        if (podeReceber == true)
        {
            vidaSapo--;
        }


    }

    void ataque()
    {
        if (podeAtacar == true)
        {
            animator.SetTrigger("ataque");
        }

    }

    IEnumerator recebeDano()
    {
        podeReceber = false;
        Debug.Log("Tempo de espera");
        yield return new WaitForSeconds(1);
        podeReceber = true;
    }

    IEnumerator TempoDeAtaque()
    { // isto é o tempo em que o inimigo espera entre cada ataque
        podeAtacar = false;
        yield return new WaitForSeconds(1);
        podeAtacar = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Dino.GetComponent<DinoBehaviour>().Damage(gameObject);
        }

        if (collision.gameObject.tag == "Bite")
        {
            LoseHP();
            if (vidaSapo <= 0)
            {
                Destroy(gameObject);
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "FireBall")
        {
            LoseHP();
            if (vidaSapo <= 0)
            {
                Destroy(gameObject);
            }

        }
    }

}



