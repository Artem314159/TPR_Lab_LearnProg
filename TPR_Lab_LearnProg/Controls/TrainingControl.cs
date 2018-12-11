using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace TPR_Lab_LearnProg.Controls
{
    public partial class TrainingControl : UserControl
    {
        #region Private fields

        double[,] matrQ = {
            { 10, 2 },
            { 3, 4 },
            { 1, 6 }
        };
        double[,] matrZ = {
            { 0.4, 0.8 },
            { 0.6, 0.2 }
        };

        #endregion

        public TrainingControl()
        {
            InitializeComponent();
            TabControl_SelectedIndexChanged(null, null);
        }

        private void TrainingControl_Load(object sender, EventArgs e)
        {
            foreach (TabPage tabPage in TabControl.TabPages)
            {
                List<RichTextBox> rTxtBoxes = tabPage.GetAllChildren<RichTextBox>();
                foreach (RichTextBox rTxtBox in rTxtBoxes)
                {
                    rTxtBox.LoadFile($"../../Sources/{rTxtBox.Name}.rtf");
                    rTxtBox.SelectAll();
                    rTxtBox.SelectionColor = Color.Black;
                }
            }

            tblLayPnlQ.InitMatrix("Q", matrQ);
            tblLayPnlZ.InitMatrix("Z", matrZ);
        }

        private void NextBtn_Click(object sender, EventArgs e)
        {
            int position = TabControl.TabPages.IndexOf(TabControl.SelectedTab);
            if (position != TabControl.TabPages.Count - 1)
                TabControl.SelectedTab = TabControl.TabPages[position + 1];
        }

        private void PrevBtn_Click(object sender, EventArgs e)
        {
            int position = TabControl.TabPages.IndexOf(TabControl.SelectedTab);
            if (position != 0)
                TabControl.SelectedTab = TabControl.TabPages[position - 1];
        }

        private void TabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            PrevBtn.Visible = TabControl.SelectedTab != TabControl.TabPages[0];
            NextBtn.Visible = TabControl.SelectedTab != TabControl.TabPages[TabControl.TabPages.Count - 1];
        }

        private void ExitBtn_Click(object sender, EventArgs e)
        {
            ControlFuncs.ChangeScene("TrainingControl", "MainMenuControl", InitFormType.InitForMainMenu);
        }
    }
}