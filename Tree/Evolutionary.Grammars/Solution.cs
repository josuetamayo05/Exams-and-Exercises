using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MatCom.Examen;

public static class Solution
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

	private static Tree Cruzamiento(Tree node, Tree tree)
	{
		string newSymbol = node.Symbol + tree.Symbol;
		List<Tree> children = new();
		int n = Math.Max(node.Children.Count, tree.Children.Count);
		for (int i = 0; i < n; i++)
		{
			if (i < node.Children.Count && i < tree.Children.Count)
			{
				children.Add(Cruzamiento(node.Children[i], tree.Children[i]));
			}
			else if (i < node.Children.Count)
			{
				children.Add(CloneTree(node.Children[i]));
			}
			else children.Add(CloneTree(tree.Children[i]));
		}
		return new Tree(newSymbol, children);
	}

	private static Tree Mutate(Tree node, Production[] productions, Func<int> sampler)
	{
		Tree clone = CloneTree(node);
		MutateNode(clone, productions, sampler);
		return clone;
	}
	private static void MutateNode(Tree node, Production[] productions, Func<int> sampler)
	{
		foreach (var child in node.Children)
		{
			MutateNode(child, productions, sampler);
		}

		var candidates = productions.Where(x => x.Head == node.Symbol).ToList();
		if (candidates.Count > 0)
		{
			int index = sampler() % candidates.Count;
			var selected = candidates[index];
			foreach (char c in selected.Body)
			{
				node.Children.Add(new Tree(c.ToString()));
			}
		}
	}
	private static Tree CloneTree(Tree start) {
		return new Tree(start.Symbol, start.Children.Select(CloneTree).ToList());
	}
	
	
}