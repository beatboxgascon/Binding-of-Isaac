using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSpriteSelector : MonoBehaviour
{

    public GameObject spU, spD, spR, spL,
            spUD, spRL, spUR, spUL, spDR, spDL,
            spULD, spRUL, spDRU, spLDR, spUDRL, 
            bossD, bossU, bossR, bossL,
            shopD, shopU, shopR, shopL,
            preBossD, preBossU, preBossR, preBossL,
            preShopD, preShopU, preShopR, preShopL;
    public bool up, down, left, right;
    public int type; // 0: normal, 3:boss, 4: shop
    public Color normalColor, enterColor;

    void Start()
    {
        
    }
    GameObject PickSprite(int type)
    { //Selecciona el gameobject adecuado dependiendo de las puertas que tiene
        GameObject habitacion;
        if (up)
        {
            if (down)
            {
                if (right)
                {
                    if (left)
                    {
                        habitacion = spUDRL;
                    }
                    else
                    {
                        habitacion = spDRU;
                    }
                }
                else if (left)
                {
                    habitacion = spULD;
                }
                else
                {
                    habitacion = spUD;
                }
            }
            else
            {
                if (right)
                {
                    if (left)
                    {
                        habitacion = spRUL;
                    }
                    else
                    {
                        habitacion = spUR;
                    }
                }
                else if (left)
                {
                    habitacion = spUL;
                }
                else
                {
                    habitacion = spU;
                }
            }
            return habitacion;
        }
        if (down)
        {
            if (right)
            {
                if (left)
                {
                    habitacion = spLDR;
                }
                else
                {
                    habitacion = spDR;
                }
            }
            else if (left)
            {
                habitacion = spDL;
            }
            else
            {
                habitacion = spD;
            }
            return habitacion;
        }
        if (right)
        {
            if (left)
            {
                habitacion = spRL;
            }
            else
            {
                habitacion = spR;
            }
        }
        else
        {
            habitacion = spL;
        }

        return habitacion;
    }

    public GameObject getRoom(int type)
    {
        GameObject room = PickSprite(type);
        if (type == 1)
        {
            room.GetComponent<Room>().FirstRoom();
        }
        return room;
    }

    public GameObject GetBossRoom(int type)
    {
        GameObject room=null;
        switch (type)
        {
            case 0:
                room =bossD;
                break;
            case 1:
                room = bossU;
                break;
            case 2:
                room = bossL;
                break;
            case 3:
                room = bossR;
                break;
        }

        return room;
    }
    public GameObject GetPreBossRoom(int type)
    {
        GameObject room = null;
        switch (type)
        {
            case 0:
                room = preBossU;
                break;
            case 1:
                room = preBossD;
                break;
            case 2:
                room = preBossR;
                break;
            case 3:
                room = preBossL;
                break;
        }

        return room;
    }

    public void FirstRoom()
    {

    }

    public GameObject GetShopRoom(int type)
    {
        GameObject room = null;
        switch (type)
        {
            case 0:
                room = shopD;
                break;
            case 1:
                room = shopU;
                break;
            case 2:
                room = shopL;
                break;
            case 3:
                room = shopR;
                break;
        }

        return room;
    }
    public GameObject GetPreShopRoom(int type)
    {
        GameObject room = null;
        switch (type)
        {
            case 0:
                room = preShopU;
                break;
            case 1:
                room = preShopD;
                break;
            case 2:
                room = preShopR;
                break;
            case 3:
                room = preShopL;
                break;
        }

        return room;
    }


}