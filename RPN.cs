using System;
using System.Collections.Generic;

namespace BooleanAlgebra
{
    class RPN
    {
        //Метод возвращает true, если проверяемый символ - оператор
        static private bool IsOperator(char с)
        {
            if ("+*!~><()".IndexOf(с) != -1)
            {
                return true;
            }

            return false;
        }

        //Метод возвращает приоритет оператора
        static private byte GetPriority(char s)
        {
            return s switch
            {
                '(' => 0,
                ')' => 1,
                '~' => 2,
                '>' => 3,
                '<' => 3,
                '+' => 4,
                '*' => 5,
                '!' => 6,
                _ => 7,
            };
        }

        //Метод преобразовывает выражение в обратную польскую запись (постфиксную)
        static private string ConvertToRPN(string input)
        {
            string output = string.Empty; //Строка для хранения выражения
            Stack<char> stackForOperations = new Stack<char>(); //Стек для хранения операторов

            for (int i = 0; i < input.Length; i++)
            {
                //Пробел пропускаем
                if (" ".IndexOf(input[i]) != -1)
                {
                    continue; //Переходим к следующему символу
                }

                //Если символ - буква, то добавляем к строке хранения выражения
                if (char.IsLetter(input[i]))
                {
                    output += input[i];
                    i++;

                    if (i == input.Length)
                    {
                        break; //Если символ - последний, то выходим из цикла
                    }
                }

                //Если символ - оператор
                if (IsOperator(input[i]))
                {
                    if (input[i] == '(')
                    {
                        stackForOperations.Push(input[i]); //Записываем открывающую скобку в стек
                    }
                    else if (input[i] == ')')
                    {
                        //Выписываем все операторы до открывающей скобки в строку
                        char s = stackForOperations.Pop();

                        while (s != '(')
                        {
                            output += s.ToString() + ' ';
                            s = stackForOperations.Pop();
                        }
                    }
                    else //Если любой другой оператор
                    {
                        if (stackForOperations.Count > 0) //Если в стеке есть элементы
                        {
                            if (GetPriority(input[i]) <= GetPriority(stackForOperations.Peek()) && input[i] != '!') //И если приоритет нашего оператора меньше или равен приоритету оператора на вершине стека
                            {
                                output += stackForOperations.Pop().ToString() + " "; //То добавляем последний оператор из стека в строку с выражением
                            }
                        }

                        stackForOperations.Push(char.Parse(input[i].ToString())); //Если стек пуст, или же приоритет оператора выше - добавляем операторов на вершину стека
                    }
                }
            }

            //Когда прошли по всем символам, выкидываем из стека все оставшиеся там операторы в строку
            while (stackForOperations.Count > 0)
            {
                output += stackForOperations.Pop() + " ";
            }

            return output; //Возвращаем выражение в постфиксной записи
        }

        //Метод решает полученное выражение
        static private int Counting(string input, BooleanFunctions booleanFunctions)
        {
            int result;
            Stack<int> temp = new Stack<int>(); //Временный стек для решения

            for (int i = 0; i < input.Length; i++)
            {
                //Если символ - буква, то записываем на вершину стека
                if (char.IsLetter(input[i]))
                {
                    temp.Push(booleanFunctions.Variables[input[i]]);
                }
                else if (IsOperator(input[i])) //Если символ - оператор
                {
                    //Берем одно значение из стека
                    int a = temp.Pop();
                    int b;

                    switch (input[i]) //И производим над ними действие, согласно оператору
                    {
                        case '+':
                            b = temp.Pop();
                            Console.WriteLine($"Над {b} и {a} производится действие {input[i]}");
                            result = booleanFunctions.Disjunction(b, a);
                            temp.Push(result);
                            break;
                        case '*':
                            b = temp.Pop();
                            Console.WriteLine($"Над {b} и {a} производится действие {input[i]}");
                            result = booleanFunctions.Conjunction(b, a);
                            temp.Push(result);
                            break;
                        case '~':
                            b = temp.Pop();
                            Console.WriteLine($"Над {b} и {a} производится действие {input[i]}");
                            result = booleanFunctions.Equivalence(b, a);
                            temp.Push(result);
                            break;
                        case '>':
                            b = temp.Pop();
                            Console.WriteLine($"Над {b} и {a} производится действие {input[i]}");
                            result = booleanFunctions.Implication(b, a);
                            temp.Push(result);
                            break;
                        case '<':
                            b = temp.Pop();
                            Console.WriteLine($"Над {a} и {b} производится действие {input[i]}");
                            result = booleanFunctions.Implication(a, b);
                            temp.Push(result);
                            break;
                        case '!':
                            Console.WriteLine($"Над {a} производится действие {input[i]}");
                            result = booleanFunctions.Negation(a);
                            temp.Push(result);
                            break;
                    }
                }
            }

            return temp.Peek(); //Забираем результат всех вычислений из стека и возвращаем его
        }

        //Основной метод класса
        static public int Calculate(string input, BooleanFunctions booleanFunctions)
        {
            string output = ConvertToRPN(input); //Переводим выражение в обратную польскую запись
            int result = Counting(output, booleanFunctions); //Считаем выражение в постфискной записи
            return result; //Возвращаем результат
        }
    }
}
