namespace hormigas
{
    public class Solution
    {
        static int[] dx = { 0, 0, 1, -1 };
        static int[] dy = { 1, -1, 0, 0 };
        public static int[] Solve(int[,] colony, int threshold)
        {
            List<int> temp = new List<int>();
            bool[,] mask = new bool[colony.GetLength(0), colony.GetLength(1)];
            if (Solve(colony, threshold, 0, 0, ref temp, mask))
            {
                int[] ans = new int[temp.Count * 2 -1];
                ans[0] = 0;
                for (int i = 1; i < temp.Count; i++)
                {
                    ans[i] = temp[i];
                }
                int[] arrTemp = new int[colony.Length - 1];
                for (int i = temp.Count-1; i >= 0; i--)
                {
                    arrTemp[temp.Count - i-1] = temp[i];
                }
                for (int i = 0; i < ans.Length/2; i++)
                {
                    ans[i + ans.Length / 2] = arrTemp[i];
                }
                return ans;
            }
            return null;
        }
        private static bool Solve(int[,] colony, int total, int x, int y, ref List<int> temp, bool[,] mask, int counter = 0)
        {
            if (counter > total) return false;
            if (counter == total) return true;
            for (int i = 0; i < 4; i++)
            {
                int nx = x + dx[i]; int ny = y + dy[i];
                if (Check(colony, nx, ny) && !mask[nx, ny] && colony[nx, ny] >= 0)
                {
                    temp.Add((nx * colony.GetLength(1)) + ny);
                    mask[nx, ny] = true;
                    if (colony[nx, ny] > 0)
                        counter += colony[nx, ny];
                    if (Solve(colony, total, nx, ny, ref temp, mask, counter)) return true;
                    mask[nx, ny] = false;
                    temp.RemoveAt(temp.Count - 1);
                }
            }
            return false;
        }

        private static bool Check(int[,] colony, int x, int y) => x >= 0 && x < colony.GetLength(0) && y >= 0 && y < colony.GetLength(1);
    }
}