using System;

class Program
{
    static void Main()
    {
        int[,] personasPorCuadra = new int[12, 12]
        {
        { 0, 0, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 2, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 2, 0, 0, 0, 0, 0, 0, 1, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 } // (11,11) es centro de trabajo, valor 0
        };

        bool[,] talleres = new bool[12, 12]
        {
        { false, false, false, false, true,  false, false, false, false, false, false, false },
        { false, false, false, false, false, false, false, false, false, false, false, false },
        { false, false, false, false, false, true,  false, false, false, false, false, false },
        { false, false, false, false, false, false, false, false, false, false, false, false },
        { false, false, false, false, false, false, false, false, false, false, false, false },
        { false, false, false, false, false, false, false, false, false, true,  false, false },
        { false, false, false, false, false, false, false, false, false, false, false, false },
        { false, false, false, false, false, false, false, false, false, false, false, false },
        { false, false, false, false, false, false, false, false, false, false, true,  false },
        { false, false, false, false, false, false, false, false, false, false, false, false },
        { false, false, false, false, false, false, false, false, false, false, false, false },
        { false, false, false, false, false, false, false, false, false, false, false, false }
        };

        int resultado = Solution.MayorCantidadDePersonas(personasPorCuadra, talleres);
        Console.WriteLine("Resultado del ejemplo del PDF (esperado: 9): " + resultado);
    }
}