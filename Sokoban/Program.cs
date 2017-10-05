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
