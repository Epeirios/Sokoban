using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban
{
    class StateWrapper
    {
        public int heuristic;
        public State state;

        public StateWrapper(int heuristic, State state) {
            this.heuristic = heuristic;
            this.state = state;
        }
    }
}
