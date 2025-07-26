using System.ComponentModel.Design;
using System.Security.Cryptography.X509Certificates;

namespace Recorrido
{
    class Solution
    {
        public static int CantidadMinimaDeMedicos(int[,] area, int radio)
        {

            List<(int x, int y)> medicos = new List<(int x, int y)>();
            List<(int x, int y)> workes = new List<(int x, int y)>();

            int row = area.GetLength(0); int col = area.GetLength(1);
            for (int i = 0; i < area.GetLength(0); i++)
            {
                for (int j = 0; j < area.GetLength(1); j++)
                {
                    if (area[i, j] == 2) workes.Add((i, j));
                    else if (area[i, j] == 1) medicos.Add((i, j));
                }
            }
            int min = int.MaxValue;
            for (int k = 1; k <= medicos.Count; k++)
                Solve(area, radio, 0, medicos, workes, k, new List<(int, int)>(), ref min);

            return min == int.MaxValue ? -1 : min;
        }

        private static void Solve(int[,] area, int radio, int index ,List<(int, int)> medicos, List<(int, int)> pacientes, int k, List<(int, int)> current, ref int min)
        {
            if (current.Count == k)
            {
                if (CubreTodos(area, radio, current, pacientes))
                    min = Math.Min(min, k);
                return;
            }

            for (int i = index; i < medicos.Count; i++)
            {
                current.Add(medicos[i]);
                Solve(area, radio, i + 1, medicos, pacientes, k, current, ref min);
                current.RemoveAt(current.Count - 1);
            }
        }

        public static bool CubreTodos(int[,] area, int radio, List<(int, int)> current, List<(int, int)> pacientes)
        {
            foreach (var (px, py) in pacientes)
            {
                bool cubierto = false;
                foreach (var (x, y) in current)
                {
                    int nx = px - x;
                    int ny = py - y;
                    if (Math.Max(Math.Abs(nx), Math.Abs(ny)) <= radio) //distancia de manthattan
                    {
                        cubierto = true;
                        break;
                    }
                }
                if (!cubierto) return false;
            }
            return true;
        }
    }
}