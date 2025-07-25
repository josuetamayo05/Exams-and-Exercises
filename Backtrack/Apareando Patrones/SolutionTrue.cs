using System.Data;

namespace Weboo.ApareandoPatrones
{
    public static class Examen
    {
        /* ! una letra cualquiera 
            ? la ocurrencia o no de una letra cualquiera
            + al menos una letra seguida de cualquier cant de letras
            * cero letras seguida de cualquier cantidad de letras   
        */
        public static int CantidadCoincidencias(string patron, string cadena)
        {
            int count = 0;
            Solve(patron, cadena, ref count, 0,0);
            return count;
        }
        private static void Solve(string patron, string cadena, ref int count, int indexPatron,int indexCadena)
        {
            if (indexPatron == patron.Length)
            {
                if (indexCadena == cadena.Length)
                {
                    count += 1;
                    return;
                }
                else
                    return;
            }
            bool SonIguales(int indexPatron, int indexCadena) => indexCadena < cadena.Length && (patron[indexPatron] == cadena[indexCadena] || patron[indexPatron] == '!');

            if (SonIguales(indexPatron, indexCadena))
                Solve(patron, cadena, ref count, indexPatron + 1, indexCadena + 1);

            else if (patron[indexPatron] == '*')
            {
                for (int i = 0; i <= cadena.Length; i++)
                {
                    Solve(patron, cadena, ref count, indexPatron + 1, indexCadena + i);
                }
            }
            else if (patron[indexPatron] == '+')
            {
                for (int i = 1; i < cadena.Length; i++)
                {
                    Solve(patron, cadena, ref count, indexPatron + 1, indexCadena + i);
                }
            }
            else if (patron[indexPatron] == '?')
            {
                for (int i = 0; i <= 1; i++)
                {
                    Solve(patron, cadena, ref count, indexPatron + 1, indexCadena + i);
                }
            }   
        }
    }
}