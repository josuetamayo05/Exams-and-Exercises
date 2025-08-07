
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.InteropServices;

public class Map
{
    private int[,] Distances { get; set; }
    public int M { get; private set; }
    public int[] A { get; private set; }
    public int[] B { get; private set; }

    public Map(int[,] distances, int[] A, int[] B)
    {
        Distances = distances;
        M = Distances.GetLength(0);
        this.A = A;
        this.B = B;
    }

    public int this[int i, int j]
    {
        get => Distances[i,j];
    }

    public bool IsTypeA(int node)
    {
        return A.Contains(node);
    }

    public bool IsTypeB(int node)
    {
        return B.Contains(node);
    }
}

public class Solution
{
    public static int Solve(Map map, int n)
    {
        int min = int.MaxValue;
        int[] leones = new int[map.M - 1];
        for (int i = 0; i < map.M - 1; i++)
        {
            leones[i] = i + 1;
        }
        List<List<List<int>>> combination = Partitioner(leones, n);
        foreach (List<List<int>> comb in combination)
        {
            bool combinacionValid = true;
            int rutaTotal = 0;
            foreach (List<int> interna in comb)
            {

                if (interna.Count == 0) continue;
                int rutaActual = CalcularCosto(map, interna);
                if (rutaActual == -1)
                {
                    combinacionValid = false;
                    break;
                }
                rutaTotal += rutaActual;
                
            }
            if (combinacionValid)
            {
                min = Math.Min(min, rutaTotal);
            }
        }

        return min == int.MaxValue ? -1 : min;
    }

    private static int CalcularCosto(Map map, List<int> interna)
    {
        int distanciaActual = 0;
        bool flag = false;
        int temp = 0;
        for (int i = 0; i < interna.Count; i++)
        {
            if (map.IsTypeB(interna[i]))
                flag = true;
            if (map.IsTypeA(interna[i]) && flag) return -1;
            distanciaActual += map[temp, interna[i]];
            temp = interna[i];
        }
        distanciaActual += map[temp, 0];
        return distanciaActual;
    }
    private static List<List<List<int>>> Partitioner(int[] items, int n)
    {
        List<List<List<int>>> permutaciones = new List<List<List<int>>>();
        List<List<int>> current = new List<List<int>>();
        for (int i = 0; i < n; i++) // Permutaciones para n barcos
        {
            current.Add(new List<int>());
        }
        PartitionerCombinations(permutaciones, current, items, 0);
        return permutaciones;
    }
    private static void PartitionerCombinations(List<List<List<int>>> permutaciones, List<List<int>> current, int[] items, int index)
    {
        if (index == items.Length)
        {
            List<List<int>> copy = new List<List<int>>();
            for (int i = 0; i < current.Count; i++)
            {
                List<int> barcoListas = new List<int>();
                for (int j = 0; j < current[i].Count; j++)
                {
                    barcoListas.Add(current[i][j]);
                }
                copy.Add(barcoListas);
            }
            permutaciones.Add(copy);
            return;
        }
        for (int i = 0; i < current.Count; i++)
        {
            current[i].Add(items[index]);
            PartitionerCombinations(permutaciones, current, items, index + 1);
            current[i].RemoveAt(current[i].Count - 1);
        }
    }
}

// Ejemplo de uso con el caso del PDF
public class Program
{
    public static void Main()
    {
        int[,] distances = {
            { 0, 1, 1, 3 },
            { 1, 0, 3, 1 },
            { 1, 3, 0, 1 },
            { 3, 1, 1, 0 }
        };
        int[] A = { 1, 2 };
        int[] B = { 3 };
        
        Map map = new Map(distances, A, B);
        int result = Solution.Solve(map, 2);
        Console.WriteLine(result); // Debería imprimir 7
    }
}
