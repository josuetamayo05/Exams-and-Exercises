using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace Weboo.Examen;

public static class Solution
{
    public static int Solve(int[,] map, int capacity, int origin)
    {
        if (map.GetLength(0) == 1) return 0;
        bool[] mask = new bool[map.GetLength(0)];
        int costo = 0;
        int min = int.MaxValue;
        Backtrack(capacity,origin);
        void Backtrack(int gasolina, int actual)
        {
            if (actual == origin)
            {
                if (mask.All(x => x))
                {
                    min = Math.Min(min, costo);
                    return;
                }
                gasolina = capacity;
                mask[actual] = false;
            }
            for (int i = 0; i < map.GetLength(0); i++)
            {
                if (actual == i) continue;
                if (!mask[i] && gasolina >= map[actual, i])
                {
                    mask[i] = true;
                    costo += map[actual, i];
                    Backtrack(gasolina - map[actual, i], i);
                    costo -= map[actual, i];
                    mask[i] = false;
                }
            }
        }
        return min == int.MaxValue ? -1 : min;
    }

    
    // private static void Backtrack(int[,] map, int capacity, int origin, int actual, int combustible, int totalCost, bool[] mask)
    // {
    //     if (actual == origin)
    //     {
    //         if (mask.All(x => x))
    //         {
    //             min = Math.Min(min, totalCost);
    //             return;
    //         }
    //         mask[actual] = false;
    //         combustible = capacity;
    //     }
    //     for (int i = 0; i < map.GetLength(0); i++)
    //     {
    //         if (actual == i) continue;
    //         if (combustible >= map[actual, i] && !mask[i])
    //         {
    //             mask[i] = true;
    //             totalCost += map[actual, i];
    //             Backtrack(map, capacity, origin, i, combustible - map[actual, i], totalCost, mask);
    //             totalCost -= map[actual, i];
    //             mask[i] = false;
    //         }
    //     }
    // }
}