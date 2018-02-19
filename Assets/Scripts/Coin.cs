using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public static GameObject[] myObjects;
    //I used this to keep track of the number of objects I spawned in the scene.
    // Use this for initialization
    void Start()
    {
        myObjects = Resources.LoadAll<GameObject>("Objetos");
        int whichItem = 0;


        GameObject myObj = Instantiate(myObjects[whichItem]) as GameObject;
        myObj.transform.position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }

   
}
