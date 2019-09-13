using System;
using System.Collections.Generic;

namespace Indexed_DataStructures
{
    internal sealed class Tree<T>
    {
        Node<T> root = NIL<T>.Instance;
        private readonly IComparer<T> comparer;

        public Tree(IComparer<T> comparer)
        {
            this.comparer = comparer;
        }

        public int Count => this.root.Count;

        public T Max => GetMax();

        public T Min => GetMin();

        private T GetMin()
        {
            if (this.root.IsNil())
            {
                return default(T);
            }
            Node<T> root = this.root;
            while (!root.Left.IsNil())
            {
                root = root.Left;
            }
            return root.Item;
        }

        private T GetMax()
        {
            if (this.root.IsNil())
            {
                return default(T);
            }
            Node<T> root = this.root;
            while (!root.Right.IsNil())
            {
                root = root.Right;
            }
            return root.Item;
        }

        public bool AddIfNotPresent(T item)
        {
            var y = NIL<T>.Instance;
            var x = this.root;
            Node<T> z = new Node<T>(item)
            {
                Left = NIL<T>.Instance,
                Right = NIL<T>.Instance
            };

            int c;
            while (!x.IsNil())
            {
                y = x;
                c = this.comparer.Compare(z.Item, x.Item);
                if (c == 0)
                {
                    return false;
                }
                else if (c < 0)
                {
                    x = x.Left;
                }
                else
                {
                    x = x.Right;
                }
            }
            z.Parent = y;
            if (y.IsNil())
            {
                this.root = z;
            }
            c = this.comparer.Compare(z.Item, y.Item);
            if (c < 0)
            {
                y.Left = z;
            }
            else
            {
                y.Right = z;
            }
            z.Left = NIL<T>.Instance;
            z.Right = NIL<T>.Instance;
            z.MarkRed();
            var temp = z;
            while (!temp.IsNil())
            {
                temp.Count++;
                temp = temp.Parent;
            }
            Balance(z);
            return true;
        }

        internal bool Contains(T item)
        {
            return this.Search(item) != null;
        }

        internal void Clear()
        {
            this.root = NIL<T>.Instance;
        }

        internal T GetNthItem(int index)
        {
            Node<T> node = this.root;
            while (true)
            {
                if (index < node.Left.Count)
                {
                    node = node.Left;
                }
                else if (index > node.Left.Count)
                {
                    index -= node.Left.Count + 1;
                    node = node.Right;
                }
                else
                {
                    return node.Item;
                }
            }
        }

        private void Balance(Node<T> z)
        {
            Node<T> y;
            while (z.Parent.IsRed())
            {
                if (z.Parent == z.Parent.Parent.Left)
                {
                    y = z.Parent.Parent.Right;
                    if (y.IsRed())
                    {
                        z.Parent.MarkBlack();
                        y.MarkBlack();
                        z.Parent.Parent.MarkRed();
                        z = z.Parent.Parent;
                    }
                    else
                    {
                        if (z == z.Parent.Right)
                        {
                            z = z.Parent;
                            LeftRotate(z);
                        }
                        z.Parent.MarkBlack();
                        z.Parent.Parent.MarkRed();
                        RightRotate(z.Parent.Parent);
                    }
                }
                else
                {
                    y = z.Parent.Parent.Left;
                    if (y.IsRed())
                    {
                        z.Parent.MarkBlack();
                        y.MarkBlack();
                        z.Parent.Parent.MarkRed();
                        z = z.Parent.Parent;
                    }
                    else
                    {
                        if (z == z.Parent.Left)
                        {
                            z = z.Parent;
                            RightRotate(z);
                        }
                        z.Parent.MarkBlack();
                        z.Parent.Parent.MarkRed();
                        LeftRotate(z.Parent.Parent);
                    }
                }
            }
            this.root.MarkBlack();
        }

        internal void ExceptWith(IEnumerable<T> other)
        {
            if (other == null)
            {
                throw new ArgumentNullException("other");
            }
            if (Count != 0)
            {
                if (ReferenceEquals(other, this))
                {
                    this.Clear();
                }
                else
                {
                    IndexedSortedSet<T> set = other as IndexedSortedSet<T>;
                    if(set != null && ReferenceEquals(set.tree, this))
                    {
                        this.Clear();
                    }
                    if ((set != null) && this.HasEqualComparer(set))
                    {
                        if (this.comparer.Compare(set.Max, this.Min) < 0)
                        {
                            return;
                        }
                        else if (this.comparer.Compare(set.Min, this.Max) > 0)
                        {
                            return;
                        }
                        else
                        {
                            T min = this.Min;
                            T max = this.Max;
                            foreach (T item in other)
                            {
                                if (this.comparer.Compare(item, min) >= 0)
                                {
                                    if (this.comparer.Compare(item, max) > 0)
                                    {
                                        break;
                                    }
                                    this.Remove(item);
                                }
                            }
                            return;
                        }
                    }
                    this.RemoveAllElements(other);
                }
            }
        }

        private void RemoveAllElements(IEnumerable<T> other)
        {
            T min = this.Min;
            T max = this.Max;
            foreach (T item in other)
            {
                if (this.comparer.Compare(item, min) < 0)
                {
                    continue;
                }
                if ((this.comparer.Compare(item, max) <= 0))
                {
                    this.Remove(item);
                }
            }
        }

        private bool HasEqualComparer(IndexedSortedSet<T> other)
        {
            return (object.ReferenceEquals(this.comparer, other.tree.comparer) || this.comparer.Equals(other.tree.comparer));
        }

