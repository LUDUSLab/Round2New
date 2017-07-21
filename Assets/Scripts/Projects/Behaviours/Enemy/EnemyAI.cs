using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

	public Transform enemyHome;
	private Transform player;
	private Vector3 positionPlayerLost;
	private Vector3 positionPlayerFind;
	private Transform enemy;
	private GameObject Dino;
    public bool isChasing;
    private float initialXPos;
    private Animator anime;
	Rigidbody2D rb;
    public float currentChaseTime;
	public int damage;
	public int lifePepe;
	public double atkRange;
    public float moveCooldown;
	public float speed;
    public float chaseRange;
    private float startTime;
	private float walkPath;
	private float journeyLenght;
    public float chaseLenght;
	public bool lostPlayer = false;
	public bool canMove = false;
	private bool canAttack = false;
	private bool canLose = false;

	void Awake()
	{
		transform.tag = "Enemy"; //isto irá por a tag Enemy
	}

	// Use this for initialization
	void Start () {
		rb = gameObject.GetComponent<Rigidbody2D>();
		enemy = GetComponent<Transform>();
		enemyHome = enemy.transform.parent;
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		Dino = GameObject.FindWithTag ("Player");
		canAttack = true;
		canLose = true;
		positionPlayerLost = enemyHome.position;
		anime = GameObject.FindGameObjectWithTag ("Enemy").GetComponent<Animator>();
        BackToHome();
	}
	
	// Update is called once per frame
	void Update () {
        /*
		if (canMove){ 

			if (lostPlayer) {
				float dist = (Time.time - startTime) * speed;
				float journey = dist / journeyLenght;

				if (enemy.position == enemyHome.position)
					canMove = false;
				enemy.position = Vector3.Lerp (positionPlayerLost, enemyHome.position, journey);
			} else
				enemy.position = Vector3.Lerp (enemy.position, player.position, 0.05f);
		}
		*/

        if (canMove)
        {
            ChasePlayer();
            /*
            walkPath = Vector2.Distance(transform.position, player.transform.position); // o inimigo irá mover-se até a posição do player
            if (walkPath < chaseRange)
            {
                if (!isChasing)
                {
                    Debug.Log("Perseguindo");
                    initialXPos = player.transform.position.x;
                    isChasing = true;
                }
                else
                {
                    ChasePlayer();
                }
            }
            if (walkPath < atkRange)
            {
                Attack();
                //Debug.Log("Ta atacando ou não?");


            }
        }
        */
        }
	}
	public void ChasePlayer()
    {
        CheckCoolDown();
        Vector2 dino;
        if (canMove)
        {
             dino = new Vector2(Dino.GetComponent<Transform>().position.x, gameObject.transform.position.y);
            anime.SetTrigger("Move");
        }
        else
        {
             dino = transform.position;
        }
        gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, dino, speed * Time.deltaTime);
    }
    void LoseHP(){
        if (canLose)
		    lifePepe--;
	}

    void CheckCoolDown()
    {
        currentChaseTime += Time.deltaTime;
        if (currentChaseTime >= moveCooldown)
        {
            StartCoroutine(MoveCooldown());
        }
    }
    void BackToHome()
    {
        startTime = Time.time;
        positionPlayerLost = enemy.position;
        journeyLenght = Vector3.Distance(positionPlayerLost, enemyHome.position);
    }
    
	void Attack(){
		if (canAttack) 
			anime.SetTrigger ("Attack");
	}

	IEnumerator TakeDamageCooldown(){ // tempo de espera do inimigo levar dano
		canLose = false;
		yield return new WaitForSeconds(1);
		canLose = true;

	}

	IEnumerator AttackCooldown(){ // isto é o tempo em que o inimigo espera entre cada ataque
		canAttack = false;
		yield return new WaitForSeconds(1);
		canAttack = true;
	}

    IEnumerator MoveCooldown(){ // intervalo de tempo para o inimigo se movimentar novamete
        canMove = false;
        yield return new WaitForSeconds(moveCooldown);
        canMove = true;
        currentChaseTime = 0;
    }

	private void OnCollisionEnter2D(Collision2D col){
		if (col.gameObject.tag == "Player")
			Dino.GetComponent<DinoBehaviour>().Damage(gameObject);
	}

	void OnTriggerEnter2D(Collider2D col){

		if (col.tag == "Player") {
			lostPlayer = false;
		}
        if (col.gameObject.tag == "Bite"){
            LoseHP();
            StartCoroutine(TakeDamageCooldown());
            if (lifePepe <= 0){
                //Debug.Log("morreu");
                Destroy(gameObject);
            }
        }
    }

}
