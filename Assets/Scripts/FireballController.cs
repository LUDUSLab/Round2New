using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballController : MonoBehaviour
{

    public DinoBehaviour Dino;
    public int Left;
    private Vector3 compag;

    private float TimeMax;
    private float compagTime;

    private float t;

    // Use this for initialization
    void Start()
    {

        Dino = FindObjectOfType<DinoBehaviour>().GetComponent<DinoBehaviour>();

        compagTime = 0;

        compag = transform.position;

        if (Dino.facingRight)
        {
            Left = 0;
        }
        else
        {
            Left = 1;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (Left == 1)
        {

            compag[0] -= Time.deltaTime * 20;
            transform.position = compag;

        }
        if (Left == 0)
        {

            compag[0] += Time.deltaTime * 20;
            transform.position = compag;
        }

        t += Time.deltaTime;
        Debug.Log(t);

        if (t > 0.6)
            Destroy(gameObject);

    }

    void OnTriggerEnter2d(Collider2D other)
    {

        if (other.CompareTag("Enemy"))
        {

            other.gameObject.SetActive(false);

        }
    }
}
