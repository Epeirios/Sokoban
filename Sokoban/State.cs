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
            //State[] nextStates

            return null;
        }

        private State NewState(int x, int y)
        {
            if (this.context.Map[y][x])
            {
                return new State()
            } else
            {
                return null;
            }
        }

        public bool GoalCheck()
        {
            foreach(Coord target in this.context.Targets)
            {
                this.chests.Where((Coord chest) => chest.X == target.X)
            }
        }

        private void CalcHeuristic()
        {
            heuristic = 0;
        }        
    }
}
