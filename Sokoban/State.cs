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
            foreach(Direction direction in Enum.GetValues(typeof(Direction)))
            {
                int x = player.X;
                int y = player.Y;
                Coord[] newChests = chests;

                switch(direction)
                {
                    case Direction.Up:
                        y = y - 1;
                        break;
                    case Direction.Down:
                        y = y + 1;
                        break;
                    case Direction.Left:
                        x = x - 1;
                        break;
                    case Direction.Right:
                        x = x + 1;
                        break;
                }

                
            }
            
            return null;
        }

        private Coord[] NewChests(int x, int y, Direction direction)
        {
            return null;
        }

        private State NewState(int newPlayerX, int newPlayerY, Coord[] chests)
        {
            if (context.Map[newPlayerY][newPlayerX])
            {
                return new State(context, this, new Coord(newPlayerX, newPlayerY), chests);
            } else {
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
