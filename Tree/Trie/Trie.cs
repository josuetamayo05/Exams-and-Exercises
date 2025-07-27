using System.Text;

namespace Trie
{
    public class Trie : ITrie
    {
        public NodoTrie Raiz { get; private set; }
        

        public Trie()
        {
            Raiz = new NodoTrie('\0');
        }

        public void AgregarPalabra(string palabra)
        {
            Add(palabra, Raiz, 0);
        }

        public void Add(string palabra, NodoTrie root, int index)
        {
            if (index == palabra.Length)
            {
                root.FinDePalabra = true;
                CantidadDePalabras++;
                return;    
            }
            var match = root.Hijos.Where(x => x.Valor == palabra[index]).FirstOrDefault();
            if (match is null)
            {
                var child = new NodoTrie(palabra[index]);
                root.Hijos.Add(child);
                Add(palabra, child, index + 1);
            }
            else
                Add(palabra, match, index + 1);
        }

        public string MayorPrefijoComun()
        {
            string prefijo = "";
            var actual = Raiz;
            var collection = new StringBuilder();
            while (actual.Hijos.Count == 1 && !actual.FinDePalabra)
            {
                actual = actual.Hijos[0];
                collection.Append(actual.Valor);
            }
            return collection.ToString();
        }
        
        public IEnumerable<string> PalabrasConPrefijo(string prefijo)
        {
            List<string> words = new List<string>();
            WorkWithPrefij(Raiz, prefijo, 0, words);
            return words;
        }

        void WorkWithPrefij(NodoTrie node, string prefijo, int index, List<string> words)
        {
            if (index == prefijo.Length)
            {
                GetWords(node, prefijo, words);
            }
            else
            {
                var match = node.Hijos.Where(x => x.Valor == prefijo[index]).FirstOrDefault();
                if (match is null) return;
                else
                {
                    WorkWithPrefij(match, prefijo, index + 1, words);
                }
            }
        }
        void GetWords(NodoTrie node, string prefijo, List<string> words)
        {
            if (node.Hijos.Count == 0 && node.FinDePalabra)
            {
                words.Add(prefijo);
                return;
            }

            else if (node.Hijos.Count != 0 && node.FinDePalabra)
            {
                words.Add(prefijo);
                foreach (var child in node.Hijos)
                {
                    GetWords(child, prefijo + child.Valor, words);
                }
                return;
            }
            else if (node.Hijos.Count != 0 && node.FinDePalabra)
            {
                words.Add(prefijo);
            }
            else
            {
                foreach (var child in node.Hijos)
                {
                    GetWords(child, prefijo + child.Valor, words);
                }
            }
        }

        public bool Contiene(string palabra)
        {
            if (Contains(Raiz, 0, palabra)) return true;
            return false;
        }

        bool Contains(NodoTrie node, int index, string palabra)
        {
            if (index == palabra.Length )
            {
                if (node.FinDePalabra) return true;
                return false;
            }   
            var match = node.Hijos.Where(x => x.Valor == palabra[index]).FirstOrDefault();
            if (match is null) return false;
            //else if (match.FinDePalabra) return true;
            return Contains(match, index + 1, palabra);
        }
        public void Vaciar()
        {
            Raiz.Hijos.Clear();
            CantidadDePalabras = 0;
        }
        public int CantidadDePalabras{ get; set; }
        
        
        public NodoTrie this[char valor]
        {
            get
            {
                var match = Raiz.Hijos.Where(x => x.Valor == valor).FirstOrDefault();
                return match;
            }
        }
    }   
}