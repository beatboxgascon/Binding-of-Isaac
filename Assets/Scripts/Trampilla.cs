using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampilla : MonoBehaviour {
    // Use this for initialization
    public GameObject prueba;
    void Start () {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectsWithTag("EnemyF").Length==0)
        {
            prueba.SetActive(true);
        }
    }

}
