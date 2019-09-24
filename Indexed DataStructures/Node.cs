namespace Indexed_DataStructures
{
    internal sealed class Node<T>
    {
        public T Item { get; internal set; }
        public Node<T> Left { get; set; }
        public Node<T> Right { get; set; }
        public Node<T> Parent { get; set; }
        public Color Color { get; set; }
        public int Count;

        public Node(T item)
        {
            this.Item = item;
        }

        internal Node()
        {

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

        internal bool IsNil()
        {
            return this == NIL<T>.Instance;
        }

        internal bool IsBlack()
        {
            return !this.IsRed();
        }
    }

    internal enum Color : byte
    {
        BLACK = 0,
        RED = 1,
    }
}
