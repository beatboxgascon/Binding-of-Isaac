using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{

    public string spriteName;
    public Sprite[] spritesObjetos;
    public int currentSprite;

    private Objeto objeto;

    void Start()
    {
        currentSprite = Random.Range(0, 5);
        spritesObjetos = Resources.LoadAll<Sprite>(spriteName);
        GetComponent<SpriteRenderer>().sprite = spritesObjetos[currentSprite];
        objeto = new Objeto(currentSprite);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public Objeto GetObjeto() { return objeto; }

  

   
}
