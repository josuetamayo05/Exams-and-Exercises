namespace croqueta;

public static class Utils
{
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
