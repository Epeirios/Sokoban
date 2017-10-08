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
            Console.WriteLine("Move : ");
            Console.WriteLine("  - x : " + state.Player.X);
            Console.WriteLine("  - y : " + state.Player.Y);
            if (state.PreviousState != null)
            {
                StateHistory(state.PreviousState);
            }
        }

        static void Main(string[] args)
        {
            // aanname is dat er een oplossing gevonden kan worden.
            // als de oplossing gevonden is dan is de laatste state de currentstate,
            // welke dan gelijk is aan de goal state.

            List<StateWrapper> F = new List<StateWrapper>();

            bool[,] map = new bool[,] 
            { 
                { false, false, false, false, false },
                { false, true, true, true, false },
                { false, true, true, true, false },
                { false, true, true, true, false },
                { false, true, true, true, false },
                { false, true, true, true, false },
                { false, true, true, true, false },
                { false, false, false, false, false },
            };

            State startState = new State(
                new Context(map, new Coord[] { new Coord(3, 6) }), 
                null, 
                new Coord(1, 1), 
                new Coord[] { new Coord(2, 3) },
                0);

            F.Add(new StateWrapper(startState.Heuristic, startState));

            while (F.Count != 0)
            {
                State currentState = F[0].state;
                Console.WriteLine("State : " + currentState.Player.X + " - " + currentState.Player.Y);

                F.RemoveAt(0);

                if (currentState.GoalCheck())
                {
                    Console.WriteLine("Solution Found!");
                    StateHistory(currentState);
                    break;
                }

                State[] nextStates = currentState.NextStates();
                foreach (State state in nextStates)
                {
                    F.Add(new StateWrapper(state.Heuristic, state));
                }
            }

            Console.ReadKey();
        }
    }
}
