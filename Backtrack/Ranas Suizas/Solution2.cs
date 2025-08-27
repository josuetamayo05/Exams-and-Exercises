using System.ComponentModel.DataAnnotations;

namespace Ranas
{
    class Solution2
    {
        static int[] dx = { 1, 1, 1 };
        static int[] dy = { -1, 1, 0 };
        public static int ComiendoChocolates(bool[,] mapa, int[] ranas)
        {
            int temp = 0;
            bool[,] visit = new bool[mapa.GetLength(0), mapa.GetLength(1)];
            List<(int, int)> debug = new();
            for (int i = 0; i < ranas.Length; i++) {
                debug.Add((0, ranas[i]));
            }
            bool[,] cloned = (bool[,])visit.Clone();
            int index = 0;
            int resp = 0;
            Solve(mapa, 0, ranas[0], 0, 0, cloned, debug,0,ref resp);

            
            return resp;
        }
        
        public static void Solve(bool[,] map, int x, int y,  int ans, int temp, bool[,] visitado, List<(int, int)> ranas, int index,ref int resp)
        {
            if (x == map.GetLength(0) - 1)
            {
                if (map[x, y]) temp += 1;

                if (index == ranas.Count - 1)
                {
                    resp = Math.Max(temp, resp);
                    return;
                }
                
                Solve(map, 0, ranas[index + 1].Item2, ans, temp, visitado, ranas, index + 1, ref resp);
                    
                
                return;
            }

            for (int i = 0; i < 3; i++)
            {
                int nx = x + dx[i]; int ny = y + dy[i];
                if (Checked(map, nx, ny) && !visitado[nx, ny])
                {
                    visitado[nx, ny] = true;
                    if (map[nx, ny])
                    {
                        map[nx, ny] = false; 
                        Solve(map, nx, ny, ans, temp+1, visitado, ranas, index, ref resp);
                        map[nx, ny] = true; 
                    }
                    else
                    {
                        Solve(map, nx, ny, ans, temp, visitado, ranas, index, ref resp);
                    }
                    visitado[nx, ny] = false;
                }
            }
        }
        
        private static bool Checked(bool[,] map, int x, int y) => x >= 0 && x < map.GetLength(0) && y >= 0 && y < map.GetLength(1);
    }
}