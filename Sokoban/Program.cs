using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban
{
    class Program
    {
        static void Main(string[] args)
        {
            // aanname is dat er een oplossing gevonden kan worden.
            // als de oplossing gevonden is dan is de laatste state de currentstate,
            // welke dan gelijk is aan de goal state.

            SortedList<int, State> F = new SortedList<int, State>();

            bool[,] map = new bool[,] { { true, true, true },
                                        { true, true, true },
                                        { true, true, true },
                                        { true, true, false },
                                        { true, true, false },
                                        { true, true, false }, };

            State startState = new State(
                new Context(map, new Coord[] { new Coord(0, 3), new Coord(0, 4) }), 
                null, 
                new Coord(0, 0), 
                new Coord[] { new Coord(1, 2), new Coord(1, 3) });

            F.Add(startState.Heuristic, startState);

            while (F.Count != 0)
            {
                State currentState = F[0];
                F.RemoveAt(0);

                if (currentState.GoalCheck())
                {
                    break;
                }

                State[] nextStates = currentState.NextStates();
                foreach (State state in nextStates)
                {
                    F.Add(state.Heuristic, state);
                }
            }
        }
    }
}
