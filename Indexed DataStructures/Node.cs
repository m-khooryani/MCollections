namespace Indexed_DataStructures
{
    internal sealed class Node<T>
    {
        public T Item { get; private set; }
        public Node<T> Left { get; set; }
        public Node<T> Right { get; set; }

        public Node(T item)
        {
            this.Item = item;
        }
    }
}
