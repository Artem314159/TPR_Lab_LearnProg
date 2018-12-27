using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TPR_Lab_LearnProg.Controls
{
    public partial class CheckKnowControl : UserControl
    {
        #region Private fields

        double[,] matrQ;
        double[,] matrZ;

        #endregion

        public CheckKnowControl()
        {
            InitializeComponent();
            InitMatr();
        }

        private void CheckKnowControl_Load(object sender, EventArgs e)
        {

        }

        private void InitMatr()
        {
            int m = (int)Q_NUD.Value + 1, r = (int)Z_NUD.Value + 1;
            tblLayPnlQ.RowCount = m;
            tblLayPnlZ.RowCount = r;
            tblLayPnlQ.RowStyles.Clear();
            tblLayPnlZ.RowStyles.Clear();
            tblLayPnlQ.Height = 0;
            tblLayPnlZ.Height = 0;
            for (int i = 0; i < m; i++)
            {
                tblLayPnlQ.Height += 50;
                if (i==0)
                {
                tblLayPnlQ.RowStyles.Add(new RowStyle(SizeType.Absolute, 50));
                    tblLayPnlQ.Controls.Add(new Label() { Text = "β1" }, 1, i);
                    tblLayPnlQ.Controls.Add(new Label() { Text = "β2" }, 2, i);
                }
                else
                {
                    AddRow(tblLayPnlQ, i, "α");
                }
            }
            SetControlsSettings(tblLayPnlQ.Controls);

            for (int i = 0; i < r; i++)
            {
                tblLayPnlZ.Height += 50;
                if (i == 0)
                {
                tblLayPnlZ.RowStyles.Add(new RowStyle(SizeType.Absolute, 50));
                    tblLayPnlZ.Controls.Add(new Label() { Text = "β1" }, 1, i);
                    tblLayPnlZ.Controls.Add(new Label() { Text = "β2" }, 2, i);
                }
                else
                {
                    AddRow(tblLayPnlZ, i, "z");
                }
            }
            SetControlsSettings(tblLayPnlZ.Controls);
        }

        private void ExitBtn_Click(object sender, EventArgs e)
        {
            ControlFuncs.ChangeScene("TrainingControl", "MainMenuControl", InitFormType.InitForMainMenu);
        }

        private void NextBtn_Click(object sender, EventArgs e)
        {
            int position = TabControl.TabPages.IndexOf(TabControl.SelectedTab);
            if (TabPageCanChaged(position) && position != TabControl.TabPages.Count - 1)
                TabControl.SelectedTab = TabControl.TabPages[position + 1];
        }

        private void PrevBtn_Click(object sender, EventArgs e)
        {
            int position = TabControl.TabPages.IndexOf(TabControl.SelectedTab);
            if (TabPageCanChaged(position) && position != 0)
                TabControl.SelectedTab = TabControl.TabPages[position - 1];
        }

        private bool tabPage1_Chaged()
        {
            bool res = (int)M_NUD.Value == (int)Math.Pow(tblLayPnlQ.RowCount, tblLayPnlZ.RowCount);
            if (!res) MessageBox.Show("Value M is wrong.");
            return res;
        }

        private bool TabPageCanChaged(int selectedTab)
        {
            switch (selectedTab)
            {
                case 0:
                    return tabPage1_Chaged();
                //case 1:
                //    return tabPage2_Chaged();
                //case 2:
                //    return tabPage3_Chaged();
                //case 3:
                //    return tabPage4_Chaged();
                //case 4:
                //    return tabPage5_Chaged();
                //case 5:
                //    return tabPage6_Chaged();
                default:
                    return false;
            }
        }

        private void Q_NUD_ValueChanged(object sender, EventArgs e)
        {
            NUD_ValueChanged(tblLayPnlQ, (int)Q_NUD.Value + 1, "α");
        }

        private void Z_NUD_ValueChanged(object sender, EventArgs e)
        {
            NUD_ValueChanged(tblLayPnlZ, (int)Z_NUD.Value + 1, "z");
        }

        private void NUD_ValueChanged(TableLayoutPanel tblLayPnl, int value, string text)
        {
            int prevRowCount = tblLayPnl.RowCount;
            tblLayPnl.RowCount = value;
            if (prevRowCount < tblLayPnl.RowCount)
            {
                for (int i = prevRowCount; i < tblLayPnl.RowCount; i++)
                {
                    AddRow(tblLayPnl, i, text);
                }
                SetControlsSettings(tblLayPnl.Controls);
            }
            else
            {
                for (int i = tblLayPnl.RowCount; i < prevRowCount; i++)
                {
                    DeleteRow(tblLayPnl);
                }
            }
        }

        private void TxtToNumber(object sender, EventArgs e)
        {
            TextBox txtBox = sender as TextBox;
            txtBox.Text = Convert.ToDouble(txtBox.Text).ToString();
        }

        private void AddRow(TableLayoutPanel tblLayPnl, int i, string text)
        {
            tblLayPnl.RowStyles.Add(new RowStyle(SizeType.Absolute, 50));
            tblLayPnl.Height += 50;
            tblLayPnl.Controls.Add(new Label() { Text = text + (i + 1) }, 0, i);
            Control c = new TextBox() { Text = "0" };
            c.TextChanged += TxtToNumber;
            tblLayPnl.Controls.Add(c, 1, i);
            c = new TextBox() { Text = "0" };
            c.TextChanged += TxtToNumber;
            tblLayPnl.Controls.Add(c, 2, i);
        }

        private void DeleteRow(TableLayoutPanel tblLayPnl)
        {
            int last = tblLayPnl.RowStyles.Count - 1;
            tblLayPnl.RowStyles.RemoveAt(last);
            tblLayPnl.Height -= 50;
            foreach (Control item in tblLayPnl.Controls)
            {
                var b = tblLayPnl.GetRow(item);
                if (tblLayPnl.GetRow(item) == last)
                    tblLayPnl.Controls.Remove(item);
            }
        }

        private void SetControlsSettings(ControlCollection controls)
        {
            foreach (Control control in controls)
            {
                if (control is Label)
                    (control as Label).AutoSize = false;
                control.Dock = DockStyle.Fill;
            }
        }
    }
}
