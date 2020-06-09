using System;

namespace palindrome
{
    class Program
    {
        class Solution
        {
            public int LongestPalindromeSubSequence(String str)
            {
                int n = str.Length;
                int i, j, cl;
                int [,] table = new int[n, n];

                for (i = 0; i < n; i++)
                    table[i, i] = 1;

                for (cl=2; cl<=n; cl++)
                {
                    for (i=0; i<n-cl+1; i++)
                    {
                        j = i + cl - 1;
                        if (str[i] == str[j] && cl == 2)
                            table[i, j] = 2;
                        else if (str[i] == str[j])
                            table[i, j] = table[i + 1, j - 1] + 2;
                        else
                            table[i, j] = Math.Max(table[i, j - 1], table[i + 1, j]);
                    }
                }

                return table[0,n - 1];
            }

            public int LongestPalindromeSubString(String str)
            {
                int length = str.Length;
                int palindromeLength = 0;
                String palindrome = "";
                bool [,] strTable = new bool[length,length];
                int i, j, k;

                for(i = 0; i<length; i++){
                    for(j = 0; j<length; j++){
                        strTable[i,j] = false;
                    }
                }
 
                // if length == 1
                for(i = 0; i<length; i++){
                    strTable[i,i] = true;
                }
 
                //if length == 2
                for(i = 0; i<length - 2; i++){
 
                    if(str[i] == str[i + 1]){ 
                        strTable[i,i + 1] = true;
                    }
                }
 
                //if length > 2
                for(k = 2; k<length; k++)
                {
                    for(i = 0; i<length; i++)
                    {
                        j = i + k;
                        if (j >= length)
                            continue;

                        if(str[i] == str[j] && strTable[i + 1,j - 1] == true)
                        {
                            if(str.Substring(i, j-i+1).Length > palindromeLength)
                            {
                                palindrome = str.Substring(i, j-i+1);
                                palindromeLength = palindrome.Length; 
 
                            }
 
                            strTable[i,j] = true; 
                        }
                    }
                }

                //Console.WriteLine(palindrome);
                return palindromeLength;
            }
        }

        static void Main(string[] args)
        {
            Solution sol = new Solution();

            int res = sol.LongestPalindromeSubSequence("aabcdcbaba");
            Console.WriteLine(res);
            res = sol.LongestPalindromeSubString("aabcdcbaba");
            Console.WriteLine(res);
        }
    }
}
