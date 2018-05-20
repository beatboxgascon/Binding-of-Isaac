using UnityEngine;
public class LaserProjectileH : MonoBehaviour
{
    private float speed;
    private float direccionY;
    private float direccionX;
    private Player jugador;
    void Start()
    {
        speed = 150;
        if (Input.GetKey(KeyCode.LeftArrow))
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
    void Update()
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        if (screenPos.y >= Screen.height)
            Destroy(gameObject);
        transform.Translate(new Vector3(direccionX, direccionY) * Time.deltaTime * speed);
        transform.position = new Vector3(transform.position.x, jugador.transform.position.y);
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