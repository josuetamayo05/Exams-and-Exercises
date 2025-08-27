using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using MatCom.Examen;

public class Solution2
{
    public static Tree DeriveFromGrammar(Tree start, int iterations, Production[] productions, Func<int> sampler)
    {
        if (iterations == 0) return start;
        Tree current = CloneTree(start);
        for (int i = 0; i < iterations; i++)
        {
            Tree mutated = Mutate(current, productions, sampler);
            current = Cruzamiento(current, mutated);
        }
        return current;
    }
    public static Tree Cruzamiento(Tree node, Tree actual)
    {
        string current = node.Symbol + actual.Symbol;
        List<Tree> children = new();
        int n = Math.Max(node.Children.Count, actual.Children.Count);
        for (int i = 0; i < n; i++)
        {
            if (i < node.Children.Count && i < actual.Children.Count)
            {
                children.Add(Cruzamiento(node.Children[i], actual.Children[i]));
            }
            else if (i < node.Children.Count)
            {
                children.Add(CloneTree(node.Children[i]));
            }
            else
            {
                children.Add(CloneTree(actual.Children[i]));
            }
        }
        return new Tree(current,children);
    }
    public static Tree CloneTree(Tree tree)
    {
        return new Tree(tree.Symbol, tree.Children.Select(CloneTree).ToList());
    }

    public static Tree Mutate(Tree node, Production[] productions, Func<int> sampler)
    {
        Tree mutated = CloneTree(node);
        MutateAux(mutated, productions, sampler);
        return mutated;
    }
    public static void MutateAux(Tree mutated, Production[] productions, Func<int> sampler)
    {
        foreach (var child in mutated.Children)
        {
            MutateAux(child, productions, sampler);
        }
        var candidates = productions.Where(x => x.Head == mutated.Symbol).ToList();
        if (candidates.Count > 0)
        {
            int count = candidates.Count();
            int index = sampler() % count;
            var selected = candidates[index];

            foreach (char c in selected.Body)
            {
                mutated.Children.Add(new Tree(c.ToString()));
            }
        }
    }
}