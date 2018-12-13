using AoCHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day08
{
    class Node
    {
        public List<Node> Children { get; set; }
        public List<int> MetaData { get; set; }

        public Node()
        {
            this.Children = new List<Node>();
            this.MetaData = new List<int>();
        }
    }

    class Program
    {
        static Node CreateNode(List<int> data)
        {
            int c, m;
            Node n = new Node();
            c = data[0];
            m = data[1];
            data.RemoveRange(0, 2);
            for(int i = 0; i < c; i++)
            {
                n.Children.Add(CreateNode(data));
            }
            for (int j = 0; j < m; j++)
            {
                n.MetaData.Add(data[j]);
            }
            data.RemoveRange(0, m);
            return n;
        }

        static Node CreateNodeTree(string input)
        {
            List<int> data = new List<int>();
            string[] splitted = input.Split(new char[] { ' ' });
            
            for(int i = 0; i < splitted.Length; i++)
            {
                data.Add(int.Parse(splitted[i]));
            }
            
            return CreateNode(data);
        }

        static int SumAllMeta(Node rootNode)
        {
            int result = 0;
            for(int i = 0; i < rootNode.MetaData.Count; i++)
            {
                result += rootNode.MetaData[i];
            }
            for (int i = 0; i < rootNode.Children.Count; i++)
            {
                result += SumAllMeta(rootNode.Children[i]);
            }
            return result;
        }

        static int SumOfNode(Node rootNode)
        {
            int result = 0;

            if (rootNode.Children.Count > 0)
            {
                for (int i = 0; i < rootNode.MetaData.Count; i++)
                {
                    if ((rootNode.MetaData[i] > 0) && (rootNode.MetaData[i] <= rootNode.Children.Count))
                    {
                        result += SumOfNode(rootNode.Children[rootNode.MetaData[i] - 1]);
                    }
                }
            }
            else
            {
                for(int i = 0; i < rootNode.MetaData.Count; i++)
                {
                    result += rootNode.MetaData[i];
                }
            }
            return result;
        }

        static void Main(string[] args)
        {
            List<string> input = InputLoader.LoadByLines("input.txt");
            Node rootNode = CreateNodeTree(input[0]);
            int sumAllMeta = SumAllMeta(rootNode);
            int sumIdxMeta = SumOfNode(rootNode);

            Console.WriteLine(string.Format("Sum of all Meta Data: {0}", sumAllMeta));
            Console.WriteLine(string.Format("Sum of all Meta Indices: {0}", sumIdxMeta));
            Console.ReadLine();
        }
    }
}
