using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CargarEnClick : MonoBehaviour
{

    public void CargarPorIndice(int indice)
    {
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
