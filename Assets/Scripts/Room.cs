using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour {

    public List<GameObject> objetos;
    public List<GameObject> puertas;
    public List<GameObject> enemigosHabitacion;
    Collider2D prueba;

    // Use this for initialization
    void Start()
    {
        setObjectState(false);
        setEnemiesState(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (AreEnemiesLeft())
            setObjectState(true);
    }

    public void setObjectState(bool active)
    {
        foreach (var item in objetos)
        {
            if (item != null)
            {
                item.SetActive(active);
            }
        }

        foreach (var item in puertas)
        {
            if (item != null)
            {
                item.SetActive(active);
            }
        }

    }

    public void setEnemiesState(bool active)
    {
        foreach (var item in enemigosHabitacion)
        {
            if (item != null)
            {
                item.SetActive(active);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D player)
    {
        if (player.CompareTag("Player"))
        {
            setEnemiesState(true);
        }

        //if (player.CompareTag("Projectile"))
        //{
        //    Destroy(player.gameObject);
        //    player.transform.position.x
        //}
    }



    public bool AreEnemiesLeft()
    {
        int muertos = 0;
        foreach (var item in enemigosHabitacion)
        {
            if (item == null)
            {
                muertos++;
            }
        }

        return enemigosHabitacion.Count == muertos;
    }
}
