using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;

public class Extraordinario
{
    static readonly Dictionary<char, string> tecladoMap = new Dictionary<char, string>
    {
        {'2', "ABC"},
        {'3', "DEF"},
        {'4', "GHI"},
        {'5', "JKL"},
        {'6', "MNO"},
        {'7', "PQRS"},
        {'8', "TUV"},
        {'9', "WXYZ"}
    };

    public static IEnumerable<string> Cadenas(string secuenciaTeclas)
    {
        List<string> ans = [];
        Solve(secuenciaTeclas, ans, 0, "", new bool[secuenciaTeclas.Length],new List<string>());
        return ans;
    }

    public static void Solve(string secuenciaTeclas, List<string> ans, int index, string temp, bool[] mask, List<string> temps)
    {
        if (index == secuenciaTeclas.Length)
        {
            ans.Add(temp);
            return;
        }
        if (tecladoMap.ContainsKey(secuenciaTeclas[index]))
        {
            foreach (char s in tecladoMap[secuenciaTeclas[index]])
            {

                if (!mask[index])
                {
                    mask[index] = true;
                    Solve(secuenciaTeclas, ans, index + 1, temp + s, mask, temps);
                    mask[index] = false;
                }
            }
        }
        else Solve(secuenciaTeclas, ans, index + 1, temp, mask, temps);
    }

   
}
