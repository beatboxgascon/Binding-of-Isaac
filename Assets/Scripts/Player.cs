using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static int lives = 5;
    public static float speed = 4.5f;
    public static float fireRate = 0.5f;
    public static float damage = 10f;
    private int escena=1;

    private float invincibleTime;
    private bool allowFire;
    private bool invincible;



    public GameObject trampilla;
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
        escena = SceneManager.GetActiveScene().buildIndex;
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

        if (GameObject.FindGameObjectsWithTag("EnemyF").Length<1)
        {
            if (trampilla)
            {
                trampilla.SetActive(true);
            }
            
        }

    }


    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "enemyProjectile" || coll.gameObject.tag == "EnemyF" ||
            coll.gameObject.tag == "EnemyF_Projectile")
            LoseLife();
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
        else if (coll.gameObject.tag == "Trampilla")
        {
            escena++;
            SceneManager.LoadScene(escena);   
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