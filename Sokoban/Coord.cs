using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban
{
    public class Coord
    {
        private int x;
        private int y;

        public int X
        {
            get
            {
                return x;
            }
        }

        public int Y
        {
            get
            {
                return y;
            }
        }

        public Coord(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
