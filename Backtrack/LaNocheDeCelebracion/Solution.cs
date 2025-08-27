using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Threading.Tasks.Dataflow;

namespace LaNocheDeCelebracion;

public class Solution
{
    public static int MinStepHome(bool[,] map, (int, int) home)
    {
        int min = int.MaxValue;
        Solve(map, 0, 0, home, 0, ref min);
        return min == int.MaxValue ? -1 : min;
    }
    // Movimientos horizontales (columnas) caballo
    static int[] dx = { 3, 1, -1, -3, -3, -1, 1, 3 };

    // Movimientos verticales (filas) caballo
    static int[] dy = { 1, 3, 3, 1, -1, -3, -3, -1 };
    public static void Solve(bool[,] map, int x, int y, (int, int) home, int count, ref int min)
    {
        if (x == home.Item1 && y == home.Item2)
        {
            if (min > count) min = count;
            return;
        }
        if (count >= min) return;
        for (int i = 0; i < 8; i++)
        {
            int nx = x + dx[i]; int ny = y + dy[i];
            if (!Check(map, nx, ny))
            {
                (int row, int col) = GetPositions(map, nx, ny);
                nx = row; ny = col;
            }
            if (!map[nx, ny])
            {
                map[nx, ny] = true;
                Solve(map, nx, ny, home, count + 1, ref min);
                map[nx, ny] = false;
            }
        }
    }
    private static bool Check(bool[,] map, int x, int y) => x >= 0 && x < map.GetLength(0) && y >= 0 && y < map.GetLength(1);
    private static (int row, int col) GetPositions(bool[,] map, int x, int y)
    {
        if (x >= map.GetLength(0))
        {
            x = Math.Abs(x - map.GetLength(0));
        }
        if (y >= map.GetLength(1))
        {
            y = Math.Abs(y - map.GetLength(1));
        }
        if (x < 0)
        {
            x = x + map.GetLength(0);
        }
        if (y < 0)
        {
            y = y + map.GetLength(1);
        }
        return (x, y);
    }
}