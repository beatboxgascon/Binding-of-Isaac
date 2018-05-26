using UnityEngine;
class EnemyF_Projectile : EnemigoDisparo
{
    private Rigidbody2D rigid;
    public GameObject projectilePrefab;
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
    void Update()
    {
        transform.position += (jugador.transform.position - transform.position).normalized * speed * Time.deltaTime;
        Die();
        FireRocket();
    }
    void FireRocket()
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