
namespace FormsTest
{
    partial class SelectFolder
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
            this.Folder = new System.Windows.Forms.Button();
            this.selectFolderForBackupDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.selectedFolderTextBox = new System.Windows.Forms.TextBox();
            this.textBoxSearchPattern = new System.Windows.Forms.TextBox();
            this.labelSearchPattern = new System.Windows.Forms.Label();
            this.listBoxFiles = new System.Windows.Forms.ListBox();
            this.buttonImportFiles = new System.Windows.Forms.Button();
            this.labelRecurseDirectory = new System.Windows.Forms.Label();
            this.comboBoxRecurseDirectory = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // Folder
            // 
            this.Folder.Location = new System.Drawing.Point(12, 12);
            this.Folder.Name = "Folder";
            this.Folder.Size = new System.Drawing.Size(104, 23);
            this.Folder.TabIndex = 0;
            this.Folder.Text = "Choose Folder";
            this.Folder.UseVisualStyleBackColor = true;
            this.Folder.Click += new System.EventHandler(this.button1_Click);
            // 
            // selectFolderForBackupDialog
            // 
            this.selectFolderForBackupDialog.HelpRequest += new System.EventHandler(this.folderBrowserDialog1_HelpRequest);
            // 
            // selectedFolderTextBox
            // 
            this.selectedFolderTextBox.Location = new System.Drawing.Point(124, 12);
            this.selectedFolderTextBox.Name = "selectedFolderTextBox";
            this.selectedFolderTextBox.ReadOnly = true;
            this.selectedFolderTextBox.Size = new System.Drawing.Size(338, 23);
            this.selectedFolderTextBox.TabIndex = 1;
            // 
            // textBoxSearchPattern
            // 
            this.textBoxSearchPattern.Location = new System.Drawing.Point(124, 41);
            this.textBoxSearchPattern.Name = "textBoxSearchPattern";
            this.textBoxSearchPattern.Size = new System.Drawing.Size(338, 23);
            this.textBoxSearchPattern.TabIndex = 2;
            // 
            // labelSearchPattern
            // 
            this.labelSearchPattern.AutoSize = true;
            this.labelSearchPattern.Location = new System.Drawing.Point(12, 44);
            this.labelSearchPattern.Name = "labelSearchPattern";
            this.labelSearchPattern.Size = new System.Drawing.Size(104, 15);
            this.labelSearchPattern.TabIndex = 3;
            this.labelSearchPattern.Text = "File Search Pattern";
            this.labelSearchPattern.Click += new System.EventHandler(this.label1_Click);
            // 
            // listBoxFiles
            // 
            this.listBoxFiles.FormattingEnabled = true;
            this.listBoxFiles.HorizontalScrollbar = true;
            this.listBoxFiles.ItemHeight = 15;
            this.listBoxFiles.Location = new System.Drawing.Point(124, 171);
            this.listBoxFiles.Name = "listBoxFiles";
            this.listBoxFiles.Size = new System.Drawing.Size(338, 139);
            this.listBoxFiles.TabIndex = 4;
            // 
            // buttonImportFiles
            // 
            this.buttonImportFiles.Location = new System.Drawing.Point(12, 171);
            this.buttonImportFiles.Name = "buttonImportFiles";
            this.buttonImportFiles.Size = new System.Drawing.Size(104, 23);
            this.buttonImportFiles.TabIndex = 5;
            this.buttonImportFiles.Text = "Import Files";
            this.buttonImportFiles.UseVisualStyleBackColor = true;
            this.buttonImportFiles.Click += new System.EventHandler(this.buttonImportFiles_Click);
            // 
            // labelRecurseDirectory
            // 
            this.labelRecurseDirectory.AutoSize = true;
            this.labelRecurseDirectory.Location = new System.Drawing.Point(12, 74);
            this.labelRecurseDirectory.Name = "labelRecurseDirectory";
            this.labelRecurseDirectory.Size = new System.Drawing.Size(99, 15);
            this.labelRecurseDirectory.TabIndex = 6;
            this.labelRecurseDirectory.Text = "Recurse Directory";
            // 
            // comboBoxRecurseDirectory
            // 
            this.comboBoxRecurseDirectory.FormattingEnabled = true;
            this.comboBoxRecurseDirectory.Location = new System.Drawing.Point(124, 71);
            this.comboBoxRecurseDirectory.Name = "comboBoxRecurseDirectory";
            this.comboBoxRecurseDirectory.Size = new System.Drawing.Size(121, 23);
            this.comboBoxRecurseDirectory.TabIndex = 7;
            // 
            // SelectFolder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(474, 322);
            this.Controls.Add(this.comboBoxRecurseDirectory);
            this.Controls.Add(this.labelRecurseDirectory);
            this.Controls.Add(this.buttonImportFiles);
            this.Controls.Add(this.listBoxFiles);
            this.Controls.Add(this.labelSearchPattern);
            this.Controls.Add(this.textBoxSearchPattern);
            this.Controls.Add(this.selectedFolderTextBox);
            this.Controls.Add(this.Folder);
            this.Name = "SelectFolder";
            this.Text = "SelectFolder";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button Folder;
        private System.Windows.Forms.FolderBrowserDialog selectFolderForBackupDialog;
        private System.Windows.Forms.TextBox selectedFolderTextBox;
        private System.Windows.Forms.TextBox textBoxSearchPattern;
        private System.Windows.Forms.Label labelSearchPattern;
        private System.Windows.Forms.ListBox listBoxFiles;
        private System.Windows.Forms.Button buttonImportFiles;
        private System.Windows.Forms.Label labelRecurseDirectory;
        private System.Windows.Forms.ComboBox comboBoxRecurseDirectory;
    }
}

