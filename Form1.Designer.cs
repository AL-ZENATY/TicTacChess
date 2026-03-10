namespace TicTacChess
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pnlBoardZy = new Panel();
            tblBoardZy = new TableLayoutPanel();
            btn00 = new Button();
            btn01 = new Button();
            btn02 = new Button();
            btn10 = new Button();
            btn11 = new Button();
            btn12 = new Button();
            btn20 = new Button();
            btn21 = new Button();
            btn22 = new Button();
            lblStatusZy = new Label();
            btnRestartZy = new Button();
            pnlBoardZy.SuspendLayout();
            tblBoardZy.SuspendLayout();
            SuspendLayout();
            // 
            // pnlBoardZy
            // 
            pnlBoardZy.BackColor = Color.Transparent;
            pnlBoardZy.Controls.Add(tblBoardZy);
            pnlBoardZy.ForeColor = SystemColors.ButtonFace;
            pnlBoardZy.Location = new Point(273, 105);
            pnlBoardZy.Name = "pnlBoardZy";
            pnlBoardZy.Size = new Size(485, 469);
            pnlBoardZy.TabIndex = 0;
            // 
            // tblBoardZy
            // 
            tblBoardZy.ColumnCount = 3;
            tblBoardZy.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 48.1595078F));
            tblBoardZy.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 51.8404922F));
            tblBoardZy.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 158F));
            tblBoardZy.Controls.Add(btn00, 0, 0);
            tblBoardZy.Controls.Add(btn01, 1, 0);
            tblBoardZy.Controls.Add(btn02, 2, 0);
            tblBoardZy.Controls.Add(btn10, 0, 1);
            tblBoardZy.Controls.Add(btn11, 1, 1);
            tblBoardZy.Controls.Add(btn12, 2, 1);
            tblBoardZy.Controls.Add(btn20, 0, 2);
            tblBoardZy.Controls.Add(btn21, 1, 2);
            tblBoardZy.Controls.Add(btn22, 2, 2);
            tblBoardZy.Dock = DockStyle.Fill;
            tblBoardZy.Location = new Point(0, 0);
            tblBoardZy.Name = "tblBoardZy";
            tblBoardZy.RowCount = 4;
            tblBoardZy.RowStyles.Add(new RowStyle(SizeType.Percent, 49.5176849F));
            tblBoardZy.RowStyles.Add(new RowStyle(SizeType.Percent, 50.4823151F));
            tblBoardZy.RowStyles.Add(new RowStyle(SizeType.Absolute, 157F));
            tblBoardZy.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tblBoardZy.Size = new Size(485, 469);
            tblBoardZy.TabIndex = 0;
            // 
            // btn00
            // 
            btn00.Dock = DockStyle.Fill;
            btn00.FlatStyle = FlatStyle.Flat;
            btn00.Location = new Point(2, 2);
            btn00.Margin = new Padding(2);
            btn00.Name = "btn00";
            btn00.Size = new Size(153, 140);
            btn00.TabIndex = 0;
            btn00.Tag = "0,0";
            btn00.UseVisualStyleBackColor = true;
            btn00.Click += BoardCell_Click;
            // 
            // btn01
            // 
            btn01.Dock = DockStyle.Fill;
            btn01.FlatStyle = FlatStyle.Flat;
            btn01.Location = new Point(159, 2);
            btn01.Margin = new Padding(2);
            btn01.Name = "btn01";
            btn01.Size = new Size(165, 140);
            btn01.TabIndex = 1;
            btn01.Tag = "0,1";
            btn01.UseVisualStyleBackColor = true;
            btn01.Click += BoardCell_Click;
            // 
            // btn02
            // 
            btn02.Dock = DockStyle.Fill;
            btn02.FlatStyle = FlatStyle.Flat;
            btn02.Location = new Point(328, 2);
            btn02.Margin = new Padding(2);
            btn02.Name = "btn02";
            btn02.Size = new Size(155, 140);
            btn02.TabIndex = 2;
            btn02.Tag = "0,2";
            btn02.UseVisualStyleBackColor = true;
            btn02.Click += BoardCell_Click;
            // 
            // btn10
            // 
            btn10.Dock = DockStyle.Fill;
            btn10.FlatStyle = FlatStyle.Flat;
            btn10.Location = new Point(2, 146);
            btn10.Margin = new Padding(2);
            btn10.Name = "btn10";
            btn10.Size = new Size(153, 143);
            btn10.TabIndex = 3;
            btn10.Tag = "1,0";
            btn10.UseVisualStyleBackColor = true;
            btn10.Click += BoardCell_Click;
            // 
            // btn11
            // 
            btn11.Dock = DockStyle.Fill;
            btn11.FlatStyle = FlatStyle.Flat;
            btn11.Location = new Point(159, 146);
            btn11.Margin = new Padding(2);
            btn11.Name = "btn11";
            btn11.Size = new Size(165, 143);
            btn11.TabIndex = 4;
            btn11.Tag = "1,1";
            btn11.UseVisualStyleBackColor = true;
            btn11.Click += BoardCell_Click;
            // 
            // btn12
            // 
            btn12.Dock = DockStyle.Fill;
            btn12.FlatStyle = FlatStyle.Flat;
            btn12.Location = new Point(328, 146);
            btn12.Margin = new Padding(2);
            btn12.Name = "btn12";
            btn12.Size = new Size(155, 143);
            btn12.TabIndex = 5;
            btn12.Tag = "1,2";
            btn12.UseVisualStyleBackColor = true;
            btn12.Click += BoardCell_Click;
            // 
            // btn20
            // 
            btn20.Dock = DockStyle.Fill;
            btn20.FlatStyle = FlatStyle.Flat;
            btn20.Location = new Point(2, 293);
            btn20.Margin = new Padding(2);
            btn20.Name = "btn20";
            btn20.Size = new Size(153, 153);
            btn20.TabIndex = 6;
            btn20.Tag = "2,0";
            btn20.UseVisualStyleBackColor = true;
            btn20.Click += BoardCell_Click;
            // 
            // btn21
            // 
            btn21.Dock = DockStyle.Fill;
            btn21.FlatStyle = FlatStyle.Flat;
            btn21.Location = new Point(159, 293);
            btn21.Margin = new Padding(2);
            btn21.Name = "btn21";
            btn21.Size = new Size(165, 153);
            btn21.TabIndex = 7;
            btn21.Tag = "2,1";
            btn21.UseVisualStyleBackColor = true;
            btn21.Click += BoardCell_Click;
            // 
            // btn22
            // 
            btn22.Dock = DockStyle.Fill;
            btn22.FlatStyle = FlatStyle.Flat;
            btn22.Location = new Point(328, 293);
            btn22.Margin = new Padding(2);
            btn22.Name = "btn22";
            btn22.Size = new Size(155, 153);
            btn22.TabIndex = 8;
            btn22.Tag = "2,2";
            btn22.UseVisualStyleBackColor = true;
            btn22.Click += BoardCell_Click;
            // 
            // lblStatusZy
            // 
            lblStatusZy.AutoSize = true;
            lblStatusZy.Location = new Point(491, 652);
            lblStatusZy.Name = "lblStatusZy";
            lblStatusZy.Size = new Size(38, 15);
            lblStatusZy.TabIndex = 1;
            lblStatusZy.Text = "label1";
            // 
            // btnRestartZy
            // 
            btnRestartZy.BackColor = Color.Transparent;
            btnRestartZy.BackgroundImage = Properties.Resources.resetBtnHover;
            btnRestartZy.BackgroundImageLayout = ImageLayout.Zoom;
            btnRestartZy.Location = new Point(233, 612);
            btnRestartZy.Name = "btnRestartZy";
            btnRestartZy.Size = new Size(156, 73);
            btnRestartZy.TabIndex = 2;
            btnRestartZy.UseVisualStyleBackColor = false;
            btnRestartZy.Click += btnRestartZy_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.background;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1145, 780);
            Controls.Add(btnRestartZy);
            Controls.Add(lblStatusZy);
            Controls.Add(pnlBoardZy);
            Name = "Form1";
            Text = "Form1";
            pnlBoardZy.ResumeLayout(false);
            tblBoardZy.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel pnlBoardZy;
        private TableLayoutPanel tblBoardZy;
        private Button btn00;
        private Button btn01;
        private Button btn02;
        private Button btn10;
        private Button btn11;
        private Button btn12;
        private Button btn20;
        private Button btn21;
        private Button btn22;
        private Label lblStatusZy;
        private Button btnRestartZy;
    }
}
