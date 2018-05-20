using System.Collections.Generic;
using UnityEngine;
public class EsconderObjetos : MonoBehaviour
{
    public List<GameObject> objetos;
    void Start()
    {
        setObjectState(false);
    }
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
    }
    public bool AreEnemiesLeft()
    {
        return ((GameObject.FindGameObjectsWithTag("EnemyF").Length < 1) &&
            (GameObject.FindGameObjectsWithTag("EnemyF_Projectile").Length < 1) &&
            (GameObject.FindGameObjectsWithTag("Enemy").Length < 1));
    }
}