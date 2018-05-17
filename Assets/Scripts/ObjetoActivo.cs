using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ObjetoActivo {

    public ObjetoActivo(int posicion)
    {
        
        int i = 0;
        StreamReader input = new StreamReader("objetos.txt");
        string linea;
        do
        {
            linea = input.ReadLine();
            if (linea != null && i == posicion)
            {
                string[] stats = linea.Split(';');
            }
        } while (linea != null);

        input.Close();
    }
}
