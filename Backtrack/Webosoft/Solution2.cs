using System.Globalization;

namespace Weboo.Examen
{
    public class Manager2
    {
        public static double DuracionProyecto(int[] tareas, double[,] desarrolladores)
        {
            bool[] mask = new bool[tareas.Length];
            double ans = int.MaxValue;
            int[] arr = new int[tareas.Length];
            for (int i = 0; i < tareas.Length; i++)
            {
                arr[i] = i;
            }
            List<List<List<int>>> combinatios = Combinations(arr, desarrolladores.GetLength(0));
            double g = 0;
            foreach (var comb in combinatios)
            {
                double temp = 0;
                double current = 0;
                int trabajador = 0;
                bool flag = true;
                foreach (var list in comb)
                {
                    if (list.Count == 0)
                    {
                        trabajador++;
                        continue;
                    }
                    current = Math.Max(current, Calculate(list, tareas, desarrolladores, trabajador));
                    temp += Calculate(list, tareas, desarrolladores, trabajador);
                    if (temp >= ans)
                    {
                        flag = false;
                        current = 0;
                        temp = 0;
                        break;
                    }
                    trabajador++;
                }

                if (flag)
                {
                    ans = Math.Min(ans, temp);
                    g = current;
                }
            }
            return g;
        }
        private static double Calculate(List<int> ints, int[] tareas, double[,] desarr,int trabajador)
        {
            double cost = 0;
            for (int i = 0; i < ints.Count; i++)
            {
                cost += desarr[trabajador, ints[i]] * tareas[ints[i]];
            }
            return cost; 
        }

        public static List<List<List<int>>> Combinations(int[] workes, int n)
        {
            List<List<List<int>>> ans = new();
            List<List<int>> current = new();
            for (int i = 0; i < n; i++)
            {
                current.Add(new List<int>());
            }
            CombinationsAux(workes, ans, current, 0);
            return ans;
        }
        private static void CombinationsAux(int[] workes, List<List<List<int>>> ans, List<List<int>> current, int index) {
            if (index == workes.Length)
            {
                List<List<int>> cur = new();
                for (int i = 0; i < current.Count; i++)
                {
                    List<int> ints = new();
                    for (int j = 0; j < current[i].Count; j++)
                    {
                        ints.Add(current[i][j]);
                    }
                    cur.Add(ints);
                }
                ans.Add(cur);
                return;
            }
            for (int i = 0; i < current.Count; i++)
            {
                current[i].Add(workes[index]);
                CombinationsAux(workes, ans, current, index+1);
                current[i].RemoveAt(current[i].Count - 1);
            }
        }
        public static void Solve(int[] tareas, double[,] desarroll, int actual, bool[] mask, double temp, double ans, int count = 0)
        {
            if (count == tareas.Length)
            {
                if (ans < temp) ans = temp;
                return;
            }
            if (temp >= ans) return;
            for (int i = 0; i < tareas.Length; i++)
            {
                if (!mask[i])
                {
                    temp += desarroll[actual, i];
                    //Solve(tareas, desarroll,)
                }
            }
        } 
    }
}