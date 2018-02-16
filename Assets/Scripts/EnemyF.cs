using UnityEngine;
using System.Collections;

public class EnemyF : MonoBehaviour
{
    public Transform target;//set target from inspector instead of looking in Update
    public float speed = 1f;


    void Start()
    {

    }

    void Update()
    {
        transform.position += (target.transform.position - transform.position).normalized * speed * Time.deltaTime;
    }


}
