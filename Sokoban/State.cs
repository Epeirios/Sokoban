using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban
{
    public class State
    {
        private Context context;
        private State previousState;
        private int heuristic;

        private Coord player;
        private Coord[] chests;

        public State PreviousState
        {
            get
            {
                return previousState;
            }
        }

        public int Heuristic
        {
            get
            {
                return heuristic;
            }
        }

        public Coord Player
        {
            get
            {
                return player;
            }
        }

        public Coord[] Chests
        {
            get
            {
                return chests;
            }
        }

        public State(Context context, State previousState, Coord player, Coord[] chests)
        {
            this.context = context;
            this.previousState = previousState;
            this.player = player;
            this.chests = chests;

            CalcHeuristic();
        }

        public State[] NextStates()
        {

            return null;
        }

        private State NewState(int x, int y)
        {
            if (context.Map[y][x])
            {
                return new State(context, this, new Coord(x, y), chests);
            }
            else
            {
                return null;
            }
        }

        public bool GoalCheck()
        {
            foreach (Coord chest in chests)
            {
                bool correct = false;
                
                foreach (Coord target in context.Targets)
                {
                    if (chest == target)
                    {
                        correct = true;
                        break;
                    }
                }

                if (!correct)
                {
                    return false;
                }
            }

            return true;
        }

        private void CalcHeuristic()
        {
            heuristic = 0;
        }
    }
}
