﻿using UnityEngine;
using System.Collections;

public class EnemyF : MonoBehaviour
{
    private float speed;

    private float health;

    private Player jugador;

    private AudioSource source;
    public AudioClip buzz;

    void Awake()
    {
        source = GetComponent<AudioSource>();
    }


    void Start()
    {
        speed = 1f;
        health = 20f;
        jugador = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        //source.PlayOneShot(buzz, 5f);
        source.Play(0);
    }

    void Update()
    {
        transform.position += (jugador.transform.position - transform.position).normalized * speed * Time.deltaTime;

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