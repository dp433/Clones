using System.Collections.Generic;
using System.Linq;

namespace Clones
{
    public class CloneVersionSystem : ICloneVersionSystem
    {
        private readonly List<Clone> clones;

        public CloneVersionSystem()
        {
            clones = new List<Clone> { new Clone() };
        }

        public string Execute(string query)
        {
            var splittedQuery = query.Split(' ');
            var cloneId = int.Parse(splittedQuery[1]) - 1;

            switch (splittedQuery.First())
            {
                case "learn":
                    clones[cloneId].Learn(splittedQuery[2]);
                    break;
                case "rollback":
                    clones[cloneId].RollBack();
                    break;
                case "relearn":
                    clones[cloneId].Relearn();
                    break;
                case "clone":
                    clones.Add(new Clone(clones[cloneId]));
                    break;
                case "check":
                    return clones[cloneId].Check();
            }
            return null;
        }
    }

    class Clone
    {
        private Stack<int> learnedCommandHistory;
        private Stack<int> rollbackHistory;

        public Clone()
        {
            learnedCommandHistory = new Stack<int>();
            rollbackHistory = new Stack<int>();
        }

        public Clone(Clone anotherClone)
        {
            learnedCommandHistory = new Stack<int>(anotherClone.learnedCommandHistory.Reverse());
            rollbackHistory = new Stack<int>(anotherClone.rollbackHistory.Reverse());
        }

        public void Learn(string requiredCommand)
        {
            rollbackHistory.Clear();
            learnedCommandHistory.Push(int.Parse(requiredCommand));
        }

        public void RollBack()
        {

            rollbackHistory.Push(learnedCommandHistory.Pop());
        }

        public void Relearn()
        {
            learnedCommandHistory.Push(rollbackHistory.Pop());
        }

        public string Check()
        {
            return learnedCommandHistory.Count == 0 ? "basic" :
                learnedCommandHistory.First().ToString();
        }
    }

    class Stack
    {
        public StackObject Last { get; set; }
        public Stack() { }
        public Stack(Stack stack) { Last = stack.Last; }

        public void Push(string value) { Last = new StackObject(value, Last); }

        public string Pop()
        {
            var value = Last.Value;
            Last = Last.Previous;
            return value;
        }

        public string GetLast() { return Last.Value; }

        public bool IsEmpty() { return Last == null; }

        public void Clear() { Last = null; }
    }

    class StackObject
    {
        public string Value { get; set; }
        public StackObject Previous { get; set; }

        public StackObject(string value, StackObject previous)
        {
            Value = value;
            Previous = previous;
        }
    }
}
