using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{

    private float speed;
    private float direccionY;
    private float direccionX;

    // Use this for initialization
    void Start()
    {
        speed = 3;

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        if (screenPos.y >= Screen.height)
            Destroy(gameObject);

        transform.Translate(new Vector3(1, 0) * Time.deltaTime * speed);
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
