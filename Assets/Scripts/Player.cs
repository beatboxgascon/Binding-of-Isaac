using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private int lives;
    private float speed;
    private float fireRate;
    private float damage;

    private float invincibleTime;
    private bool allowFire;
    private bool invincible;



    GameObject tempBullet;
    float nextFire;
    public GameObject projectilePrefab;
    public Text LivesText;
    public Text SpeedText;
    public Text DamageText;
    public Text FireRateText;
    private Animator anim;
    private Coin prueba;

    // Use this for initialization
    void Start()
    {
        lives = 5;
        speed = 4.5f;
        fireRate = 0.5f;
        damage = 10f;
        nextFire = 0f;
        LivesText.text = "Lives: " + lives;
        SpeedText.text = "Speed: " + speed;
        DamageText.text = "Damage: " + damage;
        FireRateText.text = "Fire Rate: " + fireRate*10;
        allowFire = true;
        invincible = false;
        invincibleTime = 0f;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float axisX = 0;
        float axisY = 0;
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 screenWorldPos = Camera.main.ScreenToWorldPoint(screenPos);
        anim.SetFloat("Movimiento", 0.0f);
        anim.SetFloat("MovimientoY", 0.0f);

        if (Input.GetKey("w"))
        {
            axisY = 0.5f;
            anim.SetFloat("MovimientoY", axisY);
        }
        else if (Input.GetKey("s"))
        {
            axisY = -0.5f;
            anim.SetFloat("MovimientoY", axisY);
        }
        if (Input.GetKey("d"))
        {
            axisX = 0.5f;
            anim.SetFloat("Movimiento", axisX);
        }
        else if (Input.GetKey("a"))
        {
            axisX = -0.5f;
            anim.SetFloat("Movimiento", axisX);
        }


        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
        {
            anim.SetFloat("Movimiento", 0.0f);
            anim.SetFloat("MovimientoY", 0.0f);

            if (Input.GetKey(KeyCode.RightArrow))
                anim.SetFloat("Movimiento", 0.5f);
            if (Input.GetKey(KeyCode.LeftArrow))
                anim.SetFloat("Movimiento", -0.5f);
            if (Input.GetKey(KeyCode.UpArrow))
                anim.SetFloat("MovimientoY", 0.5f);
            if (Input.GetKey(KeyCode.DownArrow))
                anim.SetFloat("MovimientoY", -0.5f);

            fireRocket();
        }


        if (screenPos.x <= 0)
            transform.position = new Vector3(screenWorldPos.x + 0.05f, screenWorldPos.y);
        else if (screenPos.x >= Screen.width)
            transform.position = new Vector3(screenWorldPos.x - 0.05f, screenWorldPos.y);
        else
            transform.Translate(new Vector3(axisX, axisY) * Time.deltaTime * speed);
    }



    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Enemy" || coll.gameObject.tag == "EnemyF")
            LoseLife();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        LoseLife();
    }


    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Coin")
        {
            Destroy(coll.gameObject);
            UpdateStats(coll.gameObject.GetComponent<Coin>());
            DamageText.text = "Damage: " + damage;
            SpeedText.text = "Speed: " + speed;
            FireRateText.text = "Fire Rate: " + fireRate * 10;
        }
        else if (coll.gameObject.tag == "Heart")
        {
            Destroy(coll.gameObject);
            lives++;
            LivesText.text = "Lives: " + lives;
        }
    }

    public void UpdateStats(Coin c)
    {
        damage += c.GetObjeto().Damage;
        speed += c.GetObjeto().Speed;
        fireRate -= c.GetObjeto().FireRate*0.1f;
    }

    public float GetDamage()
    {
        return damage;
    }


    private void LoseLife()
    {

        if (!invincible)
        {
            invincible = true;
            anim.SetFloat("Inmune", 2);
            lives--;
            Invoke("resetInvulnerability", 1.4F);
        }


        if (lives <= 0)
        {
            
                SceneManager.LoadScene(0);
            
            /*
            lives = 5;
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            for (int i = 0; i < enemies.Length; i++)
                Destroy(enemies[i]);
            invincible = false;*/
        }

        LivesText.text = "Lives: " + lives;
    }

    void resetInvulnerability()
    {
        invincible = false;
        anim.SetFloat("Inmune", 0);
    }

    void fireRocket()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;

            Instantiate(projectilePrefab, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
        }
    }

    public Player getPosition()
    {
        return this;
    }
}