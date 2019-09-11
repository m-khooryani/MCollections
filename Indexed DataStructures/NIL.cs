namespace Indexed_DataStructures
{
    internal class NIL<T>
    {
        static NIL()
        {
            Instance = new Node<T>();
        }

        public static Node<T> Instance { get; private set; }
    }
}
