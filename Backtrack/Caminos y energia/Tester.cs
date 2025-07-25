using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SolucionCaminosEnergia;

namespace ProbandoSolucion
{
    class Program
    {
        // puede sustituir la implementación del método Main por la lógica de testing que desee.
        static void Main(string[] args)
        {
            // Matriz del enunciado
            int[,] matrizEnunciado = new int[,]
            {
                { 4, 4, -5, 9},
                {3, 0, 15, 1},
                {7, -1, 8, 10},
                {4, 1, 0, 14}
            };

            // Console.WriteLine("Test 1 (esperado True): " + Examen.HayCamino(matrizEnunciado, 1, 0, 2, 3, 29));
            
            // Console.WriteLine("Test 2 (esperado True): " + Examen.HayCamino(matrizEnunciado, 1, 0, 2, 3, 30));
            
            // Console.WriteLine("Test 3 (esperado False): " + Examen.HayCamino(matrizEnunciado, 1, 0, 2, 3, 28));
            
            // Console.WriteLine("Test 4 (esperado True): " + Examen.HayCamino(matrizEnunciado, 2, 2, 2, 2, 0));
            
            Console.WriteLine("Test 5 (esperado True): " + Examen.HayCamino(matrizEnunciado, 0, 0, 0, 1, 1));
            
            Console.WriteLine("Test 6 (esperado False): " + Examen.HayCamino(matrizEnunciado, 0, 0, 3, 3, -1));
            
            // Matriz pequeña para pruebas adicionales
            int[,] matrizSimple = new int[,]
            {
                {1, 2},
                {3, 4}
            };
            
            // Caso de prueba 7: Camino en matriz pequeña (1→3→4, consumo 1 + 2 = 3)
            Console.WriteLine("Test 7 (esperado True): " + Examen.HayCamino(matrizSimple, 0, 0, 1, 1, 3));
            
            // Caso de prueba 8: Mismo camino pero con límite justo (2)
            Console.WriteLine("Test 8 (esperado False): " + Examen.HayCamino(matrizSimple, 0, 0, 1, 1, 2));
            
            // Matriz con valores negativos
            int[,] matrizNegativos = new int[,]
            {
                {-5, -2},
                {-3, -1}
            };
            
            // Caso de prueba 9: Camino con valores negativos (-5→-3→-1, consumo 1 + 3 = 4)
            Console.WriteLine("Test 9 (esperado True): " + Examen.HayCamino(matrizNegativos, 0, 0, 1, 1, 4));
        }
    }
}