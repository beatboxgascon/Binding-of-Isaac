﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratedRoom  {
    public Vector2 gridPos; //Posición del grid
    public int type; //Tipo de habitación
    public bool doorTop, doorBot, doorLeft, doorRight; //Puertas de la habitación

    public GeneratedRoom(Vector2 gridPos, int type)
    {
        this.gridPos = gridPos;
        this.type = type;
    }
}