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
        private int countPreviousSteps;

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

        public State(Context context, State previousState, Coord player, Coord[] chests, int countPreviousSteps)
        {
            this.context = context;
            this.previousState = previousState;
            this.player = player;
            this.chests = chests;
            this.countPreviousSteps = countPreviousSteps + 1;

            CalcHeuristic();
        }

        public State[] NextStates()
        {
            List<State> nextStates = new List<State>();

            foreach (Direction direction in Enum.GetValues(typeof(Direction)))
            {
                int x = player.X;
                int y = player.Y;
                Coord[] newChests = chests;

                switch (direction)
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

                newChests = NewChests(x, y, direction);
                if (newChests != null)
                {
                    State newState = NewState(x, y, newChests);
                    if (newState != null)
                    {
                        nextStates.Add(newState);
                    }
                }
            }

            return nextStates.ToArray();
        }

        private Coord[] NewChests(int x, int y, Direction direction)
        {
            Coord[] newChests = chests;

            for (int i = 0; i < newChests.Length; i++)
            {
                if (newChests[i].Y == y && newChests[i].X == x)
                {
                    switch (direction)
                    {
                        case Direction.Up:
                            if (context.Map[y - 1, x])
                            {
                                newChests[i].Y = newChests[i].Y - 1;
                            }
                            else
                            {
                                return null;
                            }
                            break;

                        case Direction.Down:
                            if (context.Map[y + 1, x])
                            {
                                newChests[i].Y = newChests[i].Y + 1;
                            }
                            else
                            {
                                return null;
                            }
                            break;

                        case Direction.Left:
                            if (context.Map[y, x - 1])
                            {
                                newChests[i].X = newChests[i].X - 1;
                            }
                            else
                            {
                                return null;
                            }
                            break;

                        case Direction.Right:
                            if (context.Map[y, x + 1])
                            {
                                newChests[i].X = newChests[i].X + 1;
                            }
                            else
                            {
                                return null;
                            }
                            break;
                    }

                    return newChests;
                }
            }
            return chests;
        }

        private State NewState(int x, int y, Coord[] chests)
        {
            if (context.Map[y, x])
            {
                return new State(context, this, new Coord(x, y), chests, countPreviousSteps);
            }
            else
            {
                return null;
            }
        }

        public bool GoalCheck()
        {
            foreach (Coord target in context.Targets)
            {
                if (chests.Where((Coord chest) => chest.X == target.X && chest.Y == target.Y).FirstOrDefault() == null)
                {
                    return false;
                }
            }
            return true;
        }

        private void CalcHeuristic()
        {
            heuristic = 0 + countPreviousSteps;
        }
    }
}
