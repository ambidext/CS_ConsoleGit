using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Category
{
    class Program
    {
        public static List<string> categories;
        public static string categoryStr;
        static TreeNode<string> treeRoot = null;

        static void Main(string[] args)
        {
            string[,] inputData = LoadData();

            string topCategory = GetTopCategory(inputData, categories);
            Console.WriteLine(topCategory);

            int numberOfSubcategories = GetNumberOfSubcategories(inputData, categoryStr);
            Console.WriteLine(numberOfSubcategories);

            //
            Category objCategory = new Category();
            string res = objCategory.GetCategory(inputData, categories);
            Console.WriteLine(res);
            int num = objCategory.GetNumberOfSubcategories(inputData, categoryStr);
            Console.WriteLine(num);
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

            // fill tree ///////////////////////////////////////////////////////////////////////////////////

            // sort inputData
            List<string[]> inputList = new List<string[]>(); 
            List<string[]> sortedList = new List<string[]>();
            for (int i=0; i<inputData.GetLength(0); i++)
            {
                string[] temp = {inputData[i,0], inputData[i, 1]};
                inputList.Add(temp);
            }

            // find root
            string rootStr = "";
            foreach(var item in inputList)
            {
                if (!ContainsValue(inputList, item[0]))
                {
                    rootStr = item[0];
                    sortedList.Add(item);
                }
            }

            RemoveInList(inputList, sortedList);
            while (true)
            {
                if (inputList.Count == 0)
                    break;
                for (int i=0; i<inputList.Count; i++)
                {
                    if (ContainsValue(sortedList, inputList[i][0]))
                    {
                        sortedList.Add(inputList[i]);
                        inputList.Remove(inputList[i]);
                        break;
                    }
                }
            }


            for (int i = 0; i < sortedList.Count; i++)
            {
                TreeNode<string> found = null;
                if (treeRoot != null)
                {
                    found = treeRoot.FindTreeNode(node => node.Data != null && node.Data == sortedList[i][0]);
                }

                if (found != null && found.Data == sortedList[i][0])
                {
                    found.AddChild(sortedList[i][1]);
                }
                else
                {
                    TreeNode<string> root = new TreeNode<string>(sortedList[i][0]);
                    root.AddChild(sortedList[i][1]);
                    treeRoot = root;
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
