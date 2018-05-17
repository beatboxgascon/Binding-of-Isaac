using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomActiveObject : MonoBehaviour {

    private Sprite[] spritesObjetos;
    private int currentSprite;

    void Start()
    {
        currentSprite = Random.Range(0, 11);
        spritesObjetos = Resources.LoadAll<Sprite>("ObjetosActivos");
        GetComponent<SpriteRenderer>().sprite = spritesObjetos[currentSprite];
    }

    public int getCurrentSprite() { return currentSprite; }


    
}