        private void LeftRotate(Node<T> x)
        {
            Node<T> y = x.Right;
            x.Right = y.Left;
            if(!y.Left.IsNil())
            {
                y.Left.Parent = x;
            }
            y.Parent = x.Parent;
            if (x.Parent.IsNil())
            {
                this.root = y;
            }
            else if (x == x.Parent.Left)
            {
                x.Parent.Left = y;
            }
            else
            {
                x.Parent.Right = y;
            }
            y.Left = x;
            x.Parent = y;

            y.Count = x.Count;
            x.Count = x.Left.Count + x.Right.Count + 1;
        }

        private void RightRotate(Node<T> x)
        {
            Node<T> y = x.Left;
            x.Left = y.Right;
            if (!y.Right.IsNil())
            {
                y.Right.Parent = x;
            }
            y.Parent = x.Parent;
            if (x.Parent.IsNil())
            {
                this.root = y;
            }
            else if (x == x.Parent.Right)
            {
                x.Parent.Right = y;
            }
            else
            {
                x.Parent.Left = y;
            }
            y.Right = x;
            x.Parent = y;

            y.Count = x.Count;
            x.Count = x.Left.Count + x.Right.Count + 1;
        }

        public void Transplant(Node<T> u, Node<T> v)
        {
            if (u.Parent.IsNil())
            {
                root = v;
            }
            else if (u == u.Parent.Left)
            {
                u.Parent.Left = v;
            }
            else
            {
                u.Parent.Right = v;
            }
            v.Parent = u.Parent;
        }

        public bool Remove(T item)
        {
            Node<T> node = root;
            while (!node.IsNil())
            {
                int c = this.comparer.Compare(item, node.Item);
                if (c < 0)
                {
                    node = node.Left;
                }
                else if (c > 0)
                {
                    node = node.Right;
                }
                else
                {
                    Remove(node);
                    return true;
                }
            }
            return false;
        }

        public void Remove(Node<T> z)
        {
            var y = z;
            var yOriginalColor = y.Color;
            Node<T> x;
            if (z.Left.IsNil())
            {
                var temp = z;
                while (!temp.IsNil())
                {
                    temp.Count--;
                    temp = temp.Parent;
                }
                x = z.Right;
                Transplant(z, z.Right);
            }
            else if (z.Right.IsNil())
            {
                var temp = z;
                while (!temp.IsNil())
                {
                    temp.Count--;
                    temp = temp.Parent;
                }
                x = z.Left;
                Transplant(z, z.Left);
            }
            else
            {
                y = Minimum(z.Right);
                var temp = y;
                while (!temp.IsNil())
                {
                    temp.Count--;
                    temp = temp.Parent;
                }
                y.Count = z.Count;
                yOriginalColor = y.Color;
                x = y.Right;
                if (y.Parent == z)
                {
                    x.Parent = y;
                }
                else
                {
                    Transplant(y, y.Right);
                    y.Right = z.Right;
                    y.Right.Parent = y;
                }
                Transplant(z, y);
                y.Left = z.Left;
                y.Left.Parent = y;
                y.Color = z.Color;
            }
            if (yOriginalColor == Color.BLACK)
            {
                DeleteBalance(x);
            }
        }

        private void DeleteBalance(Node<T> x)
        {
            while(x!=root && x.IsBlack())
            {
                if (x == x.Parent.Left)
                {
                    var w = x.Parent.Right;
                    if (w.IsRed())
                    {
                        w.MarkBlack();
                        x.Parent.MarkRed();
                        LeftRotate(x.Parent);
                        w = x.Parent.Right;
                    }
                    if (w.Left.IsBlack() && w.Right.IsBlack())
                    {
                        w.MarkRed();
                        x = x.Parent;
                    }
                    else
                    {
                        if (w.Right.IsBlack())
                        {
                            w.Left.MarkBlack();
                            w.MarkRed();
                            RightRotate(w);
                            w = x.Parent.Right;
                        }
                        w.Color = x.Parent.Color;
                        x.Parent.MarkBlack();
                        w.Right.MarkBlack();
                        LeftRotate(x.Parent);
                        x = root;
                    }
                }
                else
                {
                    var w = x.Parent.Left;
                    if (w.IsRed())
                    {
                        w.MarkBlack();
                        x.Parent.MarkRed();
                        RightRotate(x.Parent);
                        w = x.Parent.Left;
                    }
                    if (w.Right.IsBlack() && w.Left.IsBlack())
                    {
                        w.MarkRed();
                        x = x.Parent;
                    }
                    else
                    {
                        if (w.Left.IsBlack())
                        {
                            w.Right.MarkBlack();
                            w.MarkRed();
                            LeftRotate(w);
                            w = x.Parent.Left;
                        }
                        w.Color = x.Parent.Color;
                        x.Parent.MarkBlack();
                        w.Left.MarkBlack();
                        RightRotate(x.Parent);
                        x = root;
                    }
                }
            }
            x.MarkBlack();
        }

        private Node<T> Minimum(Node<T> node)
        {
            while (!node.Left.IsNil())
            {
                node = node.Left;
            }
            return node;
        }

        public IEnumerator<T> DFS()
        {
            List<T> list = new List<T>();
            this.DFS(this.root, list);
            return list.GetEnumerator();
        }

        private void DFS(Node<T> node, List<T> list)
        {
            if (node.IsNil())
            {
                return;
            }
            DFS(node.Left, list);
            list.Add(node.Item);
            DFS(node.Right, list);
        }

        public Node<T> Search(T item)
        {
            Node<T> node = this.root;
            int c;
            while (!node.IsNil())
            {
                c = this.comparer.Compare(item, node.Item);
                if (c < 0)
                {
                    node = node.Left;
                }
                else if(c > 0)
                {
                    node = node.Right;
                }
                else
                {
                    return node;
                }
            }
            return null;
        }
    }
}
