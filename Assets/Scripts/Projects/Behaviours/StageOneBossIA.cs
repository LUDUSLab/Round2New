using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageOneBossIA : MonoBehaviour {
    public GameObject right, left, middle;
    public float _timeToWait;
    int hp;
    Vector2 pos;
    Rigidbody2D rb;
    GameObject Dino;
    bool canMove, movingLeft, canTakeDamage;
    float speed = 10;
    int position; // 0 = inicio-meio, 1 = meio-fim
    public Vector2 thornPosInicial;
    Vector2 thornPosFinal;
    public GameObject[] posX;
    int numThornMax = 16;
    int numThorn;
    public GameObject Thorns;
	// Use this for initialization
	void Start () {
        position = 0;
        hp = 7;
        canMove = false;
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
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Attack();
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
                  //Debug.Log("indo pro comeco");
              }
          }
          WhereIsTheQueen();
       // Thorn();
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
            numThorn = 0;
        }
    }

    void Attack()
    {
        Debug.Log("começando ataque");
        canMove = false;
        int side = Random.Range(1, 3);
        Debug.Log(side);
        //gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, new Vector2(gameObject.transform.position.x, 8), speed/2 * Time.deltaTime);
        gameObject.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + 9);
        if (gameObject.transform.position.y <= gameObject.transform.position.y + 8)
        {
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
            canMove = false;
            Invoke("setCanMove", 2);
        }

    }
   
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bite" || collision.gameObject.tag == "FireBall")
        {
            if (canTakeDamage)
            {
                hp--;
                if (hp <= 0)
                {
                    StartCoroutine(Die());
                }
                
            } 
        }
        if (collision.gameObject.tag == "Player")
        {
            Dino.GetComponent<DinoBehaviour>().Damage(gameObject);
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
    }
}
