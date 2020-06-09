using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelPath
{
    class Program
    {
        public class Solution
        {
            //주어진 항공권을 모두 이용하여 여행경로를 짜려고 합니다.항상 ICN 공항에서 출발합니다.

            //항공권 정보가 담긴 2차원 배열 tickets가 매개변수로 주어질 때, 방문하는 공항 경로를 배열에 담아 return 하도록 solution 함수를 작성해주세요.


            //제한사항
            //모든 공항은 알파벳 대문자 3글자로 이루어집니다.
            //주어진 공항 수는 3개 이상 10,000개 이하입니다.
            //tickets의 각 행[a, b] 는 a 공항에서 b 공항으로 가는 항공권이 있다는 의미입니다.
            //주어진 항공권은 모두 사용해야 합니다.
            //만일 가능한 경로가 2개 이상일 경우 알파벳 순서가 앞서는 경로를 return 합니다.
            //모든 도시를 방문할 수 없는 경우는 주어지지 않습니다.
            //입출력 예
            //tickets return
            //[[ICN, JFK], [HND, IAD], [JFK, HND]]	[ICN, JFK, HND, IAD]
            //[[ICN, SFO], [ICN, ATL], [SFO, ATL], [ATL, ICN], [ATL, SFO]]	[ICN, ATL, ICN, SFO, ATL, SFO]
            //입출력 예 설명
            //예제 #1

            //[ICN, JFK, HND, IAD] 순으로 방문할 수 있습니다.

            //예제 #2

            //[ICN, SFO, ATL, ICN, ATL, SFO] 순으로 방문할 수도 있지만 [ICN, ATL, ICN, SFO, ATL, SFO] 가 알파벳 순으로 앞섭니다.
            public string[] answerArr;
            public string[] solution(string[,] tickets)
            {
                string[] answer = answerArr;

                List<Tuple<string, string>> pathList = new List<Tuple<string, string>>();
                for(int i=0; i<tickets.GetLength(0); i++)
                {
                    var tupleTicket = new Tuple<string, string>(tickets[i,0], tickets[i,1]);
                    pathList.Add(tupleTicket);
                }


                //
                List<string> retList = new List<string>();
                retList.Add("ICN");
                findPath("ICN", pathList, retList);

                return answer;
            }

            public void SetAnswerList(List<string> rList)
            {
                if (answerArr == null)
                {
                    answerArr = rList.ToArray(); 
                    return;
                }

                for(int i=0; i<answerArr.Length; i++)
                {
                    if (answerArr[i].CompareTo(rList[i]) > 0)
                    {
                        answerArr = rList.ToArray(); 
                        return;
                    }
                    else if (answerArr[i].CompareTo(rList[i]) < 0)
                    {
                        return;
                    }
                }
            }

            public void findPath(string city, List<Tuple<string, string>> pathList, List<string> rList)
            {
                if (pathList.Count == 0)
                {
                    //foreach(var item in rList)
                    //{
                    //    Console.WriteLine(item);
                    //}
                    //Console.WriteLine();
                    SetAnswerList(rList);
                    return;
                }

                for (int i = 0; i < pathList.Count; i++)
                {
                    if (pathList[i].Item1 == city)
                    {
                        List<Tuple<string, string>> newList = new List<Tuple<string, string>>(pathList);
                        newList.RemoveAt(i);
                        List<string> retList = new List<string>(rList);
                        retList.Add(pathList[i].Item2);
                        findPath(pathList[i].Item2, newList, retList);
                    }
                }
            }
        }

        static void Main(string[] args)
        {
            Solution sol = new Solution();
            //string[,] tickets = new string[,] { { "ICN", "JFK" }, { "HND", "IAD" }, { "JFK", "HND" } };
            string[,] tickets = new string[,] { { "ICN", "SFO" } , {"ICN", "ATL"}, {"SFO", "ATL"}, {"ATL", "ICN"}, {"ATL", "SFO"}};
            string [] answer = sol.solution(tickets);

            Console.WriteLine("ANSWER :");
            answer = sol.answerArr;
            foreach(var item in answer)
            {
                Console.WriteLine(item);
            }
        }
    }
}
