using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
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
                        if (j != 1)
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

        public static void InitGMatrix(this TableLayoutPanel tblLayPnl, int r, int m, int M)
        {
            tblLayPnl.Controls.Clear();
            tblLayPnl.RowStyles.Clear();
            tblLayPnl.ColumnStyles.Clear();
            tblLayPnl.RowCount = r + 1;
            tblLayPnl.ColumnCount = M + 1;
            for (int i = 0; i < r + 1; i++)
            {
                tblLayPnl.RowStyles.Add(new RowStyle(SizeType.Percent, 100F / (r + 1)));
                for (int j = 0; j < M + 1; j++)
                {
                    Label l = new Label()
                    {
                        Font = new Font("Calibri", 12),
                        AutoSize = false,
                        Dock = DockStyle.Fill,
                        TextAlign = ContentAlignment.MiddleCenter,
                        BorderStyle = BorderStyle.FixedSingle,
                        Margin = new Padding(0)
                    };
                    if (i == 0)
                    {
                        tblLayPnl.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F / (M + 1)));
                        if (j != 0)
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
                            l.Text = "α" + (StatistMinMax.GetStatistDecisionFunc(r, m, j - 1)[(i - 1)] + 1);
                        }
                    }
                    tblLayPnl.Controls.Add(l, j, i);
                }
            }
        }

        public static void InitIMatrix(this TableLayoutPanel tblLayPnl, List<Point> matrList)
        {
            int M = matrList.Count, n = 2;
            tblLayPnl.Controls.Clear();
            tblLayPnl.RowStyles.Clear();
            tblLayPnl.ColumnStyles.Clear();
            tblLayPnl.RowCount = M + 1;
            tblLayPnl.ColumnCount = n + 1;
            for (int i = 0; i < M + 1; i++)
            {
                tblLayPnl.RowStyles.Add(new RowStyle(SizeType.Percent, 100F / (M + 1)));
                for (int j = 0; j < n + 1; j++)
                {
                    Label l = new Label()
                    {
                        Font = new Font("Calibri", 12),
                        AutoSize = false,
                        Dock = DockStyle.Fill,
                        TextAlign = ContentAlignment.MiddleCenter,
                        BorderStyle = BorderStyle.FixedSingle,
                        Margin = new Padding(0)
                    };
                    if (i == 0)
                    {
                        tblLayPnl.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F / (n + 1)));
                        if (j != 0)
                        {
                            l.Text = "β" + j;
                        }
                    }
                    else
                    {
                        if (j == 0)
                        {
                            l.Text = "g" + i;
                        }
                        else
                        {
                            l.Text = (j == 1 ? matrList[i - 1].X : matrList[i - 1].Y).ToString();
                        }
                    }
                    tblLayPnl.Controls.Add(l, j, i);
                }
            }
        }

        public static void InitPayoffSet(this Chart chart, StatistMinMaxCriterionTask task)
        {
            chart.Series.Clear();
            List<Point> convexHull = task.CalcConvexHull();
            chart.Series.Add(new Series() {
                //Name = "ConvexHullSeries",
                ChartType = SeriesChartType.Line,
                BorderWidth = 5,
                Color = Color.Black
            });
            chart.Series.Add(new Series()
            {
                //Name = "FullSeries",
                ChartType = SeriesChartType.Point,
                MarkerBorderColor = Color.Black,
                MarkerBorderWidth = 2,
                MarkerSize = 10,
                MarkerColor = Color.Lime,
                MarkerStyle = MarkerStyle.Circle
            });
            chart.Series[0].Points.DataBind(convexHull, "X", "Y", "");
            chart.Series[1].Points.DataBind(task.GetMatrI, "X", "Y", "");

            double ratio = chart.Size.Width == 0 ? 0 : chart.Size.Height / (double)chart.Size.Width;
            chart.ChartAreas[0].AxisX.Minimum = 0;
            chart.ChartAreas[0].AxisY.Minimum = 0;

            double maxX = convexHull.Where(p => p.X == convexHull.Max(max => max.X)).First().X;
            double maxY = convexHull.Where(p => p.Y == convexHull.Max(max => max.Y)).First().Y;
            chart.ChartAreas[0].AxisX.Maximum = Math.Max(maxX, maxY / ratio);
            chart.ChartAreas[0].AxisY.Maximum = Math.Max(maxX * ratio, maxY);

            double interval = Math.Round(maxX / 5, Math.Max(0, -PowOf10(maxX / 5)));
            chart.ChartAreas[0].AxisX.Interval = interval;
            chart.ChartAreas[0].AxisY.Interval = interval;
        }

        public static int PowOf10(double n)
        {
            int res = 0;
            double absN = Math.Abs(n);
            if (absN < 1 && absN != 0)
            {
                while (absN < 1)
                {
                    res--;
                    absN *= 10;
                }
            }
            else
            {
                while (absN >= 10)
                {
                    res++;
                    absN /= 10;
                }
            }
            return res;
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
