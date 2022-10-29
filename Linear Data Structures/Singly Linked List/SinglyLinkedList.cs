namespace Problem04.SinglyLinkedList
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class SinglyLinkedList<T> : IAbstractLinkedList<T>
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

        public void AddFirst(T item)
        {
            head = new Node(item, head);
            this.Count++;
        }

        public void AddLast(T item)
        {
            if (this.Count == 0)
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

        public T GetFirst()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException("The collection is empty!");
            }

            return this.head.Value;
        }

        public T GetLast()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException("The collection is empty!");
            }

            var node = this.head;

            while (node.Next != null)
            {
                node = node.Next;
            }

            return node.Value;
        }

        public T RemoveFirst()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException("The collection is empty!");
            }

            var res = head.Value;
            this.head = this.head.Next;
            this.Count--;

            return res;
        }

        public T RemoveLast()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException("The collection is empty!");
            }

            if (this.Count == 1)
            {
                var ans = head.Value;
                head = null;
                Count--;

                return ans;
            }

            var node = this.head;

            while (node.Next.Next != null)
            {
                node = node.Next;
            }

            var res = node.Next.Value;
            node.Next = null;
            this.Count--;

            return res;
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