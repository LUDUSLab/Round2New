using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageOneBossIA : MonoBehaviour {
    public GameObject right, left, middle;
    public float _timeToWait;
    public int hp;
    Vector2 pos;
    Rigidbody2D rb;
    GameObject Dino;
    bool canMove, movingLeft, canTakeDamage, thrn = false, attk = false, canAttk = false;
    float speed = 10;
    int position; // 0 = inicio-meio, 1 = meio-fim
    Animator anim;
    bool grounded = false;

    public Vector2 thornPosInicial;
    Vector2 thornPosFinal;
    public GameObject[] posX;
    int numThornMax = 16;
    int numThorn;
    public GameObject Thorns;

	// Use this for initialization
	void Start () {
        anim = gameObject.GetComponent<Animator>();
        position = 0;
        hp = 7;
        canMove = true;
        movingLeft = true;
        canTakeDamage = false;
        pos = gameObject.transform.position;
        Dino = GameObject.FindGameObjectWithTag("Player");
        rb = gameObject.GetComponent<Rigidbody2D>();
        numThorn = 0;
        thornPosFinal = new Vector2(thornPosInicial.x + 15, thornPosInicial.y);
    }
	
	// Update is called once per frame
	void Update () {
        //Debug.Log(canTakeDamage);
           if (gameObject.transform.position.y >= left.transform.position.y + 6)
           {
                rb.velocity = new Vector2(0, 0);
           }
           if (attk)
           {
               //Debug.Log("attk");
                attk = false;
                StartCoroutine(Attack());
           }

           if (thrn)
           {
               gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, right.transform.position, speed / 2 * Time.deltaTime);
               if(gameObject.transform.position.x >= right.transform.position.x)
               {
                  thrn = false;
                  gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(-0.5926915f, -0.7440171f);
                  gameObject.GetComponent<BoxCollider2D>().size = new Vector2(3.074617f, 1.791966f);
               }
           }

          if (canMove && movingLeft && position == 0)
          {

              gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, pos,speed * Time.deltaTime);
              if(gameObject.transform.position.x == pos.x && gameObject.transform.position.y == pos.y)
              {
                  //Debug.Log("indo pro meio ida");
                  pos.x = pos.x - 0.2f;
                  pos.y = pos.y - 0.075f;
              }
          }
          if(canMove && movingLeft && position == 1)
          {
              gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, pos, speed * Time.deltaTime);
              if (gameObject.transform.position.x == pos.x && gameObject.transform.position.y == pos.y)
              {
                  pos.x = pos.x - 0.2f;
                  pos.y = pos.y + 0.075f;
                  //Debug.Log("indo pro fim ida");
              }
          }
          if (canMove && !movingLeft && position == 1)
          {
              gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, pos, speed * Time.deltaTime);
              if (gameObject.transform.position.x == pos.x && gameObject.transform.position.y == pos.y)
              {
                  pos.x = pos.x + 0.2f;
                  pos.y = pos.y - 0.075f;
                  //Debug.Log("indo pro meio volta");
              }
          }
          if (canMove && !movingLeft && position == 0)
          {
              gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, pos, speed * Time.deltaTime);
              if (gameObject.transform.position.x == pos.x && gameObject.transform.position.y == pos.y)
              {
                  pos.x = pos.x + 0.2f;
                  pos.y = pos.y + 0.075f;
                  canAttk = false;
                  //Debug.Log("indo pro comeco");
              }
          }
          WhereIsTheQueen();
    }

    IEnumerator Thorn(int side, int numThorn)
    {
        numThorn++;
        yield return new WaitForSeconds(_timeToWait);
        if (numThorn < numThornMax - 2)
        {
            if(side == 1)
            {
                posX[numThorn] = Instantiate(Thorns, new Vector2(thornPosInicial.x + numThorn, thornPosInicial.y), transform.rotation);
            }
            else
            {
                posX[numThorn] = Instantiate(Thorns, new Vector2(thornPosFinal.x - numThorn, thornPosInicial.y), transform.rotation);
            }
            StartCoroutine(Thorn(side, numThorn));
        }
        else
        {
            gameObject.transform.position = new Vector2(middle.transform.position.x, gameObject.transform.position.y);
            numThorn = 0;
            anim.SetTrigger("Thorn");
            yield return new WaitForSeconds(_timeToWait);
            rb.gravityScale = 2;
        }
    }

    IEnumerator Attack()
    {
        //canMove = false;
        int side = Random.Range(1, 3); 
        rb.AddForce(new Vector2(0, 1) * 1750);
        yield return new WaitForSeconds(_timeToWait + 1);

            if(side == 1)
            {
                posX[numThorn] = Instantiate(Thorns, thornPosInicial, transform.rotation);
            }
            else
            {
                posX[numThorn] = Instantiate(Thorns, thornPosFinal, transform.rotation);
            }
            StartCoroutine(Thorn(side, numThorn));
    }
    
    void WhereIsTheQueen()
    {
        if(gameObject.transform.position.x <= right.transform.position.x && gameObject.transform.position.x >= middle.transform.position.x)
        {
            position = 0;
        }
        if (gameObject.transform.position.x >= left.transform.position.x && gameObject.transform.position.x <= middle.transform.position.x)
        {
            position = 1;
        }
        if (gameObject.transform.position.x <= left.transform.position.x)
        {
            movingLeft = false;
            pos.y = left.transform.position.y;
            gameObject.transform.rotation = new Quaternion(0, 180, 0, 0);
            canMove = false;
            Invoke("setCanMove", 2);
        }
        if (gameObject.transform.position.x >= right.transform.position.x)
        {
            movingLeft = true;
            pos.y = left.transform.position.y;
            gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
            
            if (!canAttk)
            {
                canMove = false;
                canAttk = true;
                int wht = Random.Range(0, 100);
               // Debug.Log(wht);
                if (wht >= 0 && wht < 50)
                {
                    Invoke("setCanMove", 2);
                }
                if(wht>=50 && wht < 100)
                {
                    Invoke("setCanAttack", 2);
                }   
            }
            
            
        }

    }
   
    private IEnumerator OnCollisionEnter2D(Collision2D collision)
    {
      /*  if (collision.gameObject.tag == "Bite" || collision.gameObject.tag == "FireBall")
        {
            if (canTakeDamage)
            {
                hp--;
                if (hp <= 0)
                {
                    StartCoroutine(Die());
                }
                
            } 
        }*/
        if (collision.gameObject.tag == "Player")
        {
            Dino.GetComponent<DinoBehaviour>().Damage(gameObject);
        }
        if(collision.gameObject.tag == "Ground")
        {
            grounded = true;
            rb.gravityScale = 0;
            canTakeDamage = true;
            gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(-0.5469666f, -0.7440171f);
            gameObject.GetComponent<BoxCollider2D>().size = new Vector2(2.197943f, 1.791966f);
            yield return new WaitForSeconds(_timeToWait + 3);
            canTakeDamage = false;
            thrn = true;
            canAttk = false;
            if (grounded)
            {
                anim.SetTrigger("BackToNormal");
            }
            
            

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Bite" || collision.tag == "FireBall")
        {
            if (canTakeDamage)
            {
                hp--;
                grounded = false;
                if (hp <= 0)
                {
                    StartCoroutine(Die());
                }
                else
                {
                    
                    canTakeDamage = false;
                    thrn = true;
                    anim.SetTrigger("BackToNormal");
                    canAttk = false;
                }

            }
        }
    }

    IEnumerator Die()
    {
        rb.gravityScale = 2;
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }

    void setCanMove()
    {
        canMove = true;
        //Debug.Log("podemos nos mover");
    }
    void setCanAttack()
    {
        attk = true;
        //Debug.Log("podemos atacar");
    }
}
