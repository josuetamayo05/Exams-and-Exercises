using System.Security.Cryptography.X509Certificates;

class Solution
{
    public static int MayorCantidadDePersonas(int[,] personasPorCuadra, bool[,] talleres)
    {
        int n = talleres.GetLength(0); int m = talleres.GetLength(1);
        bool[,] mask = new bool[n, m];
        mask[0, 0] = true;
        List<(int,int)> prueba = new List<(int,int)>();
        max = 0;
        Solve(personasPorCuadra, talleres, 0, 0, n-1, m-1, personasPorCuadra[0, 0], 0, mask, prueba);
        foreach (var (w, e) in prueba)
        {
            System.Console.WriteLine($"{w},{e}");
            System.Console.WriteLine();
        }
        return max == int.MaxValue ? -1 : max;
    }
    static int[] dx = { 0, 1 };
    static int[] dy = { 1, 0 };
    static int max = 0;
    private static void Solve(int[,] personasCuadra, bool[,] talleres, int x, int y, int destino_x, int destino_y, int personas, int cuadras, bool[,] mask, List<(int, int)> prueba)
    {
        if (x == destino_x && y == destino_y)
        {
            max = Math.Max(max, personas - personasCuadra[destino_x, destino_y]);
            return;

        }

        if (cuadras > 10)
            return;
        for (int i = 0; i < 2; i++)
        {
            int nx = x + dx[i]; int ny = y + dy[i];
            if (Check(talleres, nx, ny) && !mask[nx, ny])
            {
                mask[nx, ny] = true;
                personas += personasCuadra[nx, ny];
                if (talleres[nx, ny])
                {
                    cuadras = 0;
                }
                prueba.Add((nx, ny));
                Solve(personasCuadra, talleres, nx, ny, destino_x, destino_y, personas, cuadras + 1, mask, prueba);
                prueba.RemoveAt(prueba.Count - 1);
                personas -= personasCuadra[nx, ny];
                mask[nx, ny] = false;
            }
        }
    }
    private static bool Check(bool[,] map, int nx, int ny) => nx >= 0 && nx < map.GetLength(0) && ny >= 0 && ny < map.GetLength(1);
}
