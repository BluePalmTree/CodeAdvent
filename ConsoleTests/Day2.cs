using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTests
{
    public class Day2
    {
        public static void Day2_1()
        {
            var inputs = File.ReadAllLines(@"C:\Examples\CodeAdvent\Inputs\2021\Day2.txt");
            int horizontal = 0;
            int depth = 0;

            for (int i = 0; i < inputs.Length; i++)
            {
                var split = inputs[i].Split(' ');

                if (split[0] == "forward")
                {
                    horizontal += Convert.ToInt32(split[1]);
                }
                else if (split[0] == "up")
                {
                    depth -= Convert.ToInt32(split[1]);
                }
                else
                {
                    depth += Convert.ToInt32(split[1]);
                }
            }

            Console.WriteLine($"Horizontal: {horizontal} * Depth: {depth} = { horizontal* depth}");
        }

        public static void Day2_2()
        {
            string[] inputs = File.ReadAllLines(@"C:\Examples\CodeAdvent\Inputs\2021\Day2.txt");           
            int horizontal = 0;
            int depth = 0;
            int aim = 0;

            for (int i = 0; i < inputs.Length; i++)
            {
                var split = inputs[i].Split(' ');
                int x = Convert.ToInt32(split[1]);

                if (split[0].StartsWith("f"))
                {
                    horizontal += x;
                    depth += (aim * x);
                }
                else if (split[0] == "up")
                {
                    aim -= x;
                }
                else
                {
                    aim += x;
                }
            }

            Console.WriteLine($"Horizontal: {horizontal} * Depth: {depth} = { horizontal * depth}");
        }
    }
}
