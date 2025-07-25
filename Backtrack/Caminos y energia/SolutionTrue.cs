using System.Diagnostics.Tracing;

namespace SolucionCaminosEnergia
{
    public class Examen
    {
        static int[] dx = { 0, 0, -1, 1 };
        static int[] dy = { -1, 1, 0, 0 };
        public static bool HayCamino(int[,] matrix, int f1, int c1, int f2, int c2, int maxConsume)
        {
            int r=0;
            bool[,] mask = new bool[matrix.GetLength(0), matrix.GetLength(1)];
            if (Solve(matrix, c1, f1, f2, c2, maxConsume, r, mask))
                return true;

            return false;
        }

        private static bool Solve(int[,] matrix, int x, int y, int f2, int c2, int maxConsume,  int counterConsume, bool[,] mask)
        {
            if (x == f2 && y == c2)
            {
                return counterConsume <= maxConsume;
            }
            mask[x, y] = true;
            for (int i = 0; i < 4; i++)
            {
                int nx = x + dx[i]; int ny = y + dy[i];
                if (Check(matrix, nx, ny) && !mask[nx, ny])
                {
                    int newConsume = counterConsume;
                    if (matrix[x, y] >= matrix[nx, ny])
                    {
                        newConsume += 1;
                    }
                    else
                    {
                        newConsume += 1 + (matrix[nx, ny] - matrix[x, y]);
                    }
                    if (newConsume > maxConsume) continue;
                    if (Solve(matrix, nx, ny, f2, c2, maxConsume, newConsume, mask)) return true;
                }
            }
            mask[x, y] = false;
            return false;
        }
        private static bool Check(int[,] matrix, int x, int y) => x >= 0 && x < matrix.GetLength(0) && y >= 0 && y < matrix.GetLength(1);
    }
}
