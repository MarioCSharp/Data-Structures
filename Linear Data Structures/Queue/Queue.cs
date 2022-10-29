namespace Problem03.Queue
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class Queue<T> : IAbstractQueue<T>
    {
        private Node head;

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
            var node = head;

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

        public T Dequeue()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException("Queue is empty!");
            }

            var res = head.Value;
            this.head = this.head.Next;
            this.Count--;

            return res;
        }

        public void Enqueue(T item)
        {
            if (head == null)
            {
                this.head = new Node(item, null);
                this.Count++;
                return;
            }

            var node = head;

            while (node.Next != null)
            {
                node = node.Next;
            }

            node.Next = new Node(item, null);

            this.Count++;
        }

        public T Peek()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException("Queue is empty!");
            }

            return this.head.Value;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var node = head;

            while (node != null)
            {
                yield return node.Value;

                node = node.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();
    }
}