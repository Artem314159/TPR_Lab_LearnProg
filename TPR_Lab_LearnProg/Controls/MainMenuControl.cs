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
    public partial class MainMenuControl : UserControl
    {
        public MainMenuControl()
        {
            InitializeComponent();
        }

        private void TrainingBtn_Click(object sender, EventArgs e)
        {
            ControlFuncs.ChangeScene("MainMenuControl", "TrainingControl", InitFormType.InitAfterMainMenu);
        }

        private void CheckKnowBtn_Click(object sender, EventArgs e)
        {
            ControlFuncs.ChangeScene("MainMenuControl", "CheckKnowControl", InitFormType.InitAfterMainMenu);
        }
    }
}
