using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Machines1D.Classes
{
    public class Machine
    {
        public static int[,] OneDimension(int regula, int size)
        {
            var x = size;
            var y = size;
            var matrix = new int[y, x + 2];
            var tab = new int[x + 2];
            tab[x/2] = 1;
            var binary = Convert.ToString(regula, 2).PadLeft(8, '0');
            for (int i = 0; i < x; i++)
            {
                var tab_temp = new int[x + 2];
                tab.CopyTo(tab_temp, 0);
                for (int t = 0; t < tab.Length; t++)
                {
                    matrix[i, t] = tab[t];
                }
                for (int j = 1; j < x + 1; j++)
                {
                    if (LeftNeighbor(tab_temp,j) == 1 && tab_temp[j] == 1 && RightNeihbor(tab_temp,j) == 1)
                        tab[j] = int.Parse(binary[0].ToString());
                    if (LeftNeighbor(tab_temp, j) == 1 && tab_temp[j] == 1 && RightNeihbor(tab_temp, j) == 0)
                        tab[j] = int.Parse(binary[1].ToString());
                    if (LeftNeighbor(tab_temp, j) == 1 && tab_temp[j] == 0 && RightNeihbor(tab_temp, j) == 1)
                        tab[j] = int.Parse(binary[2].ToString());
                    if (LeftNeighbor(tab_temp, j) == 1 && tab_temp[j] == 0 && RightNeihbor(tab_temp, j) == 0)
                        tab[j] = int.Parse(binary[3].ToString());
                    if (LeftNeighbor(tab_temp, j) == 0 && tab_temp[j] == 1 && RightNeihbor(tab_temp, j) == 1)
                        tab[j] = int.Parse(binary[4].ToString());
                    if (LeftNeighbor(tab_temp, j) == 0 && tab_temp[j] == 1 && RightNeihbor(tab_temp, j) == 0)
                        tab[j] = int.Parse(binary[5].ToString());
                    if (LeftNeighbor(tab_temp, j) == 0 && tab_temp[j] == 0 && RightNeihbor(tab_temp, j) == 1)
                        tab[j] = int.Parse(binary[6].ToString());
                    if (LeftNeighbor(tab_temp, j) == 0 && tab_temp[j] == 0 && RightNeihbor(tab_temp, j) == 0)
                        tab[j] = int.Parse(binary[7].ToString());
                }
            }
            return matrix;
        }
        private static int LeftNeighbor(int[] tab, int j)
        {
            if (j == 1)
                return tab[tab.Length-2];
            else
                return tab[j - 1];
        }

        private static int RightNeihbor(int[] tab, int j)
        {
            if (j == tab.Length)
                return tab[1];
            else
                return tab[j + 1];
        }
    }
}
