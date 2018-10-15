using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW3
{
    class LinkedQueue<T> : IQueue<T>
    {
        /// <summary>
        /// Thes variables are the first reference points of the queue.
        /// the front is the first position of the queue and the rear
        /// is the last position in the queue.
        /// </summary>
        private Node<T> front;
        private Node<T> rear;

        public LinkedQueue()
        {
        }
        
        /// <summary>
        /// This function checks if the queue is empty.
        /// </summary>
        /// <returns>Returns true if the queue is empty and false if it isn't.</returns>
        public bool isEmpty()
        {
            if (front == null && rear == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// This function will add an item to the end of the queue.
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public T push(T element)
        {
            if (element == null)
            {
                throw new System.NullReferenceException();
            }
            if (isEmpty())
            {
                Node<T> tmp = new Node<T>(element, null);
                front = tmp;
                rear = front;
            }
            else
            {
                Node<T> tmp = new Node<T>(element, null);
                rear.next = tmp;
                rear = rear.next;
            }

            return element;
        }

        /// <summary>
        /// This function will remove an item from the front of the queue.
        /// </summary>
        /// <returns>Data from the from of the queue.</returns>
        public T pop()
        {
            T tmp = default(T);
            if (isEmpty())
            {
                throw new QueueUnderflowException("The queue was empty when pop was invoked.");
            }
            else if (front == rear)
            {
                tmp = front.data;
                front = null;
                rear = null;
            }
            else
            {
                tmp = front.data;
                front = front.next;
            }
            return tmp;
        }

    }
}
