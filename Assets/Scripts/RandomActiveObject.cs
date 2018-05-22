using UnityEngine;
public class RandomActiveObject : MonoBehaviour
{
    private Sprite[] spritesObjetos;
    private int currentSprite;
    private int cargas;
    void Start()
    {
        currentSprite = Random.Range(0, 6);
        spritesObjetos = Resources.LoadAll<Sprite>("ObjetosActivos");
        GetComponent<SpriteRenderer>().sprite = spritesObjetos[currentSprite];
        if (currentSprite < 2)
            cargas = 3;
        else if (currentSprite >= 2 && currentSprite < 4)
            cargas = 4;
        else
            cargas = 5;
    }

    public RandomActiveObject(int currentSprite)
    {
        this.currentSprite = currentSprite;
    }
    public int getCurrentSprite() { return currentSprite; }
    public void Activate()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        switch (currentSprite)
        {
            case 0:
                player.GetComponent<Player>().FireArea();
                break;
            case 1:
                player.GetComponent<Player>().TemporalDamage();
                break;
            case 2:
                player.GetComponent<Player>().SetInmunity();
                break;
            case 3:
                player.GetComponent<Player>().GetRoom().RoomDamage();
                break;
            case 4:
                player.GetComponent<Player>().updateCoins(5);
                break;
            case 5:
                player.GetComponent<Player>().LifeUp();
                break;
            default:
                break;
        }
     
    }
    public int GetCargas() { return cargas; }
}