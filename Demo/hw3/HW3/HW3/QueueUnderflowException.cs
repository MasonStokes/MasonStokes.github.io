using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW3
{
    class QueueUnderflowException : SystemException
    {
        public QueueUnderflowException()
        {
        }
        
        /// <summary>
        /// This will return a message if an illegal operation is performed
        /// in the program.
        /// </summary>
        /// <param name="message"></param>
        public QueueUnderflowException(String message) : base(message)
        {
        }
    }
}
