namespace TPR_Lab_LearnProg.Controls
{
    partial class MainMenuControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.TrainingBtn = new System.Windows.Forms.Button();
            this.CheckKnowBtn = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // TrainingBtn
            // 
            this.TrainingBtn.Dock = System.Windows.Forms.DockStyle.Top;
            this.TrainingBtn.Font = new System.Drawing.Font("Segoe Script", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TrainingBtn.Location = new System.Drawing.Point(0, 0);
            this.TrainingBtn.Name = "TrainingBtn";
            this.TrainingBtn.Size = new System.Drawing.Size(165, 40);
            this.TrainingBtn.TabIndex = 0;
            this.TrainingBtn.Text = "Обучение";
            this.TrainingBtn.UseVisualStyleBackColor = true;
            // 
            // CheckKnowBtn
            // 
            this.CheckKnowBtn.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.CheckKnowBtn.Font = new System.Drawing.Font("Segoe Script", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CheckKnowBtn.Location = new System.Drawing.Point(0, 50);
            this.CheckKnowBtn.Name = "CheckKnowBtn";
            this.CheckKnowBtn.Size = new System.Drawing.Size(165, 40);
            this.CheckKnowBtn.TabIndex = 1;
            this.CheckKnowBtn.Text = "Проверить знания";
            this.CheckKnowBtn.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel1.Controls.Add(this.TrainingBtn);
            this.panel1.Controls.Add(this.CheckKnowBtn);
            this.panel1.Location = new System.Drawing.Point(217, 105);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(165, 90);
            this.panel1.TabIndex = 2;
            // 
            // MainMenuControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "MainMenuControl";
            this.Size = new System.Drawing.Size(600, 300);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button TrainingBtn;
        private System.Windows.Forms.Button CheckKnowBtn;
        private System.Windows.Forms.Panel panel1;
    }
}
