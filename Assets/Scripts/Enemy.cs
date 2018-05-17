using UnityEngine;
using System.Collections;

public class Enemy : Enemigo
{

    // Use this for initialization
    void Start()
    {
        speed = 1f;
        health = 10f;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);

        if (screenPos.y <= 0)
            Destroy(gameObject);

        transform.Translate(new Vector3(0, -1) * Time.deltaTime * speed);

        Kill();
    }

}
