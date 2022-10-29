namespace Problem02.Stack
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class Stack<T> : IAbstractStack<T>
    {
        private Node top;

        class Node
        {
            public Node(T value, Node next)
            {
                Value = value;
                Next = next;
            }

            public T Value { get; set; }
            public Node Next { get; set; }
        }

        public int Count { get; private set; }

        public bool Contains(T item)
        {
            var node = top;

            while (node != null)
            {
                if (node.Value.Equals(item))
                {
                    return true;
                }

                node = node.Next;
            }

            return false;
        }

        public T Peek()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException("The Stack is empty!");
            }

            return this.top.Value;
        }

        public T Pop()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException("The Stack is empty!");
            }

            var res = top.Value;
            this.top = top.Next;

            this.Count--;

            return res;
        }

        public void Push(T item)
        {
            this.top = new Node(item, top);
            this.Count++;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var node = top;

            while (node != null)
            {
                yield return node.Value;

                node = node.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
            => this.GetEnumerator();
    }
}