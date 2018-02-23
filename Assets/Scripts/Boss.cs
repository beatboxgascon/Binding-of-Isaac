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
    private float nextRoar;
    public GameObject projectilePrefab;
    private float health;
    public Text healthText;

    private AudioSource source;
    public AudioClip shoot;
    public AudioClip roar;

    void Awake()
    {
        source = GetComponent<AudioSource>();
    }
    // Use this for initialization
    void Start () {
        
        speed = 1.5f;
        health = 500f;
        healthText.text = "Enemy Health: " + health;
        nextFire = 0f;
        nextRoar = 0f;
        jugador = target.GetComponent<Player>();
    }
	
	// Update is called once per frame
	void Update () {
        transform.position += (target.transform.position - transform.position).normalized * speed * Time.deltaTime;

        if (Time.time >nextRoar)
        {
            nextRoar = Time.time + 4f;
            source.PlayOneShot(roar, 5f);
        }
        
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
            source.PlayOneShot(shoot, 5f);

            nextFire = Time.time + 1.5f;
            Vector3 p1 = transform.position;
            Vector3 p2 = target.transform.position;
            float angle = Mathf.Atan2(p2.y - p1.y, p2.x - p1.x) * 180 / Mathf.PI;
            Instantiate(projectilePrefab, transform.position,Quaternion.Euler(0,0, angle));
            Instantiate(projectilePrefab, transform.position, Quaternion.Euler(0, 0, angle+45));
            Instantiate(projectilePrefab, transform.position, Quaternion.Euler(0, 0, angle-45));


        }
    }
}
