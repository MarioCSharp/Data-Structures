namespace _01.RedBlackTree
{
    using System;
    public class RedBlackTree<T> where T : IComparable
    {
        public const bool Red = true;
        public const bool Black = false;
        public class Node
        {
            public Node(T value)
            {
                this.Value = value;
                this.Color = Red;
            }

            public T Value { get; set; }
            public Node Left { get; set; }
            public Node Right { get; set; }
            public bool Color { get; set; }
        }

        public Node root;

        public RedBlackTree()
        {

        }

        private RedBlackTree(Node node)
        {
            this.PreOrderCopy(node);
        }

        public void EachInOrder(Action<T> action)
        {
            this.EachInOrder(this.root, action);
        }

        public RedBlackTree<T> Search(T element)
        {
            Node node = this.FindNode(this.root, element);

            return new RedBlackTree<T>(node);
        }

        public void Insert(T value)
        {
            this.root = this.Insert(this.root, value);
            this.root.Color = Black;
        }
        

        public void Delete(T key)
        {
            if (this.root == null)
            {
                throw new InvalidOperationException();
            }

            this.root = this.Delete(root, key);

            if (this.root != null)
            {
                this.root.Color = Black;
            }
        }

        public void DeleteMin()
        {
            if (this.root == null)
            {
                throw new InvalidOperationException();
            }

            this.root = this.DeleteMin(this.root);
        }

        public void DeleteMax()
        {
            if (this.root == null)
            {
                throw new InvalidOperationException();
            }

            this.root = this.DeleteMax(this.root);
        }

        public int Count()
        {
            return this.Count(this.root);
        }

        private int Count(Node node)
        {
            if (node == null)
            {
                return 0;
            }

            return 1 + this.Count(node.Left) + this.Count(node.Right);
        }

        // Rotations

        private void FlipColors(Node node)
        {
            node.Color = !node.Color;
            node.Left.Color = !node.Left.Color;
            node.Right.Color = !node.Right.Color;
        }

        private bool IsRed(Node node)
        {
            if (node == null)
            {
                return false;
            }

            return node.Color == Red;
        }

        private Node RotateRight(Node node)
        {
            var temp = node.Left;
            node.Left = temp.Right;
            temp.Right = node;
            temp.Color = temp.Right.Color;
            temp.Right.Color = Red;

            return temp;
        }

        private Node RotateLeft(Node node)
        {
            var temp = node.Right;
            node.Right = temp.Left;
            temp.Left = node;
            temp.Color = temp.Left.Color;
            temp.Left.Color = Red;

            return temp;
        }

        // Helper Methods

        private bool IsLesser(T a, T b) => a.CompareTo(b) < 0;

        private bool AreEqual(T a, T b) => a.CompareTo(b) == 0;

        private void PreOrderCopy(Node node)
        {
            if (node == null)
            {
                return;
            }

            this.Insert(node.Value);
            this.PreOrderCopy(node.Left);
            this.PreOrderCopy(node.Right);
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

        private Node FindNode(Node node, T element)
        {
            var current = node;

            while (current != null)
            {
                if (IsLesser(element, current.Value))
                {
                    current = current.Left;
                }
                else if (IsLesser(current.Value, element))
                {
                    current = current.Right;
                }
                else
                {
                    break;
                }
            }

            return current;
        }

        private Node Insert(Node node, T element)
        {
            if (node == null)
            {
                return new Node(element);
            }

            if (IsLesser(element, node.Value))
            {
                node.Left = this.Insert(node.Left, element);
            }
            else
            {
                node.Right = this.Insert(node.Right, element);
            }

            node = FixUp(node);

            return node;
        }

        private Node Delete(Node node, T key)
        {
            if (IsLesser(key, node.Value))
            {
                if (!IsRed(node.Left) && !IsRed(node.Left.Left))
                {
                    node = this.MoveRedLeft(node);
                }

                node.Left = this.Delete(node.Left, key);
            }
            else
            {
                if (IsRed(node.Left))
                {
                    node = this.RotateRight(node);
                }

                if (node.Right == null && AreEqual(key, node.Value))
                {
                    return null;
                }

                if (!IsRed(node.Right) && !IsRed(node.Right.Left))
                {
                    node = this.MoveRedRight(node);
                }

                if (AreEqual(node.Value, key))
                {
                    node.Value = this.FindMinimalValueInSubTree(node.Right);
                    node.Right = this.DeleteMin(node.Right);
                }
                else
                {
                    node.Right = this.Delete(node.Right, key);
                }
            }

            return FixUp(node);
        }

        private T FindMinimalValueInSubTree(Node node)
        {
            if (node.Left == null)
            {
                return node.Value;
            }

            return FindMinimalValueInSubTree(node.Left);
        }

        private Node DeleteMin(Node node)
        {
            if (node.Left == null)
            {
                return null;
            }

            if (!IsRed(node.Left) && !IsRed(node.Left.Left))
            {
                node = this.MoveRedLeft(node);
            }

            node.Left = this.DeleteMin(node.Left);

            return FixUp(node);
        }

        private Node FixUp(Node node)
        {
            if (IsRed(node.Right))
            {
                node = this.RotateLeft(node);
            }

            if (IsRed(node.Left) && IsRed(node.Left.Left))
            {
                node = this.RotateRight(node);
            }

            if (IsRed(node.Left) && IsRed(node.Right))
            {
                FlipColors(node);
            }

            return node;
        }

        private Node MoveRedLeft(Node node)
        {
            FlipColors(node);

            if (IsRed(node.Right.Left))
            {
                node.Right = this.RotateRight(node.Right);
                node = this.RotateLeft(node);

                FlipColors(node);
            }

            return node;
        }

        private Node DeleteMax(Node node)
        {
            if (IsRed(node.Left))
            {
                node = this.RotateRight(node);
            }

            if (node.Right == null)
            {
                return null;
            }

            if (!IsRed(node.Right) && !IsRed(node.Right.Left))
            {
                node = this.MoveRedRight(node);
            }

            node.Right = this.DeleteMax(node.Right);

            return FixUp(node);
        }

        private Node MoveRedRight(Node node)
        {
            FlipColors(node);

            if (IsRed(node.Left.Left))
            {
                node = this.RotateRight(node);
                FlipColors(node);
            }

            return node;
        }
    }
}