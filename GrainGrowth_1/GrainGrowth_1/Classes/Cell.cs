using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrainGrowth_1.Classes
{
    public class Cell
    {
        public int id;
        public int x;
        public int y;
        public Brush color;
        public int value;
        public int energy;
        public double density;
        public bool isRecrystal;

        public Cell() {
            value = 0;
            isRecrystal = false;
        }

    }
}
