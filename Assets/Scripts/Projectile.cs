using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{
    private float speed;
    private float direccionY;
    private float direccionX;

    // Use this for initialization
    void Start()
    {
        speed = 3;
        if (Input.GetKey(KeyCode.DownArrow))
        {
            direccionY = -1;
            direccionX = 0;
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            direccionY = 1;
            direccionX = 0;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            direccionY = 0;
            direccionX = -1;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            direccionY = 0;
            direccionX = 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        if (screenPos.y >= Screen.height)
            Destroy(gameObject);

        transform.Translate(new Vector3(direccionX, direccionY) * Time.deltaTime * speed);
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
