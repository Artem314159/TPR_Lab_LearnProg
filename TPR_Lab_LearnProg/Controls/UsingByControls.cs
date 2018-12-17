using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;

namespace TPR_Lab_LearnProg.Controls
{
    public static class UsingByControls
    {
        public static void InitMatrix(this TableLayoutPanel tblLayPnl, string matrName, double[,] matrArray)
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

        public static void InitGMatrix(this TableLayoutPanel tblLayPnl, int r, int M)
        {
            tblLayPnl.Controls.Clear();
            tblLayPnl.RowStyles.Clear();
            tblLayPnl.RowCount = r;
            tblLayPnl.ColumnCount = M;
            for (int i = 0; i < r; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    Label l = new Label()
                    {
                        Font = new Font("Calibri", 12),
                        AutoSize = false,
                        Dock = DockStyle.Fill,
                        TextAlign = ContentAlignment.MiddleCenter,
                        BorderStyle = BorderStyle.FixedSingle
                    };
                    if (i == 0)
                    {
                        if(j != 0)
                        {
                            l.Text = "g" + j;
                        }
                    }
                    else
                    {
                        if (j == 0)
                        {
                            l.Text = "z" + i;
                        }
                        else
                        {
                            l.Text = "α" + StatistMinMax.GetStatistDecisionFunc(r, j, M)[i];
                        }
                    }
                    tblLayPnl.Controls.Add(l, j, i);
                }
            }
        }

        public static List<T> GetAllChildren<T>(this Control control) where T : Control
        {
            List<T> children = new List<T>();
            foreach (Control child in control.Controls)
            {
                if(child is T)
                    children.Add(child as T);
                if (child.Controls != null && child.Controls.Count != 0)
                    children.AddRange(child.GetAllChildren<T>());
            }
            return children;
        }
    }
}
