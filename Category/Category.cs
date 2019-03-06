using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Category
{
    class Category
    {
        public string GetCategory(string [,] inputData, List<string> categories)
        {
            string topCategory = "";
            /////////////////////
            Dictionary<string, List<string>> tMap = new Dictionary<string, List<string>>();

            for (int i=0; i<inputData.GetLength(0); i++)
            {
                String parent = inputData[i, 0];
                String child = inputData[i, 1];

                if (tMap.ContainsKey(parent))
                {
                    List<string> childs = tMap[parent];
                    childs.Add(child);
                }
                else
                {
                    List<string> childs = new List<string>();
                    childs.Add(child);

                    tMap[parent] = childs;
                }
            }

            List<string> hierarchy = new List<string>();

            int index = -1;
            bool bFirst = true;
            foreach(var inputCategory in categories)
            {
                if (bFirst)
                {
                    foreach (var item in tMap)
                    {
                        List<String> values = item.Value;
                        if (values.Contains(inputCategory))
                        {
                            hierarchy.Add(item.Key);

                            String parent = getParent(tMap, item.Key);
                            while(parent != "")
                            {
                                hierarchy.Insert(0, parent);
                                parent = getParent(tMap, parent);
                            }

                            break;
                        }
                    }
                    bFirst = false;
                }
                else
                {
                    foreach(var item in tMap)
                    {
                        List<String> values = item.Value;
                        if (values.Contains(inputCategory))
                        {
                            List<String> tHierarchy = new List<String>();

                            tHierarchy.Add(item.Key);

                            String parent = getParent(tMap, item.Key);
                            while (parent != "")
                            {
                                tHierarchy.Insert(0, parent);
                                parent = getParent(tMap, parent);
                            }

                            foreach(var tParent in tHierarchy)
                            {
                                for (int i=0; i<hierarchy.Count; i++)
                                {
                                    String oParent = hierarchy[i];
                                    if (oParent.Equals(tParent))
                                    {
                                        if (index < i)
                                            index = i;
                                        break;
                                    }
                                }
                            }

                            break;
                        }
                    }
                }
            }

            if (index > -1)
                topCategory = hierarchy[index];
            /////////////////////
            return topCategory;
        }

        public String getParent(Dictionary<string, List<string>> tMap, string child)
        {
            string parent = "";

            foreach(var item in tMap)
            {
                List<string> values = tMap[item.Key];
                if (values.Contains(child))
                {
                    parent = item.Key;
                    break;
                }
            }

            return parent;
        }

        public List<string> getChilds(Dictionary<string, List<string>> tMap, string parent)
        {
            List<string> childs = new List<string>();

            if (tMap.ContainsKey(parent))
            {
                childs = tMap[parent];
            }

            return childs;
        }

        public void getChildren(Dictionary<string, List<string>> tMap, string parent, List<String> children)
        {

            List<string> tChildren = getChilds(tMap, parent); 
            if (tChildren.Count > 0)
            {
                children.AddRange(tChildren);

                foreach (String tChildParents in tChildren)
                {
                    getChildren(tMap, tChildParents, children);
                }
            }
        }

        public int GetNumberOfSubcategories(string [,] inputData, String categoryStr)
        {
            int numberOfSubcategories = 0;
            ///////////////////////////////////////////////
            Dictionary<string, List<string>> tMap = new Dictionary<string, List<string>>();

            for (int i = 0; i < inputData.GetLength(0); i++)
            {
                String parent = inputData[i, 0];
                String child = inputData[i, 1];

                if (tMap.ContainsKey(parent))
                {
                    List<string> childs = tMap[parent];
                    childs.Add(child);
                }
                else
                {
                    List<string> childs = new List<string>();
                    childs.Add(child);

                    tMap[parent] = childs;
                }
            }

            foreach(var item in tMap)
            {
                List<string> values = tMap[item.Key];
                if (values.Contains(categoryStr))
                {
                    string parent = getParent(tMap, categoryStr);

                    List<string> children = new List<string>();
                    getChildren(tMap, parent, children);

                    numberOfSubcategories = children.Count;
                }
            }
            ///////////////////////////////////////////////
            return numberOfSubcategories;
        }
    }
}
