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
        private int pathcost;

        private Coord player;
        private readonly Coord[] chests;

        public State PreviousState
        {
            get
            {
                return previousState;
            }
        }

        public int Cost
        {
            get
            {
                return pathcost;
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

        public State(Context context, State previousState, Coord player, Coord[] chests, int pathcost)
        {
            this.context = context;
            this.previousState = previousState;
            this.player = player;
            this.chests = chests;
            this.pathcost = pathcost + 1;

            this.heuristic = CalcHeuristic();
        }

        public State[] NextStates()
        {
            List<State> nextStates = new List<State>();

            foreach (Direction direction in Enum.GetValues(typeof(Direction)))
            {
                int x = player.X;
                int y = player.Y;

                // Workaround (chests variabele bleef globaal updaten over alle states)
                Coord[] newChests = new Coord[chests.Length];
                for (int i = 0; i < chests.Length; i++)
                {
                    newChests[i] = new Coord(chests[i].X, chests[i].Y);
                }
                // END Workaround
         
                bool stateValid = false;

                switch (direction)
                {
                    case Direction.Up:
                        if (context.Map[y - 1, x])
                        {
                            y = y - 1;
                            newChests = NewChests(x, y, direction, newChests);
                            stateValid = true;
                        }
                        break;
                    case Direction.Down:
                        if (context.Map[y + 1, x])
                        {
                            y = y + 1;
                            newChests = NewChests(x, y, direction, newChests);
                            stateValid = true;
                        }
                        break;
                    case Direction.Left:
                        if (context.Map[y, x - 1])
                        {
                            x = x - 1;
                            newChests = NewChests(x, y, direction, newChests);
                            stateValid = true;
                        }
                        break;
                    case Direction.Right:
                        if (context.Map[y, x + 1])
                        {
                            x = x + 1;
                            newChests = NewChests(x, y, direction, newChests);
                            stateValid = true;
                        }
                        break;
                }

                if (stateValid && (newChests != null))
                {
                    nextStates.Add(new State(context, this, new Coord(x, y), newChests, pathcost));
                }
            }

            return nextStates.ToArray();
        }

        private Coord[] NewChests(int x, int y, Direction direction, Coord[] newChests)
        {
            for (int i = 0; i < newChests.Length; i++)
            {
                if (newChests[i].Y == y && newChests[i].X == x)
                {
                    switch (direction)
                    {
                        case Direction.Up:
                            if (context.Map[y - 1, x])
                            {
                                newChests[i].Y = y - 1;
                            }
                            else
                            {
                                return null;
                            }
                            break;

                        case Direction.Down:
                            if (context.Map[y + 1, x])
                            {
                                newChests[i].Y = y + 1;
                            }
                            else
                            {
                                return null;
                            }
                            break;

                        case Direction.Left:
                            if (context.Map[y, x - 1])
                            {
                                newChests[i].X = x - 1;
                            }
                            else
                            {
                                return null;
                            }
                            break;

                        case Direction.Right:
                            if (context.Map[y, x + 1])
                            {
                                newChests[i].X = x + 1;
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

        private int CalcHeuristic()
        {
            int heuristic = 0;

            List<int> playerChestDistances = new List<int>();

            foreach (Coord chest in chests)
            {
                int shortestpath = CalculateManhattan(context.Targets[0], chests[0]) - 1;

                foreach (Coord target in context.Targets)
                {
                    int manhattan = CalculateManhattan(target, chest) - 1;

                    if (manhattan < shortestpath)
                    {
                        shortestpath = manhattan;
                    }
                }

                playerChestDistances.Add(CalculateManhattan(player, chest)); // distance from player to chest

                heuristic += shortestpath; // add only the shortest to ensure optimality of algorithm
            }

            return heuristic + (int)playerChestDistances.Average();
        }

        private static int CalculateManhattan(Coord coordA, Coord coordB)
        {
            return Math.Abs(coordA.X - coordB.X) +
                   Math.Abs(coordA.Y - coordB.Y);
            // manhattan distance between 2 coordinates.
        }
    }
}
