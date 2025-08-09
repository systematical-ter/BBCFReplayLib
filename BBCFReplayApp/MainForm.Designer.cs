namespace BBCFReplayApp
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            ListViewItem listViewItem1 = new ListViewItem(new string[] { "Systematical", "DGF", "Terumi", "Terumi", "DGF" }, -1);
            ListViewItem listViewItem2 = new ListViewItem(new string[] { "Systematical", "TheZanderBug", "Terumi", "Terumi", "TheZanderBug" }, -1);
            ListViewItem listViewItem3 = new ListViewItem(new string[] { "Ghost", "Systematical", "Jin", "Terumi", "Ghost" }, -1);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            replayListView = new ListView();
            KeyCol = new ColumnHeader();
            DateCol = new ColumnHeader();
            P1 = new ColumnHeader();
            P2 = new ColumnHeader();
            C1 = new ColumnHeader();
            C2 = new ColumnHeader();
            WinnerCol = new ColumnHeader();
            groupBox1 = new GroupBox();
            comboBox3 = new ComboBox();
            comboBox4 = new ComboBox();
            label3 = new Label();
            label4 = new Label();
            comboBox2 = new ComboBox();
            comboBox1 = new ComboBox();
            label2 = new Label();
            label1 = new Label();
            toolStrip1 = new ToolStrip();
            toolStripButton1 = new ToolStripButton();
            toolStripSeparator1 = new ToolStripSeparator();
            toolStripSplitButton1 = new ToolStripDropDownButton();
            loadReplayFolderToolStripMenuItem = new ToolStripMenuItem();
            loadBackupFolderToolStripMenuItem = new ToolStripMenuItem();
            statusStrip1 = new StatusStrip();
            toolStripReplayFolder = new ToolStripStatusLabel();
            toolStripBackupFolder = new ToolStripStatusLabel();
            replayFolderDialog = new FolderBrowserDialog();
            backupFolderDialog = new FolderBrowserDialog();
            buttonLoadBackup = new Button();
            splitContainer1 = new SplitContainer();
            groupBox1.SuspendLayout();
            toolStrip1.SuspendLayout();
            statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            SuspendLayout();
            // 
            // replayListView
            // 
            replayListView.Columns.AddRange(new ColumnHeader[] { KeyCol, DateCol, P1, P2, C1, C2, WinnerCol });
            replayListView.Dock = DockStyle.Fill;
            replayListView.FullRowSelect = true;
            replayListView.GridLines = true;
            replayListView.Items.AddRange(new ListViewItem[] { listViewItem1, listViewItem2, listViewItem3 });
            replayListView.Location = new Point(0, 0);
            replayListView.Margin = new Padding(3, 2, 3, 2);
            replayListView.Name = "replayListView";
            replayListView.Size = new Size(558, 314);
            replayListView.TabIndex = 0;
            replayListView.UseCompatibleStateImageBehavior = false;
            replayListView.View = View.Details;
            // 
            // KeyCol
            // 
            KeyCol.Text = "Key";
            KeyCol.Width = 0;
            // 
            // DateCol
            // 
            DateCol.Text = "Date";
            DateCol.Width = 120;
            // 
            // P1
            // 
            P1.Text = "Player1";
            P1.Width = 120;
            // 
            // P2
            // 
            P2.Text = "Player 2";
            P2.Width = 120;
            // 
            // C1
            // 
            C1.Text = "Character 1";
            C1.Width = 90;
            // 
            // C2
            // 
            C2.Text = "Character 2";
            C2.Width = 90;
            // 
            // WinnerCol
            // 
            WinnerCol.Text = "Winner";
            WinnerCol.Width = 120;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(comboBox3);
            groupBox1.Controls.Add(comboBox4);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(comboBox2);
            groupBox1.Controls.Add(comboBox1);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label1);
            groupBox1.Dock = DockStyle.Bottom;
            groupBox1.Location = new Point(0, 40);
            groupBox1.Margin = new Padding(3, 2, 3, 2);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(5);
            groupBox1.Size = new Size(242, 274);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "Filter Options";
            // 
            // comboBox3
            // 
            comboBox3.FormattingEnabled = true;
            comboBox3.Location = new Point(94, 114);
            comboBox3.Margin = new Padding(3, 2, 3, 2);
            comboBox3.Name = "comboBox3";
            comboBox3.Size = new Size(133, 23);
            comboBox3.TabIndex = 7;
            // 
            // comboBox4
            // 
            comboBox4.FormattingEnabled = true;
            comboBox4.Location = new Point(94, 88);
            comboBox4.Margin = new Padding(3, 2, 3, 2);
            comboBox4.Name = "comboBox4";
            comboBox4.Size = new Size(133, 23);
            comboBox4.TabIndex = 6;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(14, 119);
            label3.Name = "label3";
            label3.Size = new Size(52, 15);
            label3.TabIndex = 5;
            label3.Text = "Player B:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(13, 94);
            label4.Name = "label4";
            label4.Size = new Size(53, 15);
            label4.TabIndex = 4;
            label4.Text = "Player A:";
            // 
            // comboBox2
            // 
            comboBox2.FormattingEnabled = true;
            comboBox2.Location = new Point(94, 43);
            comboBox2.Margin = new Padding(3, 2, 3, 2);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(133, 23);
            comboBox2.TabIndex = 3;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(94, 17);
            comboBox1.Margin = new Padding(3, 2, 3, 2);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(133, 23);
            comboBox1.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(14, 48);
            label2.Name = "label2";
            label2.Size = new Size(71, 15);
            label2.TabIndex = 1;
            label2.Text = "Character B:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(13, 23);
            label1.Name = "label1";
            label1.Size = new Size(72, 15);
            label1.TabIndex = 0;
            label1.Text = "Character A:";
            // 
            // toolStrip1
            // 
            toolStrip1.GripStyle = ToolStripGripStyle.Hidden;
            toolStrip1.Items.AddRange(new ToolStripItem[] { toolStripButton1, toolStripSeparator1, toolStripSplitButton1 });
            toolStrip1.LayoutStyle = ToolStripLayoutStyle.HorizontalStackWithOverflow;
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.RenderMode = ToolStripRenderMode.System;
            toolStrip1.Size = new Size(809, 25);
            toolStrip1.TabIndex = 2;
            toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            toolStripButton1.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripButton1.ImageTransparentColor = Color.Magenta;
            toolStripButton1.Name = "toolStripButton1";
            toolStripButton1.Size = new Size(53, 22);
            toolStripButton1.Text = "Options";
            toolStripButton1.Click += toolStripButton1_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(6, 25);
            // 
            // toolStripSplitButton1
            // 
            toolStripSplitButton1.AutoToolTip = false;
            toolStripSplitButton1.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripSplitButton1.DropDownItems.AddRange(new ToolStripItem[] { loadReplayFolderToolStripMenuItem, loadBackupFolderToolStripMenuItem });
            toolStripSplitButton1.Image = (Image)resources.GetObject("toolStripSplitButton1.Image");
            toolStripSplitButton1.ImageTransparentColor = Color.Magenta;
            toolStripSplitButton1.Name = "toolStripSplitButton1";
            toolStripSplitButton1.Size = new Size(87, 22);
            toolStripSplitButton1.Text = "Load Folders";
            // 
            // loadReplayFolderToolStripMenuItem
            // 
            loadReplayFolderToolStripMenuItem.Name = "loadReplayFolderToolStripMenuItem";
            loadReplayFolderToolStripMenuItem.Size = new Size(180, 22);
            loadReplayFolderToolStripMenuItem.Text = "Load Replay Folder";
            loadReplayFolderToolStripMenuItem.Click += loadReplayFolderToolStripMenuItem_Click;
            // 
            // loadBackupFolderToolStripMenuItem
            // 
            loadBackupFolderToolStripMenuItem.Name = "loadBackupFolderToolStripMenuItem";
            loadBackupFolderToolStripMenuItem.Size = new Size(180, 22);
            loadBackupFolderToolStripMenuItem.Text = "Load Backup Folder";
            loadBackupFolderToolStripMenuItem.Click += loadBackupFolderToolStripMenuItem_Click;
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripReplayFolder, toolStripBackupFolder });
            statusStrip1.Location = new Point(0, 339);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(809, 22);
            statusStrip1.TabIndex = 3;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripReplayFolder
            // 
            toolStripReplayFolder.Name = "toolStripReplayFolder";
            toolStripReplayFolder.Size = new Size(143, 17);
            toolStripReplayFolder.Text = "Replay Folder Not Loaded";
            // 
            // toolStripBackupFolder
            // 
            toolStripBackupFolder.Name = "toolStripBackupFolder";
            toolStripBackupFolder.Size = new Size(147, 17);
            toolStripBackupFolder.Text = "Backup Folder Not Loaded";
            // 
            // replayFolderDialog
            // 
            replayFolderDialog.ShowNewFolderButton = false;
            // 
            // backupFolderDialog
            // 
            backupFolderDialog.ShowNewFolderButton = false;
            // 
            // buttonLoadBackup
            // 
            buttonLoadBackup.Dock = DockStyle.Top;
            buttonLoadBackup.Enabled = false;
            buttonLoadBackup.Location = new Point(0, 0);
            buttonLoadBackup.Name = "buttonLoadBackup";
            buttonLoadBackup.Size = new Size(242, 23);
            buttonLoadBackup.TabIndex = 4;
            buttonLoadBackup.Text = "Load Backup Data";
            buttonLoadBackup.UseVisualStyleBackColor = true;
            buttonLoadBackup.Click += buttonLoadBackup_Click;
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.FixedPanel = FixedPanel.Panel2;
            splitContainer1.Location = new Point(0, 25);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(replayListView);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(groupBox1);
            splitContainer1.Panel2.Controls.Add(buttonLoadBackup);
            splitContainer1.Panel2.Padding = new Padding(0, 0, 5, 0);
            splitContainer1.Size = new Size(809, 314);
            splitContainer1.SplitterDistance = 558;
            splitContainer1.TabIndex = 5;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(809, 361);
            Controls.Add(splitContainer1);
            Controls.Add(statusStrip1);
            Controls.Add(toolStrip1);
            Margin = new Padding(3, 2, 3, 2);
            MinimumSize = new Size(0, 400);
            Name = "MainForm";
            Text = "Form1";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListView replayListView;
        private ColumnHeader KeyCol;
        private ColumnHeader P2;
        private ColumnHeader C1;
        private ColumnHeader C2;
        private ColumnHeader WinnerCol;
        private GroupBox groupBox1;
        private Label label2;
        private Label label1;
        private ComboBox comboBox2;
        private ComboBox comboBox1;
        private ComboBox comboBox3;
        private ComboBox comboBox4;
        private Label label3;
        private Label label4;
        private ToolStrip toolStrip1;
        private ToolStripButton toolStripButton1;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStripReplayFolder;
        private ToolStripStatusLabel toolStripBackupFolder;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripDropDownButton toolStripSplitButton1;
        private ToolStripMenuItem loadReplayFolderToolStripMenuItem;
        private ToolStripMenuItem loadBackupFolderToolStripMenuItem;
        private FolderBrowserDialog replayFolderDialog;
        private FolderBrowserDialog backupFolderDialog;
        private Button buttonLoadBackup;
        private ColumnHeader P1;
        private ColumnHeader DateCol;
        private SplitContainer splitContainer1;
    }
}
