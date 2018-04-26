using UnityEngine;

public class AnotherBoss : MonoBehaviour
{
    public Transform target;//set target from inspector instead of looking in Update
    public GameObject projectilePrefab;
    private float health;
    private Player jugador;
    float nextFire;
    int angle1, angle2, angle3;

    private AudioSource source;
    public AudioClip shoot;

    void Awake()
    {
        source = GetComponent<AudioSource>();
    }
    void Start()
    {
        nextFire = 0f;
        health = 40f;
        jugador = target.GetComponent<Player>();
        /*
         * --Version con 2 proyectiles de misma direccion pero sentido opuesto.
         * 
        angle1 = 0;
        angle2 = 180;
        */

        angle1 = 0;
        angle2 = 120;
        angle3 = 240;
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
            /*
             * --Version con 2 proyectiles con misma direccion pero sentido opuesto.
             * 
            Instantiate(projectilePrefab, transform.position, Quaternion.Euler(new Vector3(0, 0, angle1)));
            Instantiate(projectilePrefab, transform.position, Quaternion.Euler(new Vector3(0, 0, angle2)));
            if (angle1 >= 360)
            {
                angle1 = 0;
            }
            else if(angle2 >= 360)
            {
                angle2 = 0;
            }
            angle1 += 15;
            angle2 += 15;
            */
            Instantiate(projectilePrefab, transform.position, Quaternion.Euler(new Vector3(0, 0, angle1)));
            Instantiate(projectilePrefab, transform.position, Quaternion.Euler(new Vector3(0, 0, angle2)));
            Instantiate(projectilePrefab, transform.position, Quaternion.Euler(new Vector3(0, 0, angle3)));
            if (angle1 >= 360)
            {
                angle1 = 0;
            }
            else if (angle2 >= 360)
            {
                angle2 = 0;
            }
            else if (angle3 >= 360)
            {
                angle3 = 0;
            }
            angle1 += 15;
            angle2 += 15;
            angle3 += 15;


        }
    }
}