using UnityEngine;

public class LaserProjectile : MonoBehaviour
{
    private float speed;
    private float direccionY;
    private float direccionX;
    private Player jugador;



    // Use this for initialization
    void Start()
    {
        speed = 60;
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

        jugador = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        if (screenPos.y >= Screen.height)
            Destroy(gameObject);

        transform.Translate(new Vector3(direccionX, direccionY) * Time.deltaTime * speed);
        transform.position = new Vector3(jugador.transform.position.x, transform.position.y);
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag.Contains("Enemy"))
        {
            Destroy(gameObject);
        }
    }
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}