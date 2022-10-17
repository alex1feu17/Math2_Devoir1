using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PIF1006_tp1
{
    public class Automate
    {
        private State InitialState { get; set; }
        private State CurrentState { get; set; }

        private void Reset() => CurrentState = InitialState;

        public void LoadFromFile(string filePath)
        {
            if (!File.Exists(filePath)) Console.Write(filePath + " not found.\n");

            List<State> states = new List<State>();
            foreach (var input in File.ReadAllLines(filePath))
            {
                String[] args = input.Split(' ');
                
                switch(args[0])
                {
                    case "state":
                        if (args.Length < 3) break;
                        states.Add(new State(args[1], args[2] == "1"));
                        break;
                    case "transition":
                        if (args.Length < 4) break;
                        State s = states.Find(x => x.Name.Equals(args[1]));
                        State s1 = states.Find(x => x.Name.Equals(args[3]));
                        if (s == null || s1 == null) break;
                        s.Transitions.Add(new Transition(args[2][0], s1));
                        break;
                }
            }

            InitialState = states.First();
            Reset();
            Console.Write(filePath + " loaded.\n");
        }

        public bool Validate(string input)
        {
            Reset();
            bool isValid = false;

            for (int i = 0; i < input.Length; i++)
            {
                char c = input[i];
                Transition t = CurrentState.Transitions.Find(x => x.Input.Equals(c));
                if (t == null) break;

                CurrentState = t.TransiteTo;
                if (i == input.Length - 1) isValid = CurrentState.IsFinal;
            }

            return isValid;
        }

        private void feedStates(State init, List<State> states)
        {
            states.Add(init);
            init.Transitions.ForEach(x =>
            {
                if (states.Contains(x.TransiteTo)) return;
                feedStates(x.TransiteTo, states);
            });
        }

        public override string ToString()
        {
            String res = "";
            List<State> states = new List<State>();
            if(InitialState != null) feedStates(InitialState, states);
            foreach (State s in states) res += s.ToString();
            return res;
        }
    }
}
