using Torres;
namespace Probador.PruebaFinal.Torres
{
    
    class Program
    {
       static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        var pruebas = new (bool[,], int, string)[]
        {
            (
                new bool[,]
                {
                    { false, false, false, false, false },
                    { false, true,  true,  true,  false },
                    { false, false, true,  false, false },
                    { false, true,  false, false, false },
                    { false, false, false, false, false }
                },
                3,
                "Ejemplo del examen (torre A elimina 3)"
            ),
            (
                new bool[,]
                {
                    { true, true, false },
                    { false, true, false },
                    { false, false, false }
                },
                1,
                "Pequeño tablero: solo una torre puede ser eliminada"
            ),
            (
                new bool[,]
                {
                    { false, false, false },
                    { false, false, false },
                    { false, false, false }
                },
                0,
                "Tablero vacío"
            ),
            (
                new bool[,]
                {
                    { true }
                },
                0,
                "Tablero con una sola torre"
            )
        };

        int caso = 1;
        foreach (var (tablero, esperado, descripcion) in pruebas)
        {
            Console.WriteLine($"\n🧪 Prueba #{caso}: {descripcion}");
            ImprimirTablero(tablero);

            int resultado = Juego.MayorEliminacion(tablero);
            bool ok = resultado == esperado;

            Console.WriteLine($"Esperado: {esperado}");
            Console.WriteLine($"Obtenido: {resultado}");
            Console.WriteLine(ok ? "✅ Correcto" : "❌ Incorrecto");

            caso++;
        }
    }

    static void ImprimirTablero(bool[,] tablero)
    {
        int filas = tablero.GetLength(0);
        int columnas = tablero.GetLength(1);

        for (int i = 0; i < filas; i++)
        {
            for (int j = 0; j < columnas; j++)
            {
                Console.Write(tablero[i, j] ? "♜ " : ". ");
            }
            Console.WriteLine();
        }
    }
    }
}
