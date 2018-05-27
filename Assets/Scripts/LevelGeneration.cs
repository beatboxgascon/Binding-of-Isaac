using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    Vector2 worldSize = new Vector2(4, 4); //Tamaño del grid donde estarán las habitaciones
    GeneratedRoom[,] rooms;
    List<Vector2> takenPositions = new List<Vector2>();
    int gridSizeX, gridSizeY, numberOfRooms = 15;
    bool bossRoom = false, shopRoom = false;
    public GameObject roomWhiteObj;
    void Start()
    {
        // Comprueba que el número de habitaciones no sea mayor que el número de celdas disponibles
        if (numberOfRooms >= (worldSize.x * 2) * (worldSize.y * 2))
        {
            numberOfRooms = Mathf.RoundToInt((worldSize.x * 2) * (worldSize.y * 2));
        }
        gridSizeX = Mathf.RoundToInt(worldSize.x);
        gridSizeY = Mathf.RoundToInt(worldSize.y);
        CreateRooms(); //Coloca las habitaciones de forma aleatoria
        SetRoomDoors(); //Coloca las puertas para conectarlas a las habitaciones adyacentes
        SetShopAndBoss(); //Coloca las habitaciones del jefe y la tienda, además de sus entradas
        DrawMap(); //Dibuja las habitaciones en el mapa
    }
    void CreateRooms()
    {

        rooms = new GeneratedRoom[gridSizeX * 2, gridSizeY * 2];
        rooms[gridSizeX, gridSizeY] = new GeneratedRoom(Vector2.zero, 1); //Crea la primera habitación en el centro del mapa y sus alrededores
        rooms[gridSizeX + 1, gridSizeY] = new GeneratedRoom(Vector2.right, 0);
        rooms[gridSizeX - 1, gridSizeY] = new GeneratedRoom(Vector2.left, 0);
        rooms[gridSizeX, gridSizeY + 1] = new GeneratedRoom(Vector2.up, 0);
        rooms[gridSizeX, gridSizeY - 1] = new GeneratedRoom(Vector2.down, 0);
        takenPositions.Insert(0, Vector2.zero); //Pone esas posiciones como ocupadas
        takenPositions.Insert(1, Vector2.right);
        takenPositions.Insert(2, Vector2.left);
        takenPositions.Insert(3, Vector2.up);
        takenPositions.Insert(4, Vector2.down);
        Vector2 checkPos = Vector2.zero;

        //magic numbers
        float randomCompare = 0.2f, randomCompareStart = 0.2f, randomCompareEnd = 0.01f;

        //Añadir habitaciones
        for (int i = 0; i < numberOfRooms - 1; i++)
        {
            //Cuanto más avancemos en el bucle, menos probabilidades de que se ramifiquen las habitaciones
            float randomPerc = ((float)i) / (((float)numberOfRooms - 1));

            randomCompare = Mathf.Lerp(randomCompareStart, randomCompareEnd, randomPerc);

            //Selecciona una posición
            checkPos = NewPosition();

            //En caso de que tenga más de una habitación adyacente y RandomCompare sea mayor a un número entre 0 y 1
            //Intentará ramificarse
            if (NumberOfNeighbors(checkPos, takenPositions) > 1 && Random.value > randomCompare)
            {
                //Buscará una nueva posición que tenga solamente una habitación que conecte
                int iterations = 0;
                do
                {
                    checkPos = SelectiveNewPosition();
                    iterations++;
                } while (NumberOfNeighbors(checkPos, takenPositions) > 1 && iterations < 100);
                if (iterations >= 50)
                    print("error: could not create with fewer neighbors than : " + NumberOfNeighbors(checkPos, takenPositions));
            }

            //Añade las posiciones a la habitación y añade a la lista de posiciones ocupadas
            rooms[(int)checkPos.x + gridSizeX, (int)checkPos.y + gridSizeY] = new GeneratedRoom(checkPos, 0);
            takenPositions.Insert(0, checkPos);
        }
    }

    //Una posición es válida cuando una es adyacente a una habitación que ya existe
    Vector2 NewPosition()
    {
        int x = 0, y = 0;
        Vector2 checkingPos = Vector2.zero;
        do
        {
            // Selecciona una habitación aleatoria
            int index = Mathf.RoundToInt(Random.value * (takenPositions.Count - 1));
            x = (int)takenPositions[index].x;
            y = (int)takenPositions[index].y;

            //Aleatoriamente selecciona si buscar en el eje X o Y con su signo
            bool UpDown = (Random.value < 0.5f);
            bool positive = (Random.value < 0.5f);
            if (UpDown)
            { //find the position bnased on the above bools
                if (positive)
                {
                    y += 1;
                }
                else
                {
                    y -= 1;
                }
            }
            else
            {
                if (positive)
                {
                    x += 1;
                }
                else
                {
                    x -= 1;
                }
            }
            checkingPos = new Vector2(x, y);
            //Se repite hasta que encuentre una posición no ocupada y esté dentro de los límites
        } while (takenPositions.Contains(checkingPos) || x >= gridSizeX || x < -gridSizeX || y >= gridSizeY || y < -gridSizeY);
        return checkingPos;
    }
    //Similar al anterior, con ligeros cambios
    Vector2 SelectiveNewPosition()
    {
        int index = 0, inc = 0;
        int x = 0, y = 0;
        Vector2 checkingPos = Vector2.zero;
        do
        {
            inc = 0;

            //Nos aseguramos de coger una habitación que tenga solamente una habitación adyacente
            //Así es más probable que consigamos una habitación que se ramifique
            do
            {
                index = Mathf.RoundToInt(Random.value * (takenPositions.Count - 1));
                inc++;
            } while (NumberOfNeighbors(takenPositions[index], takenPositions) > 1 && inc < 100);
            x = (int)takenPositions[index].x;
            y = (int)takenPositions[index].y;
            bool UpDown = (Random.value < 0.5f);
            bool positive = (Random.value < 0.5f);
            if (UpDown)
            {
                if (positive)
                {
                    y += 1;
                }
                else
                {
                    y -= 1;
                }
            }
            else
            {
                if (positive)
                {
                    x += 1;
                }
                else
                {
                    x -= 1;
                }
            }
            checkingPos = new Vector2(x, y);
        } while (takenPositions.Contains(checkingPos) || x >= gridSizeX || x < -gridSizeX || y >= gridSizeY || y < -gridSizeY);

        return checkingPos;
    }

    //Comprueba el número de habitaciones adyacentes de la habitación
    int NumberOfNeighbors(Vector2 checkingPos, List<Vector2> usedPositions)
    {
        int ret = 0;
        if (usedPositions.Contains(checkingPos + Vector2.right))
        {
            ret++;
        }
        if (usedPositions.Contains(checkingPos + Vector2.left))
        {
            ret++;
        }
        if (usedPositions.Contains(checkingPos + Vector2.up))
        {
            ret++;
        }
        if (usedPositions.Contains(checkingPos + Vector2.down))
        {
            ret++;
        }
        return ret;
    }

    //Dibuja las habitaciones en el mapa
    void DrawMap()
    {
        foreach (GeneratedRoom room in rooms)
        {
            if (room != null)
            {
                Vector3 drawPos = room.gridPos;

                drawPos.x *= 15;
                drawPos.y *= 9;
                drawPos.z = 15;

                MapSpriteSelector mapper = roomWhiteObj.GetComponent<MapSpriteSelector>();
                mapper.type = room.type;
                mapper.up = room.doorTop;
                mapper.down = room.doorBot;
                mapper.right = room.doorRight;
                mapper.left = room.doorLeft;

                if (room.type == 3)
                {
                    if (room.doorBot)
                    {
                        Instantiate(mapper.GetPreBossRoom(0), drawPos, Quaternion.identity);
                        Instantiate(mapper.GetBossRoom(0), new Vector3(drawPos.x, drawPos.y + 9, 15), Quaternion.identity);
                    }
                    else if (room.doorLeft)
                    {
                        Instantiate(mapper.GetPreBossRoom(2), drawPos, Quaternion.identity);
                        Instantiate(mapper.GetBossRoom(2), new Vector3(drawPos.x + 15, drawPos.y, 15), Quaternion.identity);

                    }
                    else if (room.doorRight)
                    {
                        Instantiate(mapper.GetPreBossRoom(3), drawPos, Quaternion.identity);
                        Instantiate(mapper.GetBossRoom(3), new Vector3(drawPos.x - 15, drawPos.y, 15), Quaternion.identity);
                    }
                    else if (room.doorTop)
                    {
                        Instantiate(mapper.GetPreBossRoom(1), drawPos, Quaternion.identity);
                        Instantiate(mapper.GetBossRoom(1), new Vector3(drawPos.x, drawPos.y - 9, 15), Quaternion.identity);
                    }

                }
                else if (room.type == 4)
                {
                    if (room.doorBot)
                    {
                        Instantiate(mapper.GetPreShopRoom(0), drawPos, Quaternion.identity);
                        Instantiate(mapper.GetShopRoom(0), new Vector3(drawPos.x, drawPos.y + 9, 15), Quaternion.identity);
                    }
                    else if (room.doorLeft)
                    {
                        Instantiate(mapper.GetPreShopRoom(2), drawPos, Quaternion.identity);
                        Instantiate(mapper.GetShopRoom(2), new Vector3(drawPos.x + 15, drawPos.y, 15), Quaternion.identity);

                    }
                    else if (room.doorRight)
                    {
                        Instantiate(mapper.GetPreShopRoom(3), drawPos, Quaternion.identity);
                        Instantiate(mapper.GetShopRoom(3), new Vector3(drawPos.x - 15, drawPos.y, 15), Quaternion.identity);
                    }
                    else if (room.doorTop)
                    {
                        Instantiate(mapper.GetPreShopRoom(1), drawPos, Quaternion.identity);
                        Instantiate(mapper.GetShopRoom(1), new Vector3(drawPos.x, drawPos.y - 9, 15), Quaternion.identity);
                    }

                }
                else
                {
                    Instantiate(mapper.GetRoom(room.type), drawPos, Quaternion.identity);
                }
            }

        }
    }

    // Añade las puertas a las habitaciones para conectarlas 
    void SetRoomDoors()
    {
        for (int x = 0; x < ((gridSizeX * 2)); x++)
        {
            for (int y = 0; y < ((gridSizeY * 2)); y++)
            {
                if (rooms[x, y] != null)
                {
                    if (y - 1 < 0)
                    {
                        rooms[x, y].doorBot = false;
                    }
                    else
                    {
                        rooms[x, y].doorBot = (rooms[x, y - 1] != null);
                    }
                    if (y + 1 >= gridSizeY * 2)
                    {
                        rooms[x, y].doorTop = false;
                    }
                    else
                    {
                        rooms[x, y].doorTop = (rooms[x, y + 1] != null);
                    }
                    if (x - 1 < 0)
                    {
                        rooms[x, y].doorLeft = false;
                    }
                    else
                    {
                        rooms[x, y].doorLeft = (rooms[x - 1, y] != null);
                    }
                    if (x + 1 >= gridSizeX * 2)
                    {
                        rooms[x, y].doorRight = false;
                    }
                    else
                    {
                        rooms[x, y].doorRight = (rooms[x + 1, y] != null);
                    }
                }

            }
        }
    }

    void SetShopAndBoss()
    {
        foreach (GeneratedRoom room in rooms)
        {
            if (room != null)
            {
                if (!bossRoom)
                {
                    if ((room.doorBot && !room.doorLeft && !room.doorRight && !room.doorTop) ||
                        (!room.doorBot && room.doorLeft && !room.doorRight && !room.doorTop) ||
                        (!room.doorBot && !room.doorLeft && room.doorRight && !room.doorTop) ||
                        (!room.doorBot && !room.doorLeft && !room.doorRight && room.doorTop))
                    {
                        room.type = 3;
                        bossRoom = true;
                    }
                }
                else if (!shopRoom)
                {
                    if ((room.doorBot && !room.doorLeft && !room.doorRight && !room.doorTop) ||
                       (!room.doorBot && room.doorLeft && !room.doorRight && !room.doorTop) ||
                       (!room.doorBot && !room.doorLeft && room.doorRight && !room.doorTop) ||
                       (!room.doorBot && !room.doorLeft && !room.doorRight && room.doorTop))
                    {
                        room.type = 4;
                        shopRoom = true;
                    }
                }
            }


        }
    }



}
