using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TupleLab
{
    public class Program
    {
        private const float MAX_SCORE = 10f;
        
        public static void Main()
        {
            if (File.Exists("input.txt") == false)
            {
                throw new FileNotFoundException("Input file does not exist! Let your instructor know ASAP!");
            }

            String[] lines = File.ReadAllLines("input.txt");

            float score = MAX_SCORE;
            float ptsPerTestCase = MAX_SCORE / (lines.Length / 3);
            Source src = new Source();

            for (int i = 0; i < lines.Length; i += 3)
            {
                (int, int) vars = (Int32.Parse(lines[i].Trim().Split()[0]), Int32.Parse(lines[i].Trim().Split()[1]));
                List<Boolean> testCases = lines[i + 1].Trim().Split().Select(x => x == "1").ToList();
                int expectedAnswer = Int32.Parse(lines[i + 2].Trim());
                int studentAnswer = src.Run(vars, testCases);

                if (studentAnswer != expectedAnswer)
                {
                    score -= ptsPerTestCase;
                    ConsoleColor c = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Test case {i / 3 + 1} failed.");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"You returned {studentAnswer} but expected {expectedAnswer}.");
                    Console.ForegroundColor = c;
                }
                else
                {
                    ConsoleColor c = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Test case {i / 3 + 1} passed.");
                    Console.ForegroundColor = c;
                }

                if (i + 3 < lines.Length)
                {
                    Console.WriteLine("---------------------");
                }
            }

            Console.Write("\n*** ");
            Console.WriteLine($"Final score: {score}/{MAX_SCORE} | {Math.Round(score / MAX_SCORE * 100, 2)}%");
        }
    }
}