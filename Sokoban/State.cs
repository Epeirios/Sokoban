using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban
{
    public class State
    {
        private Context initialState;
        private State previousState;
        private int heuristic;

        private Coord player;
        private Coord chest1;
        private Coord chest2;

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

        public Coord Chest1
        {
            get
            {
                return chest1;
            }
        }

        public Coord Chest2
        {
            get
            {
                return chest2;
            }
        }

        public State(Context initialState, State previousState, Coord player, Coord chest1, Coord chest2)
        {
            this.initialState = initialState;
            this.previousState = previousState;
            this.player = player;
            this.chest1 = chest1;
            this.chest2 = chest2;

            CalcHeuristic();
        }

        public State[] NextStates()
        {
            return new State[new State(initialState)];
        }

        public bool GoalCheck()
        {
            if ((chest1 == initialState.Target1 || chest1 == initialState.Target2) &&
                (chest2 == initialState.Target1 || chest2 == initialState.Target2) &&
                chest1 != chest2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void CalcHeuristic()
        {
            heuristic = 0;
        }
    }
}
