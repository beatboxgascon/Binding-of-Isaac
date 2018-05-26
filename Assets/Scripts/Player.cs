using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Player : MonoBehaviour
{
    //8f como maximo para la velocidad.
    //0.2f como limite para el firerate.
    public static int lives;
    public static float speed;
    public static float fireRate;
    public static float damage;
    public static int coins;
    public string tipoProyectil;
    public static int cargaObjeto;
    public RandomActiveObject activeObject;
    private bool invincible;
    float nextFire;
    public GameObject tear;
    public GameObject tearXRay;
    public GameObject tearBounce;
    public GameObject tearShield;
    public GameObject laserPrefabV;
    public GameObject laserPrefabH;
    public GameObject areaTear;
    public Text LivesText;
    public Text SpeedText;
    public Text DamageText;
    public Text FireRateText;
    public Text CoinsText;
    public Text ChargeText;
    private Animator anim;
    private GameObject camara;
    public GameObject room;
    private AudioSource source;
    public AudioClip shoot;
    public AudioClip hitSound;
    private bool temporalDamage;

    void Awake()
    {
        source = GetComponent<AudioSource>();
    }
    void Start()
    {
        camara = GameObject.FindGameObjectWithTag("MainCamera");
        lives = 5;
        speed = 10f;
        fireRate = 0.7f;
        damage = 3f;
        cargaObjeto = 0;
        coins = 15;
        Screen.SetResolution(1920, 1080, true);
        nextFire = 0f;
        tipoProyectil = "doble";
        UpdateTexts();
        invincible = false;
        anim = GetComponent<Animator>();
        temporalDamage = false;
    }

    void Update()
    {      
        Move();
        Fire();
        ActiveObject(); 
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

    private void Fire()
    {
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

            FireProjectile();

        }
    }

    private void ActiveObject()
    {
        if (Input.GetKeyDown(KeyCode.Space) && activeObject != null
                   && cargaObjeto == activeObject.GetCargas())
        {
            cargaObjeto = 0;
            UpdateTexts();
            activeObject.Activate();
        }
    }

    private void Move()
    {
        anim.SetFloat("Movimiento", 0.0f);
        anim.SetFloat("MovimientoY", 0.0f);
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 screenWorldPos = Camera.main.ScreenToWorldPoint(screenPos);
        float axisX = 0;
        float axisY = 0;

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

        if (screenPos.x <= 0)
            transform.position = new Vector3(screenWorldPos.x + 0.05f, screenWorldPos.y);
        else if (screenPos.x >= Screen.width)
            transform.position = new Vector3(screenWorldPos.x - 0.05f, screenWorldPos.y);
        else
            transform.Translate(new Vector3(axisX, axisY) * Time.deltaTime * speed);
    }

    private void FireProjectile()
    {
        if (tipoProyectil == "laser")
        {
            FireLaser();
        }
        else if (tipoProyectil == "normal")
        {
            FireTear();
        }
        else if (tipoProyectil == "doble")
        {
            FireDoubleTear();
        }
        else if (tipoProyectil == "triple")
        {
            FireTripleTear();
        }
        else if (tipoProyectil == "Xray")
        {
            FireTearXRay();
        }
        else if (tipoProyectil == "escudo")
        {
            FireTearShield();
        }
        else if (tipoProyectil == "rebote")
        {
            FireTearBounce();
        }
    }

    private void UpdateTexts()
    {

        LivesText.text = "Lives: " + lives;
        SpeedText.text = "Speed: " + speed;
        DamageText.text = "Damage: " + damage;
        CoinsText.text = "Coins: " + coins;
        FireRateText.text = "Fire Rate: " + fireRate * 10;
        if (activeObject == null)
        {
            ChargeText.text = "";
        }
        else
        {
            ChargeText.text = "Charge: " + cargaObjeto;
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
            UpdateStats(coll.gameObject.GetComponent<RandomObject>());
        }
        else if (coll.gameObject.tag == "Heart")
        {
            Destroy(coll.gameObject);
            LifeUp();
        }
        else if (coll.gameObject.tag == "Entrada_N")
        {
            ChangeRoom(0, 9, 0, 3.5f);
        }
        else if (coll.gameObject.tag == "Entrada_S")
        {
            ChangeRoom(0, -9, 0, -3.5f);
        }
        else if (coll.gameObject.tag == "Entrada_E")
        {
            ChangeRoom(15, 0, 4, 0);
        }
        else if (coll.gameObject.tag == "Entrada_W")
        {
            ChangeRoom(-15, 0, -4, 0);
        }
        else if (coll.gameObject.tag == "ObjetoTienda" && coins >= 15)
        {
            Destroy(coll.gameObject);
            UpdateStats(coll.gameObject.GetComponent<RandomObject>());
            updateCoins(-15);
        }
        else if (coll.gameObject.tag == "ObjetoActivoTienda" && coins >= 15)
        {
            activeObject = coll.gameObject.GetComponent<RandomActiveObject>();
            cargaObjeto = activeObject.GetCargas();
            coll.gameObject.SetActive(false);
            UpdateTexts();
            updateCoins(-15);
        }
        else if (coll.gameObject.tag == "CorazonTienda" && coins >= 5)
        {
            Destroy(coll.gameObject);
            LifeUp();
            updateCoins(-5);
        }
        else if (coll.gameObject.tag == "ObjetoActivo")
        {
            activeObject = coll.gameObject.GetComponent<RandomActiveObject>();
            cargaObjeto = activeObject.GetCargas();
            coll.gameObject.SetActive(false);
            UpdateTexts();
        }
        else if (coll.gameObject.tag == "ObjetoLagrimaTienda")
        {
            tipoProyectil = coll.gameObject.GetComponent<RandomTearObject>().getTear();
            Destroy(coll.gameObject);
            UpdateTexts();
            updateCoins(-15);
        }
        else if (coll.gameObject.tag == "ObjetoLagrima")
        {
            tipoProyectil = coll.gameObject.GetComponent<RandomTearObject>().getTear();
            Destroy(coll.gameObject);
            UpdateTexts();
        }
    }

    public void SetRoom(GameObject room)
    {
        this.room = room;
    }

    public void updateCharges(int carga)
    {
        if (cargaObjeto <= activeObject.GetCargas() && carga > 0)
            cargaObjeto += carga;
        UpdateTexts();
    }

    public Room GetRoom() { return room.GetComponent<Room>(); }

    public void updateCoins(int price)
    {
        coins += price;
        UpdateTexts();
    }

    public bool hasActiveObject()
    {
        return activeObject != null;
    }

    public void TemporalDamage()
    {
        temporalDamage = true;
        damage += 5;
        UpdateTexts();
    }

    public void ChangeRoom(float camaraX, float camaraY, float personajeX, float personajeY)
    {
        camara.transform.position = new Vector3(camara.transform.position.x + camaraX, camara.transform.position.y + camaraY, camara.transform.position.z);
        transform.position = new Vector3(transform.position.x + personajeX, transform.position.y + personajeY, transform.position.z);
        if (temporalDamage)
        {
            damage -= 5;
            UpdateTexts();
        }
    }

    public void SetInmunity()
    {
        invincible = true;
        GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.5f);
        Invoke("resetInmunity", 7F);
        
    }

    public void UpdateStats(RandomObject c)
    {
        damage += c.GetObjeto().Damage;
        speed += c.GetObjeto().Speed;
        fireRate -= c.GetObjeto().FireRate * 0.1f;
        UpdateTexts();
    }

    public void LifeUp()
    {
        if (lives < 9)
        {
            lives++;
            UpdateTexts();
        }
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
        UpdateTexts();
    }

    void resetInvulnerability()
    {
        invincible = false;
        anim.SetFloat("Inmune", 0);

    }

    void resetInmunity()
    {
        invincible = false;
        GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
    }

    void FireTear()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(tear, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
            source.PlayOneShot(shoot, 5f);
        }
    }

    void FireTearXRay()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;

            if (Input.GetKey(KeyCode.UpArrow))
            {
                tearXRay.transform.Rotate(Vector3.forward);
                Instantiate(tearXRay, transform.position, Quaternion.Euler(new Vector3(0, 0, 90)));
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                Instantiate(tearXRay, transform.position, Quaternion.Euler(new Vector3(0, 0, -90)));
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                
                Instantiate(tearXRay, transform.position, Quaternion.Euler(new Vector3(0, 0, 180)));
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                Instantiate(tearXRay, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
            }
            
            source.PlayOneShot(shoot, 5f);
        }
    }
    void FireTearShield()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(tearShield, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
            source.PlayOneShot(shoot, 5f);
        }
    }

    public void FireArea()
    {
        Instantiate(areaTear, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
        Instantiate(areaTear, transform.position, Quaternion.Euler(new Vector3(0, 0, 30)));
        Instantiate(areaTear, transform.position, Quaternion.Euler(new Vector3(0, 0, 60)));
        Instantiate(areaTear, transform.position, Quaternion.Euler(new Vector3(0, 0, 90)));
        Instantiate(areaTear, transform.position, Quaternion.Euler(new Vector3(0, 0, 120)));
        Instantiate(areaTear, transform.position, Quaternion.Euler(new Vector3(0, 0, 150)));
        Instantiate(areaTear, transform.position, Quaternion.Euler(new Vector3(0, 0, 180)));
        Instantiate(areaTear, transform.position, Quaternion.Euler(new Vector3(0, 0, 210)));
        Instantiate(areaTear, transform.position, Quaternion.Euler(new Vector3(0, 0, 240)));
        Instantiate(areaTear, transform.position, Quaternion.Euler(new Vector3(0, 0, 270)));
        Instantiate(areaTear, transform.position, Quaternion.Euler(new Vector3(0, 0, 300)));
        Instantiate(areaTear, transform.position, Quaternion.Euler(new Vector3(0, 0, 330)));
        Instantiate(areaTear, transform.position, Quaternion.Euler(new Vector3(0, 0, 360)));
        source.PlayOneShot(shoot, 5f);

    }

    void FireTearBounce()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            if (Input.GetKey(KeyCode.UpArrow))
            {
                Instantiate(tearBounce, new Vector3(transform.position.x, transform.position.y + 0.02f, transform.position.z)
                    , laserPrefabV.transform.rotation);
                source.PlayOneShot(shoot, 5f);
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                Instantiate(tearBounce, new Vector3(transform.position.x, transform.position.y - 0.02f, transform.position.z)
                    , laserPrefabV.transform.rotation);
                source.PlayOneShot(shoot, 5f);
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                Instantiate(tearBounce, new Vector3(transform.position.x - 0.02f, transform.position.y, transform.position.z),
                    laserPrefabH.transform.rotation);
                source.PlayOneShot(shoot, 5f);
            }

            else if (Input.GetKey(KeyCode.RightArrow))
            {
                Instantiate(tearBounce, new Vector3(transform.position.x + 0.02f, transform.position.y, transform.position.z),
                    laserPrefabH.transform.rotation);
                source.PlayOneShot(shoot, 5f);
            }
            source.PlayOneShot(shoot, 5f);
        }
    }
    void FireDoubleTear()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate / 0.8f;
            if (Input.GetKey(KeyCode.UpArrow))
            {
                Instantiate(tear, new Vector3(transform.position.x - 0.25F, transform.position.y, transform.position.z)
                    , laserPrefabV.transform.rotation);
                Instantiate(tear, new Vector3(transform.position.x + 0.25f, transform.position.y, transform.position.z)
                    , laserPrefabV.transform.rotation);
                source.PlayOneShot(shoot, 5f);
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                Instantiate(tear, new Vector3(transform.position.x - 0.25f, transform.position.y, transform.position.z)
                    , laserPrefabV.transform.rotation);
                Instantiate(tear, new Vector3(transform.position.x + 0.25f, transform.position.y, transform.position.z)
                    , laserPrefabV.transform.rotation);
                source.PlayOneShot(shoot, 5f);
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                Instantiate(tear, new Vector3(transform.position.x, transform.position.y + 0.25f, transform.position.z),
                    laserPrefabH.transform.rotation);
                Instantiate(tear, new Vector3(transform.position.x, transform.position.y - 0.25f, transform.position.z),
                    laserPrefabH.transform.rotation);
                source.PlayOneShot(shoot, 5f);
            }

            else if (Input.GetKey(KeyCode.RightArrow))
            {
                Instantiate(tear, new Vector3(transform.position.x, transform.position.y + 0.25f, transform.position.z),
                    laserPrefabH.transform.rotation);
                Instantiate(tear, new Vector3(transform.position.x, transform.position.y - 0.25f, transform.position.z),
                    laserPrefabH.transform.rotation);
                source.PlayOneShot(shoot, 5f);
            }
            source.PlayOneShot(shoot, 5f);
        }
    }
    void FireTripleTear()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate / 0.6f;
            if (Input.GetKey(KeyCode.UpArrow))
            {
                Instantiate(tear, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
                Instantiate(tear, transform.position, Quaternion.Euler(new Vector3(0, 0, 120)));
                Instantiate(tear, transform.position, Quaternion.Euler(new Vector3(0, 0, 240)));
                source.PlayOneShot(shoot, 5f);
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                Instantiate(tear, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
                Instantiate(tear, transform.position, Quaternion.Euler(new Vector3(0, 0, 120)));
                Instantiate(tear, transform.position, Quaternion.Euler(new Vector3(0, 0, 240)));
                source.PlayOneShot(shoot, 5f);
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                Instantiate(tear, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
                Instantiate(tear, transform.position, Quaternion.Euler(new Vector3(0, 0, 120)));
                Instantiate(tear, transform.position, Quaternion.Euler(new Vector3(0, 0, 240)));
                source.PlayOneShot(shoot, 5f);
            }

            else if (Input.GetKey(KeyCode.RightArrow))
            {
                Instantiate(tear, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
                Instantiate(tear, transform.position, Quaternion.Euler(new Vector3(0, 0, 120)));
                Instantiate(tear, transform.position, Quaternion.Euler(new Vector3(0, 0, 240)));
                source.PlayOneShot(shoot, 5f);
            }
            source.PlayOneShot(shoot, 5f);
        }
    }
    void FireLaser()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate / 45;
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

    public Player GetPosition()
    {
        return this;
    }
}