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

        StatistMinMaxCriterionTask task;

        #endregion

        public CheckKnowControl()
        {
            InitializeComponent();
            InitPage1();
        }

        private void CheckKnowControl_Load(object sender, EventArgs e)
        {

        }

        private void InitPage1()
        {
            int m = (int)Q_NUD.Value, r = (int)Z_NUD.Value;
            tblLayPnlQ.RowCount = m + 1;
            tblLayPnlZ.RowCount = r + 1;
            tblLayPnlQ.RowStyles.Clear();
            tblLayPnlZ.RowStyles.Clear();

            tblLayPnlQ.Height = 50;
            tblLayPnlQ.RowStyles.Add(new RowStyle(SizeType.Absolute, 50));
            tblLayPnlQ.Controls.Add(new Label() { Text = "β1" }, 1, 0);
            tblLayPnlQ.Controls.Add(new Label() { Text = "β2" }, 2, 0);
            for (int i = 0; i < m; i++)
            {
                tblLayPnlQ.tblLayPnlAddCustomRow(i + 1, "α");
            }
            UsingByControls.SetControlsSettings(tblLayPnlQ.Controls);

            tblLayPnlZ.Height = 50;
            tblLayPnlZ.RowStyles.Add(new RowStyle(SizeType.Absolute, 50));
            tblLayPnlZ.Controls.Add(new Label() { Text = "β1" }, 1, 0);
            tblLayPnlZ.Controls.Add(new Label() { Text = "β2" }, 2, 0);
            for (int i = 0; i < r; i++)
            {
                tblLayPnlZ.tblLayPnlAddCustomRow(i + 1, "z");
            }
            UsingByControls.SetControlsSettings(tblLayPnlZ.Controls);
        }

        private void InitPage2()
        {
            task = new StatistMinMaxCriterionTask(matrQ, matrZ);
            int m = (int)Q_NUD.Value;//, r = (int)Z_NUD.Value + 1;
            tblLayPnlQlabel.NUD_ValueChanged(m, "α", true);
            tblLayPnlL.NUD_ValueChanged(m, "α");
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < tblLayPnlQlabel.ColumnCount - 1; j++)
                {
                    tblLayPnlQlabel.GetControlFromPosition(j + 1, i + 1).Text = matrQ[i, j].ToString();
                }
            }
        }

        private void InitPage3()
        {
            int m = (int)Q_NUD.Value, r = (int)Z_NUD.Value, M = (int)M_NUD.Value;
            tblLayPnlLlabel.NUD_ValueChanged(m, "α", true);
            tblLayPnlZlabel.NUD_ValueChanged(r, "z", true);
            tblLayPnlI.NUD_ValueChanged(M, "g");

            double[,] matrL = task.GetMatrL;
            double[,] matrZ = task.GetMatrZ;
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < tblLayPnlLlabel.ColumnCount - 1; j++)
                {
                    tblLayPnlLlabel.GetControlFromPosition(j + 1, i + 1).Text = matrL[i, j].ToString();
                }
            }
            for (int i = 0; i < r; i++)
            {
                for (int j = 0; j < tblLayPnlZlabel.ColumnCount - 1; j++)
                {
                    tblLayPnlZlabel.GetControlFromPosition(j + 1, i + 1).Text = matrZ[i, j].ToString();
                }
            }
        }

        private void InitPage4()
        {
            chart1.InitPayoffSet(task);
            //chart1.InitTaskSolution(task);
            //StringBuilder text = new StringBuilder("X=(");
            //List<Point> calcConvexHull = task.CalcConvexHull();
            //double[] arr = MinMax.GetSolution(calcConvexHull, out Point taskPoint);
            //foreach (Point item in task.GetMatrI)
            //{
            //    double value = Array.IndexOf(arr, item) != -1 ? arr[Array.IndexOf(arr, item)] : 0;
            //    text.Append(value + "; ");
            //}
            //text.Remove(text.Length - 2, 2);
            //text.Append(").");
            //RTxtBox.Text = text.ToString();
            RTxtBox.SelectAll();
            RTxtBox.SelectionColor = Color.Black;
        }

        private void ExitBtn_Click(object sender, EventArgs e)
        {
            ControlFuncs.ChangeScene("CheckKnowControl", "MainMenuControl", InitFormType.InitForMainMenu);
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
            if (/*TabPageCanChaged(position) && */position != 0)
                TabControl.SelectedTab = TabControl.TabPages[position - 1];
        }

        private bool tabPage1_Chaged()
        {
            for (int i = 1; i < tblLayPnlZ.ColumnCount; i++)
            {
                double iSum = 0;
                for (int j = 1; j < tblLayPnlZ.RowCount; j++)
                {
                    iSum += Convert.ToDouble(tblLayPnlZ.GetControlFromPosition(i, j).Text);
                }
                if (iSum != 1)
                {
                    MessageBox.Show("Matrix Z is wrong.");
                    return false;
                }
            }

            bool res = (int)M_NUD.Value == (int)Math.Pow(tblLayPnlQ.RowCount - 1, tblLayPnlZ.RowCount - 1);
            if (!res)
            {
                MessageBox.Show("Value M is wrong.");
                return false;
            }

            tblLayPnlQ.CreateMatr(out matrQ);
            tblLayPnlZ.CreateMatr(out matrZ);
            InitPage2();
            return true;
        }

        private bool tabPage2_Chaged()
        {
            double[,] matrL = task.GetMatrL;
            for (int i = 0; i < matrQ.GetLength(0); i++)
            {
                for (int j = 0; j < matrQ.GetLength(1); j++)
                {
                    if(matrL[i,j] != Convert.ToDouble(tblLayPnlL.GetControlFromPosition(j+1, i+1).Text))
                    {
                        MessageBox.Show("Matrix L is wrong.");
                        return false;
                    }
                }
            }
            InitPage3();
            return true;
        }

        private bool tabPage3_Chaged()
        {
            List<Point> matrI = task.GetMatrI;
            for (int i = 0; i < matrQ.GetLength(0); i++)
            {
                if (matrI[i].X != Convert.ToDouble(tblLayPnlI.GetControlFromPosition(1, i + 1).Text) ||
                    matrI[i].Y != Convert.ToDouble(tblLayPnlI.GetControlFromPosition(2, i + 1).Text))
                {
                    MessageBox.Show("Matrix I is wrong.");
                    return false;
                }
            }
            InitPage4();
            return true;
        }

        private bool TabPageCanChaged(int selectedTab)
        {
            switch (selectedTab)
            {
                case 0:
                    return tabPage1_Chaged();
                case 1:
                    return tabPage2_Chaged();
                case 2:
                    return tabPage3_Chaged();
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
            tblLayPnlQ.NUD_ValueChanged((int)Q_NUD.Value, "α");
        }

        private void Z_NUD_ValueChanged(object sender, EventArgs e)
        {
            tblLayPnlZ.NUD_ValueChanged((int)Z_NUD.Value, "z");
        }
    }
}
