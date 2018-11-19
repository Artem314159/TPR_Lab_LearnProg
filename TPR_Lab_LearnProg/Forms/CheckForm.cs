using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TPR_Lab_LearnProg
{
    public partial class CheckForm : Form
    {
        public CheckForm()
        {
            InitializeComponent();
        }

        private void CheckForm_Load(object sender, EventArgs e)
        {
            StringBuilder s = new StringBuilder();
            double[,] arr = StatistMinMax.CreateMatrI(
                (new double[,] { { 0, 2 }, { 2, 0 }, { 1, 1.5 } }), 
                (new double[,] { { 0.9, 0.3 }, { 0.1, 0.7 } }));
            foreach (var item in arr)
            {
                s.Append(item + ", ");
            }
            MessageBox.Show(s.ToString());
        }
    }
}
