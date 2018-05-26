using UnityEngine;
public class AnotherBoss : EnemigoDisparo
{
    public GameObject projectilePrefab;
    int angle1, angle2, angle3;

    void Awake()
    {
        source = GetComponent<AudioSource>();
    }
    void Start()
    {
        nextFire = 0f;
        health = 30f;
        jugador = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        
        angle1 = 0;
        angle2 = 120;
        angle3 = 240;
    }

    void Update()
    {
        Die();
        FireRocket();
    }


    void FireRocket()
    {
        if (Time.time > nextFire)
        {
            source.PlayOneShot(shoot, 5f);
            nextFire = Time.time + 1f;
            
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