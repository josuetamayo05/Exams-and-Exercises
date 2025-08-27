using System.ComponentModel.Design;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Channels;

namespace Tiritas
{
    public class Tiritas
    {
        public int StartRow { get; set; }
        public int EndRow { get; set; }
        public int StartCol { get; set; }
        public int EndCol { get; set; }
        public Tiritas(int startRow, int endRow, int startCol, int endCol)
        {
            StartRow = startRow; EndRow = endRow;
            StartCol = startCol; EndCol = endCol;
        }
    }
    public class Solution
    {
        static int[] dx = { 1, 0, -1, 0 };
        static int[] dy = { 0, 1, 0, -1 };
        public static int Resolver(bool[,] patron)
        {
            int min = int.MaxValue;
            int filas = patron.GetLength(0);
            int columnas = patron.GetLength(1);
            List<Tiritas> positions = CrearTiritas(patron, filas,columnas);
            return Solve(patron, 0, positions, min, new bool[positions.Count], new bool[filas,columnas]);
        }

        static int Solve(bool[,] patron, int count, List<Tiritas> tiras, int min, bool[] used, bool[,]mask2)
        {
            bool[,] mask = new bool[patron.GetLength(0), patron.GetLength(1)];
            for (int i = 0; i < tiras.Count; i++)
            {
                if (used[i])
                {
                    for (int j = tiras[i].StartRow; j <= tiras[i].EndRow; j++)
                    {
                        for (int k = tiras[i].StartCol; k <= tiras[i].EndCol; k++)
                        {
                            mask[j, k] = true;
                        }
                    }
                }
            }
            if (count >= min) return int.MaxValue;
            if (AllFalse(patron, mask))
                return count;

            for (int i = 0; i < tiras.Count; i++)
            {
                if (!used[i])
                {
                    used[i] = true;
                    min = Math.Min(min, Solve(patron, count + 1, tiras, min, used, mask));
                    used[i] = false;
                }
            }
            return min;
        }
        static bool AllFalse(bool[,] patron, bool[,] mask) {
            for (int i = 0; i < patron.GetLength(0); i++)
            {
                for (int j = 0; j < patron.GetLength(1); j++)
                {
                    if (patron[i, j]&&!mask[i,j]) return false;
                }
            }
            return true;
        }
        static bool Check(bool[,] map, int x, int y) => x >= 0 && x < map.GetLength(0) && y >= 0 && y < map.GetLength(1);
        static List<Tiritas> CrearTiritas(bool[,] patron, int filas, int columnas)
        {
            List<Tiritas> tiritas = [];
            for (int i = 0; i < filas; i++)
            {
                int start = -1;
                for (int j = 0; j < columnas; j++)
                {
                    if (patron[i, j])
                    {
                        if (start == -1) start = j;
                    }
                    else
                    {
                        if (start != -1)
                        {
                            tiritas.Add(new Tiritas(i, i, start, j - 1));
                            start = -1;
                        }
                    }
                }
                if (start != -1)
                {
                    tiritas.Add(new Tiritas(i, i, start, columnas - 1));
                }

            }
            for (int j = 0; j < columnas; j++)
            {
                int start = -1;
                for (int i = 0; i < filas; i++)
                {
                    if (patron[i, j])
                    {
                        if (start == -1) start = i;
                    }
                    else
                    {
                        if (start != -1)
                        {
                            tiritas.Add(new Tiritas(start, i - 1, j, j));
                        }
                    }
                }
                if (start != -1)
                {
                    tiritas.Add(new Tiritas(start, filas - 1, j, j));
                }
            }
            return tiritas;
        }
    }

}
