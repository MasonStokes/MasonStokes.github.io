using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW3
{
    interface IQueue<T>
    {
        /// <summary>
        /// Add an element to the rear of the queue
        /// </summary>
        /// <param name="element"></param>
        /// <returns>Return the element that was enqueued</returns>
        T push(T element);

        /// <summary>
        /// Remove and return the front element.
        /// </summary>
        /// <exception>Thrown if the queue is empty</exception>
        T pop();

        /// <summary>
        /// Test if the queue is empty
        /// </summary>
        /// <returns>returns true if the queue is empty; otherwise false</returns>
        bool isEmpty();
    }
}
