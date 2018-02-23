﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoP : MonoBehaviour {


    public Transform target;//set target from inspector instead of looking in Update
    public GameObject projectilePrefab;
    private float health;
    private Player jugador;
    float nextFire;



    void Start()
    {
        nextFire = 0f;
        health = 20f;
        jugador = target.GetComponent<Player>();
    }

    void Update()
    { 
        fireRocket();
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Projectile")
        {
            health -= jugador.GetDamage();
            if (health <= 0)
            {
                Destroy(gameObject);
            }

            Destroy(coll.gameObject);
        }
    }

    void fireRocket()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + 1f;

            Instantiate(projectilePrefab, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
        }
    }
}