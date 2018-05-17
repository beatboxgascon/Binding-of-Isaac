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

    public RandomActiveObject(int currentSprite)
    {
        this.currentSprite = currentSprite;
    }

    public int getCurrentSprite() { return currentSprite; }

    public void Activate()
    {
        //if(currentSprite%2==0)
        //    GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().LifeUp();
        //else
        //    GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().updateCoins(5);
        RoomDamage();
    }

    public void RoomDamage()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().GetRoom().RoomDamage();
    }
    

}
