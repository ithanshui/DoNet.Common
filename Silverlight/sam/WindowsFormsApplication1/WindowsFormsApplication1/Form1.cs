using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var ns = GetCheckedNodes(treeView1.Nodes);
        }

        private List<TreeNode> GetCheckedNodes(TreeNodeCollection nodes)
        {
            var list = new List<TreeNode>();
            if (nodes != null && nodes.Count > 0)
            {
                foreach (TreeNode n in nodes)
                {
                    if (n.Checked) list.Add(n);                    
                    list.AddRange(GetCheckedNodes(n.Nodes));
                }
            }
            return list;
        }
    }
}
