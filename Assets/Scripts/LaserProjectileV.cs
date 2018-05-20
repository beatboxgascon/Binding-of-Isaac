using UnityEngine;
public class LaserProjectileV : MonoBehaviour
{
    private float speed;
    private float direccionY;
    private float direccionX;
    private Player jugador;
    void Start()
    {
        speed = 150;
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
        jugador = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
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