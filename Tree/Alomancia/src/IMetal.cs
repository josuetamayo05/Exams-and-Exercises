public interface IMetal
{
    public string Name { get; set; }
    public List<IMetal> Children { get; set; }

    public void AddChild(IMetal child);
    public void Union(IMetal other);
    public void Intersect(IMetal other);
    public void Difference(IMetal other);
    public void Filter(Func<IMetal, bool> predicate);
    public List<IMetal> Flatten(Func<IMetal, IMetal, int> comparison);
}