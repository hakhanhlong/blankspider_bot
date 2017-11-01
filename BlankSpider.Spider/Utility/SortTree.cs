using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;

namespace BlankSpider.Spider.Utility
{
    public class SortTree
    {
        public SortTreeNode Root;
        public int Count;
        public bool Modified;

        public SortTree()
        {
        }
        public void Clear()
        {
            Root = null;
            Count = 0;
            Modified = false;
        }
        private SortTreeNode Add(string strText, int nCount, object Tag)
        {
            SortTreeNode node = Add(ref strText);
            node.Count = nCount;
            node.Tag = Tag;
            return node;
        }

        public SortTreeNode Add(ref string str)
        {
            SortTreeNode node;
            if (Root == null)
            {
                Root = new SortTreeNode();
                node = Root;
            }
            else
            {
                node = Root;
                while (true)
                {
                    if (node.Text == str)
                    {
                        node.Count++;
                        return node;
                    }
                    if (node.Text.CompareTo(str) > 0)
                    {	// add the node at the small branch
                        if (node.Small == null)
                        {
                            node.Small = new SortTreeNode();
                            node.Small.Parent = node;
                            node = node.Small;
                            break;
                        }
                        node = node.Small;
                    }
                    else
                    {	// add the node at the great branch
                        if (node.Great == null)
                        {
                            node.Great = new SortTreeNode();
                            node.Great.Parent = node;
                            node = node.Great;
                            break;
                        }
                        node = node.Great;
                    }
                }
            }
            node.Text = str;
            node.ID = this.Count++;
            node.Count++;
            Modified = true;

            return node;
        }

        public void Save(string fileName, System.Text.Encoding code)
        {
            FileStream stream = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None);
            StreamWriter writer = new StreamWriter(stream, code);

            BitArray bitArray = new BitArray(this.Count, false);
            SortTreeNode node = this.Root;
            int nCount = this.Count;
            while (nCount > 0)
            {
                if (node.Small != null && bitArray.Get(node.Small.ID) == false)
                    node = node.Small;
                else if (bitArray.Get(node.ID) == false)
                    OutNode(node, writer, bitArray, ref nCount);
                else if (node.Great != null && bitArray.Get(node.Great.ID) == false)
                    node = node.Great;
                else
                {
                    if (bitArray.Get(node.ID) == false)
                        OutNode(node, writer, bitArray, ref nCount);
                    node = node.Parent;
                }
            }
            writer.Close();
            stream.Close();

            Modified = false;
        }

        bool OutNode(SortTreeNode node, StreamWriter writer, BitArray bits, ref int nCount)
        {
            if (node == null || bits.Get(node.ID) == true)
                return false;
            string str = node.Text + '\t' + node.Count.ToString() + '\t';
            if (node.Tag != null)
                str += node.Tag.ToString().Replace("\r\n", "\r\n\t\t");
            writer.WriteLine(str.TrimEnd('\t'));

            bits.Set(node.ID, true);
            nCount--;

            return true;
        }
    }
}
