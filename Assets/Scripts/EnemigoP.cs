using UnityEngine;
public class EnemigoP : EnemigoDisparo
{
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
        Die();
        FireRocket();
    }
     void FireRocket()
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