using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban
{
    public class InitialState
    {
        int[][] map;

        Coord target1;
        Coord target2;

        public int[][] Map
        {
            get
            {
                return map;
            }
        }

        public Coord Target1
        {
            get
            {
                return target1;
            }
        }

        public Coord Target2
        {
            get
            {
                return target2;
            }
        }

        public InitialState(int[][] map, Coord target1, Coord target2)
        {
            this.map = map;
            this.target1 = target1;
            this.target2 = target2;
        }
    }
}
