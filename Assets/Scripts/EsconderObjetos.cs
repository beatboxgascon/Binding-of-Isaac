using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EsconderObjetos : MonoBehaviour {

    public List<GameObject> objetos;

	// Use this for initialization
	void Start () {
        foreach (var item in objetos)
        {
            if (item!=null)
            {
                item.SetActive(false);
            }
            
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (AreEnemiesLeft())
        {
            foreach (var item in objetos)
            {
                if (item!=null)
                {
                    item.SetActive(true);
                } 
            }
        }
	}

    public bool AreEnemiesLeft()
    {
        return ((GameObject.FindGameObjectsWithTag("EnemyF").Length < 1) &&
            (GameObject.FindGameObjectsWithTag("EnemyF_Projectile").Length < 1));
    }
}
