using UnityEngine;
using UnityEngine.SceneManagement;

public class CargarEnClick : MonoBehaviour
{
    public void CargarPorIndice(int indice)
    {
        if (indice == 1)
        {
            Player.lives = 5;
            Player.speed = 4.5f;
            Player.fireRate = 0.5f;
            Player.damage = 10f;
        }
        SceneManager.LoadScene(indice);
    }
    void Start()
    {

    }

    void Update()
    {

    }
}
