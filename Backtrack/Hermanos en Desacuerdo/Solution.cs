using System.Runtime.CompilerServices;

namespace Weboo.Examen.Extra
{
    public class Padre
    {
        public static void ResuelveVacaciones(int[,] ciudades, out int[] viaje1, out int[] viaje2)
        {
            List<int> temp1 = new List<int>();
            List<int> temp2 = new List<int>();
            bool[] mask = new bool[ciudades.GetLength(0)];
            bool[] mask2 = new bool[ciudades.GetLength(0)];
            int[] resp1 = new int[ciudades.GetLength(0)]; int[] resp2 = new int[ciudades.GetLength(0)];
            Solve(ciudades, ref temp1, ref temp2, 0, 0, 0, mask, mask2, ref resp1, ref resp2);
            viaje1 = resp1;
            viaje2 = resp2;
        }

        static int min = int.MaxValue;

        private static void Solve(int[,] ciudades, ref List<int> viaje1, ref List<int> viaje2, int ciudadActual1, int ciudadActual2, int cost, bool[] mask, bool[] maskTemp, ref int[] arr1, ref int[] arr2)
        {
            if (viaje1.Count == ciudades.GetLength(0) && viaje2.Count == ciudades.GetLength(0))
            {
                if (min > cost)
                {
                    min = cost;
                    arr1 = viaje1.ToArray();
                    arr2 = viaje2.ToArray();
                }
                return;
            }
            if (cost >= min)
                return;
            for (int i = 0; i < ciudades.GetLength(0); i++)
            {
                if (ciudadActual1 == i || mask[i]) continue;

                for (int j = 0; j < ciudades.GetLength(0); j++)
                {
                    if (ciudadActual2 == j || maskTemp[j]) continue;
                    {
                        if (i == j && (i != 0 || j != 0)) continue;
                        mask[i] = true;
                        maskTemp[j] = true;
                        viaje1.Add(i);
                        viaje2.Add(j);
                        cost += ciudades[ciudadActual1, i] + ciudades[ciudadActual2, j];

                        Solve(ciudades, ref viaje1, ref viaje2, i, j, cost, mask, maskTemp,ref arr1, ref arr2);
                        cost -= ciudades[ciudadActual1, i] + ciudades[ciudadActual2, j];
                        viaje1.RemoveAt(viaje1.Count - 1);
                        viaje2.RemoveAt(viaje2.Count - 1);
                        mask[i] = false;
                        maskTemp[j] = false;
                    }
                }
            }
        }
       
    }
}