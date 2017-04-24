using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMPSearch
{
    class Program
    {
        public int[] computeFail(String pattern)
        {
            int[] fail = new int[pattern.Length];
            fail[0] = 0;

            int m = pattern.Length;
            int j = 0;
            int i = 1;

            while (i < m)
            {
                if (pattern[j] == pattern[i])
                {
                    fail[i] = j + 1;
                    i++;
                    j++;
                }
                else if (j > 0)
                {
                    j = fail[j - 1];
                }
                else
                {
                    fail[i] = 0;
                    i++;
                }
            }
            return fail;
        }

        public int kmpMatch(String text, String pattern)
        {
            int n = text.Length;
            int m = pattern.Length;

            int[] fail = computeFail(pattern);

            int i = 0;
            int j = 0;

            while (i < n)
            {
                if (pattern[j] == text[i])
                {
                    if (j == m - 1)
                    {
                        return i - m + 1;
                    }
                    i++;
                    j++;
                }
                else if (j > 0)
                {
                    j = fail[j - 1];
                }
                else
                {
                    i++;
                }
            }
            return -1;
        }

        public static void Main(string[] args)
        {
            int posn;
            Program search = new Program();
            posn = search.kmpMatch("Ada Agung", "Agung");
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
