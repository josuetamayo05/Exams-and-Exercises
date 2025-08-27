public class Lienzo : ILienzo
{
    public int size;
    public QuadTreeNode root;
    public Lienzo(int size)
    {
        this.size = size;
        root = new QuadTreeNode(0, 0, size, size, Color.Blanco);
    }
    public int Size => size;

    public QuadTreeNode Node => root;
    public bool EstaPintado(int fila, int columna) => IsPaint(root, fila, columna);
    public int Resolucion => root.Ancho * root.Largo;
    public int CantidadDeNodosBlancos => CountNodesByColor(root, Color.Blanco);
    public int CantidadDeNodosGrises => CountNodesByColor(root, Color.Gris);
    public int CantidadDeNodosNegros => CountNodesByColor(root, Color.Negro);

    public bool IsPaint(QuadTreeNode root, int fila, int columna)
    {
        if (root.Color == Color.Negro) return true;
        if (root.Color == Color.Blanco || root.Hijos == null) return false;
        if (root.Hijos != null)
        {
            foreach (var hijo in root.Hijos)
            {
                return IsPaint(hijo, fila, columna);
            }
        }
        return false;
    }
    public int CountNodesByColor(QuadTreeNode root, Color color)
    {
        int count = 0;
        if (root == null) return 0;
        if (color== Color.Gris) count++;
        if (root.Hijos == null && root.Color == color) count++;
        if (root.Hijos != null)
        {
            foreach (var hijo in root.Hijos)
            {
                count += CountNodesByColor(hijo, color);
            }
        }
        return count;
    }
    public void Dibuja(int fila, int columna, int ancho, int alto) => DibujaRectangulo(root, fila, columna, ancho, alto);
    public void DibujaRectangulo(QuadTreeNode root, int fila, int columna, int ancho, int alto)
    {
        if (root == null) return;
        if (root.Color == Color.Negro) return;

    }
}