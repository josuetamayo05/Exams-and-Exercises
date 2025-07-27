namespace Trie
{
    public class NodoTrie
    {
        //Caracter que representa el nodo 
        public char Valor { get; private set; }

        //Representa si una palabra termina en este nodo 
        public bool FinDePalabra { get; set; }
        //Nodos Hijos 
        public List<NodoTrie> Hijos { get; private set; }

        //Indexer para acceder al nodo hijo que corresponda a ese caracter. 
        //De no existir devuelve null 
        public NodoTrie this[char valor]
        {
            get
            {
                foreach (var child in Hijos)
                {
                    if (child.Valor == valor)
                        return child;
                }
                return null;
            }
            set { }
        }

        //Constructor 
        public NodoTrie(char valor, bool finDePalabra = false, params NodoTrie[] hijos)
        {
            Valor = valor;
            FinDePalabra = finDePalabra;
            Hijos = hijos.ToList();
        }
        
    }   
}