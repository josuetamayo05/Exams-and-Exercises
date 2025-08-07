namespace Torres
{
    public class Juego
    {
        public static int MayorEliminacion(bool[,] tablero)
        {
            int maxEliminated = 0;
            for (int i = 0; i < tablero.GetLength(0); i++)
            {
                for (int j = 0; j < tablero.GetLength(1); j++)
                {
                    if (tablero[i, j])
                    {
                        maxEliminated = Math.Max(maxEliminated, Solve(tablero, i, j));
                    }
                }
            }
            return maxEliminated;
        }
        public static int Solve(bool[,] tablero, int x, int y)
        {
            int max = 0;
            int[] dx = { 1, -1, 0, 0 };
            int[] dy = { 0, 0, 1, -1 };

            for (int i = 0; i < 4; i++)
            {
                int nx = x + dx[i];
                int ny = y + dy[i];

                while (Check(tablero, nx, ny) && !tablero[nx, ny])
                {
                    nx += dx[i];ny += dy[i];
                }
                int tx = nx + dx[i];
                int ty = ny + dy[i];
                if (Check(tablero, tx, ty) && !tablero[tx, ty] && Check(tablero,nx,ny))
                {
                    tablero[tx, ty] = true;
                    tablero[nx, ny] = false;
                    tablero[x, y] = false;

                    int temp = 1 + Solve(tablero, tx, ty);
                    max = Math.Max(max, temp);

                    tablero[tx, ty] = false;
                    tablero[nx, ny] = true;
                    tablero[x, y] = true;
                }
            }
            return max;
        }

        private static bool Check(bool[,] tablero, int x, int y) => x >= 0 && x < tablero.GetLength(0) && y >= 0 && y < tablero.GetLength(1);
    }
}