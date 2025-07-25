using System.Runtime.InteropServices;

namespace HomeComming
{
    public static class Solution
    {
        public static double MinDanger(CityNode RootCity)
        {
            double count = int.MaxValue;
            bool[] mask = new bool[RootCity.Roads().Length];
            if (RootCity.Roads().Length == 0) return 0;
            return Solve(RootCity, 0, count, false);
        }

        private static double Solve(CityNode root, double actualDanger, double ans, bool road, int counter = 1)
        {
            if (actualDanger > ans) return actualDanger;

            var child = root.Roads();
            var tp = root.HasTeleport();
            if (root.Roads().Length == 0 && !tp.Item1) return actualDanger;

            if (tp.Item1 && road)
            {
                return ans = Math.Min(ans, Solve(tp.Item2, actualDanger,ans, false, counter + 1));
            }
            for (int i = 0; i < child.Length; i++)
            {
                ans = Math.Min(ans, Solve(child[i].Item2, actualDanger + (counter * child[i].Item1), ans, true, counter + 1));
            }
            return ans;
        }
    }
}