using UnityEngine;
public class TearXRay : MonoBehaviour
{
    private float speed;
    private float direccionY;
    private float direccionX;
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
    void Update()
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        if (screenPos.y >= Screen.height)
            Destroy(gameObject);

        transform.Translate(new Vector3(direccionX, direccionY) * Time.deltaTime * speed);
    }
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}