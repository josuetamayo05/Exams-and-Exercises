using System.Diagnostics.SymbolStore;
using System.Threading.Channels;

namespace Paracaidas
{
    class Paracaidista
    {
        static int[] dx = { 1, 0, -1, 0 };
        static int[] dy = { 0, 1, 0, -1 };
        public static int EscogeMayorArea(int[,] terreno, int delta)
        {
            int max = 0;
            int n = terreno.GetLength(0);
            int m = terreno.GetLength(1);
            bool[,] mask = new bool[n, m];
            for (int i = 0; i < terreno.GetLength(0); i++)
            {
                for (int j = 0; j < terreno.GetLength(1); j++)
                {
                    Solve(terreno, delta, new bool[n, m], ref max, 0, i, j);
                }
            }
            return max == 0 ? -1 : max;
        }
        private static void Solve(int[,] terreno, int delta, bool[,] mask, ref int max, int count, int x, int y)
        {
            if (count > max)
            {
                max = count;
            }
            for (int i = 0; i < 4; i++)
            {
                int nx = x + dx[i]; int ny = y + dy[i];
                if (Check(terreno, nx, ny) && !mask[nx, ny])
                {
                    int actual = terreno[x, y]; int later = terreno[nx, ny];
                    if (Math.Abs(later - actual) <= delta)
                    {
                        mask[nx, ny] = true;
                        Solve(terreno, delta, mask, ref max, count + 1, nx, ny);
                        mask[nx, ny] = false;
                    }

                }
            }
        }
        private static bool Check(int[,] terreno, int x, int y) => x >= 0 && x < terreno.GetLength(0) && y >= 0 && y < terreno.GetLength(1);
    }
}