﻿using UnityEngine;
using System.Collections;

public class EnemyF : MonoBehaviour
{
    public Transform target;//set target from inspector instead of looking in Update
    private float speed;

    private float health;

    private Player jugador;


    void Start()
    {
        speed = 1f;
        health = 20f;
        jugador = target.GetComponent<Player>();
    }

    void Update()
    {
        transform.position += (target.transform.position - transform.position).normalized * speed * Time.deltaTime;
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


}