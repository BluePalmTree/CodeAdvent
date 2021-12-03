using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTests
{
    public static class Day3
    {
        public static void Day3_1()
        {
            string[] inputs = System.IO.File.ReadAllLines(@"C:\Examples\CodeAdvent\Inputs\2021\Day3.txt");

            int[] gamma = new int[inputs[0].Length];            
            int[] epsilon = new int[inputs[0].Length];

            Array.Fill<int>(gamma, 0);
            Array.Fill<int>(epsilon, 0);

            foreach (var input in inputs)
            {
                char[] b = new char[input.Length];
                b = input.ToCharArray();

                for (int i = 0; i < b.Length; i++)
                {
                    int value = int.Parse(b[i].ToString());
                    if (value == 0)
                        gamma[i]--;
                    else
                        gamma[i]++;
                }
            }

            string gammeRate = "";
            string epsilonRate = "";
            foreach(var gam in gamma)
            {
                if (gam > 0)
                {
                    gammeRate += "1";
                    epsilonRate += "0";
                }
                else
                {
                    gammeRate += "0";
                    epsilonRate += "1";
                }
            }

            Console.WriteLine($"Gamma string: {gammeRate} Gamma int: {Convert.ToInt64(gammeRate, 2)}");
            Console.WriteLine($"Epsilon string: {epsilonRate} Epsilon int: {Convert.ToInt64(epsilonRate, 2)}");
            Console.WriteLine($"Power consumption: {Convert.ToInt64(gammeRate, 2) * Convert.ToInt64(epsilonRate, 2)}");
        }

        public static void Day3_2()
        {
            string[] strings = System.IO.File.ReadAllLines(@"C:\Examples\CodeAdvent\Inputs\2021\Day3.txt");            

            var inputs = Array.ConvertAll(strings, s => Convert.ToInt32(s, 2));
            var length = strings[0].Length;

            int zero = 0;
            int one = 0;

            int oxygen = 0;
            int co2scrubber = 0;

            for (int i = length; i > 0; i--)
            {
                zero = 0;
                one = 0;

                foreach (var input in inputs)
                {
                    bool isSet = (input & (1 << i - 1)) != 0;
                    if (isSet)
                        one++;
                    else
                        zero++;
                }
                
                if (one > zero)
                {
                    inputs = inputs.Where(n => (n & (1 << i -1 )) != 0).ToArray();
                }
                else if (one == zero)
                {
                    inputs = inputs.Where(n => (n & (1 << i - 1)) != 0).ToArray();
                }
                else
                {
                    inputs = inputs.Where(n => (n & (1 << i-1)) == 0).ToArray();
                }

                if (inputs.Length == 1)
                {
                    oxygen = inputs[0];
                }
            }

            inputs = Array.ConvertAll(strings, s => Convert.ToInt32(s, 2));

            for (int i = length; i > 0; i--)
            {                
                zero = 0;
                one = 0;

                foreach (var input in inputs)
                {
                    bool isSet = (input & (1 << i - 1)) != 0;
                    if (isSet)
                        one++;
                    else
                        zero++;
                }

                if (one < zero)
                {
                    inputs = inputs.Where(n => (n & (1 << i - 1)) != 0).ToArray();
                }
                else if (one == zero)
                {
                    inputs = inputs.Where(n => (n & (1 << i - 1)) == 0).ToArray();                    
                }
                else
                {
                    inputs = inputs.Where(n => (n & (1 << i - 1)) == 0).ToArray();
                }

                if (inputs.Length == 1)
                {
                    co2scrubber = inputs[0];
                }
            }

            Console.WriteLine($"oxygen: {oxygen} co2scrubber: {co2scrubber} life support rating: {oxygen * co2scrubber}");
        }
    }
}
