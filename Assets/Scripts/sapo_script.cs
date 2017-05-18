using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class sapo_script : MonoBehaviour {
    private Transform target;
    private Animator animator;
    private float Distancia;
    public float chaseRange;// distancia de onde o sapo começa a seguir o dino
    public double atqRange = 1.0;//range do ataque do sapo
    public int vidaSapo;//vida do sapo
    private GameObject Sapo;
    private GameObject Dino;
    public int Damage;//dano do ataque do sapo
    private bool podeAtacar;
    public float vel;
    Rigidbody2D rb;



    void Awake()
    {
        transform.tag = "Enemy"; //isto irá por a tag Enemy
    }

    // Use this for initialization
    void Start () {
        rb = gameObject.GetComponent<Rigidbody2D>();
        Sapo = GameObject.FindWithTag("Enemy");
        target = GameObject.FindGameObjectWithTag("Player").transform;//player
        Dino = GameObject.FindWithTag("Player");
        podeAtacar = true;
    }

    // Update is called once per frame
    void Update() {
        Distancia = Vector2.Distance(transform.position, target.transform.position); // o inimigo irá mover-se até a posição do player
        if (Distancia < chaseRange)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, Dino.GetComponent<Transform>().position, 2*Time.deltaTime);
            //rb.AddForce(Vector2.left * vel, 0);
        }
        if (Distancia < atqRange)
        { 
            ataque();

        }
        //aqui o enemy recebe dano do dino REVER
         if (Distancia < 1.5) { 
                vidaSapo -= 1;
         }
    
        if (vidaSapo <= 0) { // se a vida do inimigo for menor ou igual a 0 ele ira auto-destruir-se 
            Destroy(Sapo);
    }
 

    }
    


    void ataque() {
        if (podeAtacar == true)
        {
 
            Dino.GetComponent<DinoBehaviour>().hp -= Damage; 
        }

    }

    IEnumerator TempoDeAtaque()
    { // isto é o tempo em que o inimigo espera entre cada ataque
        podeAtacar = false;
        yield return new WaitForSeconds(1); 
        podeAtacar = true;
    }

    }
        
   
     
    