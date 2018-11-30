using System;
using System.Drawing;
using System.Windows.Forms;

namespace TPR_Lab_LearnProg.Controls
{
    public partial class TrainingControl : UserControl
    {
        private object[,] matrA = {
            { "A:", },
        };

        public TrainingControl()
        {
            InitializeComponent();
            tabControl1_SelectedIndexChanged(null, null);
        }

        private void TrainingControl_Load(object sender, EventArgs e)
        {
            RTxtBoxTab1.SelectAll();
            RTxtBoxTab1.SelectionColor = Color.Black;
            RTxtBoxTab1.SelectionAlignment = HorizontalAlignment.Center;
            RTxtBoxTab1.Select(RTxtBoxTab1.GetFirstCharIndexFromLine(0), RTxtBoxTab1.Lines[0].Length);
            RTxtBoxTab1.SelectionFont = new System.Drawing.Font("Microsoft Sans Serif", 18, FontStyle.Bold);

            InitMatrices();
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

        private void InitMatrices()
        {
            double[,] matrA = {
                { 10, 2 },
                { 3, 4 },
                { 1, 6 }
            };
            double[,] matrZ = {
                { 0.4, 0.8 },
                { 0.6, 0.2 }
            };
            for (int i = 1; i < 4; i++)
            {
                for (int j = 2; j < 4; j++)
                {
                    tblLayPnlA.Controls.Add(
                        new Label()
                        {
                            Text = matrA[i - 1, j - 2].ToString(),
                            Font = new Font("Calibri", 20)
                        },
                        j, i);
                }
            }
            for (int i = 1; i < 3; i++)
            {
                for (int j = 2; j < 4; j++)
                {
                    tblLayPnlZ.Controls.Add(
                        new Label()
                        {
                            Text = matrZ[i - 1, j - 2].ToString(),
                            Font = new Font("Calibri", 20)
                        },
                        j, i);
                }
            }
        }
        
        private void FillMatr(TableLayoutPanel tblLayPnl, )
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
