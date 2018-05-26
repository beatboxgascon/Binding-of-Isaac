using UnityEngine;
//Clase padre de la que heredaran todos los enemigos que se encuentran a lo largo del juego.
public class Enemigo : MonoBehaviour
{
    protected float health;
    protected float speed;
    protected AudioSource source;
    protected AudioClip shoot;
    protected AudioClip hitSound;
    protected Player jugador;

    void Start()
    {
        jugador = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    public void doDamage(float damage)
    {
        health -= damage;
    }
    protected void Die()
    {
        if (health <= 0)
            Destroy(gameObject);
    }

    //void OnCollisionEnter2D(Collision2D coll)
    //{
    //    if (coll.gameObject.tag=="Projectile")
    //    {
    //        GetComponent<SpriteRenderer>().color = new Color(1f, 0.1f, 0.1f, 1f);
    //        Invoke("Damage", 5F);
    //        doDamage(jugador.GetDamage());
    //        Destroy(coll.gameObject);
    //    }
    //}

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag=="Projectile")
        {
            GetComponent<SpriteRenderer>().color = new Color(1f, 0.1f, 0.1f, 1f);
            Invoke("Damage", 0.2F);
            doDamage(jugador.GetDamage());
            Destroy(coll.gameObject);
        }
    }

    void Damage()
    {
        GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
    }
}