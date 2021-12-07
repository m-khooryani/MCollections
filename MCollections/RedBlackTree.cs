using System;
using System.Collections.Generic;

namespace MCollections
{
    internal class RedBlackTree<T> : ISelfBalanceTree<T>
    {
        internal Node<T> root;
        public IComparer<T> Comparer { get; private set; }
        private readonly Node<T> nilInstance;

        public RedBlackTree(IComparer<T> comparer)
        {
            this.Comparer = comparer;
            this.nilInstance = new NilNode<T>();
            this.root = nilInstance;
        }

        public int Count => this.root.Count;

        public T Max => GetMax();

        public T Min => GetMin();

        Node<T> ISelfBalanceTree<T>.root => root;

        private T GetMin()
        {
            if (this.root.IsNil())
            {
                return default;
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
                return default;
            }
            Node<T> root = this.root;
            while (!root.Right.IsNil())
            {
                root = root.Right;
            }
            return root.Item;
        }

        public virtual bool AddIfNotPresent(T item)
        {
            var y = nilInstance;
            var x = this.root;
            Node<T> z = new Node<T>(item)
            {
                Left = nilInstance,
                Right = nilInstance
            };

            int c;
            while (!x.IsNil())
            {
                y = x;
                c = this.Comparer.Compare(z.Item, x.Item);
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
            c = this.Comparer.Compare(z.Item, y.Item);
            if (c < 0)
            {
                y.Left = z;
            }
            else
            {
                y.Right = z;
            }
            z.Left = nilInstance;
            z.Right = nilInstance;
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

        public bool Contains(T item)
        {
            return this.Search(item) != null;
        }

        public void Clear()
        {
            this.root = nilInstance;
        }

        public T GetByIndex(int index)
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
        
        public T SetByIndex(int index, T value)
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
                    node.Item = value;
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

        public void SymmetricExceptWith(IEnumerable<T> other)
        {
            if (other == null)
            {
                throw new ArgumentNullException("other");
            }
            HashSet<T> set = new HashSet<T>(other);
            foreach (T local in set)
            {
                if (!this.Remove(local))
                {
                    this.AddIfNotPresent(local);
                }
            }
        }

        public void UnionWith(IEnumerable<T> other)
        {
            if (other == null)
            {
                throw new ArgumentNullException("other");
            }
            foreach (var item in other)
            {
                this.AddIfNotPresent(item);
            }
        }

        public bool SetEquals(IEnumerable<T> other)
        {
            if (other == null)
            {
                throw new ArgumentNullException("other");
            }
            HashSet<T> set = new HashSet<T>(other);
            if (set.Count != this.Count)
            {
                return false;
            }
            foreach (var item in set)
            {
                if (!this.Contains(item))
                {
                    return false;
                }
            }
            return true;
        }

        public bool Overlaps(IEnumerable<T> other)
        {
            if (other == null)
            {
                throw new ArgumentNullException("other");
            }
            foreach (var item in other)
            {
                if (this.Contains(item))
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsProperSupersetOf(IEnumerable<T> other)
        {
            if (other == null)
            {
                throw new ArgumentNullException("other");
            }
            HashSet<T> set = new HashSet<T>(other);
            if (this.Count <= set.Count)
            {
                return false;
            }
            foreach (var item in set)
            {
                if (!this.Contains(item))
                {
                    return false;
                }
            }
            return true;
        }

        public bool IsProperSubsetOf(IEnumerable<T> other)
        {
            if (other == null)
            {
                throw new ArgumentNullException("other");
            }
            HashSet<T> set = new HashSet<T>(other);
            if (set.Count <= this.Count)
            {
                return false;
            }
            var enumerator = this.InOrder();
            while (enumerator.MoveNext())
            {
                if (!set.Contains(enumerator.Current))
                {
                    return false;
                }
            }
            return true;
        }

        public bool IsSuperSetOf(IEnumerable<T> other)
        {
            if (other == null)
            {
                throw new ArgumentNullException("other");
            }
            foreach (var item in other)
            {
                if (!this.Contains(item))
                {
                    return false;
                }
            }
            return true;
        }

        public bool IsSubsetOf(IEnumerable<T> other)
        {
            if (other == null)
            {
                throw new ArgumentNullException("other");
            }
            if (this.Count == 0)
            {
                return true;
            }
            HashSet<T> set = new HashSet<T>(other);
            var enumerator = this.InOrder();
            while (enumerator.MoveNext())
            {
                if (!set.Contains(enumerator.Current))
                {
                    return false;
                }
            }
            return true;
        }

        public void IntersectWith(IEnumerable<T> other)
        {
            if (other == null)
            {
                throw new ArgumentNullException("other");
            }
            if (Count != 0)
            {
                if (!ReferenceEquals(other, this))
                {
                    if (!(other is IndexedSet<T> set && ReferenceEquals(set.tree, this)))
                    {
                        RedBlackTree<T> tree = new RedBlackTree<T>(this.Comparer);
                        foreach (var item in other)
                        {
                            if (this.Contains(item))
                            {
                                tree.AddIfNotPresent(item);
                            }
                        }
                        this.root = tree.root;
                    }
                }
            }
        }

        public void ExceptWith(IEnumerable<T> other)
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
                    IndexedSet<T> set = other as IndexedSet<T>;
                    if (set != null && ReferenceEquals(set.tree, this))
                    {
                        this.Clear();
                    }
                    if ((set != null) && this.HasEqualComparer(set))
                    {
                        if (this.Comparer.Compare(set.Max, this.Min) < 0)
                        {
                            return;
                        }
                        else if (this.Comparer.Compare(set.Min, this.Max) > 0)
                        {
                            return;
                        }
                        else
                        {
                            T min = this.Min;
                            T max = this.Max;
                            foreach (T item in other)
                            {
                                if (this.Comparer.Compare(item, min) >= 0)
                                {
                                    if (this.Comparer.Compare(item, max) > 0)
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
                if (this.Comparer.Compare(item, min) < 0)
                {
                    continue;
                }
                if (this.Comparer.Compare(item, max) <= 0)
                {
                    this.Remove(item);
                }
            }
        }

        private bool HasEqualComparer(IndexedSet<T> other)
        {
            return (object.ReferenceEquals(this.Comparer, other.tree.Comparer) || this.Comparer.Equals(other.tree.Comparer));
        }

        private void LeftRotate(Node<T> x)
        {
            Node<T> y = x.Right;
            x.Right = y.Left;
            if (!y.Left.IsNil())
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

        private void Transplant(Node<T> u, Node<T> v)
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
                int c = this.Comparer.Compare(item, node.Item);
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

        private void Remove(Node<T> z)
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
            while (x != root && x.IsBlack())
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

        public IEnumerator<T> InOrder()
        {
            if (root.IsNil())
            {
                yield break;
            }
            Stack<Node<T>> stack = new Stack<Node<T>>();
            Node<T> node = root;
            while ((!node.IsNil()) || stack.Count > 0)
            {
                while (!node.IsNil())
                {
                    stack.Push(node);
                    node = node.Left;
                }
                node = stack.Pop();
                yield return node.Item;
                node = node.Right;
            }
        }

        public Node<T> Search(T item)
        {
            Node<T> node = this.root;
            int c;
            while (!node.IsNil())
            {
                c = this.Comparer.Compare(item, node.Item);
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
                    return node;
                }
            }
            return null;
        }
    }
}