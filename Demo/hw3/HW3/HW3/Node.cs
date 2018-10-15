using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW3
{
    class Node<T>
    {
        /// <summary>
        /// These are generic variables which hold the place for new
        /// nodes that can be added.
        /// </summary>
        public T data;
        public Node<T> next;
        
        /// <summary>
        /// Moving the pointer of the node to the next node that is added.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="next"></param>
        public Node(T data, Node<T> next)
        {
            this.data = data;
            this.next = next;
        }
    }
}
