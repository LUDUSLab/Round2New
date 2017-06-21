using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinoBehaviour : MonoBehaviour
{
    bool canSpawn;
    GameObject EnemySpawned;
    Vector2 dinoAfterTime;
    public GameObject Enemy;
    public float maxSpeed;
    public float maxAccel;
    private float accel = 0f;
    public float accelInc;
    private int SpeedMinus;
    public Vector3 RespawnPoint;
    public bool isBitting, slimed, isShooting;
    float delayToJump = 1f;
    bool canTakeDamage;
    public GameObject Fireball;
    bool canMove;
    float timeToSpawnNewEnemies, time;

    private float TimeSlime;
    private float compagTime;

    Rigidbody2D rb;
    public bool facingRight = true;
    float move;

    private int respawn;

    bool grounded = false;

    public Transform groundCheck1;
    public Transform groundCheck2;
    public LayerMask whatIsGround, slimePuddle;
    public float jumpForce;
    public float falling;

    Animator anim;

    public int hp;
    public float knock;
    float takingDamage = 0f;
    bool jumped;

    LoadManager lm;

    void Start()
    {
        canSpawn = false;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        isBitting = false;
        isShooting = false;
        lm = FindObjectOfType<LoadManager>();
        //Fireball = GameObject.FindGameObjectWithTag("FireBall");
        RespawnPoint = transform.position;
        canMove = true;
        timeToSpawnNewEnemies = 50;
        time = 0;

        hp = 3;
        respawn = 0;
        SpeedMinus = 0;
        TimeSlime = 0;
        slimed = false;
        jumped = false;
        canTakeDamage = true;
    }

    void Update()
    {
        jump();
        attack();
        Die();
        FireBall();
        if (move == 0)
        {
            time += Time.deltaTime;
        }
        else
        {
            time = 0;
        }

        if (time >= timeToSpawnNewEnemies)
        {
            time = 0;

            dinoAfterTime = new Vector2(gameObject.transform.position.x + 10, gameObject.transform.position.y);
            //instanciar aqui novos inimigos apos certo tempo
            EnemySpawned = Instantiate(Enemy, new Vector3(gameObject.transform.position.x + 10, gameObject.transform.position.y, gameObject.transform.position.z), Quaternion.identity);
            canSpawn = true;
            //Debug.Log(EnemySpawned.transform.position);
        }
        if (canSpawn)
        {
            if (EnemySpawned.transform.position.x > dinoAfterTime.x - 4.5f)
            {
                Vector2 vec = new Vector2(dinoAfterTime.x - 4.5f, dinoAfterTime.y);
                EnemySpawned.transform.position = Vector2.MoveTowards(EnemySpawned.transform.position, vec, 2 * Time.deltaTime);
            }
            else
            {
                canSpawn = false;
            }
        }


    }

    void FixedUpdate()
    {
        getValues();
        setAnimations();
        walk();
        accelIncrement();

        if (maxSpeed == 2)
        {

            compagTime = Time.time;

            if ((TimeSlime + 3) < compagTime)
            {

                maxAccel = 2;
                maxSpeed = 3;

            }
        }
    }


    #region Collisions

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Checkpoint"))
        {
            respawn = 1;
            RespawnPoint = other.transform.position;
        }

        if (other.CompareTag("Life"))
        {

            if (hp < 3)
            {

                hp++;
                other.gameObject.SetActive(false);

            }

        }
    }


    #endregion

    #region DinoActions

    void walk()
    {
        if (takingDamage <= 0f)
            rb.velocity = new Vector2(move * maxSpeed * accel, rb.velocity.y);
        else
            takingDamage = takingDamage - Time.deltaTime;

        if (move > 0 && !facingRight) Flip();
        else if (move < 0 && facingRight) Flip();
    }

    void jump()
    {

        if ((grounded && Input.GetButtonDown("Jump") && takingDamage <= 0 && !jumped && !isBitting) || (slimed) && Input.GetButtonDown("Jump"))
        {

            //rb.velocity = new Vector2(rb.velocity.x, 0);
            jumped = true;
            grounded = false;

            rb.AddForce(new Vector2(0, jumpForce));
            StartCoroutine(SpamBlockco());
        }
        if (rb.velocity == new Vector2(rb.velocity.x, 0) && !grounded)
        {
            rb.AddForce(new Vector2(0, -55));
        }
    }

    void attack()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (!isBitting)
            {
                StartCoroutine(DelayToBite());
            }
        }
    }

    void FireBall()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            if (!isShooting)
            {
                StartCoroutine(DelayToShoot());
            }
        }
    }

    public void Damage(GameObject enemy)
    {
        StartCoroutine(takeDamage(enemy));
    }

    public IEnumerator takeDamage(GameObject enemy)
    {
        if (canTakeDamage)
        {
            canTakeDamage = false;
            hp--;
            rb.velocity = Vector3.zero;

            if (enemy.transform.position.x > transform.position.x)
            {
                rb.AddForce(new Vector2(knock * (-250f), 100));
            }
            else
            {
                rb.AddForce(new Vector2(knock * (250f), 100));
            }

            takingDamage = 0.15f;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            yield return new WaitForSeconds(0.8f);
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
            canTakeDamage = true;
        }

    }



    #endregion

    #region Actions

    void setAnimations()
    {
        anim.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        anim.SetBool("Grounded", grounded);
        anim.SetFloat("Falling", falling);
        //anim.SetFloat("takingDamage", takingDamage);
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void getValues()
    {
        grounded = Physics2D.OverlapArea(groundCheck1.position, groundCheck2.position, whatIsGround);
        slimed = Physics2D.OverlapArea(groundCheck1.position, groundCheck2.position, slimePuddle);
        if (canMove)
        {
            move = Input.GetAxis("Horizontal");
        }
        else
        {
            move = 0;
        }

        falling = rb.velocity.y;
    }

    void accelIncrement()
    {
        if (move != 0f && accel < maxAccel) accel += accelInc;
        else if (move == 0f) accel = 0f;
    }

    public bool getFlip()
    {
        return facingRight;
    }

    public int getHp()
    {
        return hp;
    }

    #endregion

    public IEnumerator SpamBlockco()
    {
        if (jumped == true)
        {
            yield return new WaitForSeconds(delayToJump);
        }
        yield return null;
        jumped = false;
    }

    IEnumerator BiteCheck()
    {
        GetComponentInChildren<biteScript>().SetCollider(true);
        yield return new WaitForSeconds(0.5f);
        GetComponentInChildren<biteScript>().SetCollider(false);
        //isBitting = false;
    }

    public IEnumerator DelayToBite()
    {
        if (isBitting == false)
        {
            isBitting = true;
            anim.SetBool("isBitting", isBitting);
            anim.SetTrigger("Bite");
            //rb.velocity = new Vector2(0, 0);

            //canMove = false;
            if (grounded)
            {
                canMove = false;
            }
           
            StartCoroutine(BiteCheck());
            //Invoke("setMoveDelay", 0.37f);
            Invoke("setBiteDelay", 0.7f);
            yield return new WaitForSeconds(0.3f);
            //isBitting = false;
            anim.SetBool("isBitting", false);
            canMove = true;
        }
        yield return null;
        //isBitting = false;
    }

    public IEnumerator DelayToShoot()
    {
        if (isShooting == false)
        {
            anim.SetBool("isBitting", isShooting);
            isShooting = true;
            anim.SetTrigger("Bite");

            //rb.velocity = new Vector2(0, 0);
            canMove = false;
            yield return new WaitForSeconds(0.32f);

            Vector3 vec = new Vector3(gameObject.transform.position.x + 0.5f, gameObject.transform.position.y, gameObject.transform.position.z);
            Instantiate(Fireball, vec, gameObject.transform.rotation);
            canMove = true;
            Invoke("setShootDelay", 1f);
        }
        yield return null;
        //isShooting = false;
    }

    void Die()
    {
        if (hp <= 0 && respawn != 1)
        {
            lm.LoadLevel("MenuRound2");
        }
    }

    void setShootDelay()
    {
        isShooting = false;
    }

    void setBiteDelay()
    {
        isBitting = false;
    }
    void setMoveDelay()
    {
        canMove = true;
    }
}
