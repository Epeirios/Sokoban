using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban
{
    public class Context
    {
        bool[][] map;

        Coord[] targets;

        public bool[][] Map
        {
            get
            {
                return map;
            }
        }

        public Coord[] Targets
        {
            get
            {
                return targets;
            }
        }

        public Context(bool[][] map, Coord[] targets)
        {
            this.map = map;
            this.targets = targets;
        }
    }
}
