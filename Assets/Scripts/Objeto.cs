using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    public class Objeto
    {
        public float Speed { get; set; }
        public float FireRate { get; set; }
        public float Damage { get; set; }

        public Objeto(int posicion)
        {
            Speed = 0;
            FireRate = 1;
            Damage = 0;
            int i = 0;
            StreamReader input = new StreamReader("objetos.txt");
            string linea;
            do
            {
                linea = input.ReadLine();
                if (linea != null && i == posicion)
                {
                    string[] stats = linea.Split(';');
                    Speed = Convert.ToSingle(stats[0]);
                    FireRate = Convert.ToSingle(stats[1]);
                    Damage = Convert.ToSingle(stats[2]);
                }
            } while (linea != null);

            input.Close();
        }
    }
}
