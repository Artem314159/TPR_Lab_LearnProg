using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

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
                    try
                    {
                        rTxtBox.LoadFile($"../../Sources/{rTxtBox.Name}.rtf");
                        int start = 0;
                        for (int i = 0; i < rTxtBox.Lines.Length; i++)
                        {
                            rTxtBox.Select(start, rTxtBox.Lines[i].Length);
                            // checking if line is math formula (line is empty)
                            if (string.IsNullOrWhiteSpace(rTxtBox.Lines[i]))
                            {
                                rTxtBox.SelectionColor = Color.White;
                                rTxtBox.SelectionAlignment = HorizontalAlignment.Center;
                            }
                            else rTxtBox.SelectionColor = Color.Black;
                            start += rTxtBox.Lines[i].Length + 1;
                        }
                    }
                    catch { }
                    rTxtBox.Select(0,0);
                }
            }

            InitMatrices();
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

        private void InitMatrices()
        {
            StatistMinMaxCriterionTask task = new StatistMinMaxCriterionTask(matrQ, matrZ);
            tblLayPnlQ1.InitMatrix("Q", matrQ);
            tblLayPnlZ1.InitMatrix("Z", matrZ);
            tblLayPnlL1.InitMatrix("L", task.GetMatrL);
            tblLayPnlZ2.InitMatrix("Z", matrZ);
            tblLayPnlG.InitGMatrix(2, 3, 9);
            tblLayPnlI.InitIMatrix(task.GetMatrI);
            chart1.InitPayoffSet(task);
            chart2.InitPayoffSet(task);
            chart2.InitTaskSolution(task);
        }
    }
}