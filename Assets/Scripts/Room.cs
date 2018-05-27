using System.Collections.Generic;
using UnityEngine;
public class Room : MonoBehaviour
{
    public List<GameObject> objetos;
    public List<GameObject> puertas;
    public List<GameObject> enemigosHabitacion;
    Collider2D prueba;
    bool addedCharge;
    void Start()
    {
        SetObjectState(false);
        SetEnemiesState(false);
        addedCharge = false;
        
    }
    void Update()
    {
        if (AreEnemiesLeft() && !addedCharge)
        {
            addedCharge = true;
            if (GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().HasActiveObject())
                GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().UpdateCharges(1);
            SetObjectState(true);
        }
    }
    public void SetObjectState(bool active)
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


    public void SetEnemiesState(bool active)
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
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().SetRoom(this.gameObject);
            SetEnemiesState(true);
        }


    }
    public void RoomDamage()
    {
        foreach (var item in enemigosHabitacion)
        {
            if (item != null)
            {
                item.GetComponent<Enemigo>().doDamage(50f);
            }
        }
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

    public void FirstRoom()
    {
        foreach (var item in enemigosHabitacion)
        {
            if (item != null)
            {
                print("AAAAAAA");
                item.SetActive(true);
                item.SetActive(false);
            }
        }
    }
}