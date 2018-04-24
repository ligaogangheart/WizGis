namespace DXApplication1
{
    partial class quexian
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
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.timeEdit1 = new DevExpress.XtraEditors.TimeEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.timeEdit2 = new DevExpress.XtraEditors.TimeEdit();
            this.QxxxData = new System.Windows.Forms.DataGridView();
            this.splashScreenManager1 = new DevExpress.XtraSplashScreen.SplashScreenManager(this, typeof(global::DXApplication1.floatPage), true, true);
            ((System.ComponentModel.ISupportInitialize)(this.timeEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeEdit2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.QxxxData)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(30, 26);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(54, 14);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "StartTime";
            this.labelControl1.Click += new System.EventHandler(this.labelControl1_Click);
            // 
            // timeEdit1
            // 
            this.timeEdit1.EditValue = new System.DateTime(2018, 4, 23, 0, 0, 0, 0);
            this.timeEdit1.Location = new System.Drawing.Point(125, 23);
            this.timeEdit1.Name = "timeEdit1";
            this.timeEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.timeEdit1.Size = new System.Drawing.Size(100, 20);
            this.timeEdit1.TabIndex = 2;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(606, 29);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(48, 14);
            this.labelControl2.TabIndex = 3;
            this.labelControl2.Text = "EndTime";
            this.labelControl2.Click += new System.EventHandler(this.labelControl2_Click);
            // 
            // timeEdit2
            // 
            this.timeEdit2.EditValue = new System.DateTime(2018, 4, 23, 0, 0, 0, 0);
            this.timeEdit2.Location = new System.Drawing.Point(709, 26);
            this.timeEdit2.Name = "timeEdit2";
            this.timeEdit2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.timeEdit2.Size = new System.Drawing.Size(100, 20);
            this.timeEdit2.TabIndex = 4;
            // 
            // QxxxData
            // 
            this.QxxxData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.QxxxData.Location = new System.Drawing.Point(12, 70);
            this.QxxxData.Name = "QxxxData";
            this.QxxxData.RowTemplate.Height = 23;
            this.QxxxData.Size = new System.Drawing.Size(808, 340);
            this.QxxxData.TabIndex = 5;
            this.QxxxData.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.QxxxData_CellContentClick_1);
            // 
            // splashScreenManager1
            // 
            this.splashScreenManager1.ClosingDelay = 500;
            // 
            // quexian
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(832, 433);
            this.Controls.Add(this.QxxxData);
            this.Controls.Add(this.timeEdit2);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.timeEdit1);
            this.Controls.Add(this.labelControl1);
            this.Name = "quexian";
            this.Text = "quexian";
            ((System.ComponentModel.ISupportInitialize)(this.timeEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeEdit2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.QxxxData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TimeEdit timeEdit1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TimeEdit timeEdit2;
        private System.Windows.Forms.DataGridView QxxxData;
        private DevExpress.XtraSplashScreen.SplashScreenManager splashScreenManager1;
    }
}