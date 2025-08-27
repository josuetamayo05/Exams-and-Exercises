using System.ComponentModel;
using System.Data.Common;
public static class Examen    //implementacion tipo TSM
{
    
    public static int MinEstudiantesAvisar(bool[,] amigos, int k)
    {
        if (k == 0) return amigos.GetLength(0);
        int combinaciones = 1;
        while (combinaciones < amigos.GetLength(0) && !Solve(amigos, new bool[amigos.GetLength(0)], combinaciones, k))
        {
            combinaciones++;
        }
        return combinaciones < amigos.GetLength(0) ? combinaciones : amigos.GetLength(0);
    }

    public static bool Solve(bool[,] amigos, bool[] visitados, int aux, int k)
    {
        if (aux == 0) return VisitarTodos(amigos, visitados, k);
        for (int i = 0; i < visitados.Length; i++)
        {
            if (!visitados[i])
            {
                visitados[i] = true;
                if (Solve(amigos, visitados, aux - 1, k)) return true;
                visitados[i] = false;
            }
        }
        return false;
    }
    public static bool VisitarTodos(bool[,] amigos,  bool[] visitados, int k)
    {
        if (k == 0) return visitados.All(x => x);
        bool[] aux = (bool[])visitados.Clone();
        for (int i = 0; i < amigos.GetLength(0); i++)
        {
            if (visitados[i])
            {
                for (int j = 0; j < amigos.GetLength(1); j++)
                {
                    if (amigos[i, j]) aux[j] = true;
                }
            }
        }
        return VisitarTodos(amigos,  aux, k - 1);
    }
    static bool[] CloneMask(bool[]mask)
    {
        List<bool> a = [.. mask];
        return [.. a];
    }
    
}




