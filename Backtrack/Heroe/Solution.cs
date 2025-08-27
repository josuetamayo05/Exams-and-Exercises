using System.Collections.Immutable;
using System.Runtime.CompilerServices;

public class Solution
{
    public static IEnumerable<Tuple<int, int>> Resuelve(int[,] bonificaciones)
    {
        if (bonificaciones == null || bonificaciones.GetLength(1) != 3)
            return Enumerable.Empty<Tuple<int, int>>();
        int n = bonificaciones.GetLength(0);
        int m = bonificaciones.GetLength(1);
        List<Tuple<int, int>> tuples = new();
        List<Tuple<int, int>> combinaciones = new List<Tuple<int, int>>()
        {
            new Tuple<int, int>(0, 1),
                new Tuple<int, int>(0, 2),
                new Tuple<int, int>(1, 2)
        };
        List<Tuple<int, int>> actualComb = new();
        int mejorAcumulado = int.MinValue;
        Solve(bonificaciones, ref tuples, combinaciones, actualComb, ref mejorAcumulado, 0, 0, new int[m]);
        return tuples;
    }

    public static void Solve(int[,] bonif, ref List<Tuple<int, int>> mejorSeleccion, List<Tuple<int,int>> combinaciones, List<Tuple<int,int>> combAct, ref int mejorAcumulado, int actual, int acumuladoTotal, int[] acumuladoTemp)
    {
        if (actual == bonif.GetLength(0))
        {
            if (acumuladoTemp[0] == acumuladoTemp[1] && acumuladoTemp[1] == acumuladoTemp[2]) 
            {
                acumuladoTotal = acumuladoTemp[0];
                if (acumuladoTotal > mejorAcumulado)
                {
                    mejorAcumulado = acumuladoTotal;
                    mejorSeleccion = new List<Tuple<int, int>>(combAct);
                }
            }
            return;
        }

        foreach (var comb in combinaciones)
        {
            int c1 = comb.Item1;
            int c2 = comb.Item2;
            acumuladoTemp[c1] += bonif[actual, c1];
            acumuladoTemp[c2]+= bonif[actual, c2];
            combAct.Add(new Tuple<int, int>(c1, c2));
            if (acumuladoTemp.Min() >= mejorAcumulado)
            {
                Solve(bonif, ref mejorSeleccion, combinaciones, combAct, ref mejorAcumulado, actual + 1, acumuladoTotal, acumuladoTemp);

            }
            acumuladoTemp[c1] -= bonif[actual, c1];
            acumuladoTemp[c2] -= bonif[actual, c2];
            combAct.RemoveAt(combAct.Count - 1);
        }
    }
}