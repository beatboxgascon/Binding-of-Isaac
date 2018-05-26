using UnityEngine;
using UnityEngine.UI;
public class Boss : EnemigoDisparo
{
    private float nextRoar;
    public GameObject projectilePrefab;
  //  public Text healthText;
    public AudioClip roar;

    void Awake()
    {
        source = GetComponent<AudioSource>();
    }
    void Start()
    {
        speed = 1.5f;
        health = 250f;
        //  healthText.text = "Enemy Health: " + health;
        nextFire = 0f;
        nextRoar = 0f;
        jugador = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
    void Update()
    {
        transform.position += (jugador.transform.position - transform.position).normalized * speed * Time.deltaTime;
        if (Time.time > nextRoar)
        {
            nextRoar = Time.time + 4f;
            source.PlayOneShot(roar, 5f);
        }
        FireRocket();
    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Projectile")
        {
            health -= jugador.GetDamage();
           // healthText.text = "Enemy Health: " + health;
            if (health <= 0)
            {
                Destroy(gameObject);
            }
            Destroy(coll.gameObject);
        }
    }

    void FireRocket()
    {
        if (Time.time > nextFire)
        {
            source.PlayOneShot(shoot, 5f);
            nextFire = Time.time + 1.5f;
            Vector3 p1 = transform.position;
            Vector3 p2 = jugador.transform.position;
            float angle = Mathf.Atan2(p2.y - p1.y, p2.x - p1.x) * 180 / Mathf.PI;
            Instantiate(projectilePrefab, transform.position, Quaternion.Euler(0, 0, angle));
            Instantiate(projectilePrefab, transform.position, Quaternion.Euler(0, 0, angle + 45));
            Instantiate(projectilePrefab, transform.position, Quaternion.Euler(0, 0, angle - 45));
        }
    }
}