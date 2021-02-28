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

namespace MSM6295Loader.Dialogs
{
    public partial class ExportDialog : Form
    {
        public ExportDialog()
        {
            InitializeComponent();
        }

        public string FilePathName
        {
            get { return textBoxFilename.Text; }
            set { textBoxFilename.Text = value; }
        }

        public bool PadToFullSize
        {
            get { return checkBoxFullSize.Checked; }
            set { checkBoxFullSize.Checked = value; }
        }

        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            SaveFileDialog sd = new SaveFileDialog();
            sd.InitialDirectory = Path.GetDirectoryName(FilePathName);
            sd.FileName = Path.GetFileName(FilePathName);
            sd.Filter = sd.Filter = "Binary Files(*.bin)| *.bin|All Files(*.*)| *.*";
            DialogResult dr = sd.ShowDialog();
            if (dr == DialogResult.OK)
            {
                FilePathName = sd.FileName;
            }
        }
    }
}
