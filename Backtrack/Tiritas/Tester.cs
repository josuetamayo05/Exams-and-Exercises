using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Tiritas
{
    public class Tester
    {
        static void Main()
        {
            bool[,] patron = new bool[,]
            {
                {false, false,  true, false, false},
                { true,  true,  true,  true,  true},
                {false, false,  true, false, false},
                {false, false,  true, false, false},
            };
            //imprimirTablero(patron);
            int resultado = Solution.Resolver(patron);
            Console.WriteLine(resultado);  // Debe imprimir 2

        }
        static void imprimirTablero(bool[,] tablero)
        {
            for (int i = 0; i < tablero.GetLength(0); i++)
            {
                for (int j = 0; j < tablero.GetLength(1); j++)
                {
                    if (tablero[i, j]) Console.Write("⬜");
                    else Console.Write(" ");
                }
                Console.WriteLine();
            }
        }
    }
}