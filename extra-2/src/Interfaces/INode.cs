namespace croqueta;

public interface INode
{
    public string Name { get; }
    public int Tastiness { get; }

    public INode? Parent { get; set; }
    public INode? Left { get; set; }
    public INode? Right { get; set; }

    public INode Tasty { get; }
    public INode Bland { get; }
}