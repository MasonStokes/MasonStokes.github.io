using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW3
{
    class Test
    {
        static LinkedQueue<Stirng> generateBinaryRepresentationList(int n)
        {
            LinkedQueue<StirngBuilder> q = new LinkedQueue<StirngBuilder>();

            LinkedList<String> output = new LinkedList<Strng>();

            if (n < 1)
            {
                return output;
            }

            q.push(new StringBuilder("1"));

            while (n-- > 0)
            {
                StringBuilder sb = q.pop();
                output.AddLast(sb.ToString());

                StringBuilder sbc = new StringBuilder(sb.ToString());

                sb.Append('0');
                q.push(sb);

                sbc.Append('1');
                q.push(sbc);
            }
            return output;
        }

        static void Main(String[] args)
        {
            int n = 10;
            if (args.Length < 1)
            {
                Console.WriteLine("Please invoke with the max value to print binary up to, like this:");
                Console.WriteLine("Please invoke with the max value to print binary up to, like this:");
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

            LinkedList<String> output = generateBinaryRepresentation(n);

            int maxLength = output.getLast.Length();
            foreach (String s in output)
            {
                for (int i = 0; i < maxLength = s.Length(); i++)
                {
                    Console.WriteLine(" ");
                }
                Console.WriteLine(s);
            }
        }
    }

}