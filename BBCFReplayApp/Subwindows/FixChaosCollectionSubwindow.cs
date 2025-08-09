using BBCFReplayApp.Tools;
using BBCFReplayLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BBCFReplayApp.Subwindows
{
    public partial class FixChaosCollectionSubwindow : UserControl
    {
        string _inDirDirectory;
        bool _inSet = false;
        string _outDirDirectory;
        bool _outSet = false;
        List<(ReplayHeader, string)> _replays;

        public FixChaosCollectionSubwindow()
        {
            InitializeComponent();
        }

        private void setInDirButton_Click(object sender, EventArgs e)
        {
            if (inDirDialogue.ShowDialog() == DialogResult.OK)
            {
                inDirTextbox.Text = inDirDialogue.SelectedPath;
                _inDirDirectory = inDirDialogue.SelectedPath;

                string[] t = FileMgmt.CollectFromChaosFolders(_inDirDirectory);
                _replays = FileMgmt.FilterToUnique(t);

                nOrigFoundLabel.Text = t.Length.ToString();
                nUniqueFoundLabel.Text = _replays.Count().ToString();

                _inSet = true;
                if (_inSet && _outSet)
                {
                    copyFilesButton.Enabled = true;
                }
            }
        }

        private void setOutDirButton_Click(object sender, EventArgs e)
        {
            if (outDirDialogue.ShowDialog() == DialogResult.OK)
            {
                outDirTextbox.Text = outDirDialogue.SelectedPath;
                _outDirDirectory = outDirDialogue.SelectedPath;

                _outSet = true;
                if (_inSet && _outSet)
                {
                    copyFilesButton.Enabled = true;
                }
            }
        }

        private void copyFilesButton_Click(object sender, EventArgs e)
        {
            statusLabel.Text = "Copying files...";
            FileMgmt.CopyFiles(_replays, _outDirDirectory);
            statusLabel.Text = "Copied!";
        }
    }
}
