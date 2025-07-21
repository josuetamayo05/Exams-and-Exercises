using System.ComponentModel;

public class Program
{
    static void Main()
    {
        bool[,] matrix = new bool[4, 4];
        matrix[0, 0] = true;matrix[0, 1] = true;
        matrix[1, 1] = true;matrix[0, 2] = true;
        matrix[0, 3] = true;
        int ans = Solution.Solve(matrix, 0, 0);
        Console.WriteLine(ans);
    }
}