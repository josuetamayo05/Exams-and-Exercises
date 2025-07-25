namespace Weboo.Examen
{
    public class Pelotas
    {
        public static int EnlazaOptimo(int[] pelotas)
        {
            int cost = int.MaxValue;
            bool[] mask = new bool[pelotas.Length];
            mask[0] = true;
            List<int> temp = new List<int>();
            temp.Add(pelotas[0]);
            Solve(pelotas, 0, ref cost, mask, temp);
            return cost;
        }
        private static void Solve(int[] pelotas, int index,ref int cost,bool[] mask,List<int> temp ,int counter=0)
        {
            if (temp.Count == pelotas.Length)
            {
                if (counter < cost)
                { cost = counter; return; }
            }
            if (counter >= cost) return;
            for (int i = index; i < pelotas.Length; i++)
            {

                if (pelotas[index] == temp[^1])
                {
                    temp.Add(pelotas[index]);
                    Solve(pelotas, index + 1, ref cost, mask, temp, counter);
                    temp.RemoveAt(temp.Count - 1);
                }
                if (pelotas[index] != temp[^1])
                {
                    temp.Add(pelotas[i]);
                    Solve(pelotas, index + 1, ref cost, mask, temp, counter + 1);
                    temp.RemoveAt(temp.Count - 1);
                }
            }
        }
    }
}