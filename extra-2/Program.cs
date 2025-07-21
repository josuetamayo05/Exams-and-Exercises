using System.Diagnostics;
using croqueta;

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
Console.WriteLine(Utils.TreeRepresentation(rankings.Root));

rankings.Remove(root.Name);
Console.WriteLine(Utils.TreeRepresentation(rankings.Root));
Debug.Assert(rankings.Root.Name == a.Name);
Debug.Assert(rankings.Root.Tasty.Name == b.Name);
Debug.Assert(rankings.Root.Bland.Name == ab.Name);


Node friedBacon = new Node("bacon frito", 150);
rankings.Insert(friedBacon);
Console.WriteLine(Utils.TreeRepresentation(rankings.Root));
Debug.Assert(rankings.Root.Name == friedBacon.Name);
Debug.Assert(rankings.Root.Tasty.Name == a.Name);
Debug.Assert(rankings.Root.Bland.Name == b.Name);