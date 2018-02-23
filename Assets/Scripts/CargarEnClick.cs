using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CargarEnClick : MonoBehaviour
{

    public void CargarPorIndice(int indice)
    {
        if (indice == 0)
        {
            Player.lives = 5;
            Player.speed = 4.5f;
            Player.fireRate = 0.5f;
            Player.damage = 10f;
        }
        SceneManager.LoadScene(indice);
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
