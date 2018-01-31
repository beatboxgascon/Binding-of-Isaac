using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private int lives;
    private float speed;
    private bool allowFire;
    GameObject tempBullet;
    float fireRate = 0.5f;
    float nextFire = 0f;

    public GameObject projectilePrefab;
    public Text LivesText;

    // Use this for initialization
    void Start()
    {
        lives = 2;
        speed = 4.5f;
        LivesText.text = "Lives: " + lives;
        allowFire = true;
    }

    // Update is called once per frame
    void Update()
    {
        float axisX=0;
        float axisY=0;
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 screenWorldPos = Camera.main.ScreenToWorldPoint(screenPos);

        if (Input.GetKey("w"))
        {
            axisY = 0.5f;
        }
        else if (Input.GetKey("s"))
        {
            axisY = -0.5f;
        }
        if (Input.GetKey("d"))
        {
            axisX = 0.5f;
        }
        else if (Input.GetKey("a"))
        {
            axisX = -0.5f;
        }


        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
            fireRocket();

        if (screenPos.x <= 0)
            transform.position = new Vector3(screenWorldPos.x + 0.05f, screenWorldPos.y);
        else if (screenPos.x >= Screen.width)
            transform.position = new Vector3(screenWorldPos.x - 0.05f, screenWorldPos.y);
        else
            transform.Translate(new Vector3(axisX, axisY) * Time.deltaTime * speed);
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Enemy")
            LoseLife();
    }

    private void LoseLife()
    {
        lives--;
        if (lives <= 0)
        {
            lives = 2;
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            for (int i = 0; i < enemies.Length; i++)
                Destroy(enemies[i]);
        }

        LivesText.text = "Lives: " + lives;
    }

    void fireRocket()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;

            Instantiate(projectilePrefab, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
        }
    }

}
