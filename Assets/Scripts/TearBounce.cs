using UnityEngine;
public class TearBounce : MonoBehaviour
{
    private float speed;
    private float direccionY;
    private float direccionX;
    private float posicionX;
    private float posicionY;
    private float posicionZ;
    private float direccionYAux;
    private float direccionXAux;
    public Player jugador;
    TearBounce currentTearBounce;

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
        //print("bounces: " + GameObject.FindObjectsOfType<TearBounce>().Length);
        jugador = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        //if (GameObject.FindObjectsOfType<TearBounce>().Length > 0)
        //{
        //    if (gameObject.transform.position.x > jugador.transform.position.x &&
        //         gameObject.transform.position.y > jugador.transform.position.y)
        //    {
        //        direccionY = -1;
        //        direccionX = -1;
        //    }
        //    else if (gameObject.transform.position.x > jugador.transform.position.x &&
        //        gameObject.transform.position.y < jugador.transform.position.y)
        //    {
        //        direccionY = 1;
        //        direccionX = 0;
        //    }
        //    else if (gameObject.transform.position.x > jugador.transform.position.x &&
        //        gameObject.transform.position.x > jugador.transform.position.x)
        //    {
        //        direccionY = 0;
        //        direccionX = -1;
        //    }
        //    else if (gameObject.transform.position.x < jugador.transform.position.x &&
        //        gameObject.transform.position.x < jugador.transform.position.x)
        //    {
        //        direccionY = 1;
        //        direccionX = 1;
        //    }
        //}
    }
    void Update()
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        if (screenPos.y >= Screen.height)
            Destroy(gameObject);

        transform.Translate(new Vector2(direccionX, direccionY) * Time.deltaTime * speed);
    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag.Contains("Enemy"))
        {
            Destroy(gameObject);
        }
        if (coll.GetComponent<PolygonCollider2D>())
        {
            Rotate(135);
        }
    }



    public void  Rotate(float degrees)
    {
        float sin = Mathf.Sin(degrees * Mathf.Deg2Rad);
        float cos = Mathf.Cos(degrees * Mathf.Deg2Rad);

        float tx = direccionX;
        float ty = direccionY;
        direccionX = (cos * tx) - (sin * ty);
        direccionY = (sin * tx) + (cos * ty);
       
    }
    //void OnBecameInvisible()
    //{
    //    currentTearBounce = GameObject.FindObjectOfType<TearBounce>().GetComponent<TearBounce>();
    //    posicionX = gameObject.transform.position.x;
    //    posicionY = gameObject.transform.position.y;
    //    posicionZ = gameObject.transform.position.z;
    //    direccionXAux = currentTearBounce.direccionX * -1;
    //    direccionYAux = currentTearBounce.direccionY * -1;
    //    Destroy(gameObject);
    //    Instantiate(jugador.tearBounce, new Vector3(posicionX - 2.25f, posicionY, posicionZ)
    //               , currentTearBounce.transform.rotation);


    //}
}