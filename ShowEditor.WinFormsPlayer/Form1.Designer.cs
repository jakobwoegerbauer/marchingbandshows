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
            this.tvElements = new System.Windows.Forms.TreeView();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.lvActions = new System.Windows.Forms.ListView();
            this.dgAction = new System.Windows.Forms.DataGridView();
            this.Parameter = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.panel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgAction)).BeginInit();
            this.SuspendLayout();
            // 
            // panel
            // 
            this.panel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel.Location = new System.Drawing.Point(659, 11);
            this.panel.Margin = new System.Windows.Forms.Padding(2);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(677, 755);
            this.panel.TabIndex = 0;
            this.panel.TabStop = false;
            this.panel.Click += new System.EventHandler(this.panel_Click);
            // 
            // lblStep
            // 
            this.lblStep.AutoSize = true;
            this.lblStep.Location = new System.Drawing.Point(337, 24);
            this.lblStep.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblStep.Name = "lblStep";
            this.lblStep.Size = new System.Drawing.Size(29, 13);
            this.lblStep.TabIndex = 1;
            this.lblStep.Text = "Step";
            // 
            // btnStep
            // 
            this.btnStep.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnStep.Location = new System.Drawing.Point(891, 773);
            this.btnStep.Margin = new System.Windows.Forms.Padding(2);
            this.btnStep.Name = "btnStep";
            this.btnStep.Size = new System.Drawing.Size(75, 21);
            this.btnStep.TabIndex = 2;
            this.btnStep.Text = "Next Step";
            this.btnStep.UseVisualStyleBackColor = true;
            this.btnStep.Click += new System.EventHandler(this.btnStep_Click);
            // 
            // tvElements
            // 
            this.tvElements.Location = new System.Drawing.Point(12, 12);
            this.tvElements.Name = "tvElements";
            this.tvElements.Size = new System.Drawing.Size(311, 370);
            this.tvElements.TabIndex = 3;
            this.tvElements.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvElements_AfterSelect);
            this.tvElements.DoubleClick += new System.EventHandler(this.tvElements_DoubleClick);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(329, 255);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(311, 127);
            this.dataGridView1.TabIndex = 4;
            // 
            // lvActions
            // 
            this.lvActions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lvActions.Location = new System.Drawing.Point(12, 430);
            this.lvActions.Name = "lvActions";
            this.lvActions.Size = new System.Drawing.Size(311, 359);
            this.lvActions.TabIndex = 5;
            this.lvActions.UseCompatibleStateImageBehavior = false;
            this.lvActions.View = System.Windows.Forms.View.List;
            this.lvActions.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvActions_ItemSelectionChanged);
            // 
            // dgAction
            // 
            this.dgAction.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dgAction.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgAction.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgAction.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Parameter,
            this.Value});
            this.dgAction.Location = new System.Drawing.Point(329, 430);
            this.dgAction.Name = "dgAction";
            this.dgAction.Size = new System.Drawing.Size(311, 359);
            this.dgAction.TabIndex = 6;
            // 
            // Parameter
            // 
            this.Parameter.HeaderText = "Parameter";
            this.Parameter.Name = "Parameter";
            this.Parameter.Width = 80;
            // 
            // Value
            // 
            this.Value.HeaderText = "Wert";
            this.Value.Name = "Value";
            this.Value.Width = 55;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1347, 801);
            this.Controls.Add(this.dgAction);
            this.Controls.Add(this.lvActions);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.tvElements);
            this.Controls.Add(this.btnStep);
            this.Controls.Add(this.lblStep);
            this.Controls.Add(this.panel);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgAction)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox panel;
        private System.Windows.Forms.Label lblStep;
        private System.Windows.Forms.Button btnStep;
        private System.Windows.Forms.TreeView tvElements;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ListView lvActions;
        private System.Windows.Forms.DataGridView dgAction;
        private System.Windows.Forms.DataGridViewTextBoxColumn Parameter;
        private System.Windows.Forms.DataGridViewTextBoxColumn Value;
    }
}

