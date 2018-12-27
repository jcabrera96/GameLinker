using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameLinker.Forms
{
    public partial class UploadProgressForm : Form
    {
        public UploadProgressForm()
        {
            InitializeComponent();
        }

        private void acceptButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void UploadProgressForm_Load(object sender, EventArgs e)
        {

        }
    }
}
