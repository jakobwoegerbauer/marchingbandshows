namespace ShowEditor.WinFormsPlayer
{
    partial class Form1
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel = new System.Windows.Forms.PictureBox();
            this.lblStep = new System.Windows.Forms.Label();
            this.btnStep = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.panel)).BeginInit();
            this.SuspendLayout();
            // 
            // panel
            // 
            this.panel.Location = new System.Drawing.Point(12, 12);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(1058, 606);
            this.panel.TabIndex = 0;
            this.panel.TabStop = false;
            this.panel.Click += new System.EventHandler(this.panel_Click);
            this.panel.Paint += new System.Windows.Forms.PaintEventHandler(this.panel_Paint);
            // 
            // lblStep
            // 
            this.lblStep.AutoSize = true;
            this.lblStep.Location = new System.Drawing.Point(26, 629);
            this.lblStep.Name = "lblStep";
            this.lblStep.Size = new System.Drawing.Size(37, 17);
            this.lblStep.TabIndex = 1;
            this.lblStep.Text = "Step";
            // 
            // btnStep
            // 
            this.btnStep.Location = new System.Drawing.Point(129, 624);
            this.btnStep.Name = "btnStep";
            this.btnStep.Size = new System.Drawing.Size(100, 26);
            this.btnStep.TabIndex = 2;
            this.btnStep.Text = "Next Step";
            this.btnStep.UseVisualStyleBackColor = true;
            this.btnStep.Click += new System.EventHandler(this.btnStep_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1082, 662);
            this.Controls.Add(this.btnStep);
            this.Controls.Add(this.lblStep);
            this.Controls.Add(this.panel);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panel)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox panel;
        private System.Windows.Forms.Label lblStep;
        private System.Windows.Forms.Button btnStep;
    }
}

