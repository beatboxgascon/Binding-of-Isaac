using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyF_Projectile : EnemigoDisparo
{
    private Rigidbody2D rigid;
    public GameObject projectilePrefab;
    // Use this for initialization

    void Awake()
    {
        source = GetComponent<AudioSource>();

        jugador = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        speed = 1f;
        health = 30f;
        nextFire = 0f;
        jugador = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        rigid.gravityScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (jugador.transform.position - transform.position).normalized * speed * Time.deltaTime;
        //Congelar();
        Kill();
        fireRocket();
    }
    //Me temo que esto no se llega a usar o no funciona como se esperaba.
    private IEnumerator Congelar()
    {
        rigid.constraints = RigidbodyConstraints2D.FreezeAll;
        yield return new WaitForSeconds(2);
        rigid.constraints = RigidbodyConstraints2D.None;
        transform.position += (jugador.transform.position - transform.position).normalized * speed * Time.deltaTime;

    }

    void fireRocket()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + 1.5f;
            source.PlayOneShot(shoot, 5f);
            Instantiate(projectilePrefab, transform.position, Quaternion.Euler(0, 0, 90));
            Instantiate(projectilePrefab, transform.position, Quaternion.Euler(0, 0, 270));
            Instantiate(projectilePrefab, transform.position, Quaternion.Euler(0, 0, 180));
            Instantiate(projectilePrefab, transform.position, Quaternion.Euler(0, 0, 0));


        }
    }
}
