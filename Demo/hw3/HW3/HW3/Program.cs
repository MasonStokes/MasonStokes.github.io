using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW3
{
    class Program
    {
        /// <summary>
        /// This will print the binary repesentation of all numbers from 1 up to n
        /// By doing this it will design a virtual binary tree that will perform
        /// a level order traversal using the FIFO queue. 
        /// Result should be like this:
        ///         1
        ///     /       \
        ///    10       11
        ///   /  \     /  \
        /// 100  101 110  111
        ///
        /// etc.
        /// which will store each value that was visited in the list.
        /// </summary>
        static LinkedList<string> generateBinaryRepresentationList(int n)
        {
            /// <summary>
            /// Creates an empty queue of strings
            /// </summary>
            LinkedQueue<StringBuilder> x = new LinkedQueue<StringBuilder>();

            /// <summary>
            /// The list is returning the binary values
            /// </summary>
            LinkedList<string> output = new LinkedList<string>();

            if (n < 1)
            {
                /// <summary>
                /// binary values and representation do not support negative values
                /// return an empty list
                /// </summary>
                return output;
            }

            /// <summary>
            /// This will add the first binary number. This uses a dynamic string
            /// to avoid string concat.
            /// </summary>
            x.push(new StringBuilder("1"));

            while (n > 0)
            {
                /// <summary>
                /// print the first part of the queue
                /// </summary>
                StringBuilder sb = x.pop();
                output.AddLast(sb.ToString());

                /// < summary >
                /// Make a copy
                /// </summary>
                StringBuilder sbc = new StringBuilder(sb.ToString());

                /// <summary>
                /// Left Child
                /// </summary>
                sb.Append("0");
                x.push(sb);

                /// <summary>
                /// Right Child
                /// </summary>
                sbc.Append("1");
                x.push(sbc);
                n--;

            }
            return output;

        }

        /// <summary>
        /// This function will drive the program. It starts with checking for a valid value to convert
        /// and it prints to console an example. 
        /// </summary>
        /// <param name="args"></param>
        static void Main(String[] args)
        {
            
            int n = 10;

            if (args.Length < 1)
            {
                Console.WriteLine("Please invoke with the max value to print binary up to, like this:");
                Console.WriteLine("\t Main 12");
                return;
            }
            try
            {
                n = Int16.Parse(args[0]);
            }
            catch (FormatException e)
            {
                Console.WriteLine("I'm sorry, I can't understand the number: " + args[0]);
                return;
            }

            
            LinkedList<String> output = generateBinaryRepresentationList(n);


            int maxLength = output.Count()
                ;
            foreach (String s in output)
            {
                for (int i = 0; i < maxLength - s.Length; i++)
                {
                    Console.WriteLine(" ");
                }
                Console.WriteLine(s);
            }
        }
    }

}