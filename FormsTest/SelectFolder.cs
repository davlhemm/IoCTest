using IoCTest.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormsTest
{
    public partial class SelectFolder : Form
    {

        public SelectFolder()
        {
            InitializeComponent();
            comboBoxRecurseDirectory.DataSource = Enum.GetValues(typeof(SearchOption));
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ChooseFolder();
        }

        private void ChooseFolder()
        {
            if (selectFolderForBackupDialog.ShowDialog() == DialogResult.OK)
            {
                selectedFolderTextBox.Text = selectFolderForBackupDialog.SelectedPath;
            }
        }

        private void folderBrowserDialog1_HelpRequest(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void buttonImportFiles_Click(object sender, EventArgs e)
        {
            ImportFiles();
        }

        private void ImportFiles()
        {
            SearchOption    recurseOption  = (SearchOption)comboBoxRecurseDirectory.SelectedItem;
            IFileImportInfo fileImportInfo = new FileImportInfo(selectedFolderTextBox.Text, textBoxSearchPattern.Text, recurseOption);
            IFileImporter   fileImporter   = new FileImporter(fileImportInfo);
            
            listBoxFiles.DataSource = fileImporter.GetImportFiles();
            listBoxFiles.Refresh();
            MessageBox.Show($@"Imported files from selected location: {selectedFolderTextBox.Text}...", @"Files", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
