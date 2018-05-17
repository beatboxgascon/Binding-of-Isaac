using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    //8f como maximo para la velocidad.
    //0.2f como limite para el firerate.
    public static int lives = 5;
    public static float speed = 5f;
    public static float fireRate = 0.7f;
    public static float damage = 10f;
    public static int coins = 0;
    public string tipoProyectil;

    private bool invincible;

    float nextFire;
    public GameObject projectilePrefab;
    public GameObject laserPrefabV;
    public GameObject laserPrefabH;
    public Text LivesText;
    public Text SpeedText;
    public Text DamageText;
    public Text FireRateText;
    public Text CoinsText;
    private Animator anim;
    private Coin prueba;
    public GameObject camara;

    private AudioSource source;
    public AudioClip shoot;
    public AudioClip hitSound;

    void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    // Use this for initialization
    void Start()
    {
        Screen.SetResolution(1920, 1080, true);
        nextFire = 0f;
        tipoProyectil = "laser";
        LivesText.text = "Lives: " + lives;
        SpeedText.text = "Speed: " + speed;
        DamageText.text = "Damage: " + damage;
        CoinsText.text = "Coins: " + coins;
        FireRateText.text = "Fire Rate: " + fireRate * 10;
        invincible = false;
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
            

            if (tipoProyectil == "laser")
            {
                fireLaser();
            }
            else
            {
                fireRocket();
            }

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
        if (coll.gameObject.tag == "enemyProjectile" || coll.gameObject.tag == "EnemyF" ||
            coll.gameObject.tag == "EnemyF_Projectile")
            LoseLife();

        if (coll.gameObject.tag == "Trophy")
        {
            SceneManager.LoadScene("Victoria");
        }

        if (coll.gameObject.tag == "Moneda")
        {
            updateCoins(1);
            Destroy(coll.gameObject);
        }


    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "EnemyF" ||
            collision.gameObject.tag == "EnemyF_Projectile")
            LoseLife();
    }

    void OnCollisionEnter2D(Collision2D coll)
    {

        if (coll.gameObject.tag == "Coin")
        {
            Destroy(coll.gameObject);
            UpdateStats(coll.gameObject.GetComponent<Coin>());
        }
        else if (coll.gameObject.tag == "Heart")
        {
            Destroy(coll.gameObject);
            LifeUp();
        }
        else if (coll.gameObject.tag == "Entrada_N")
        {
            changeRoom(0,9,0,3.5f);
        }
        else if (coll.gameObject.tag == "Entrada_S")
        {
            changeRoom(0, -9, 0, -3.5f);
        }
        else if (coll.gameObject.tag == "Entrada_E")
        {
            changeRoom(15, 0, 4, 0);
        }
        else if (coll.gameObject.tag == "Entrada_W")
        {
            changeRoom(-15, 0, -4, 0);
        }
        else if (coll.gameObject.tag == "ObjetoTienda" && coins>=15)
        {
            Destroy(coll.gameObject);
            UpdateStats(coll.gameObject.GetComponent<Coin>());
            updateCoins(-15);
        }
        else if (coll.gameObject.tag == "CorazonTienda" && coins >= 5)
        {
            Destroy(coll.gameObject);
            LifeUp();
            updateCoins(-5);
        }
    }

    public void updateCoins(int price)
    {
        coins += price;
        CoinsText.text = "Coins: " + coins;
    }


    public void changeRoom(float camaraX, float camaraY, float personajeX, float personajeY)
    {
        camara.transform.position = new Vector3(camara.transform.position.x+camaraX, camara.transform.position.y + camaraY, camara.transform.position.z);
        transform.position = new Vector3(transform.position.x+personajeX, transform.position.y + personajeY, transform.position.z);
    }

    public void UpdateStats(Coin c)
    {
        damage += c.GetObjeto().Damage;
        speed += c.GetObjeto().Speed;
        fireRate -= c.GetObjeto().FireRate * 0.1f;
        DamageText.text = "Damage: " + damage;
        SpeedText.text = "Speed: " + speed;
        FireRateText.text = "Fire Rate: " + fireRate * 10;
    }

    public void LifeUp()
    {
        lives++;
        LivesText.text = "Lives: " + lives;
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
            source.PlayOneShot(hitSound, 5f);
            Invoke("resetInvulnerability", 1.4F);

        }

        if (lives <= 0)
        {
            SceneManager.LoadScene("Derrota");
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
            source.PlayOneShot(shoot, 5f);

        }
    }
    void fireLaser()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate / 45;

            //COMO COÑO RECOJO EL INPUT PARA PASARLO A UN SWITCH ?
            if (Input.GetKey(KeyCode.UpArrow))
            {
                Instantiate(laserPrefabV, new Vector3(transform.position.x, transform.position.y + 3.5f, transform.position.z)
                    , laserPrefabV.transform.rotation);
                source.PlayOneShot(shoot, 5f);
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                Instantiate(laserPrefabV, new Vector3(transform.position.x, transform.position.y - 3.5f, transform.position.z)
                    , laserPrefabV.transform.rotation);
                source.PlayOneShot(shoot, 5f);
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {

                Instantiate(laserPrefabH, new Vector3(transform.position.x - 6.5f, transform.position.y, transform.position.z), laserPrefabH.transform.rotation);
                source.PlayOneShot(shoot, 5f);
            }

            else if (Input.GetKey(KeyCode.RightArrow))
            {

                Instantiate(laserPrefabH, new Vector3(transform.position.x + 6.5f, transform.position.y, transform.position.z), laserPrefabH.transform.rotation);
                source.PlayOneShot(shoot, 5f);
            }



        }
    }

    public Player getPosition()
    {
        return this;
    }
}