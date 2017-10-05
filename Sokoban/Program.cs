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

            // 

            SortedList<int, State> F = new SortedList<int, State>();

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
