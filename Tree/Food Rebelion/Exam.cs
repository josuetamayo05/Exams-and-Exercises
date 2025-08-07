using System.ComponentModel;
using System.Reflection.Metadata.Ecma335;
using FoodRebellion;


namespace Exam
{
    public class Rankings : IRankings
    {
        public INode Root { get; set; }

        public Rankings(INode root)
        {
            Root = root;
        }
        public static IRankings CreateRankings(INode root)
        {
            return new Rankings(root);
        }

        public void Remove(string name)
        {
            var node = Find(x => x.Name == name);

            RemoveAux(name, node);
        }
        private void RemoveAux(string name, INode node)
        {
            if (node.Left != null && node.Right != null)
            {
                INode act = node.Tasty;
                INode mal = node.Bland;
                if (node.Parent == null)
                {
                    Root = act;
                    act.Parent = null;
                }
                else
                {
                    if (node.Parent.Left == node)
                        node.Parent.Left = act;
                    else node.Parent.Right = act;
                }
                INode current = act;
                INode bland_current = null;
                while (true)
                {
                    if (current.Left == null)
                    {
                        current.Left = mal;
                        break;
                    }
                    else if (current.Right == null)
                    {
                        current.Right = mal;
                        break;
                    }
                    else
                    {
                        bland_current = current.Bland;
                        if (current.Left == bland_current)
                            current.Left = null;
                        else if (current.Right == bland_current)
                            current.Right = null;
                        if (current.Left == null)
                            current.Left = mal;
                        else current.Right = mal;
                        mal = bland_current;
                        current = current.Tasty;
                    }
                }
            }
        }
        public void Insert(INode node)
        {
            var vacio = Find(node => (node.Left is null && node.Right is not null)||(node.Left is not null && node.Right is null));
            node.Parent = vacio;
            if (vacio.Left is null)
                vacio.Left = node;
            else if (vacio.Right is null)
                vacio.Right = node;
            ActualizarArbol(Root);
        }

        private void ActualizarArbol(INode node)
        {
            if (node is null) return;
            ActualizarArbol(node.Left);
            ActualizarArbol(node.Right);
            Intercambiar(node);
        }

        private void Intercambiar(INode node)
        {
            while (true)
            {
                INode current = node;
                if (current.Right != null && current.Left.Tastiness > current.Tastiness)
                {
                    current = node.Left;
                }
                if (current.Left != null && current.Right.Tastiness > current.Tastiness)
                {
                    current = node.Right;
                }
                if (current != node)
                {
                    Swap(current, node);
                }
                else break;
            }
        }
        private void Swap(INode node1, INode node2)
        {
            string current = node1.Name;
            int tastCurrent = node1.Tastiness;
            if (node1 is Node nod1 && node2 is Node nod2)
            {
                nod1.Name = node2.Name;
                nod1.Tastiness = nod2.Tastiness;
                nod2.Name = current;
                nod2.Tastiness = tastCurrent;
            }
        }
        private void InsertNode(INode inserted, INode actualNode)
        {
            if (actualNode is null)
            {
                Root = inserted;
                return;
            }
            if (inserted.Tastiness > actualNode.Tastiness)
            {
                inserted.Parent = actualNode.Parent;
                actualNode.Parent = inserted;
                if (HasParent(inserted) && inserted.Parent.Left is not null && inserted.Parent.Left == inserted)
                    inserted.Parent.Left = inserted;
                if (HasParent(inserted) && inserted.Parent.Right is not null && inserted.Parent.Right == inserted)
                    inserted.Parent.Right = inserted;
                var L = inserted.Left;
                var R = inserted.Right;
                if (actualNode.Left != inserted)
                {
                    inserted.Left = actualNode.Left;
                    inserted.Left.Parent = inserted;
                    inserted.Right = actualNode;
                }
                if (actualNode.Right != inserted)
                {
                    inserted.Right = actualNode.Right;
                    inserted.Left.Parent = inserted;
                    inserted.Right = actualNode;
                }
                actualNode.Left = L;
                actualNode.Right = R;
                if (actualNode.Left is not null) actualNode.Left.Parent = actualNode;
                if (actualNode.Right is not null) actualNode.Right.Parent = actualNode;
                InsertNode(inserted, inserted.Parent);
            }
        }
        private bool HasParent(INode node) => node.Parent is not null;
       
        public IEnumerable<INode> GetByLevel(int level)
        {
            if (level <= 0) throw new Exception();
            List<INode> temp = new List<INode>();
            GetByLevelAux(level, Root, 1, temp);
            return temp;
        }
        private void GetByLevelAux(int level, INode root, int nivAct,List<INode> result)
        {
            if (nivAct == level)
            {
                result.Add(root);
                return;
            }
            if (nivAct < level)
            {
                GetByLevelAux(level, root.Left!, nivAct + 1,result);
                GetByLevelAux(level, root.Right!, nivAct + 1,result);
            }
        }
        public INode Find(Func<INode, bool> query)
        {
            return FindX(Root, query);
        }
        public INode FindX(INode? node, Func<INode, bool> query)
        {
            if (node == null) return null;
            if (query(node)) return node;
            var left = FindX(node.Left, query); if (left != null) return left;
            return FindX(node.Right, query);
        }
    }
    public class Node : INode
    {
        public string Name { get; set; }
        public int Tastiness { get; set; }
        public INode Left { get; set; }
        public INode Right { get; set; }
        public INode Parent { get; set; }

        public Node(string name, int tastiness, INode left = null, INode right = null)
        {
            Name = name;
            Tastiness = tastiness;
            Right = right;
            Left = left;
        }

        public void SetLeft(INode child)
        {
            Left = child;
            if (child != null) child.Parent = this;
        }
        public void SetRight(INode child)
        {
            this.Right = child;
            if (child != null) child.Parent = this;
        }

        public INode Tasty
        {
            get
            {
                return Left.Tastiness > Right.Tastiness ? Left : Right;
            }
        }

        public INode Bland
        {
            get
            {
                return Left.Tastiness < Right.Tastiness ? Left : Right;
            }
        }

    }
}