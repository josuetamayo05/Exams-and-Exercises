using System.Formats.Asn1;
using System.Net.Mail;
using System.Runtime.CompilerServices;
using Microsoft.VisualBasic;

public class Metal : IMetal
{
    public string Name { get; set; }
    public List<IMetal> Children { get; set; } = new List<IMetal>();
    public Metal(string name)
    {
        Name = name;
    }

    public void Union(IMetal other)
    {
        if (other == null) return;
        Dictionary<string, IMetal> map = new();
        for (int i = 0; i < Children.Count; i++)
        {
            map[Children[i].Name] = Children[i];
        }
        foreach (var otro in other.Children)
        {
            var temp = this;
            if (!map.ContainsKey(otro.Name))
                Children.Add(otro);
            else
            {
                temp = (map[otro.Name] as Metal);
            }
            temp.Union(otro);
        }
    }
    public void Intersect(IMetal other)
    {
        if (other is null) return;
        Dictionary<string, IMetal> map = new();
        List<IMetal> current = new();

        for (int i = 0; i < other.Children.Count; i++)
        {
            map[other.Children[i].Name] = other.Children[i];
        }

        foreach (var child in Children)
        {
            var temp = this;

            if (map.ContainsKey(child.Name))
            {
                current.Add(child);
                temp = (child as Metal);
                temp.Intersect(map[child.Name]);
            }
        }
        Children = current;
    }
    public void Difference(IMetal other)
    {
        if (other is null) return;
        Dictionary<string, IMetal> map = new();
        List<IMetal> metals = GetMetals(other);
        for (int i = 0; i < metals.Count; i++)
        {
            map[metals[i].Name] = metals[i];
        }
        List<IMetal> current = new();

        foreach (var child in Children)
        {
            var temp = this;
            if (!map.ContainsKey(child.Name))
            {
                current.Add(child);
                temp = (child as Metal);
                temp.Difference(other);
            }
        }
        Children = current;
    }
    public List<IMetal> GetMetals(IMetal other)
    {
        List<IMetal> ans = new();
        GetMetalsRecursive(other, ans);
        return ans;
    }

    public void GetMetalsRecursive(IMetal other, List<IMetal> ans)
    {
        if (other == null) return;
        ans.Add(other);
        foreach (var child in other.Children)
        {
            GetMetalsRecursive(child, ans);
        }
    }

    public void AddChild(IMetal child)
    {
        Children.Add(child);
    }

    public void Filter(Func<IMetal, bool> predicate)
    {
        FilterAux(predicate, this);
    }
    public void FilterAux(Func<IMetal, bool> predicate, IMetal node)
    {
        if (!predicate(node))
        {
            node.Children = null;
            node = null;
        }
        foreach (var child in node.Children)
        {
            FilterAux(predicate, child);
        }
    }
    public List<IMetal> Flatten(Func<IMetal, IMetal, int> comparison)
    {
        List<IMetal> list = new();
        Dfs(this, list);

        var ordenOriginal = list.Select((metal, idx) => (metal, idx)).ToDictionary(x => x.metal, x => x.idx); //guardar orden original
        list.Sort((m1, m2) =>
        {
            int cmp = comparison(m1, m2);
            if (cmp != 0) return cmp;
            // Si son "iguales" según el criterio, usar orden original DFS
            return ordenOriginal[m1].CompareTo(ordenOriginal[m2]);
        });
        return list;
    }
    public void Dfs(IMetal metal, List<IMetal> list)
    {
        list.Add(metal);
        foreach (var child in metal.Children)
        {
            Dfs(child, list);
        }
    }
}