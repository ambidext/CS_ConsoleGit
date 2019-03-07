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
            TreeNode<string> treeRoot = new TreeNode<string>("root");
            {
                TreeNode<string> node0 = treeRoot.AddChild("node0");
                TreeNode<string> node1 = treeRoot.AddChild("node1");
                TreeNode<string> node2 = treeRoot.AddChild("node2");
                {
                    TreeNode<string> node20 = node2.AddChild(null);
                    TreeNode<string> node21 = node2.AddChild("node21");
                    {
                        TreeNode<string> node210 = node21.AddChild("node210");
                        {
                            TreeNode<string> node2101 = node210.AddChild("node2101");
                        }
                        TreeNode<string> node211 = node21.AddChild("node211");
                        {
                            TreeNode<string> node2111 = node211.AddChild("node2111");
                            TreeNode<string> node2112 = node211.AddChild("node2112");
                        }
                    }
                }
                TreeNode<string> node3 = treeRoot.AddChild("node3");
                {
                    TreeNode<string> node30 = node3.AddChild("node30");
                }
            }

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

            // root 삽입
            treeRoot = new TreeNode<string>("NewRoot", treeRoot);
            Console.WriteLine("NewRoot : " + treeRoot.Data);
            foreach (var item in treeRoot.Children)
            {
                Console.WriteLine("NewRoot's Child : " + item);
            }
        }
    }
}
