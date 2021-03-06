﻿using UnityEngine;

public class EnemigoP : EnemigoDisparo
{
    public Transform target;//set target from inspector instead of looking in Update
    public GameObject projectilePrefab;
    

    void Awake()
    {
        source = GetComponent<AudioSource>();
    }
    void Start()
    {
        nextFire = 0f;
        health = 20f;
        jugador = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
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
            source.PlayOneShot(shoot, 5f);
            nextFire = Time.time + 1f;
            if (transform.rotation.y != 0)
            {
                Instantiate(projectilePrefab, transform.position, Quaternion.Euler(new Vector3(0, 0, 180)));
            }
            else
            {
                Instantiate(projectilePrefab, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
            }

        }
    }
}
