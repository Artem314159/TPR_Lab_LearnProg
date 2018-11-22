using System;
using System.Drawing;
using System.Windows.Forms;

namespace TPR_Lab_LearnProg.Controls
{
    public partial class TrainingControl : UserControl
    {
        public TrainingControl()
        {
            InitializeComponent();
            tabControl1_SelectedIndexChanged(null, null);
        }

        private void NextBtn_Click(object sender, EventArgs e)
        {
            int position = tabControl1.TabPages.IndexOf(tabControl1.SelectedTab);
            if (position != tabControl1.TabPages.Count - 1)
                tabControl1.SelectedTab = tabControl1.TabPages[position + 1];
        }

        private void PrevBtn_Click(object sender, EventArgs e)
        {
            int position = tabControl1.TabPages.IndexOf(tabControl1.SelectedTab);
            if (position != 0)
                tabControl1.SelectedTab = tabControl1.TabPages[position - 1];
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            PrevBtn.Visible = tabControl1.SelectedTab != tabControl1.TabPages[0];
            NextBtn.Visible = tabControl1.SelectedTab != tabControl1.TabPages[tabControl1.TabPages.Count - 1];
        }

        private void ExitBtn_Click(object sender, EventArgs e)
        {
            ControlFuncs.ChangeScene("TrainingControl", "MainMenuControl", InitFormType.InitForMainMenu);
        }
    }
}
