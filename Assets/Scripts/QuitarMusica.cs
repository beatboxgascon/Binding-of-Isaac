using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitarMusica : MonoBehaviour {

	// Use this for initialization
	void Start () {
        if (GameObject.FindGameObjectWithTag("Music"))
        {
            Destroy(GameObject.FindGameObjectWithTag("Music"));
        }
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
