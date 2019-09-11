using System.Collections.Generic;

namespace Indexed_DataStructures
{
    internal sealed class Tree<T>
    {
        Node<T> root;
        private IComparer<T> comparer;
        private int count;

        public Tree(IComparer<T> comparer)
        {
            this.comparer = comparer;
        }

        public bool AddIfNotPresent(T item)
        {
            if (this.root == null)
            {
                this.root = new Node<T>(item);
                this.count = 1;
                return true;
            }
            Node<T> node = this.root;
            Node<T> parent = this.root;
            int x = 0;
            while (node != null)
            {
                x = this.comparer.Compare(item, node.Item);
                if (x == 0)
                {
                    return false;
                }
                var temp = node;
                node = x > 0 ? parent.Right : parent.Left;
                parent = temp;
            }
            node = new Node<T>(item);
            if (x > 0)
            {
                parent.Right = node;
            }
            else
            {
                parent.Left = node;
            }
            this.count++;
            return true;
        }

        public IEnumerator<T> DFS()
        {
            List<T> list = new List<T>();
            this.DFS(this.root, list);
            return list.GetEnumerator();
        }

        private void DFS(Node<T> node, List<T> list)
        {
            if (node == null)
            {
                return;
            }
            DFS(node.Left, list);
            list.Add(node.Item);
            DFS(node.Right, list);
        }
    }
}
