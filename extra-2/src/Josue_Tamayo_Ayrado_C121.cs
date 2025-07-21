// Recuerde no borrar nada de esta plantilla, a excepción de los comentarios.
using System.ComponentModel.Design.Serialization;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;

namespace croqueta;

public class Rankings : IRankings
{
    // Implemente esta clase. Recuerde descomentar la implementación de IRankings.
    INode root;
    INode IRankings.Root => root;

    public Rankings(INode root)
    {
        this.root = root;
    }
    public static IRankings CreateRankings(INode root)
    {
        return new Rankings(root);
    }
    public IEnumerable<INode> Find(Func<INode, bool> query)
    {
        List<INode> evaluateChild = new List<INode>();
        if (root is Node node)
        {
            if (query == null) yield return null;
            foreach (var child in node.Children)
            {
                if (query(child))
                {
                    yield return child;
                }
            }
        }
    }
    public void Remove(string name)
    {
        if (root is Node node)
        {
            if (root == null) return;
            Dictionary<string, int> map = new Dictionary<string, int>();
            List<INode> aux = new List<INode>();
            List<INode> temp = new List<INode>();

            if (node.Name == name && node.Children.Count == 0)
            {
                return;
            }
            if (node.Left.Name == name)
            {

            }
            else if (node.Name == name)
            {
                if (node.Children.Count != 0 && node.Children.Count == 2)
                {
                    for (int i = 0; i < node.Children.Count; i++)
                    {
                        for (int j = i + 1; j < node.Children.Count; j++)
                        {
                            var a = node.Children[i];
                            var b = node.Children[j];
                            if (a.Tastiness > b.Tastiness)
                            {
                                temp.Add(a);
                            }
                        }
                    }
                    // aux.Add(node.Children);
                }
            }
            else
            {
                Remove(node.Left.Name);
                Remove(node.Right.Name);
            }
            node.Children = temp;

        }

    }
    public void Insert(INode node)
    {
        int[] temp = new int[2];
        if (root is Node node1)
        {
            int count = 0;
            for (int i = 0; i < node1.Children.Count; i++)
            {
                count++;
            }
            if (count != 2)
            {
                
            }
        }
    }
}

public class Node : INode
{
    public string Name { get; set; }
    public int Tastiness { get; set; }
    public INode Left { get; set; }
    public INode Right { get; set; }
    public List<INode> ChildLeft { get; set; }
    public List<INode> ChildRight { get; set; }
    public INode Parent { get; set; }
    public INode Tasty { get; set; }
    public INode Bland { get; set; }
    public List<INode> Children{ get; set; }

    public Node(string name, int tastiness,INode left=null,INode right=null)
    {
        Name = name;
        Tastiness = tastiness;
        ChildLeft = new List<INode>();
        ChildRight = new List<INode>();
        Left = left;
        Right = right;
        Children = new List<INode>();
    }


   
    public void SetLeft(INode other)
    {
        Left = other;
        Children.Add(other);
        ChildRight.Add(other);
    }
    public void SetRight(INode other)
    {
        Right = other;
        Children.Add(other);
        ChildRight.Add(other);
    }
    
}