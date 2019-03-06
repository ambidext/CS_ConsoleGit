using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpTree
{
    class Program
    {
        static void Main(string[] args)
        {
            // data 입력
            TreeNode<string> treeRoot = SampleData.GetSet1();

            // 특정 data의 node찾기 
            TreeNode<string> found = treeRoot.FindTreeNode(node => node.Data != null && node.Data.Contains("node21"));
            Console.WriteLine("Found: " + found);

            // 특정 node의 하위 데이터 개수 구하기 
            int NumChildrenNode = found.ElementsIndex.Count - 1; // minus self node
            Console.WriteLine("Number Of Children Node : " + NumChildrenNode);

            // 특정 node 하위 데이터 모두 출력
            foreach(var item in found.ElementsIndex)
            {
                if (item != found) // do not count self node
                    Console.Write(item.Data + " ");
            }
            Console.WriteLine();

            // 특정 node 상위 데이터 모두 출력
            Console.WriteLine("[Parents] Level : "+ found.Level);
            TreeNode<string> parent = found.Parent;
            while (true)
            {            
                if (parent == null)
                    break;
                Console.Write(parent.Data + " ");
                parent = parent.Parent;
            }
            Console.WriteLine();
        }
    }
}
