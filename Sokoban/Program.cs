using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban
{
    class Program
    {
        private static void StateHistory(State state)
        {
            if (state.PreviousState != null)
            {
                StateHistory(state.PreviousState);

                Console.WriteLine("Move : ");
                Console.WriteLine("  - x : " + state.Player.X);
                Console.WriteLine("  - y : " + state.Player.Y);
                Console.WriteLine("{0} {1}", state.Chests[0].X, state.Chests[0].Y);
            }
        }

        static void Main(string[] args)
        {
            // aanname is dat er een oplossing gevonden kan worden.
            // als de oplossing gevonden is dan is de laatste state de currentstate,
            // welke dan gelijk is aan de goal state.

            List<State> F = new List<State>();

            bool[,] map1 = new bool[,]
            {
                { false, false, false, false, false },
                { false, true , true , true , false },
                { false, true , true , true , false },
                { false, true , true , true , false },
                { false, true , true , true , false },
                { false, true , true , true , false },
                { false, true , true , true , false },
                { false, false, false, false, false }
            };

            bool[,] map2 = new bool[,]
            {
                { false, false, false, false, false, false, false, false, false, false, false },
                { false, false, false, false, false, false, false, true , true , true , false },
                { false, true , true , true , true , true , true , true , true , true , false },
                { false, true , true , true , true , true , true , true , true , true , false },
                { false, true , true , true , true , true , true , true , true , true , false },
                { false, true , false, true , false, false, false, false, false, false, false },
                { false, true , true , true , false, false, false, false, false, false, false },
                { false, false, false, false, false, false, false, false, false, false, false }
            };

            bool[,] map3 = new bool[,]
            {
                { false, false, false, false, false, false, false, false, false, false, false },
                { false, false, true , true , true , true , true , true , true , false, false },
                { false, false, true , false, false, false, false, false, true , false, false },
                { false, true , true , false, true , true , true , false, true , false, false },
                { false, true , true , true , true , true , true , false, true , false, false },
                { false, false, true , false, false, true , false, false, true , false, false },
                { false, false, true , true , true , true , true , true , true , true , false },
                { false, false, true , false, false, false, false, true , true , true , false },
                { false, false, true , true , true , true , true , true , false, false, false },
                { false, false, false, false, false, false, false, false, false, false, false }
            };

            bool[,] map4 = new bool[,]
            {
                { false, false, false, false, false },
                { false, true , true , true , false },
                { false, true , true , true , false },
                { false, true , true , true , false },
                { false, true , true , true , false },
                { false, true , true , true , false },
                { false, true , true , true , false },
                { false, false, false, false, false }
               };

            bool[,] map5 = new bool[,]
            {
                { false, false, false, false, false },
                { false, true , true , true , false },
                { false, true , true , true , false },
                { false, false, true , true , false },
                { false, false, true , true , false },
                { false, false, true , true , false },
                { false, false, false, false, false }
            };

            State game1 = new State(
                new Context(map1, new Coord[] { new Coord(3, 6) }),
                null,
                new Coord(1, 1),
                new Coord[] { new Coord(2, 3) },
                0, Direction.Left);

            State game2 = new State(
                new Context(map2, new Coord[] { new Coord(1, 3), new Coord(2, 3), new Coord(3, 3), new Coord(4, 3),
                                                new Coord(5, 3), new Coord(6, 3), new Coord(7, 3), new Coord(8, 3), new Coord(9, 3) }),
                null,
                new Coord(8, 1),
                new Coord[] { new Coord(2, 2), new Coord(4, 2), new Coord(6, 2), new Coord(7, 2), new Coord(8, 2),
                              new Coord(1, 4), new Coord(3, 4), new Coord(5, 4), new Coord(7, 4)},
                0, Direction.Left);

            State game3 = new State(
                new Context(map3, new Coord[] { new Coord(2, 2), new Coord(2, 5), new Coord(2, 7) }),
                null,
                new Coord(5, 4),
                new Coord[] { new Coord(8, 3), new Coord(8, 4), new Coord(8, 5) },
                0, Direction.Left);

            State game4 = new State(
                new Context(map1, new Coord[] { new Coord(3, 6) }),
                null,
                new Coord(1, 1),
                new Coord[] { new Coord(2, 3) },
            0, Direction.Left);

            State game5 = new State(
                new Context(map5, new Coord[] { new Coord(3, 3), new Coord(3, 6) }),
                null,
                new Coord(1, 1),
                new Coord[] { new Coord(2, 2), new Coord(2, 4) },
            0, Direction.Left);

            SokobanSolver(game5);

            Console.ReadKey();
        }

        private static void SokobanSolver(State initialState)
        {
            List<State> F = new List<State>();

            F.Add(initialState);

            while (F.Count != 0)
            {
                State currentState = F[0];
                Console.WriteLine("State : {0} {1} - {2} - {3} {4}", currentState.Player.X, currentState.Player.Y, currentState.Cost, currentState.Chests[0].X, currentState.Chests[0].Y);

                F.RemoveAt(0);

                if (currentState.GoalCheck())
                {
                    Console.WriteLine("Solution Found!");
                    StateHistory(currentState);
                    break;
                }

                F.AddRange(currentState.NextStates());

                F = F.OrderBy(state => state.Cost).ToList(); // sort list by state cost
            }
        }
    }
}
