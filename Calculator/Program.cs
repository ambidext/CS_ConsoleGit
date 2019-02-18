using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    class Program
    {
        static string IntCalc(string input)
        {
            string result = null;

            Stack<string> numStack = new Stack<string>();
            string strNum = "";

            if (input[0] == '-' || input[0] == '+')
                input = "0" + input;

            // 숫자 추출  
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] >= '0' && input[i] <= '9')
                {
                    strNum += input[i];
                }
                else // +-*/ 기호 
                {
                    numStack.Push(strNum);
                    strNum = "";
                    numStack.Push(input[i].ToString());
                }
            }
            if (strNum != "")
            {                
                numStack.Push(strNum);
            }

            // 곱하기, 나누기 처리 
            Stack<string> calcStack = new Stack<string>();
            foreach (var item in numStack)
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
                        int num1 = int.Parse(item);
                        string str = calcStack.Pop();
                        str = calcStack.Pop();
                        int num2 = int.Parse(str);
                        if (num2 == 0)
                            return "INF";

                        calcStack.Push((num1 / num2).ToString());
                    }
                    else
                    {
                        calcStack.Push(item);
                    }
                }
            }

            //// 더하기, 빼기 처리 

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

                num1 = int.Parse(str);
                sym = calcStack.Pop();
                str = calcStack.Pop();
                num2 = int.Parse(str);

                int total = 0;
                if (sym == "+")
                    total = num1 + num2;
                else if (sym == "-")
                    total = num1 - num2;
                calcStack.Push(total.ToString());
            }

            return result;
        }

        static string NormalCalc(string input)
        {
            string result = null;
            //------------------------------

            // 괄호 처리 
            Stack<char> charStack = new Stack<char>();
            string inBrace = null;
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == ')')
                {
                    while (true)
                    {
                        char temp = charStack.Pop();
                        if (temp == '(')
                        {
                            string dc = IntCalc(inBrace);
                            foreach (var item in dc)
                            {
                                charStack.Push(item);
                            }

                            inBrace = "";
                            break;
                        }
                        else
                        {
                            inBrace = temp + inBrace;
                        }
                    }
                }
                else
                {
                    charStack.Push(input[i]);
                }
            }

            string str = "";
            foreach (var item in charStack)
            {
                str = item + str;
            }
            result = IntCalc(str);
            //------------------------------

            return result;
        }

        static string Conv10(string strNum)
        {
            int decNum = 0;
            if ((strNum.Length > 1) && (strNum[0] == '0' && strNum[1] == 'x'))
            {
                string hexNum = strNum.Substring(2);
                for (int i=0; i<hexNum.Length; i++)
                {
                    decNum += (hexNum[hexNum.Length - 1 - i] - '0') * (int)Math.Pow(16.0, (double)i);
                }
                return decNum.ToString();
            }
            else if ((strNum.Length > 1) && strNum[strNum.Length-1] == 'b')
            {
                for (int i = 0; i < strNum.Length-1; i++)
                {
                    decNum += (strNum[strNum.Length - 2 - i] - '0') * (int)Math.Pow(2.0, (double)i);
                }
                return decNum.ToString();
            }
            else
            {
                return strNum;
            }
        }

        static string ProgrammerCalc(string input)
        {
            string result = null;
            //------------------------------
            string numQ = ""; 
            string strNum = "";

            // 숫자 추출 & 10진수 변환
            for (int i = 0; i < input.Length; i++)
            {
                if ((input[i] >= '0' && input[i] <= '9') || (input[i] == 'x') || input[i] == 'b')
                {
                    strNum += input[i];
                }
                else
                {
                    if (strNum != "")
                        numQ = numQ + Conv10(strNum);
                    numQ = numQ + (input[i] + "");
                    strNum = "";
                }
            }
            if (strNum != "")
                numQ = numQ + Conv10(strNum);

            string decRes = NormalCalc(numQ);
            string hexRes = "0x" + int.Parse(decRes).ToString("X");
            string binRes = Convert.ToString(int.Parse(decRes), 2) + "b"; 
            result = decRes + " " + hexRes + " " + binRes;
            //------------------------------
            return result;
        }

        static int factorial(int n)
        {
            if (n == 0 || n == 1)
                return 1;
            return n * factorial(n - 1);
        }

        static double GetTriValue(string strNum)
        {
            double dNum;
            if (strNum[0] == 'S')
            {
                dNum = double.Parse(strNum.Substring(1));
                dNum = Math.Sin(dNum*Math.PI/180.0);
            }
            else if (strNum[0] == 'C')
            {
                dNum = double.Parse(strNum.Substring(1));
                dNum = Math.Cos(dNum * Math.PI / 180.0);
            }
            else if (strNum[0] == 'T')
            {
                dNum = double.Parse(strNum.Substring(1));
                dNum = Math.Tan(dNum * Math.PI / 180.0);
            }
            else if (strNum[0] == 'L')
            {
                dNum = double.Parse(strNum.Substring(1));
                dNum = Math.Log10(dNum);
            }
            else
            {
                dNum = double.Parse(strNum);
            }
            return dNum;
        }

        static string DoubleCalc(string input)
        {
            string result = null;

            Stack<string> numStack = new Stack<string>();
            string strNum = "";

            if (input[0] == '-' || input[0] == '+')
                input = "0" + input;

            // 숫자 추출 & factorial 처리 
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] >= '0' && input[i] <= '9' || input[i] == '.' || input[i] == 'S' || input[i] == 'C' || input[i] == 'T' || input[i] == 'L')
                {
                    strNum += input[i];
                }
                else if (input[i] == '!')
                {
                    int nNum = factorial(int.Parse(strNum));
                    strNum = "";
                    numStack.Push(nNum.ToString());
                }
                else // +-*/ 기호 
                {
                    double dNum = GetTriValue(strNum);

                    numStack.Push(dNum.ToString());
                    strNum = "";
                    numStack.Push(input[i].ToString());
                }
            }
            if (strNum != "")
            {
                double dNum = GetTriValue(strNum);
                numStack.Push(dNum.ToString());
            }

            // 곱하기, 나누기, 거듭제곱 처리 
            Stack<string> calcStack = new Stack<string>();
            foreach (var item in numStack)
            {
                if (item == "*" || item == "/" || item == "+" || item == "-" || item == "^")
                {
                    calcStack.Push(item);
                }
                else
                {
                    if (calcStack.Count > 0 && calcStack.First() == "*")
                    {
                        double num2 = double.Parse(item);
                        string str = calcStack.Pop();
                        str = calcStack.Pop();
                        double num1 = double.Parse(str);

                        calcStack.Push((num1 * num2).ToString());
                    }
                    else if (calcStack.Count > 0 && calcStack.First() == "/")
                    {
                        double num1 = double.Parse(item);
                        string str = calcStack.Pop();
                        str = calcStack.Pop();
                        double num2 = double.Parse(str);
                        if (num2 == 0)
                            return "INF";

                        calcStack.Push((num1 / num2).ToString());
                    }
                    else if (calcStack.Count > 0 && calcStack.First() == "^")
                    {
                        double num1 = double.Parse(item);
                        string str = calcStack.Pop();
                        str = calcStack.Pop();
                        double num2 = double.Parse(str);

                        calcStack.Push(Math.Pow(num1, num2).ToString());
                    }
                    else
                    {
                        calcStack.Push(item);
                    }
                }
            }

            //// 더하기, 빼기 처리 

            while (true)
            {
                if (calcStack.Count == 1)
                {
                    result = calcStack.Pop();
                    break;
                }

                double num1, num2;
                string sym;
                string str = calcStack.Pop();

                num1 = double.Parse(str);
                sym = calcStack.Pop();
                str = calcStack.Pop();
                num2 = double.Parse(str);

                double total = 0;
                if (sym == "+")
                    total = num1 + num2;
                else if (sym == "-")
                    total = num1 - num2;
                calcStack.Push(total.ToString());
            }

            return result;
        }

        static string EngineeringCalc(string input)
        {
            string result = null;
            //------------------------------
            input = input.Replace("SIN", "S");
            input = input.Replace("COS", "C");
            input = input.Replace("TAN", "T");
            input = input.Replace("LOG", "L");

            // 괄호 처리 
            Stack<char> charStack = new Stack<char>();
            string inBrace = null;
            for (int i=0; i<input.Length; i++)
            {
                if (input[i] == ')')
                {
                    while (true)
                    {
                        char temp = charStack.Pop();
                        if (temp == '(')
                        {
                            string dc = DoubleCalc(inBrace);
                            foreach(var item in dc)
                            {
                                charStack.Push(item);
                            }

                            inBrace = "";
                            break;
                        }
                        else
                        {
                            inBrace = temp + inBrace;
                        }
                    }
                }
                else
                {
                    charStack.Push(input[i]);
                }
            }

            string str = "";
            foreach (var item in charStack)
            {
                str = item + str; 
            }
            result = DoubleCalc(str);
            double dRes = double.Parse(result);
            result = string.Format("{0:F3}", dRes);
            //------------------------------

            return result;
        }

        static void Main(string[] args)
        {
            string NormalInput = "12+3*(7+3*2)+19/(3*(3/2))";//"-2+5*7+19/((2+1)*(2+1))";// 
            string ProgrammerInput = "11b+(0x11*10b+1)-9";//"10b+0x23+9/3";//
            string EngineeringInput = "-0.123+2^(1/2)+SIN((40+5)*SIN(89+SIN(90)))+LOG(2*3+4)+5!";//"-2+2^(1/2)+SIN((40+5)*2)+LOG(2*3+4)+5!"; //"-2+2^(1/2)+SIN((40+5)*2)+LOG(2*3+4)+5!"; // 

            Console.WriteLine("Normal Calculator: " + NormalInput);
            Console.WriteLine(NormalCalc(NormalInput));
            Console.WriteLine();
            Console.WriteLine("Programmer Calculator: " + ProgrammerInput);
            Console.WriteLine(ProgrammerCalc(ProgrammerInput));
            Console.WriteLine();
            Console.WriteLine("Engineering Calculator: " + EngineeringInput);
            Console.WriteLine(EngineeringCalc(EngineeringInput));
        }
    }
}
