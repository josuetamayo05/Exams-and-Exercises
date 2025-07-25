// Recuerde no borrar nada de esta plantilla, a excepción de los comentarios.
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.Design.Serialization;

namespace croqueta;

public class Rankings : IRankings
{
    // Implemente esta clase. Recuerde descomentar la implementación de IRankings.
    public INode Root { get; private set; }
    public string Name { get; private set; }
    public int Tastiness{ get; private set; }
    public Rankings(INode root)
    {
        Root = root;
        Name = root.Name;
        Tastiness = root.Tastiness;
    }
    public static IRankings CreateRankings(INode root)
    {
        return new Rankings(root);
    }
    public void Remove(string name)
    {
        var node = Find(x => x.Name == name).FirstOrDefault();
        if (node == null) throw new Exception("Node not found");
        RemoveNode(node);
    }

    public void Insert(INode node)
    {
        // if (IsComplete(node))
        //     throw new Exception("can not into");

        INode intersectPoint = PointToIntersection(Root);

        if (intersectPoint.Left == null)
        {
            intersectPoint.Left = node;
            node.Parent = intersectPoint;
        }
        else if (intersectPoint.Right == null)
        {
            intersectPoint.Right = node;
            node.Parent = intersectPoint;
        }

        ActualizarArbol(Root);
    }
    private void ActualizarArbol(INode root)
    {
        if (root == null) return;

        ActualizarArbol(root.Left);
        ActualizarArbol(root.Right);

        Intercambiar(root);
    }
    public INode PointToIntersection(INode node)
    {
        if (node == null) return null;

        if ((node.Left == null && node.Right != null) || (node.Left != null && node.Right == null))
            return node;

        var leftNode = PointToIntersection(node.Left);

        if (leftNode != null)
            return leftNode;

        return PointToIntersection(node.Right);
    }
    public void Intercambiar(INode node)
    {
        while (true)
        {
            INode largest = node;
            if (largest.Left != null && largest.Right.Tastiness > largest.Tastiness)
            {
                largest = node.Right;
            }

            if (largest.Right != null && largest.Left.Tastiness > largest.Tastiness)
            {
                largest = node.Left;
            }

            if (largest != node)
            {
                Swap(node, largest);
            }

            else
                break;
        }
    }
    public void Swap(INode a, INode b)
    {
        string temp = a.Name;
        int tastiness = a.Tastiness;
        if (a is Node nodeA && b is Node nodeB)
        {
            nodeA.Name = nodeB.Name;
            nodeA.Tastiness = nodeB.Tastiness;
            nodeB.Name = temp;
            nodeB.Tastiness = tastiness;
        }
    }
    public bool IsComplete(INode root)
    {
        int level = CountNodes(root);
        int h = GetHeight(root);
        return level == (int)Math.Pow(2, h) - 1;
    }
    public int GetHeight(INode node)
    {
        if (node == null) return 0;
        int heightLeft = GetHeight(node.Left);
        int heightRight = GetHeight(node.Right);
        return 1+ Math.Max (heightLeft, heightRight);
    }
    public int CountNodes(INode root)
    {
        if (root == null) return 0;
        return 1 + CountNodes(root.Left) + CountNodes(root.Right);
    }
    public void RemoveNode(INode node)
    {
        if (node.Left == null && node.Right == null)
        {
            if (node.Parent == null)
            {
                Root = null;
            }
            else if (node.Parent.Left == node)
                node.Parent.Left = null;
            else
                node.Parent.Right = null;
        }
        if (node.Left != null && node.Right != null)
        {
            INode I = node.Bland;
            INode B = node.Tasty;
            if (node.Parent == null)
            {
                Root = B;
                B.Parent = null;
            }
            else
            {
                if (node.Parent.Left == node)
                    node.Parent.Left = B;
                else
                    node.Parent.Right = B;
            }
            INode current = B;
            INode I_current = null;
            while (true)
            {
                if (current.Left == null)
                {
                    current.Left = I;
                    break;
                }
                else if (current.Right == null)
                {
                    current.Right = I;
                    break;
                }
                else
                {
                    I_current = current.Bland;
                    if (current.Left == I_current)
                        current.Left = null;
                    else
                        current.Right = null;

                    if (current.Left == null)
                        current.Left = I;
                    else
                        current.Right = I;

                    I = I_current;
                    current = current.Tasty;
                }

            }
        }
        else
        {
            var nodeChild = node.Left ?? node.Right;
            if (node.Parent == null)
            {
                Root = nodeChild;
                nodeChild.Parent = null;
            }
            else
            {
                if (node.Parent.Left == node)
                    node.Parent.Left = nodeChild;
                else
                    node.Parent.Right = nodeChild;

                nodeChild.Parent = node.Parent;
            }

        }
    }
    public IEnumerable<INode> Find(Func<INode, bool> query)
    {
        return Trasverse(Root).Where(query);
    }
    public IEnumerable<INode> Trasverse(INode node)
    {
        if (node == null) yield break;
        yield return node;
        foreach (var left in Trasverse(node.Left))
        {
            yield return left;
        }
        foreach (var right in Trasverse(node.Right))
        {
            yield return right;
        }
    }
}

public class Node : INode
{
    public string Name { get; set; }
    public int Tastiness { get; set; }
    public INode? Parent { get; set; }
    public INode? Left { get; set; }
    public INode? Right { get; set; }

    public Node(string name, int tastiness)
    {
        Name = name;
        Tastiness = tastiness;
        Parent = null;
        Left = null;
        Right = null;
    }
    public INode Tasty
    {
        get
        {
            if (Left == null || Right == null)
            {
                throw new Exception("No puede compararse si no tienen hijos");
            }
            else
            {
                return Left.Tastiness > Right.Tastiness ? Left : Right;
            }
        }
    }
    public INode Bland
    {
        get
        {
            if (Left == null || Right == null)
                throw new Exception("No pueden compararse si no tienen hijos");
            return Left.Tastiness > Right.Tastiness ? Right : Left;
        }
    }

    public void SetLeft(INode left)
    {
        Left = left;
        if (left != null) left.Parent = this;
    }
    public void SetRight(INode right)
    {
        Right = right;
        if (right != null) right.Parent = this;
    }
}