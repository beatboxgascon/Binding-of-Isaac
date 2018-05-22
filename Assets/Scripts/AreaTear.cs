using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaTear : MonoBehaviour {

    int speed;

    void Start()
    {
        speed = 3;
    }
    void Update()
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        if (screenPos.y >= Screen.height)
            Destroy(gameObject);
        transform.Translate(new Vector3(1, 0) * Time.deltaTime * speed);
    }
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag.Contains("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}
