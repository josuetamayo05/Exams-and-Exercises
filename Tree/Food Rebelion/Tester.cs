using FoodRebellion;
using System.Diagnostics;
using Exam;


class Program
{

    public static void Main()
    {
        // Se inicializan los nodos del arbol
        Node root = new Node("bacon", 100);
        Node a = new Node("queso", 80);
        Node b = new Node("leche", 60);

        Node aa = new Node("brocolí", 1);
        Node ab = new Node("hígado", 2);

        Node ba = new Node("mango", 50);
        Node bb = new Node("tomate", 45);

        // Se conforma el arbol
        root.SetLeft(a);
        root.SetRight(b);
        a.SetLeft(aa);
        a.SetRight(ab);
        b.SetLeft(ba);
        b.SetRight(bb);

        IRankings rankings = Rankings.CreateRankings(root);
        Console.WriteLine(TreeRepresentation(rankings.Root));
        System.Console.WriteLine();
        System.Console.WriteLine("Remove");
        rankings.Remove(root.Name);
        Console.WriteLine(TreeRepresentation(rankings.Root));
        System.Console.WriteLine();
        Debug.Assert(rankings.Root.Name == a.Name);
        Debug.Assert(rankings.Root.Tasty.Name == b.Name);
        Debug.Assert(rankings.Root.Bland.Name == ab.Name);


        Node friedBacon = new Node("bacon frito", 150);
        System.Console.WriteLine("Insert");
        rankings.Insert(friedBacon);
        Console.WriteLine(TreeRepresentation(rankings.Root));
        System.Console.WriteLine();
        // Debug.Assert(rankings.Root.Name == friedBacon.Name);
        // Debug.Assert(rankings.Root.Tasty.Name == a.Name);
        // Debug.Assert(rankings.Root.Bland.Name == b.Name);

        INode brocoli = rankings.Find((node) => node.Name == aa.Name);
        Debug.Assert(brocoli.Parent.Name == a.Name);

        IEnumerable<INode> lvl2Food = rankings.GetByLevel(2);
        Debug.Assert(lvl2Food.Count() == 4);
        Debug.Assert(lvl2Food.Contains(aa));
        Debug.Assert(lvl2Food.Contains(ab));
        Debug.Assert(lvl2Food.Contains(ba));
        Debug.Assert(lvl2Food.Contains(bb));

        rankings.Remove(aa.Name);
        Console.WriteLine(TreeRepresentation(rankings.Root));
        System.Console.WriteLine();
        lvl2Food = rankings.GetByLevel(2);
        Debug.Assert(lvl2Food.Count() == 3);
        Debug.Assert(lvl2Food.Contains(ab));
        Debug.Assert(lvl2Food.Contains(ba));
        Debug.Assert(lvl2Food.Contains(bb));

        rankings.Insert(aa);
        Console.WriteLine(TreeRepresentation(rankings.Root));
        System.Console.WriteLine();
        lvl2Food = rankings.GetByLevel(2);
        Debug.Assert(lvl2Food.Count() == 4);
        Debug.Assert(lvl2Food.Contains(aa));
        Debug.Assert(lvl2Food.Contains(ab));
        Debug.Assert(lvl2Food.Contains(ba));
        Debug.Assert(lvl2Food.Contains(bb));

        rankings.Remove(a.Name);
        Console.WriteLine(TreeRepresentation(rankings.Root));
        System.Console.WriteLine();

        Node pizza = new Node("Pizza Hawaiana", 120);
        rankings.Insert(pizza);
        Console.WriteLine(TreeRepresentation(rankings.Root));
        System.Console.WriteLine();
        Debug.Assert(rankings.Root.Tasty.Name == pizza.Name);
    }

    // Metodo para imprimir un arbol
    public static string TreeRepresentation(INode node, int tabspace = 1)
    {
        if (node.Left is null && node.Right is null)
            return $"{node.Name}-{node.Tastiness}";

        string leftMember = node.Left is null ?
            "Null" : TreeRepresentation(node.Left, tabspace + 1);
        string rightMember = node.Right is null ?
            "Null" : TreeRepresentation(node.Right, tabspace + 1);

        string tabs = new string('\t', tabspace);
        return $"{node.Name}-{node.Tastiness}:\n{tabs}L: {leftMember}\n{tabs}R: {rightMember}";
    }
}
