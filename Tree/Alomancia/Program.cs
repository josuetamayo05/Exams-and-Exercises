class Program
{
    static void Main()
    {
        var rootA = new Metal("Fe")
        {
            Children = new List<IMetal>
            {
                new Metal("Sn"){
                    Children= new List<IMetal>
                    {
                        new Metal("Cu"),
                        new Metal("Pb")
                    }
                },
                new Metal("Zn")
            }
        };
        var rootB = new Metal("Br")
        {
            Children = new List<IMetal>
            {
                new Metal("Ag"),
                new Metal("Zn"){
                    Children=new List<IMetal>{
                        new Metal("Cu")
                    }
                }
            }
        };
        var lista = rootA.Flatten((m1, m2) => m2.Children.Count - m1.Children.Count);
        foreach (var child in lista)
        {
            Console.WriteLine(child.Name);
            
        }
        
        rootA.Difference(rootB);
        PrintTree(rootA);
    }
    static void PrintTree(IMetal node, int depth = 0) {
        Console.WriteLine(new string(' ', depth * 2) + node.Name);
        foreach (var child in node.Children) {
            PrintTree(child, depth + 1);
        }
    }
}