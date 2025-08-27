using System.Runtime.CompilerServices;
using System.Security.Authentication;
using System.Xml;

public class Lienzo
{
    public int Size { get; set; } 
    public QuadTreeNode root { get; set; }
    public Lienzo(int N)
    {
        Size = (int)Math.Pow(2, N);
        root = new QuadTreeNode(0, 0, Size, Size, Color.Blanco);
    }
    public int Resolucion => Size * Size;

    public int CantidadDeNodosBlancos => CantidadNodos(root, Color.Blanco);

    public int CantidadDeNodosNegros => CantidadNodos(root, Color.Negro);

    private int CantidadNodos(QuadTreeNode node, Color color)
    {
        if (node == null) return 0;
        //if (node.Hijos == null) return node.Color == color && color != Color.Gris ? 1 : 0;
        int count = node.Color == color?1:0;
        if (node.Hijos != null)
        {
            foreach (var hijo in node.Hijos)
            {
                count += CantidadNodos(hijo, color);
            }
        }
        return count;
    }

    public int CantidadDeNodosGrises => CantidadNodos(root, Color.Gris);

    public void Dibuja(int fila, int columna, int ancho, int alto) => DibujaRectangulo(root, fila, columna, ancho, alto);

    private void DibujaRectangulo(QuadTreeNode node, int fila, int columna, int ancho, int alto)
    {
        if (node.Color == Color.Negro) return;
        if (RectanguloCubreNodo(node, fila, columna, ancho, alto))
        {
            node.Color = Color.Negro;
            node.Hijos = null;
            return;
        }
        if (!RectanguloIntersecaNodo(node, fila, columna, alto, ancho)) return;
        if (node.Color == Color.Blanco)
        {
            SubdividirNodo(node, fila, columna);
            node.Color = Color.Gris;
        }
        foreach (var hijo in node.Hijos)
        {
            if (RectanguloIntersecaNodo(hijo, fila, columna, alto, ancho))
            {
                DibujaRectangulo(hijo, fila, columna, ancho, alto);
            }
        }

        ConsolidarNodo(node);
    }
    void ConsolidarNodo(QuadTreeNode node)
    {
        if (node.Hijos == null) return;
        Color primerColor = node.Hijos[0].Color;
        bool todosIguales = true;
        foreach (var hijo in node.Hijos)
        {
            if (hijo.Color != primerColor || hijo.Color == Color.Gris)
            {
                todosIguales = false;
                break;
            }
        }
        if (todosIguales)
        {
            node.Color = primerColor;
            node.Hijos = null;

        }
        else node.Color = Color.Gris;
    }
    private bool TodosLosHijosSonNegros(QuadTreeNode node)
    {
        return node.Hijos != null && node.Hijos.All(hijos => hijos.Color == Color.Negro);
    }   

    private bool RectanguloIntersecaNodo(QuadTreeNode node, int fila, int columna, int alto, int ancho)
    {
        return !(fila+alto<=node.X||node.X+node.Alto<fila||columna+ancho<=node.Y||node.Y+node.Ancho<columna);
    }
    private bool RectanguloCubreNodo(QuadTreeNode node, int fila, int columna, int ancho, int alto)
    {
        return fila<=node.X && (fila+alto) >= (node.Alto + node.X) && columna<=node.Y && (columna + ancho) >= (node.Ancho+node.Y);    
    }
    private void SubdividirNodo(QuadTreeNode node, int fila, int columna)
    { 
        node.Hijos = new QuadTreeNode[4];
        node.Hijos[0] = new QuadTreeNode(node.X, node.Y, node.Ancho / 2, node.Alto / 2, Color.Blanco);
        node.Hijos[1] = new QuadTreeNode(node.X, node.Y + node.Ancho / 2, node.Ancho / 2, node.Alto / 2, Color.Blanco);
        node.Hijos[2] = new QuadTreeNode(node.X + node.Alto / 2, node.Y, node.Ancho / 2, node.Alto / 2, Color.Blanco);
        node.Hijos[3] = new QuadTreeNode(node.X + node.Alto / 2, node.Y + node.Ancho / 2, node.Ancho / 2, node.Alto / 2, Color.Blanco);
    }
    public bool EstaPintado(int fila, int columna) => IsPaint(root, fila, columna);

    private bool IsPaint(QuadTreeNode node, int fila, int columna)
    {
        if (node.Color == Color.Negro) return true;
        if (root.Color == Color.Blanco) return false;
        if (node.Hijos == null) return false;
        
        foreach (var hijo in node.Hijos)
        {
            if (PuntoDentroDelNodo(hijo, fila, columna)) return IsPaint(hijo, fila, columna);
        }
        
        return false;
    }
    private bool PuntoDentroDelNodo(QuadTreeNode node, int fila, int columna)
    {
        return node.X <= fila && fila < node.Alto + node.X && node.Y <= columna && columna < node.Ancho + node.Y;
    }
}