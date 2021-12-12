using System;
using System.Collections.Generic;

namespace BooleanAlgebra
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\t\t\t\tBoolean Algebra Calculator\n\t\t\t\tSE-21-1\n\t\t\t\tBy Mikhail Zhmaytsev\n");
            Console.ResetColor();

            while (true)
            {
                /*Создание экземпляра класса BooleanFunctions*/
                BooleanFunctions booleanFunctions = new BooleanFunctions();

                /*Ввод формулы*/
                Console.WriteLine("\nNotation keys: ");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("\tDisjunction — \"+\" | Conjunction — \"*\" | Negation — \"!\" | Equivalence — \"~\" | Implication — \">\" or \"<\"");
                Console.ResetColor();
                Console.Write("Formula: ");
                Console.ForegroundColor = ConsoleColor.Red;
                string formula = Console.ReadLine();
                char[] variables = BooleanFunctions.GetAllVariablesFromString(formula);
                Console.ResetColor();

                /*Ввод значений для соответствующих переменных*/
                Console.WriteLine("Enter values of following variables separated by space:");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write($"\t{string.Join(" ", variables)}\n\t");
                int[] values = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);
                booleanFunctions.SetVariables(variables, values);
                Console.ResetColor();

                /*Выводим результат*/
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\nResult: {RPN.Calculate(formula, booleanFunctions)}\n");
                Console.ResetColor();
            }
        }
    }
}
