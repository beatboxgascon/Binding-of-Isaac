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

    public void doDamage(float damage)
    {
        health -= damage;
        Destroy(gameObject);
    }
    protected void Die()
    {
        if (health <= 0)
            Destroy(gameObject);
    }
}