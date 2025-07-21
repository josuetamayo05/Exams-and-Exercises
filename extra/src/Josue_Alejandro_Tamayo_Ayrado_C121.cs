using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Formats.Tar;

public static class Solution
{
    static int[] dx = { 1, 1, 0, -1, 0, -1, 1, -1 };
    static int[] dy = { 0, 1, -1, 1, 1, 0, -1, -1 };
    public static int Solve(bool[,] matrix, int i, int j)
    {
        int min = int.MaxValue;
        int counter = 0;
        foreach (var item in matrix) if (item) counter++;
        if (counter == matrix.GetLength(0) * matrix.GetLength(1)) return 0;
        Solve2(matrix, i, j, ref min);
        return min;
    }

    private static void Solve2(bool[,] aux, int x, int y, ref int min, int temp = 0)
    {
        if (IsComplete(aux))
        {
            if (temp < min) min = temp;
            return;
        }

        if (temp >= min) return;

        for (int i = 0; i < 8; i++)
        {
            int d_x = dx[i];
            int d_y = dy[i];
            int nx = x + d_x;
            int ny = y + d_y;

            List<(int, int)> activatedMov = [];

            while (Check(aux, nx, ny))
            {
                if (!aux[nx, ny])
                {
                    aux[nx, ny] = true;
                    activatedMov.Add((nx, ny));
                }

                if (activatedMov.Count > 0) //intent moverme si hay al menos uno encendido
                {
                    Solve2(aux, nx, ny, ref min, temp + 1);
                }
                nx += d_x;
                ny += d_y;
            }

            foreach (var (bx, by) in activatedMov)
                aux[bx, by] = false;//backtrack
        }
    }
    
    public static bool IsComplete(bool[,] aux)
    {
        foreach (var s in aux)
        {
            if (!s) return false;
        }
        return true;
    }
   
    
    public static bool Check(bool[,] matrix, int x, int y) => x >= 0 && x < matrix.GetLength(0) && y >= 0 && y < matrix.GetLength(1);
}