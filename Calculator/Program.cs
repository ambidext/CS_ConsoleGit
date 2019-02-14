using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    class Program
    {
        // 괄호 안의 것만 입력으로 
        static string Calc(Stack<string> input)
        {
            string result = null;
            Stack<string> calcStack = new Stack<string>();

            // 곱하기, 나누기 처리 
            foreach(var item in input)
            {
                if (item == "*" || item == "/" || item == "+" || item == "-")
                {
                    calcStack.Push(item);
                }
                else
                {
                    if (calcStack.Count > 0 && calcStack.First() == "*")
                    {
                        int num2 = int.Parse(item);
                        string str = calcStack.Pop();
                        str = calcStack.Pop();
                        int num1 = int.Parse(str);

                        calcStack.Push((num1 * num2).ToString());
                    }
                    else if (calcStack.Count > 0 && calcStack.First() == "/")
                    {
                        int num2 = int.Parse(item);
                        string str = calcStack.Pop();
                        str = calcStack.Pop();
                        int num1 = int.Parse(str);

                        calcStack.Push((num1 / num2).ToString());
                    }
                    else
                    {
                        calcStack.Push(item);
                    }
                }
            }

            // 더하기, 빼기 처리 
            while (true)
            {
                if (calcStack.Count == 1)
                {
                    result = calcStack.Pop();
                    break;
                }

                int num1, num2;
                string sym;
                string str = calcStack.Pop();
                if (str == "-")
                {
                    num1 = 0;
                    str = calcStack.Pop();
                    num2 = int.Parse(str);
                    sym = "-";
                }
                else
                {
                    num1 = int.Parse(str);
                    sym = calcStack.Pop();
                    str = calcStack.Pop();
                    num2 = int.Parse(str);
                }

                int total = 0;
                if (sym == "+")
                    total = num1 + num2;
                else if (sym == "-")
                    total = num1 - num2;
                calcStack.Push(total+"");
            }

            return result;
        }

        static string NormalCalc(string input)
        {
            string result = null;
            //------------------------------
            Queue<string> numQ = new Queue<string>();
            string strNum = "";

            // 숫자 추출
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] >= '0' && input[i] <= '9')
                {
                    strNum += input[i];
                }
                else 
                {
                    Console.WriteLine(strNum);
                    if (strNum != "")
                        numQ.Enqueue(strNum);
                    numQ.Enqueue(input[i]+"");
                    strNum = "";
                }
            }

            Console.WriteLine("Queue:");
            foreach (var item in numQ)
            {
                Console.WriteLine(item);
            }

            // 곱하기, 나누기 연산부터
            Stack<string> numStack = new Stack<string>();
            foreach (var item in numQ)
            {
                if (item == "+")
                {
                    numStack.Push(item);
                }
                else if (item == "-")
                {
                    numStack.Push(item);
                }
                else if (item == "*")
                {
                    numStack.Push(item);
                }
                else if (item == "/")
                {
                    numStack.Push(item);
                }
                else if (item == "(")
                {
                    numStack.Push(item);
                }
                else if (item == ")")
                {
                    Stack<string> inBrace = new Stack<string>();
                    while (true)
                    {
                        string str = numStack.Pop();
                        if (str == "(")
                            break;
                        inBrace.Push(str);
                    }
                    strNum = Calc(inBrace);
                    numStack.Push(strNum);
                }
                else
                {
                    if (numStack.First() == "*")
                    {
                        string temp = numStack.Pop();
                        temp = numStack.Pop();

                        int num1 = int.Parse(item);
                        int num2 = int.Parse(temp);
                        numStack.Push((num1 * num2).ToString());
                    }
                    else if (numStack.Last() == "/")
                    {
                        string temp = numStack.Pop();
                        temp = numStack.Pop();

                        int num1 = int.Parse(item);
                        int num2 = int.Parse(temp);
                        numStack.Push((num1 / num2).ToString());
                    }
                    else
                    {
                        numStack.Push(item);
                    }
                }
            }

            Console.WriteLine("Stack:");
            foreach (var item in numStack)
            {
                Console.WriteLine(item);
            }
            //------------------------------
            return result;
        }

        static string ProgrammerCalc(string input)
        {
            string result = null;
            //------------------------------

            //------------------------------
            return result;
        }

        static string EngineeringCalc(string input)
        {
            string result = null;
            //------------------------------

            //------------------------------
            return result;
        }

        static void Main(string[] args)
        {
            string NormalInput = "-2+5*7+19/(2*(2+1))";
            string ProgrammerInput = "";
            string EngineerInput = "";

            //NormalCalc(NormalInput);            
            Stack<string> test = new Stack<string>();
            test.Push("-");
            test.Push("2");
            test.Push("+");
            test.Push("4");
            test.Push("*");
            test.Push("5");
            test.Push("+");
            test.Push("6");
            test.Push("-");
            test.Push("2");

            Console.WriteLine(Calc(test));
        }
    }
}
