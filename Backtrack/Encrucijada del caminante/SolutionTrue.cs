using System.Globalization;
using System.Reflection.Metadata;

namespace Caminante
{
    public class Caminante
    {
        public static int CantidadCombinacionesCajas(int[] cajas, int alturaMax)
        {
            int count = 0;

            Solve(0, new List<int>(), cajas, alturaMax, ref count);

            return count;
        }
        private static void Solve(int index, List<int> camino,int[] cajas, int alturaMax,ref int count)
        {
            if (camino.Count >= 2)
                count++;

            for (int i = index; i < cajas.Length; i++)
            {
                if (camino.Count == 0 || Math.Abs(cajas[i] - camino[^1]) <= alturaMax)
                {
                    camino.Add(cajas[i]);
                    Solve(i + 1, camino, cajas, alturaMax, ref count);
                    camino.RemoveAt(camino.Count - 1);
                }
            }
        }
    }
}