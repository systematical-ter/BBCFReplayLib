using BBCFReplayApp.Tools;
using BBCFReplayLib;

namespace BBCFReplayApp
{
    public partial class MainForm : Form
    {
        private Options _optionsForm;
        private bool _isOptionsShowing = false;
        private string _replayDirectory;
        private string _backupDirectory;

        List<ReplayHeader> _loadedHeaders;
        IEnumerable<ReplayHeader> _sortedHeaders;

        public MainForm()
        {
            _loadedHeaders = new List<ReplayHeader>();
            InitializeComponent();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (!_isOptionsShowing)
            {
                _optionsForm = new Options();
                _optionsForm.Show();
                _isOptionsShowing = true;
            }
            else
            {
                _optionsForm.Activate();
            }

        }

        private void loadReplayFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (replayFolderDialog.ShowDialog() == DialogResult.OK)
            {
                toolStripReplayFolder.Text = replayFolderDialog.SelectedPath;
                _replayDirectory = replayFolderDialog.SelectedPath;
            }
        }

        private void loadBackupFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (backupFolderDialog.ShowDialog() == DialogResult.OK)
            {
                toolStripBackupFolder.Text = backupFolderDialog.SelectedPath;
                _backupDirectory = backupFolderDialog.SelectedPath;
                buttonLoadBackup.Enabled = true;
            }
        }

        private void buttonLoadBackup_Click(object sender, EventArgs e)
        {
            string[] files = Directory.GetFiles(_backupDirectory, "*.dat", SearchOption.AllDirectories);
            foreach (string f in files)
            {
                _loadedHeaders.Add(ReplayHeader.FromFile(f));
            }
            _sortedHeaders = _loadedHeaders.OrderBy(x => x.Date1).Reverse();
            fillListView();
        }

        private void fillListView()
        {
            replayListView.Items.Clear();
            foreach (ReplayHeader header in _sortedHeaders)
            {
                string[] replayListItemData = {header.UID, header.Date1.ToString("MM/dd/yyyy hh:mmtt"), header.P1.Name, header.P2.Name, header.GetP1CharName(), header.GetP2CharName(), header.GetWinnerName() };
                ListViewItem replayListItem = new ListViewItem(replayListItemData);
                replayListView.Items.Add(replayListItem);
            }
        }
    }
}
