namespace BBCFReplayApp.Subwindows
{
    partial class FixChaosCollectionSubwindow
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            setInDirButton = new Button();
            inDirTextbox = new TextBox();
            label2 = new Label();
            nOrigFoundLabel = new Label();
            label3 = new Label();
            nUniqueFoundLabel = new Label();
            setOutDirButton = new Button();
            outDirTextbox = new TextBox();
            statusLabel = new Label();
            label4 = new Label();
            inDirDialogue = new FolderBrowserDialog();
            outDirDialogue = new FolderBrowserDialog();
            copyFilesButton = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(3, 9);
            label1.Name = "label1";
            label1.Size = new Size(167, 15);
            label1.TabIndex = 0;
            label1.Text = "Chaos Collection Repair Utility";
            // 
            // setInDirButton
            // 
            setInDirButton.Location = new Point(3, 27);
            setInDirButton.Name = "setInDirButton";
            setInDirButton.Size = new Size(109, 23);
            setInDirButton.TabIndex = 1;
            setInDirButton.Text = "Set Search Dir";
            setInDirButton.UseVisualStyleBackColor = true;
            setInDirButton.Click += setInDirButton_Click;
            // 
            // inDirTextbox
            // 
            inDirTextbox.Location = new Point(118, 27);
            inDirTextbox.Name = "inDirTextbox";
            inDirTextbox.Size = new Size(234, 23);
            inDirTextbox.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(3, 60);
            label2.Name = "label2";
            label2.Size = new Size(95, 15);
            label2.TabIndex = 3;
            label2.Text = "Total files found:";
            // 
            // nOrigFoundLabel
            // 
            nOrigFoundLabel.AutoSize = true;
            nOrigFoundLabel.Location = new Point(104, 60);
            nOrigFoundLabel.Name = "nOrigFoundLabel";
            nOrigFoundLabel.Size = new Size(13, 15);
            nOrigFoundLabel.TabIndex = 4;
            nOrigFoundLabel.Text = "0";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(3, 79);
            label3.Name = "label3";
            label3.Size = new Size(111, 15);
            label3.TabIndex = 5;
            label3.Text = "Unique Files Found:";
            // 
            // nUniqueFoundLabel
            // 
            nUniqueFoundLabel.AutoSize = true;
            nUniqueFoundLabel.Location = new Point(120, 79);
            nUniqueFoundLabel.Name = "nUniqueFoundLabel";
            nUniqueFoundLabel.Size = new Size(13, 15);
            nUniqueFoundLabel.TabIndex = 6;
            nUniqueFoundLabel.Text = "0";
            // 
            // setOutDirButton
            // 
            setOutDirButton.Location = new Point(3, 115);
            setOutDirButton.Name = "setOutDirButton";
            setOutDirButton.Size = new Size(111, 23);
            setOutDirButton.TabIndex = 7;
            setOutDirButton.Text = "Set Save Dir";
            setOutDirButton.UseVisualStyleBackColor = true;
            setOutDirButton.Click += setOutDirButton_Click;
            // 
            // outDirTextbox
            // 
            outDirTextbox.Location = new Point(120, 115);
            outDirTextbox.Name = "outDirTextbox";
            outDirTextbox.Size = new Size(232, 23);
            outDirTextbox.TabIndex = 8;
            // 
            // statusLabel
            // 
            statusLabel.AutoSize = true;
            statusLabel.Location = new Point(51, 188);
            statusLabel.Name = "statusLabel";
            statusLabel.Size = new Size(57, 15);
            statusLabel.TabIndex = 9;
            statusLabel.Text = "Waiting...";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(3, 188);
            label4.Name = "label4";
            label4.Size = new Size(42, 15);
            label4.TabIndex = 10;
            label4.Text = "Status:";
            // 
            // copyFilesButton
            // 
            copyFilesButton.Enabled = false;
            copyFilesButton.Location = new Point(3, 162);
            copyFilesButton.Name = "copyFilesButton";
            copyFilesButton.Size = new Size(349, 23);
            copyFilesButton.TabIndex = 11;
            copyFilesButton.Text = "Copy Unique Replays";
            copyFilesButton.UseVisualStyleBackColor = true;
            copyFilesButton.Click += copyFilesButton_Click;
            // 
            // FixChaosCollectionSubwindow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(setInDirButton);
            Controls.Add(inDirTextbox);
            Controls.Add(label1);
            Controls.Add(copyFilesButton);
            Controls.Add(label4);
            Controls.Add(statusLabel);
            Controls.Add(outDirTextbox);
            Controls.Add(setOutDirButton);
            Controls.Add(nUniqueFoundLabel);
            Controls.Add(label3);
            Controls.Add(nOrigFoundLabel);
            Controls.Add(label2);
            Name = "FixChaosCollectionSubwindow";
            Size = new Size(827, 453);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Button setInDirButton;
        private TextBox inDirTextbox;
        private Label label2;
        private Label nOrigFoundLabel;
        private Label label3;
        private Label nUniqueFoundLabel;
        private Button setOutDirButton;
        private TextBox outDirTextbox;
        private Label statusLabel;
        private Label label4;
        private FolderBrowserDialog inDirDialogue;
        private FolderBrowserDialog outDirDialogue;
        private Button copyFilesButton;
    }
}
