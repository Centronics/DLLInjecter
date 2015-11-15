using System;
using System.Windows.Forms;

namespace Loader
{
    public partial class FrmHelp : Form
    {
        public FrmHelp()
        {
            InitializeComponent();
        }

        private byte NextPage = 1;

        private void button1_Click(object sender, EventArgs e)
        {
            switch (NextPage)
            {
                case 0:
                    D1.Visible = true;
                    D3.Visible = false;
                    NextPage = 1;
                    PageSwitcher.Text = "Следующая страница";
                    break;
                case 1:
                    D1.Visible = false;
                    D2.Visible = true;
                    NextPage = 2;
                    break;
                case 2:
                    D2.Visible = false;
                    D3.Visible = true;
                    NextPage = 0;
                    PageSwitcher.Text = "Первая страница";
                    break;
            }
        }

        private void FrmHelp_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }
    }
}
