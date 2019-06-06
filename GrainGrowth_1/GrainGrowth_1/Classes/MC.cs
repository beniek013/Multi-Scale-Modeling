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
        private List<Brush> brushes;
        
        public MC(Board board)
        {
            matrix = board.GetMatrix();
            brushes = board.GetBrushes();
            rnd = new Random();
        }

        public void GetNextIteration() {
            randomCells = new List<Cell>();
            GetRadnomCells();

            foreach (var cell in randomCells) {
                VonNeuman(cell);
            }
        }

        private void VonNeuman(Cell cell)
        {
            int temp1, temp2, temp3, temp4;
            var neighbours = new List<Cell>();

            temp1 = cell.x == 0 ? matrix.GetLength(0) - 1 : cell.x - 1;      //i-1
            temp2 = cell.y == 0 ? matrix.GetLength(1) - 1 : cell.y - 1;      //j-1
            temp3 = cell.x == matrix.GetLength(0) - 1 ? 0 : cell.x + 1;      //i+1
            temp4 = cell.y == matrix.GetLength(1) - 1 ? 0 : cell.y + 1;      //j+1

            neighbours.Add(matrix[temp1, cell.y]);
            neighbours.Add(matrix[temp3, cell.y]);
            neighbours.Add(matrix[cell.x, temp2]);
            neighbours.Add(matrix[cell.x, temp4]);

            var randNeighbour = neighbours[rnd.Next(0, neighbours.Count)];

            int energyBefore = CountEnergy(neighbours, cell.value);
            int energyAfter = CountEnergy(neighbours, randNeighbour.value);

            if ((energyAfter - energyBefore) <= 0)
            {
                matrix[cell.x, cell.y].value = randNeighbour.value;
                matrix[cell.x, cell.y].energy = energyAfter;
            }
            else
            {
                double value = rnd.NextDouble();
                if (Math.Exp(-1 * energyAfter - energyBefore / 6) < value)
                {
                    matrix[cell.x, cell.y].value = randNeighbour.value;
                    matrix[cell.x, cell.y].energy = energyAfter;
                }
            }
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


        public void Paint(Graphics g)
        {
            if (matrix != null)
            {
                for (int r = 0; r < matrix.GetLength(0); r++)
                {
                    for (int c = 0; c < matrix.GetLength(1); c++)
                    {
                        if (matrix[r, c].value != 0)
                        {
                            g.FillRectangle(brushes[matrix[r, c].value], c * 5, r * 5, 5, 5);
                            matrix[r, c].color = brushes[matrix[r, c].value];
                        }
                    }
                }
            }
        }
    }
}
