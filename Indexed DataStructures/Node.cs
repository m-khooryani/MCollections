namespace IndexedCollections
{
    internal class Node<T>
    {
        public T Item { get; internal set; }
        public Node<T> Left { get; set; }
        public Node<T> Right { get; set; }
        public Node<T> Parent { get; set; }
        public Color Color { get; set; }
        public int Count { get; set; }

        public Node(T item)
            : this()
        {
            this.Item = item;
        }

        internal Node()
        {
            // default color is black(0)
        }

        public void MarkRed()
        {
            this.Color = Color.RED;
        }

        public void MarkBlack()
        {
            this.Color = Color.BLACK;
        }

        internal bool IsRed()
        {
            return this.Color == Color.RED;
        }

        virtual internal bool IsNil()
        {
            return false;
        }

        internal bool IsBlack()
        {
            return !this.IsRed();
        }
    }
}
