using UnityEngine;
public class EnemyF : Enemigo
{
    public AudioClip buzz;
    void Awake()
    {
        source = GetComponent<AudioSource>();
    }
    void Start()
    {
        speed = 4f;
        health = 20f;
        jugador = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        source.Play(0);
    }
    void Update()
    {
        transform.position += (jugador.transform.position - transform.position).normalized * speed * Time.deltaTime;
        Die();
    }
    protected void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Projectile")
        {
            health -= jugador.GetDamage();
        }
    }
}