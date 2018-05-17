using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomObject : MonoBehaviour
{

    private Sprite[] spritesObjetos;
    private int currentSprite;

    private Objeto objeto;

    void Start()
    {
        currentSprite = Random.Range(0, 4);
        spritesObjetos = Resources.LoadAll<Sprite>("Objetos");
        GetComponent<SpriteRenderer>().sprite = spritesObjetos[currentSprite];
        objeto = new Objeto(currentSprite);
    }

    public Objeto GetObjeto() { return objeto; }




}
