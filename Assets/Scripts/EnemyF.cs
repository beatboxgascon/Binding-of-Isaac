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
        speed = 1f;
        health = 20f;
        jugador = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        source.Play(0);
    }
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, jugador.transform.position,
                                                  speed * Time.deltaTime);
        Die();
    }

}