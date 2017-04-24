using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMSearch
{
    class Program
    {
        public int[] buildLast(String pattern)
        {
            int[] last = new int[128];
            for (int i = 0; i < 128; i++)
            {
                last[i] = -1;
            }
            for (int i = 0; i < pattern.Length; i++)
            {
                last[pattern[i]] = i;
            }
            return last;
        }
        public int bmMatch(String text, String pattern)
        {
            int[] last = buildLast(pattern);
            int n = text.Length;
            int m = pattern.Length;
            int i = m - 1;
            if (i > n - 1)
            {
                return -1;
            }
            int j = m - 1;
            do
            {
                if (pattern[j] == text[i])
                {
                    if (j == 0)
                    {
                        return i;
                    }
                    else
                    {
                        i--;
                        j--;
                    }
                }
                else
                {
                    int lo = last[text[i]];
                    i = i + m - Math.Min(j, 1 + lo);
                    j = m - 1;
                }
            } while (i <= n - 1);
            return -1;
        }

        static void Main(string[] args)
        {
            int posn;
            Program search = new Program();
            posn = search.bmMatch("Ada Agung", "Agung");
            if (posn == -1)
            {
                Console.WriteLine("Pattern not found");
            }
            else
            {
                Console.WriteLine("Pattern starts at posn " + posn);
            }
            Console.ReadKey();
        }
    }
}
