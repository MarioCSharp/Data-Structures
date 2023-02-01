namespace AVLTree
{
    using System;

    public class AVL<T> where T : IComparable<T>
    {
        public class Node
        {
            public Node(T value)
            {
                this.Value = value;
                this.Height = 1;
            }

            public T Value { get; set; }
            public Node Left { get; set; }
            public Node Right { get; set; }
            public int Height { get; set; }
        }

        public Node Root { get; private set; }

        public bool Contains(T element)
        {
            var current = this.Root;

            while (current != null)
            {
                if (current.Value.Equals(element))
                {
                    return true;
                }
                else if (current.Value.CompareTo(element) < 0)
                {
                    current = current.Right;
                }
                else
                {
                    current = current.Left;
                }
            }

            return false;
        }

        public void Delete(T element)
        {
            this.Root = this.Delete(this.Root, element);
        }

        private Node Delete(Node node, T element)
        {
            if (node == null)
            {
                return null;
            }

            if (element.CompareTo(node.Value) < 0)
            {
                node.Left = this.Delete(node.Left, element);
            }
            else if (element.CompareTo(node.Value) > 0)
            {
                node.Right = this.Delete(node.Right, element);
            }
            else
            {
                if (node.Left == null || node.Right == null)
                {
                    Node temp;
                    if (node.Left == null)
                    {
                        temp = node.Right;
                    }
                    else
                    {
                        temp = node.Left;
                    }

                    if (temp == null)
                    {
                        return null;
                    }
                    else
                    {
                        node = temp;
                    }
                }
                else
                {
                    Node temp = this.FindSmallestChild(node.Right);
                    node.Value = temp.Value;

                    node.Right = this.Delete(node.Right, temp.Value);
                }
            }

            node = this.Balnace(node);
            node.Height = func(node);

            return node;
        }

        private Node FindSmallestChild(Node node)
        {
            if (node.Left == null)
            {
                return node;
            }

            return this.FindSmallestChild(node.Left);
        }

        public void DeleteMin()
        {
            if (this.Root == null)
            {
                return;
            }

            var minValue = this.FindSmallestChild(this.Root).Value;

            this.Delete(minValue);
        }

        public void Insert(T element)
        {
            this.Root = this.Insert(this.Root, element);
        }

        private Node Insert(Node node, T element)
        {
            if (node == null)
            {
                return new Node(element);
            }

            if (node.Value.CompareTo(element) < 0)
            {
                node.Right = this.Insert(node.Right, element);
            }
            else
            {
                node.Left = this.Insert(node.Left, element);
            }

            node = this.Balnace(node);
            node.Height = func(node);

            return node;
        }

        private Node Balnace(Node node)
        {
            var balanceFactor = this.GetHeight(node.Left) - this.GetHeight(node.Right);

            if (balanceFactor > 1)
            {
                var childBalance = this.GetHeight(node.Left.Left) - this.GetHeight(node.Left.Right);

                if (childBalance < 0)
                {
                    node.Left = this.RotateLeft(node.Left);
                }

                node = this.RotateRight(node);
            }
            else if (balanceFactor < -1)
            {
                var childBalance = this.GetHeight(node.Right.Left) - this.GetHeight(node.Right.Right);

                if (childBalance > 0)
                {
                    node.Right = this.RotateRight(node.Right);
                }

                node = this.RotateLeft(node);
            }

            return node;
        }

        private Node RotateRight(Node node)
        {
            var temp = node.Left;
            node.Left = temp.Right;
            temp.Right = node;

            node.Height = func(node);

            return temp;
        }

        private Node RotateLeft(Node node)
        {
            var temp = node.Right;
            node.Right = temp.Left;
            temp.Left = node;

            node.Height = func(node);

            return temp;
        }
        private Func<Node, int> func => x => Math.Max(this.GetHeight(x.Left), this.GetHeight(x.Right)) + 1;

        private int GetHeight(Node node)
        {
            if (node == null)
            {
                return 0;
            }

            return node.Height;
        }

        public void EachInOrder(Action<T> action)
        {
            this.EachInOrder(this.Root, action);
        }

        private void EachInOrder(Node node, Action<T> action)
        {
            if (node == null)
            {
                return;
            }

            this.EachInOrder(node.Left, action);
            action(node.Value);
            this.EachInOrder(node.Right, action);
        }
    }
}
