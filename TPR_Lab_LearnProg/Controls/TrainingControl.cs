using System;
using System.Drawing;
using System.Windows.Forms;

namespace TPR_Lab_LearnProg.Controls
{
    public partial class TrainingControl : UserControl
    {
        double[,] matrQ = {
            { 10, 2 },
            { 3, 4 },
            { 1, 6 }
        };
        double[,] matrZ = {
            { 0.4, 0.8 },
            { 0.6, 0.2 }
        };

        public TrainingControl()
        {
            InitializeComponent();
            TabControl_SelectedIndexChanged(null, null);
        }

        private void TrainingControl_Load(object sender, EventArgs e)
        {
            RTxtBoxTab1.SelectAll();
            RTxtBoxTab1.SelectionColor = Color.Black;
            RTxtBoxTab1.SelectionAlignment = HorizontalAlignment.Center;
            RTxtBoxTab1.Select(RTxtBoxTab1.GetFirstCharIndexFromLine(0), RTxtBoxTab1.Lines[0].Length);
            RTxtBoxTab1.SelectionFont = new System.Drawing.Font("Microsoft Sans Serif", 18, FontStyle.Bold);

            InitMatrix(tblLayPnlQ, "Q", matrQ);
            InitMatrix(tblLayPnlZ, "Z", matrZ);
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

        private static void InitMatrix(TableLayoutPanel tblLayPnl, string matrName, double[,] matrArray)
        {
            int m = matrArray.GetLength(0),
                n = matrArray.GetLength(1);

            Label matrNameLbl = new Label()
            {
                Text = matrName + ":",
                Font = new Font("Calibri", 18),
                AutoSize = false,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter
            };
            tblLayPnl.Controls.Add(matrNameLbl, 0, 1);
            tblLayPnl.SetRowSpan(matrNameLbl, m);

            for (int i = 0; i < m + 1; i++)
            {
                for (int j = 1; j < n + 2; j++)
                {
                    Label l = new Label()
                    {
                        //Text = matrA[i - 1, j - 2].ToString(),
                        Font = new Font("Calibri", 12),
                        AutoSize = false,
                        Dock = DockStyle.Fill,
                        TextAlign = ContentAlignment.MiddleCenter
                    };
                    if (i == 0)
                    {
                        if (j == 1)
                            continue;
                        l.Text = "β" + (j - 1);
                    }
                    else
                    {
                        if (j == 1)
                        {
                            l.Text = "α" + i;
                        }
                        else
                        {
                            l.Text = matrArray[i - 1, j - 2].ToString();
                            l.Font = new Font("Calibri", 20);
                            l.BorderStyle = BorderStyle.FixedSingle;
                            l.Margin = new Padding(0);
                        }
                    }
                    tblLayPnl.Controls.Add(l, j, i);
                }
            }
        }
    }
}
