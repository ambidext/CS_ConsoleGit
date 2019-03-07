using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Category
{
    class Category3
    {
        Dictionary<string, List<string>> dicInput;

        string findKey(string str)
        {
            foreach (var item in dicInput)
            {
                if (item.Value.Contains(str))
                {
                    return item.Key;
                }
            }

            return null;
        }

        List<string> findValue(string str)
        {
            List<string> res = new List<string>();
            foreach (var item in dicInput)
            {
                if (item.Key == str)
                {
                    res.AddRange(item.Value);
                    return res;
                }
            }
            return null;
        }

        string findEqual(List<string> list1, List<string> list2)
        {
            string res = null;
            for (int i = 0; i < list1.Count; i++)
            {
                for (int j = 0; j < list2.Count; j++)
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

        public string GetCategory(string[,] inputData, List<string> categories)
        {
            string topCategory = "";
            ///////////////////////////////////////////////////////////////////////////////////////
            dicInput = new Dictionary<string, List<string>>();

            for (int i = 0; i < inputData.GetLength(0); i++)
            {
                if (dicInput.ContainsKey(inputData[i, 0]))
                {
                    dicInput[inputData[i, 0]].Add(inputData[i, 1]);

                }
                else
                {
                    dicInput.Add(inputData[i, 0], new List<string>() { inputData[i, 1] });
                }
            }

            List<string>[] findList = new List<string>[2];
            findList[0] = new List<string>();
            findList[1] = new List<string>();
            for (int i = 0; i < categories.Count; i++)
            {
                string findStr = categories[i];
                while (true)
                {
                    string fKey = findKey(findStr);
                    if (fKey != null)
                    {
                        findList[i].Add(fKey);
                        findStr = fKey;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            topCategory = findEqual(findList[0], findList[1]);
            //////////////////////////////////////////////////////////////////////////////////////////
            return topCategory;
        }

        public int GetNumberOfSubcategories(string[,] inputData, String categoryStr)
        {
            int numberOfSubcategories = 0;
            ///////////////////////////////////////////////
            string parent = findKey(categoryStr);
            List<string> child = findValue(parent);
            numberOfSubcategories += child.Count;

            while (true)
            {
                if (child.Count == 0)
                    break;
                parent = child[0];
                child.RemoveAt(0);
                List<string> findChild = findValue(parent);
                if (findChild == null)
                    continue;
                else
                {
                    numberOfSubcategories += findChild.Count;
                    child.AddRange(findChild);
                }
            }

            ///////////////////////////////////////////////
            return numberOfSubcategories;
        }
    }
}
