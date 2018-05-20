using UnityEngine;
public class RandomActiveObject : MonoBehaviour
{
    private Sprite[] spritesObjetos;
    private int currentSprite;
    private int cargas;
    void Start()
    {
        currentSprite = Random.Range(0, 11);
        spritesObjetos = Resources.LoadAll<Sprite>("ObjetosActivos");
        GetComponent<SpriteRenderer>().sprite = spritesObjetos[currentSprite];
        if (currentSprite < 4)
        {
            cargas = 3;
        }
        else if (currentSprite >= 4 && currentSprite < 8)
        {
            cargas = 4;
        }
        else
        {
            cargas = 5;
        }
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
    public int GetCargas() { return cargas; }
}