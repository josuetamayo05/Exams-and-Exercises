public class Solution2
{
    public static int CombustibleDiario(int[] pesos, int[,] combustible)
    {
        int min = int.MaxValue;
        int n = combustible.GetLength(0) - 1;
        int[] array = new int[n];
        for (int i = 0; i < combustible.GetLength(0)-1; i++)
        {
            array[i] = i + 1;
        }
        List<List<List<int>>> combinations = Combinations(array, combustible.GetLength(0) - 1);
        int index = 0;
        foreach (var listGrande in combinations)
        {
            bool flag = true;
            int acumulado = 0;
            int temp = 0;
            foreach (List<int> listaMediana in listGrande)
            {
                if (listaMediana.Count == 0) continue;
                acumulado = CalculateCosto(listaMediana, pesos, combustible);
                temp += acumulado;
                if (acumulado == 0)
                {
                    flag = false;
                    break;
                }
            }
            if (flag)
            {
                if (temp < min) min = temp;
            }
            index++;
        }

        return min == int.MaxValue ? -1 : min;
    }

    static int CalculateCosto(List<int> current, int[] pesos, int[,] combustible)
    {
        int cost = 0;
        int temp = 0;
        int actual = 0;
        for (int i = 0; i < current.Count; i++)
        {
            if (temp == current[i]) continue;
            actual += pesos[current[i]];
            cost += combustible[temp, current[i]];
            if (actual > pesos[0] || actual > pesos[current[i]]) return 0;
            temp = current[i];
        }
        cost += combustible[temp, 0];
        actual = combustible[temp, 0];
        if (cost > pesos[0] || actual > pesos[0]) return 0;

        return cost;
    }
    static List<List<List<int>>> Combinations(int[] array, int n)
    {
        List<List<List<int>>> combinations = new();
        List<List<int>> current = new();
        for (int i = 0; i < array.Length; i++)
        {
            current.Add(new List<int>());
        }
        Combinations(array, combinations, current, 0);
        return combinations;
    }
    static void Combinations(int[] array, List<List<List<int>>> combinations, List<List<int>> current, int index)
    {
        if (index == array.Length)
        {
            List<List<int>> mapeo = new();
            for (int i = 0; i < current.Count; i++)
            {
                List<int> mapeoMenor = [];
                for (int j = 0; j < current[i].Count; j++)
                {
                    mapeoMenor.Add(current[i][j]);
                }
                mapeo.Add(mapeoMenor);
            }
            combinations.Add(mapeo);
            return;
        }
        for (int i = 0; i < current.Count; i++)
        {
            current[i].Add(array[index]);
            Combinations(array, combinations, current, index + 1);
            current[i].RemoveAt(current[i].Count - 1);
        }
    }
}
