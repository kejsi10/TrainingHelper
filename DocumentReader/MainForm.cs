using System;
using System.Windows.Forms;

namespace traininghelper
{
    public partial class MainForm : Form
    {
        WordTableReader getWordPlainText = null;

        public MainForm()
        {
            InitializeComponent();
            this.btnSaveas.Enabled = false;
        }

        /// <summary>
        ///  Handle the btnOpen Click event to load an Word file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpen_Click(object sender, EventArgs e)
        {
            SelectWordFile(); 
        }

        /// <summary>
        /// Show an OpenFileDialog to select a Word document.
        /// </summary>
        /// <returns>
        /// The file name.
        /// </returns>
        private string SelectWordFile()
        {
            string fileName = null;
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = "Word document (*.docx)|*.docx";
                dialog.InitialDirectory = Environment.CurrentDirectory;

                // Retore the directory before closing
                dialog.RestoreDirectory = true;
                if (dialog.ShowDialog()== DialogResult.OK)
                {
                    fileName = dialog.FileName;
                    tbxFile.Text = dialog.FileName;
                    rtbText.Clear();
                }
            }

            return fileName;
        }

        /// <summary>
        /// Get Plain Text from Word file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGetPlainText_Click(object sender, EventArgs e)
        {
            try
            {
                getWordPlainText = new WordTableReader(tbxFile.Text);
                getWordPlainText.ChangeTextInCell();
                this.rtbText.Clear();
                this.rtbText.Text = "text";

                // After read text in word document successfully，make "save as text" button to be enabled.
                this.btnSaveas.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        ///  Save the text to text file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveas_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog savefileDialog = new SaveFileDialog())
            {
                savefileDialog.Filter = "txt document(*.txt)|*.txt";

                // default file name extension
                savefileDialog.DefaultExt = ".txt";

                // Retore the directory before closing
                savefileDialog.RestoreDirectory = true;
                if (savefileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filename = savefileDialog.FileName;
                    rtbText.SaveFile(filename, RichTextBoxStreamType.PlainText);
                    MessageBox.Show("Save Text file successfully, the file path is： " + filename);
                }
            }
        }
    }
}
