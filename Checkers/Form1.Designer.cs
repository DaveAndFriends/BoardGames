namespace BoardGames
{
    partial class FormMain
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
            this.tb_Main = new System.Windows.Forms.TextBox();
            this.BtnUpdate = new System.Windows.Forms.Button();
            this.TbEnd = new System.Windows.Forms.TextBox();
            this.TbStart = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ofdMain = new System.Windows.Forms.OpenFileDialog();
            this.BtnSelectFile = new System.Windows.Forms.Button();
            this.BtnNextMove = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tb_Main
            // 
            this.tb_Main.Location = new System.Drawing.Point(445, 145);
            this.tb_Main.Multiline = true;
            this.tb_Main.Name = "tb_Main";
            this.tb_Main.ReadOnly = true;
            this.tb_Main.Size = new System.Drawing.Size(318, 246);
            this.tb_Main.TabIndex = 3;
            // 
            // BtnUpdate
            // 
            this.BtnUpdate.Location = new System.Drawing.Point(129, 18);
            this.BtnUpdate.Name = "BtnUpdate";
            this.BtnUpdate.Size = new System.Drawing.Size(75, 23);
            this.BtnUpdate.TabIndex = 2;
            this.BtnUpdate.Text = "Update";
            this.BtnUpdate.UseVisualStyleBackColor = true;
            this.BtnUpdate.Click += new System.EventHandler(this.BtnUpdate_Click);
            // 
            // TbEnd
            // 
            this.TbEnd.Location = new System.Drawing.Point(77, 20);
            this.TbEnd.Name = "TbEnd";
            this.TbEnd.Size = new System.Drawing.Size(46, 20);
            this.TbEnd.TabIndex = 1;
            // 
            // TbStart
            // 
            this.TbStart.Location = new System.Drawing.Point(25, 20);
            this.TbStart.Name = "TbStart";
            this.TbStart.Size = new System.Drawing.Size(46, 20);
            this.TbStart.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Start";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(77, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "End";
            // 
            // ofdMain
            // 
            this.ofdMain.DefaultExt = "txt";
            this.ofdMain.FileName = "openFileDialog1";
            this.ofdMain.Filter = "Text files|*.txt";
            this.ofdMain.InitialDirectory = "C:\\Users\\Dave\\Desktop\\Temp";
            this.ofdMain.Title = "Select a moves file";
            // 
            // BtnSelectFile
            // 
            this.BtnSelectFile.Location = new System.Drawing.Point(25, 55);
            this.BtnSelectFile.Name = "BtnSelectFile";
            this.BtnSelectFile.Size = new System.Drawing.Size(75, 23);
            this.BtnSelectFile.TabIndex = 6;
            this.BtnSelectFile.Text = "Moves file...";
            this.BtnSelectFile.UseVisualStyleBackColor = true;
            this.BtnSelectFile.Click += new System.EventHandler(this.BtnSelectFile_Click);
            // 
            // BtnNextMove
            // 
            this.BtnNextMove.Location = new System.Drawing.Point(161, 54);
            this.BtnNextMove.Name = "BtnNextMove";
            this.BtnNextMove.Size = new System.Drawing.Size(75, 23);
            this.BtnNextMove.TabIndex = 7;
            this.BtnNextMove.Text = "Next Move";
            this.BtnNextMove.UseVisualStyleBackColor = true;
            this.BtnNextMove.Click += new System.EventHandler(this.BtnNextMove_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(777, 429);
            this.Controls.Add(this.BtnNextMove);
            this.Controls.Add(this.BtnSelectFile);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TbStart);
            this.Controls.Add(this.TbEnd);
            this.Controls.Add(this.BtnUpdate);
            this.Controls.Add(this.tb_Main);
            this.Name = "FormMain";
            this.Text = "Form1";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.FormMain_Paint);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tb_Main;
        private System.Windows.Forms.Button BtnUpdate;
        private System.Windows.Forms.TextBox TbEnd;
        private System.Windows.Forms.TextBox TbStart;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.OpenFileDialog ofdMain;
        private System.Windows.Forms.Button BtnSelectFile;
        private System.Windows.Forms.Button BtnNextMove;
    }
}

