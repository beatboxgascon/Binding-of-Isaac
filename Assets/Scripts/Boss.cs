using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour {
    private Player jugador;
    public Transform target;
    //private Rigidbody2D rigid;
    private float speed;
    private float nextFire;
    public GameObject projectilePrefab;
    private float health;
    public Text healthText;
    // Use this for initialization
    void Start () {
        
        speed = 1.5f;
        health = 500f;
        healthText.text = "Enemy Health: " + health;
        nextFire = 0f;
        jugador = target.GetComponent<Player>();
    }
	
	// Update is called once per frame
	void Update () {
        transform.position += (target.transform.position - transform.position).normalized * speed * Time.deltaTime;

        fireRocket();
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Projectile")
        {
            health -= jugador.GetDamage();
            healthText.text = "Enemy Health: " + health;
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
            Vector3 p1 = transform.position;
            Vector3 p2 = target.transform.position;
            float angle = Mathf.Atan2(p2.y - p1.y, p2.x - p1.x) * 180 / Mathf.PI;
            Instantiate(projectilePrefab, transform.position,Quaternion.Euler(0,0, angle));
            Instantiate(projectilePrefab, transform.position, Quaternion.Euler(0, 0, angle+45));
            Instantiate(projectilePrefab, transform.position, Quaternion.Euler(0, 0, angle-45));


        }
    }
}
