public class QuadTreeNode
{
    public int X { get; set; }
    public int Y { get; set; }
    public int Ancho{ get; set; }
    public Color Color{ get; set; }
    public int Alto { get; set; }

    public QuadTreeNode[] Hijos { get; set; }
    public QuadTreeNode(int x, int y, int ancho, int alto, Color color) => (X, Y, Ancho, Alto, Hijos,Color) = (x, y, ancho, alto, null,color);
    
}