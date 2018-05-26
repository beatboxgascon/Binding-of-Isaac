using UnityEngine;
public class EnemyProjectile : MonoBehaviour
{
    private float speed;
    void Start()
    {
        speed = 3;
    }
    void Update()
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        if (screenPos.y >= Screen.height)
            Destroy(gameObject);
        transform.Translate(new Vector3(1, 0) * Time.deltaTime * speed);
    }
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}