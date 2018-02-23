using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyF_Projectile : MonoBehaviour
{
    private Player jugador;
    public Transform target;
    private float speed;

    private float health;
    // Use this for initialization
    void Start()
    {
        speed = 1f;
        health = 20f;
    }

    // Update is called once per frame
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
