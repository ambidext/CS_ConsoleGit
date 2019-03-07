using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Category
{
    class Program
    {
        class cData
        {
            public string str1;
            public string str2;

            public cData() { }
            public cData(string a, string b)
            {
                str1 = a;
                str2 = b;
            }
        }

        public static List<string> categories;
        public static string categoryStr;
        static TreeNode<string> treeRoot = null;

        static void Main(string[] args)
        {
            string[,] inputData = LoadData();

            // solution1
            string topCategory = GetTopCategory(inputData, categories);
            Console.WriteLine(topCategory);

            int numberOfSubcategories = GetNumberOfSubcategories(inputData, categoryStr);
            Console.WriteLine(numberOfSubcategories);

            // solution2
            Category objCategory = new Category();
            string res = objCategory.GetCategory(inputData, categories);
            Console.WriteLine(res);
            int num = objCategory.GetNumberOfSubcategories(inputData, categoryStr);
            Console.WriteLine(num);

            // solution3
            Category3 objCategory3 = new Category3();
            string res3 = objCategory3.GetCategory(inputData, categories);
            Console.WriteLine(res3);
            int num3 = objCategory3.GetNumberOfSubcategories(inputData, categoryStr);
            Console.WriteLine(num3);
        }

        static bool ContainsValue(List<string[]> list, string str)
        {
            foreach(var item in list)
            {
                if (item[1] == str)
                {
                    return true;
                }
            }
            return false;
        }

        static bool ContainsKey(List<string[]> list, string str)
        {
            foreach (var item in list)
            {
                if (item[0] == str)
                {
                    return true;
                }
            }
            return false;
        }

        static void RemoveInList(List<string[]> iList, List<string[]> sList)
        {
            foreach(var item in sList)
            {
                if (iList.Contains(item))
                {
                    iList.Remove(item);
                }
            }
        }

        static string GetTopCategory(string[,] inputData, List<string>categories)
        {
            string res = null;

            // insert list
            List<cData> inputList = new List<cData>();            

            for (int i=0; i<inputData.GetLength(0); i++)
            {
                inputList.Add(new cData(inputData[i,0], inputData[i, 1]));
            }

            // make tree
            while (true)
            {
                if (inputList.Count == 0)
                    break;
                cData first = inputList[0]; 
                inputList.RemoveAt(0);

                if (treeRoot == null)
                {
                    treeRoot = new TreeNode<string>(first.str1);
                    treeRoot.AddChild(first.str2);
                }
                else
                {
                    TreeNode<string> found = treeRoot.FindTreeNode(node => node.Data != null && node.Data.Contains(first.str1));
                    if (found != null)
                    {
                        found.AddChild(first.str2);
                    }
                    else if (treeRoot.Data == first.str2)
                    {
                        treeRoot = new TreeNode<string>(first.str1, treeRoot);
                    }
                    else
                    {
                        inputList.Add(first);
                    }
                }
            }
            
            //////////////////////////////////////////////

            TreeNode<string> n1 = treeRoot.FindTreeNode(node => node.Data != null && node.Data == categories[0]);
            TreeNode<string> n2 = treeRoot.FindTreeNode(node => node.Data != null && node.Data == categories[1]);

            List<string> list1 = new List<string>();
            while (true)
            {
                if (n1.Parent == null)
                    break;
                list1.Add(n1.Parent.Data);
                n1 = n1.Parent;
            }

            List<string> list2 = new List<string>();
            while (true)
            {
                if (n2.Parent == null)
                    break;
                list2.Add(n2.Parent.Data);
                n2 = n2.Parent;
            }

            for (int i=0; i<list1.Count; i++)
            {
                for (int j=0; j<list2.Count; j++)
                {
                    if (list1[i] == list2[j])
                    {
                        res = list1[i];
                        return res;
                    }
                }
            }
            return res;
        }

        static int GetNumberOfSubcategories(string[,] inputData, string categoryStr)
        {
            int res = 0;

            TreeNode<string> found = treeRoot.FindTreeNode(node => node.Data != null && node.Data == categoryStr);

            res = found.Parent.ElementsIndex.Count - 1;

            return res;
        }

        static string[,] LoadData()
        {
            //string[,] inputData = {
            //    {"M","B"},
            //    {"M","C"},
            //    {"M","K"},
            //    {"B","E"},
            //    {"C","F"},
            //    {"C","G"},
            //    {"C","H"},
            //    {"K","I"},
            //    {"K","J"},
            //    {"E","D"},
            //    {"F","L"},
            //    {"F","A"},
            //    {"H","N"},
            //    {"H","O"},
            //    {"J","P"},
            //    {"J","Q"}
            //};

            //categories = new List<string>()
            //{
            //    "F", "N"
            //};

            //categoryStr = "J";

            //string[,] inputData = {
            //     {"Z","B"},
            //     {"Z","W"},
            //     {"Z","V"},
            //     {"B","E"},
            //     {"W","F"},
            //     {"W","G"},
            //     {"V","H"},
            //     {"V","I"},
            //     {"V","J"},
            //     {"E","K"},
            //     {"F","L"},
            //     {"G","M"},
            //     {"G","N"},
            //     {"H","O"},
            //     {"I","P"},
            //     {"J","Q"}
            //};

            //categories = new List<string>()
            //{
            //    "I", "O"
            //};

            //categoryStr = "G";

            string[,] inputData = {
                 {"A","B"},
                 {"A","C"},
                 {"D","A"},
                 {"D","Z"},
                 {"F","Q"},
                 {"Y","F"},
                 {"Y","D"},
                 {"T","H"},
                 {"T","K"},
                 {"F","T"}
            };

            categories = new List<string>()
            {
                "Q", "H"
            };

            categoryStr = "D";

            return inputData;
        }
    }
}
