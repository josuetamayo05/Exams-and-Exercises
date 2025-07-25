namespace croqueta;

public interface IRankings
{
    public INode Root { get; }

    public void Remove(string name);

    public void Insert(INode node);

    public IEnumerable<INode> Find(Func<INode, bool> query);
}