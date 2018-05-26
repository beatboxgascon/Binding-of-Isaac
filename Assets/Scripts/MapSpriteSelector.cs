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
    public int type; // 0: normal, 1: enter, 3:boss
    public Color normalColor, enterColor;
    Color mainColor;
    GameObject rend;


    void Start()
    {
    }
    GameObject PickSprite(int type)
    { //picks correct sprite based on the four door bools
        GameObject prueba;
        if (up)
        {
            if (down)
            {
                if (right)
                {
                    if (left)
                    {
                        prueba = spUDRL;
                    }
                    else
                    {
                        prueba = spDRU;
                    }
                }
                else if (left)
                {
                    prueba = spULD;
                }
                else
                {
                    prueba = spUD;
                }
            }
            else
            {
                if (right)
                {
                    if (left)
                    {
                        prueba = spRUL;
                    }
                    else
                    {
                        prueba = spUR;
                    }
                }
                else if (left)
                {
                    prueba = spUL;
                }
                else
                {
                    if (type == 3)
                    {
                        prueba = bossU;
                    }
                    else if (type == 4)
                    {
                        prueba = shopU;
                    }
                    else
                    {
                        prueba = spU;
                    }
                }
            }
            return prueba;
        }
        if (down)
        {
            if (right)
            {
                if (left)
                {
                    prueba = spLDR;
                }
                else
                {
                    prueba = spDR;
                }
            }
            else if (left)
            {
                prueba = spDL;
            }
            else
            {
                if (type == 3)
                {
                    prueba = bossD;
                }
                else if (type == 4)
                {
                    prueba = shopD;
                }
                else
                {
                    prueba = spD;
                }
                
            }
            return prueba;
        }
        if (right)
        {
            if (left)
            {
                prueba = spRL;
            }
            else
            {
                if (type == 3)
                {
                    prueba = bossR;
                }
                else if (type == 4)
                {
                    prueba = shopR;
                }
                else
                {
                    prueba = spR;
                }
            }
        }
        else
        {
            if (type == 3)
            {
                prueba = bossL;
            }
            else if (type == 4)
            {
                prueba = shopL;
            }
            else
            {
                prueba = spL;
            }
        }

        return prueba;
    }

    public GameObject getRoom(int type)
    {
        return PickSprite(type);
    }

    public GameObject getBossRoom(int type)
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
    public GameObject getPreBossRoom(int type)
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

    public GameObject getShopRoom(int type)
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
    public GameObject getPreShopRoom(int type)
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