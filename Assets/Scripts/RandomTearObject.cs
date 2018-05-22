using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTearObject : MonoBehaviour {

    private Sprite[] spritesObjetos;
    private int currentSprite;
    
    void Start () {
        currentSprite = Random.Range(0, 6);
        spritesObjetos = Resources.LoadAll<Sprite>("ObjetosLagrimas");
        GetComponent<SpriteRenderer>().sprite = spritesObjetos[currentSprite];
    }
	
    public string getTear()
    {
        string tearType="";
        switch (currentSprite)
        {
            case 0:
                tearType = "laser";
                break;
            case 1:
                tearType = "triple";
                break;
            case 2:
                tearType = "escudo";
                break;
            case 3:
                tearType = "rebote";
                break;
            case 4:
                tearType = "doble";
                break;
            case 5:
                tearType = "Xray";
                break;
        }
        return tearType;
    }
}
