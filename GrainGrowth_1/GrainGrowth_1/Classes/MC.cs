using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrainGrowth_1.Classes
{
    class MC
    {
        private Random rnd;
        private List<Cell> randomCells;
        private Cell[,] matrix;
        private double kt;
        private string type;
        
        public MC(Board board, double kt, string type)
        {
            matrix = board.GetMatrix();
            rnd = new Random();
            this.type = type;   
            this.kt = kt;
        }

        public void GetNextIteration() {
            randomCells = new List<Cell>();
            GetRadnomCells();

            foreach (var cell in randomCells) {
                Fun(cell);
            }
        }

        private void Fun(Cell cell)
        {
            int temp1, temp2, temp3, temp4;
            temp1 = cell.x == 0 ? matrix.GetLength(0) - 1 : cell.x - 1;      //i-1
            temp2 = cell.y == 0 ? matrix.GetLength(1) - 1 : cell.y - 1;      //j-1
            temp3 = cell.x == matrix.GetLength(0) - 1 ? 0 : cell.x + 1;      //i+1
            temp4 = cell.y == matrix.GetLength(1) - 1 ? 0 : cell.y + 1;      //j+1
            var neighbours = new List<Cell>();
            if (type == "Moore")
            {

                neighbours.Add(matrix[temp1, cell.y]);
                neighbours.Add(matrix[temp3, cell.y]);
                neighbours.Add(matrix[cell.x, temp2]);
                neighbours.Add(matrix[cell.x, temp4]);

                neighbours.Add(matrix[temp1, temp2]);
                neighbours.Add(matrix[temp1, temp4]);
                neighbours.Add(matrix[temp3, temp2]);
                neighbours.Add(matrix[temp3, temp4]);
            }
            else
            {
                neighbours.Add(matrix[temp1, cell.y]);
                neighbours.Add(matrix[temp3, cell.y]);
                neighbours.Add(matrix[cell.x, temp2]);
                neighbours.Add(matrix[cell.x, temp4]);
            }
            var randNeighbour = neighbours[rnd.Next(0, neighbours.Count)];

            int energyBefore = CountEnergy(neighbours, cell.value);
            int energyAfter = CountEnergy(neighbours, randNeighbour.value);

            if ((energyAfter - energyBefore) <= 0)
            {
                cell.value = randNeighbour.value;
                cell.energy = energyAfter;
            }
            /*else
            {
                if (Math.Exp(-1 * (energyAfter - energyBefore) / kt) < rnd.NextDouble())
                {
                    cell.value = randNeighbour.value;
                    cell.energy = energyAfter;
                }
            }*/
        }

        private int CountEnergy(List<Cell> neighbours, int value)
        {
            int energy = 0;
            foreach (var neighbour in neighbours) {
                if (value != neighbour.value)
                    energy++;
            }
            return energy;
        }

        private void GetRadnomCells()
        {
            var counter = matrix.GetLength(0) * matrix.GetLength(1);
            int randomI, randomJ;
            while (counter > 0)
            {
                randomI = rnd.Next(matrix.GetLength(0));
                randomJ = rnd.Next(matrix.GetLength(1));
                if (!randomCells.Contains(matrix[randomI, randomJ]))
                {
                    randomCells.Add(matrix[randomI, randomJ]);
                    counter--;
                }
            }
        }
    }
}
