namespace CSharpTree
{
    class SampleData
    {
        public static TreeNode<string> GetSet1()
        {
            //TreeNode<string> root = new TreeNode<string>("root");
            //{
            //    TreeNode<string> node0 = root.AddChild("node0");
            //    TreeNode<string> node1 = root.AddChild("node1");
            //    TreeNode<string> node2 = root.AddChild("node2");
            //    {
            //        //TreeNode<string> node20 = node2.AddChild(null);
            //        TreeNode<string> node21 = node2.AddChild("node21");
            //        {
            //            TreeNode<string> node210 = node21.AddChild("node210");
            //            TreeNode<string> node211 = node21.AddChild("node211");
            //        }
            //    }
            //    TreeNode<string> node3 = root.AddChild("node3");
            //    {
            //        TreeNode<string> node30 = node3.AddChild("node30");
            //    }
            //}

            //return root;

            TreeNode<string> root = new TreeNode<string>("root");
            {
                TreeNode<string> node0 = root.AddChild("node0");
                TreeNode<string> node1 = root.AddChild("node1");
                TreeNode<string> node2 = root.AddChild("node2");
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
                TreeNode<string> node3 = root.AddChild("node3");
                {
                    TreeNode<string> node30 = node3.AddChild("node30");
                }
            }

            return root;
        }
    }
}
