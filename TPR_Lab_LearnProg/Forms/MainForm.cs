using System;
using System.Windows.Forms;

namespace TPR_Lab_LearnProg.Forms
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.AddControl("MainMenuControl");
        }
    }
}
