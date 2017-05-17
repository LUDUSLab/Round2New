using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]


public class sapo_script : MonoBehaviour {
    public float chaseRange;// distancia de onde o sapo começa a seguir o dino
    public double atqRange = 1.0;//range do ataque do sapo
    public int vidaSapo;//vida do sapo
    private GameObject Dino;
    private GameObject Sapo;
    public int Damage;//dano do ataque do sapo
    private UnityEngine.AI.NavMeshAgent navMesh;
    private bool podeAtacar;



    void Awake()
    {
        transform.tag = "Enemy"; //isto irá por a tag Enemy
    }

    // Use this for initialization
    void Start () {
        Sapo = GameObject.FindWithTag("Enemy");
        Dino = GameObject.FindWithTag("Player");
        navMesh = GetComponent<UnityEngine.AI.NavMeshAgent>();
        podeAtacar = true;
    }

    // Update is called once per frame
    void Update() {
        navMesh.destination = Dino.transform.position; // o inimigo irá mover-se até a posição do inimigo
        if (Vector3.Distance(transform.position, Dino.transform.position) < atqRange)
        { 
            ataque();
        }
        //aqui o enemy recebe dano do dino REVER
         if (Vector3.Distance (transform.position, Dino.transform.position) < 2) { 
                vidaSapo -= 1;
         }
    
        if (vidaSapo <= 0) { // se a vida do inimigo for menor ou igual a 0 ele ira auto-destruir-se 
            Destroy(Sapo);
    }
 

    }

    void ataque() {
    if (podeAtacar == true)
    {
        StartCoroutine("TempoDeAtaque");
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
        


     
    