using UnityEngine;
abstract public class Enemy : Enemigo
{
    void Start()
    {
        speed = 1f;
        health = 10f;
    }
    void Update()
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        if (screenPos.y <= 0)
            Destroy(gameObject);
        transform.Translate(new Vector3(0, -1) * Time.deltaTime * speed);
        Die();
    }
}