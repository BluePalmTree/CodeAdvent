using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTests
{
    public class Day1
    {
        public static void Day1_1()
        {
            int[] inputs = Array.ConvertAll(File.ReadAllLines(@"C:\Examples\CodeAdvent\Inputs\2021\Day1.txt"), s => int.Parse(s));
            int increases = 0;

            for (int i = 0; i < inputs.Length; i++)
            {
                if (i+1 < inputs.Length && inputs[i+1] > inputs[i])
                {
                    increases++;
                }
            }

            Console.WriteLine("Increases: " + increases);
        }

        public static void Day1_2()
        {
            int[] inputs = Array.ConvertAll(File.ReadAllLines(@"C:\Examples\CodeAdvent\Inputs\2021\Day1.txt"), s => int.Parse(s));

            List<int> groups = new List<int>();
            int increases = 0;            

            for (int i = 0; i < inputs.Length; i++)
            {
                if (i + 2 < inputs.Length)
                {
                    groups.Add(inputs[i] + inputs[i + 1] + inputs[i + 2]);
                }
                else
                {
                    break;
                }
            }

            for (int i = 1; i < groups.Count; i++)
            {
                if (groups[i] > groups[i - 1])
                {
                    increases++;
                }
            }

            Console.WriteLine("Increases: " + increases);
        }
    }
}
