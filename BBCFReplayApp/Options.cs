using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BBCFReplayApp
{
    public partial class Options : Form
    {
        public Options()
        {
            InitializeComponent();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string selected = e.Node.TreeView.SelectedNode.Text;
            if(selected == "Files")
            {
                var fixCollectionSubwindow = new Subwindows.FixChaosCollectionSubwindow();
                panel1.Controls.Add(fixCollectionSubwindow);
            }
        }
    }
}
