using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GrainGrowth_1.Classes;

namespace GrainGrowth_1.Classes
{
    class DRX
    {
        private Cell[,] matrix;
        private double previousRo;
        private double ro = 1;
        double A = 86710969050178.5;
        double B = 9.41268203527779;
        double t;
        double step;
        double criticalRo;
        private List<Cell> matrixPreviousIteration;

        public DRX(Board board, double step) {
            matrix = board.GetMatrix();
            this.step = step;
            t = 0;
            criticalRo = 4.21584E+12 / (matrix.GetLength(0) * matrix.GetLength(1));
            
        }

        public Cell[,] GetNextIteration()
        {
            matrixPreviousIteration = new List<Cell>();
            previousRo = ro;
            t = t += step;
            ro = (A / B) + (1 - (A / B)) * Math.Pow(Math.E, (-B * t));
            if (previousRo != 1)
            {
                Distribute();
                Recrystalize();
            }
            return matrix;
           
        }

        private void Recrystalize()
        {
            int temp1, temp2, temp3, temp4;
            List<Cell> neighbours = new List<Cell>();
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    temp1 = i == 0 ? matrix.GetLength(0) - 1 : i - 1;      //i-1
                    temp2 = j == 0 ? matrix.GetLength(1) - 1 : j - 1;      //j-1
                    temp3 = i == matrix.GetLength(0) - 1 ? 0 : i + 1;      //i+1
                    temp4 = j == matrix.GetLength(1) - 1 ? 0 : j + 1;      //j+1

                    neighbours.Add(matrix[temp1, j]);
                    neighbours.Add(matrix[temp3, j]);
                    neighbours.Add(matrix[i, temp2]);
                    neighbours.Add(matrix[i, temp4]);

                    if (matrix[i, j].density > criticalRo && Addons.IsLimit(matrix[i, j], neighbours))
                    {
                        matrix[i, j].isRecrystal = true;
                        matrix[i, j].density = 0;
                        matrixPreviousIteration.Add(matrix[i, j]);
                    }
                }
            }
        }

        private void Distribute()
        {
            double equalDistributionPercentage = 0.2;
            double deltaRo = ro - previousRo;
            double roPerCell = deltaRo / (matrix.GetLength(0) * matrix.GetLength(1));
            double equalDistribution = roPerCell * equalDistributionPercentage;
            double randomDistributionPackage = roPerCell * (1 - equalDistributionPercentage);
            double chance;
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[i,j].density += equalDistribution;
                    deltaRo -= equalDistribution;
                }
            }

            var rand = new Random();
            List<Cell> neighbours = new List<Cell>();
            int temp1, temp2, temp3, temp4;
            while (deltaRo > 0)
            {
                int i = rand.Next(matrix.GetLength(0));
                int j = rand.Next(matrix.GetLength(1));

                temp1 = i == 0 ? matrix.GetLength(0) - 1 : i - 1;      //i-1
                temp2 = j == 0 ? matrix.GetLength(1) - 1 : j - 1;      //j-1
                temp3 = i == matrix.GetLength(0) - 1 ? 0 : i + 1;      //i+1
                temp4 = j == matrix.GetLength(1) - 1 ? 0 : j + 1;      //j+1

                neighbours.Add(matrix[temp1, j]);
                neighbours.Add(matrix[temp3, j]);
                neighbours.Add(matrix[i, temp2]);
                neighbours.Add(matrix[i, temp4]);
                
                chance = 0.2;
                if (Addons.IsLimit(matrix[i, j], neighbours))
                    chance = 0.8;
                

                if (rand.NextDouble() <= chance)
                {
                    matrix[i, j].density += randomDistributionPackage;
                    deltaRo -= randomDistributionPackage;
                }
            }
        }
    }
}
