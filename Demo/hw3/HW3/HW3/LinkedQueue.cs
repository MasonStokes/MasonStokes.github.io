using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW3
{
    class LinkedQueue<T> : IQueue<T>
    {
        private Node<T> front;
        private Node<T> rear;

        public LinkedQueue()
        {
        }

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
                read = front;
            }
            else
            {
                Node<T> tmp = new Node<T>(element, null);
                rear.next = tmp;
                reat = rear.next;
            }

            return element;
        }

        public T pop()
        {
            T tmp = null;
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
                front = front.next();
            }
            return tmp;
        }

    }
}
