using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCopy : EnemigoDisparo
{
    public GameObject projectilePrefab;

    void Awake()
    {
        source = GetComponent<AudioSource>();
        jugador = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    void Start () {
        
        health = 100f;
        nextFire = 0f;
        jugador = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        speed = jugador.GetSpeed();
    }
	
	void Update ()
    {
        if (speed <= 0)
        {
            speed = jugador.GetSpeed();
        }
        Die();
        Move();
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
        {
            FireRocket();
        }
    }

    void FireRocket()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + 1.5f;
            source.PlayOneShot(shoot, 5f);
            Instantiate(projectilePrefab, transform.position, Quaternion.Euler(0, 0, 0));
        }
    }

    void Move()
    {

        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 screenWorldPos = Camera.main.ScreenToWorldPoint(screenPos);
        float axisX = 0;
        float axisY = 0;
        if (Input.GetKey("w"))
        {
            axisY = -0.5f;
        }
        else if (Input.GetKey("s"))
        {
            axisY = 0.5f;
        }
        if (Input.GetKey("d"))
        {
            axisX = -0.5f;
        }
        else if (Input.GetKey("a"))
        {
            axisX = 0.5f;
        }

        if (screenPos.x <= 0)
            transform.position = new Vector3(screenWorldPos.x + 0.05f, screenWorldPos.y);
        else if (screenPos.x >= Screen.width)
            transform.position = new Vector3(screenWorldPos.x - 0.05f, screenWorldPos.y);
        else
            transform.Translate(new Vector3(axisX, axisY) * Time.deltaTime * speed);
    }
}
