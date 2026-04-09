
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
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
            btnRestartZy = new Button();
            picLampGoldZy = new PictureBox();
            picLampSilverZy = new PictureBox();
            pbGoldZy = new PictureBox();
            pbSilverZy = new PictureBox();
            picSetup1 = new PictureBox();
            picSetup2 = new PictureBox();
            picSetup3 = new PictureBox();
            videoPlayerZy = new AxWMPLib.AxWindowsMediaPlayer();
            videoHideTimerZy = new System.Windows.Forms.Timer(components);
            pbxTagZy = new PictureBox();
            lblStatusZy = new Label();
            btnGoldSetupZy = new Button();
            btnSilverSetupZy = new Button();
            btnZeroRobotZy = new Button();
            pictureBox1 = new PictureBox();
            picSetup4 = new PictureBox();
            picSetup5 = new PictureBox();
            btnTogglePiecesZy = new Button();
            pnlBoardZy.SuspendLayout();
            tblBoardZy.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picLampGoldZy).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picLampSilverZy).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbGoldZy).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbSilverZy).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picSetup1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picSetup2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picSetup3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)videoPlayerZy).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbxTagZy).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picSetup4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picSetup5).BeginInit();
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
            tblBoardZy.RowStyles.Add(new RowStyle(SizeType.Percent, 48.844883F));
            tblBoardZy.RowStyles.Add(new RowStyle(SizeType.Percent, 51.155117F));
            tblBoardZy.RowStyles.Add(new RowStyle(SizeType.Absolute, 155F));
            tblBoardZy.RowStyles.Add(new RowStyle(SizeType.Absolute, 10F));
            tblBoardZy.Size = new Size(485, 469);
            tblBoardZy.TabIndex = 0;
            // 
            // btn00
            // 
            btn00.Cursor = Cursors.Hand;
            btn00.Dock = DockStyle.Fill;
            btn00.FlatAppearance.BorderSize = 0;
            btn00.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btn00.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btn00.FlatStyle = FlatStyle.Flat;
            btn00.ForeColor = Color.Transparent;
            btn00.Location = new Point(2, 2);
            btn00.Margin = new Padding(2);
            btn00.Name = "btn00";
            btn00.Size = new Size(153, 144);
            btn00.TabIndex = 0;
            btn00.Tag = "0,0";
            btn00.UseVisualStyleBackColor = false;
            btn00.Click += BoardCell_Click;
            btn00.MouseEnter += BoardCell_MouseEnter;
            btn00.MouseLeave += BoardCell_MouseLeave;
            // 
            // btn01
            // 
            btn01.Cursor = Cursors.Hand;
            btn01.Dock = DockStyle.Fill;
            btn01.FlatAppearance.BorderSize = 0;
            btn01.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btn01.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btn01.FlatStyle = FlatStyle.Flat;
            btn01.ForeColor = Color.Transparent;
            btn01.Location = new Point(159, 2);
            btn01.Margin = new Padding(2);
            btn01.Name = "btn01";
            btn01.Size = new Size(165, 144);
            btn01.TabIndex = 1;
            btn01.Tag = "0,1";
            btn01.UseVisualStyleBackColor = false;
            btn01.Click += BoardCell_Click;
            btn01.MouseEnter += BoardCell_MouseEnter;
            btn01.MouseLeave += BoardCell_MouseLeave;
            // 
            // btn02
            // 
            btn02.Cursor = Cursors.Hand;
            btn02.Dock = DockStyle.Fill;
            btn02.FlatAppearance.BorderSize = 0;
            btn02.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btn02.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btn02.FlatStyle = FlatStyle.Flat;
            btn02.ForeColor = Color.Transparent;
            btn02.Location = new Point(328, 2);
            btn02.Margin = new Padding(2);
            btn02.Name = "btn02";
            btn02.Size = new Size(155, 144);
            btn02.TabIndex = 2;
            btn02.Tag = "0,2";
            btn02.Text = " ";
            btn02.UseVisualStyleBackColor = false;
            btn02.Click += BoardCell_Click;
            btn02.MouseEnter += BoardCell_MouseEnter;
            btn02.MouseLeave += BoardCell_MouseLeave;
            // 
            // btn10
            // 
            btn10.Cursor = Cursors.Hand;
            btn10.Dock = DockStyle.Fill;
            btn10.FlatAppearance.BorderSize = 0;
            btn10.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btn10.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btn10.FlatStyle = FlatStyle.Flat;
            btn10.ForeColor = Color.Transparent;
            btn10.Location = new Point(2, 150);
            btn10.Margin = new Padding(2);
            btn10.Name = "btn10";
            btn10.Size = new Size(153, 151);
            btn10.TabIndex = 3;
            btn10.Tag = "1,0";
            btn10.UseVisualStyleBackColor = false;
            btn10.Click += BoardCell_Click;
            btn10.MouseEnter += BoardCell_MouseEnter;
            btn10.MouseLeave += BoardCell_MouseLeave;
            // 
            // btn11
            // 
            btn11.Cursor = Cursors.Hand;
            btn11.Dock = DockStyle.Fill;
            btn11.FlatAppearance.BorderSize = 0;
            btn11.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btn11.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btn11.FlatStyle = FlatStyle.Flat;
            btn11.ForeColor = Color.Transparent;
            btn11.Location = new Point(159, 150);
            btn11.Margin = new Padding(2);
            btn11.Name = "btn11";
            btn11.Size = new Size(165, 151);
            btn11.TabIndex = 4;
            btn11.Tag = "1,1";
            btn11.UseVisualStyleBackColor = false;
            btn11.Click += BoardCell_Click;
            btn11.MouseEnter += BoardCell_MouseEnter;
            btn11.MouseLeave += BoardCell_MouseLeave;
            // 
            // btn12
            // 
            btn12.Cursor = Cursors.Hand;
            btn12.Dock = DockStyle.Fill;
            btn12.FlatAppearance.BorderSize = 0;
            btn12.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btn12.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btn12.FlatStyle = FlatStyle.Flat;
            btn12.ForeColor = Color.Transparent;
            btn12.Location = new Point(328, 150);
            btn12.Margin = new Padding(2);
            btn12.Name = "btn12";
            btn12.Size = new Size(155, 151);
            btn12.TabIndex = 5;
            btn12.Tag = "1,2";
            btn12.UseVisualStyleBackColor = false;
            btn12.Click += BoardCell_Click;
            btn12.MouseEnter += BoardCell_MouseEnter;
            btn12.MouseLeave += BoardCell_MouseLeave;
            // 
            // btn20
            // 
            btn20.Cursor = Cursors.Hand;
            btn20.Dock = DockStyle.Fill;
            btn20.FlatAppearance.BorderSize = 0;
            btn20.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btn20.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btn20.FlatStyle = FlatStyle.Flat;
            btn20.ForeColor = Color.Transparent;
            btn20.Location = new Point(2, 305);
            btn20.Margin = new Padding(2);
            btn20.Name = "btn20";
            btn20.Size = new Size(153, 151);
            btn20.TabIndex = 6;
            btn20.Tag = "2,0";
            btn20.UseVisualStyleBackColor = false;
            btn20.Click += BoardCell_Click;
            btn20.MouseEnter += BoardCell_MouseEnter;
            btn20.MouseLeave += BoardCell_MouseLeave;
            // 
            // btn21
            // 
            btn21.Cursor = Cursors.Hand;
            btn21.Dock = DockStyle.Fill;
            btn21.FlatAppearance.BorderSize = 0;
            btn21.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btn21.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btn21.FlatStyle = FlatStyle.Flat;
            btn21.ForeColor = Color.Transparent;
            btn21.Location = new Point(159, 305);
            btn21.Margin = new Padding(2);
            btn21.Name = "btn21";
            btn21.Size = new Size(165, 151);
            btn21.TabIndex = 7;
            btn21.Tag = "2,1";
            btn21.UseVisualStyleBackColor = false;
            btn21.Click += BoardCell_Click;
            btn21.MouseEnter += BoardCell_MouseEnter;
            btn21.MouseLeave += BoardCell_MouseLeave;
            // 
            // btn22
            // 
            btn22.Cursor = Cursors.Hand;
            btn22.Dock = DockStyle.Fill;
            btn22.FlatAppearance.BorderSize = 0;
            btn22.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btn22.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btn22.FlatStyle = FlatStyle.Flat;
            btn22.ForeColor = Color.Transparent;
            btn22.Location = new Point(328, 305);
            btn22.Margin = new Padding(2);
            btn22.Name = "btn22";
            btn22.Size = new Size(155, 151);
            btn22.TabIndex = 8;
            btn22.Tag = "2,2";
            btn22.UseVisualStyleBackColor = false;
            btn22.Click += BoardCell_Click;
            btn22.MouseEnter += BoardCell_MouseEnter;
            btn22.MouseLeave += BoardCell_MouseLeave;
            // 
            // btnRestartZy
            // 
            btnRestartZy.BackColor = Color.Transparent;
            btnRestartZy.BackgroundImage = Properties.Resources.resetBtn;
            btnRestartZy.BackgroundImageLayout = ImageLayout.Stretch;
            btnRestartZy.Cursor = Cursors.Hand;
            btnRestartZy.FlatAppearance.BorderSize = 0;
            btnRestartZy.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnRestartZy.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnRestartZy.FlatStyle = FlatStyle.Flat;
            btnRestartZy.Location = new Point(234, 617);
            btnRestartZy.Name = "btnRestartZy";
            btnRestartZy.Size = new Size(153, 63);
            btnRestartZy.TabIndex = 2;
            btnRestartZy.UseVisualStyleBackColor = false;
            btnRestartZy.Click += btnRestartZy_Click;
            btnRestartZy.MouseEnter += btnRestartZy_MouseEnter;
            btnRestartZy.MouseLeave += btnRestartZy_MouseLeave;
            // 
            // picLampGoldZy
            // 
            picLampGoldZy.BackColor = Color.Transparent;
            picLampGoldZy.BackgroundImage = Properties.Resources.lightOff;
            picLampGoldZy.BackgroundImageLayout = ImageLayout.Stretch;
            picLampGoldZy.Location = new Point(814, 164);
            picLampGoldZy.Name = "picLampGoldZy";
            picLampGoldZy.Size = new Size(34, 81);
            picLampGoldZy.TabIndex = 4;
            picLampGoldZy.TabStop = false;
            // 
            // picLampSilverZy
            // 
            picLampSilverZy.BackColor = Color.Transparent;
            picLampSilverZy.BackgroundImage = Properties.Resources.lightOff;
            picLampSilverZy.BackgroundImageLayout = ImageLayout.Stretch;
            picLampSilverZy.Location = new Point(814, 419);
            picLampSilverZy.Name = "picLampSilverZy";
            picLampSilverZy.Size = new Size(36, 81);
            picLampSilverZy.TabIndex = 5;
            picLampSilverZy.TabStop = false;
            // 
            // pbGoldZy
            // 
            pbGoldZy.BackColor = Color.Transparent;
            pbGoldZy.BackgroundImage = Properties.Resources.tagGoldshort;
            pbGoldZy.BackgroundImageLayout = ImageLayout.Stretch;
            pbGoldZy.Location = new Point(807, 249);
            pbGoldZy.Name = "pbGoldZy";
            pbGoldZy.Size = new Size(50, 34);
            pbGoldZy.TabIndex = 6;
            pbGoldZy.TabStop = false;
            // 
            // pbSilverZy
            // 
            pbSilverZy.BackColor = Color.Transparent;
            pbSilverZy.BackgroundImage = Properties.Resources.tagSilvershort;
            pbSilverZy.BackgroundImageLayout = ImageLayout.Stretch;
            pbSilverZy.Location = new Point(807, 506);
            pbSilverZy.Name = "pbSilverZy";
            pbSilverZy.Size = new Size(50, 34);
            pbSilverZy.TabIndex = 7;
            pbSilverZy.TabStop = false;
            // 
            // picSetup1
            // 
            picSetup1.BackColor = Color.Transparent;
            picSetup1.BackgroundImage = Properties.Resources.spQ;
            picSetup1.BackgroundImageLayout = ImageLayout.Zoom;
            picSetup1.Cursor = Cursors.Hand;
            picSetup1.Location = new Point(937, 107);
            picSetup1.Name = "picSetup1";
            picSetup1.Size = new Size(127, 176);
            picSetup1.SizeMode = PictureBoxSizeMode.Zoom;
            picSetup1.TabIndex = 12;
            picSetup1.TabStop = false;
            picSetup1.Visible = false;
            picSetup1.Click += picSetup1_Click;
            picSetup1.MouseEnter += SetupPiece_MouseEnter;
            picSetup1.MouseLeave += SetupPiece_MouseLeave;
            // 
            // picSetup2
            // 
            picSetup2.BackColor = Color.Transparent;
            picSetup2.BackgroundImage = Properties.Resources.spR;
            picSetup2.BackgroundImageLayout = ImageLayout.Zoom;
            picSetup2.Cursor = Cursors.Hand;
            picSetup2.Location = new Point(937, 302);
            picSetup2.Name = "picSetup2";
            picSetup2.Size = new Size(127, 176);
            picSetup2.SizeMode = PictureBoxSizeMode.Zoom;
            picSetup2.TabIndex = 13;
            picSetup2.TabStop = false;
            picSetup2.Visible = false;
            picSetup2.Click += picSetup2_Click;
            picSetup2.MouseEnter += SetupPiece_MouseEnter;
            picSetup2.MouseLeave += SetupPiece_MouseLeave;
            // 
            // picSetup3
            // 
            picSetup3.BackColor = Color.Transparent;
            picSetup3.BackgroundImage = Properties.Resources.spKN;
            picSetup3.BackgroundImageLayout = ImageLayout.Zoom;
            picSetup3.Cursor = Cursors.Hand;
            picSetup3.Location = new Point(937, 502);
            picSetup3.Name = "picSetup3";
            picSetup3.Size = new Size(127, 176);
            picSetup3.SizeMode = PictureBoxSizeMode.Zoom;
            picSetup3.TabIndex = 14;
            picSetup3.TabStop = false;
            picSetup3.Visible = false;
            picSetup3.Click += picSetup3_Click;
            picSetup3.MouseEnter += SetupPiece_MouseEnter;
            picSetup3.MouseLeave += SetupPiece_MouseLeave;
            // 
            // videoPlayerZy
            // 
            videoPlayerZy.Enabled = true;
            videoPlayerZy.Location = new Point(-31, 666);
            videoPlayerZy.Name = "videoPlayerZy";
            videoPlayerZy.OcxState = (AxHost.State)resources.GetObject("videoPlayerZy.OcxState");
            videoPlayerZy.Size = new Size(259, 133);
            videoPlayerZy.TabIndex = 15;
            videoPlayerZy.Visible = false;
            // 
            // videoHideTimerZy
            // 
            videoHideTimerZy.Interval = 3000;
            videoHideTimerZy.Tick += videoHideTimerZy_Tick;
            // 
            // pbxTagZy
            // 
            pbxTagZy.BackColor = Color.Transparent;
            pbxTagZy.BackgroundImage = Properties.Resources.tag;
            pbxTagZy.BackgroundImageLayout = ImageLayout.Zoom;
            pbxTagZy.Location = new Point(389, 622);
            pbxTagZy.Name = "pbxTagZy";
            pbxTagZy.Size = new Size(306, 79);
            pbxTagZy.SizeMode = PictureBoxSizeMode.CenterImage;
            pbxTagZy.TabIndex = 16;
            pbxTagZy.TabStop = false;
            // 
            // lblStatusZy
            // 
            lblStatusZy.BackColor = Color.Transparent;
            lblStatusZy.Font = new Font("Copperplate Gothic Light", 10F);
            lblStatusZy.ForeColor = Color.Peru;
            lblStatusZy.Location = new Point(404, 647);
            lblStatusZy.Name = "lblStatusZy";
            lblStatusZy.Size = new Size(276, 36);
            lblStatusZy.TabIndex = 1;
            lblStatusZy.Text = "\"\"";
            lblStatusZy.TextAlign = ContentAlignment.MiddleCenter;
            lblStatusZy.UseMnemonic = false;
            // 
            // btnGoldSetupZy
            // 
            btnGoldSetupZy.BackColor = Color.Transparent;
            btnGoldSetupZy.BackgroundImage = Properties.Resources.goldOff;
            btnGoldSetupZy.BackgroundImageLayout = ImageLayout.Stretch;
            btnGoldSetupZy.Cursor = Cursors.Hand;
            btnGoldSetupZy.FlatAppearance.BorderSize = 0;
            btnGoldSetupZy.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnGoldSetupZy.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnGoldSetupZy.FlatStyle = FlatStyle.Flat;
            btnGoldSetupZy.ForeColor = Color.Transparent;
            btnGoldSetupZy.Location = new Point(886, 11);
            btnGoldSetupZy.Name = "btnGoldSetupZy";
            btnGoldSetupZy.Size = new Size(128, 50);
            btnGoldSetupZy.TabIndex = 17;
            btnGoldSetupZy.UseVisualStyleBackColor = false;
            btnGoldSetupZy.Click += btnGoldSetupZy_Click;
            // 
            // btnSilverSetupZy
            // 
            btnSilverSetupZy.BackColor = Color.Transparent;
            btnSilverSetupZy.BackgroundImage = Properties.Resources.silverOn;
            btnSilverSetupZy.BackgroundImageLayout = ImageLayout.Stretch;
            btnSilverSetupZy.Cursor = Cursors.Hand;
            btnSilverSetupZy.FlatAppearance.BorderSize = 0;
            btnSilverSetupZy.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnSilverSetupZy.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnSilverSetupZy.FlatStyle = FlatStyle.Flat;
            btnSilverSetupZy.ForeColor = Color.Transparent;
            btnSilverSetupZy.Location = new Point(1012, 10);
            btnSilverSetupZy.Name = "btnSilverSetupZy";
            btnSilverSetupZy.Size = new Size(132, 51);
            btnSilverSetupZy.TabIndex = 18;
            btnSilverSetupZy.UseVisualStyleBackColor = false;
            btnSilverSetupZy.Click += btnSilverSetupZy_Click;
            // 
            // btnZeroRobotZy
            // 
            btnZeroRobotZy.BackColor = Color.Transparent;
            btnZeroRobotZy.BackgroundImage = Properties.Resources.zeroOff;
            btnZeroRobotZy.BackgroundImageLayout = ImageLayout.Zoom;
            btnZeroRobotZy.Cursor = Cursors.Hand;
            btnZeroRobotZy.FlatAppearance.BorderSize = 0;
            btnZeroRobotZy.FlatStyle = FlatStyle.Flat;
            btnZeroRobotZy.ForeColor = Color.Transparent;
            btnZeroRobotZy.Location = new Point(72, 294);
            btnZeroRobotZy.Name = "btnZeroRobotZy";
            btnZeroRobotZy.Size = new Size(161, 224);
            btnZeroRobotZy.TabIndex = 19;
            btnZeroRobotZy.UseVisualStyleBackColor = false;
            btnZeroRobotZy.Click += btnZeroRobotZy_Click;
            btnZeroRobotZy.MouseDown += btnZeroRobotZy_MouseDown;
            btnZeroRobotZy.MouseEnter += btnZeroRobotZy_MouseEnter;
            btnZeroRobotZy.MouseLeave += btnZeroRobotZy_MouseLeave;
            btnZeroRobotZy.MouseUp += btnZeroRobotZy_MouseUp;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Transparent;
            pictureBox1.BackgroundImage = Properties.Resources.zeroSign;
            pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox1.Location = new Point(97, 227);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(131, 61);
            pictureBox1.TabIndex = 20;
            pictureBox1.TabStop = false;
            // 
            // picSetup4
            // 
            picSetup4.BackColor = Color.Transparent;
            picSetup4.BackgroundImage = Properties.Resources.spK;
            picSetup4.BackgroundImageLayout = ImageLayout.Zoom;
            picSetup4.Cursor = Cursors.Hand;
            picSetup4.Location = new Point(937, 107);
            picSetup4.Name = "picSetup4";
            picSetup4.Size = new Size(127, 176);
            picSetup4.SizeMode = PictureBoxSizeMode.Zoom;
            picSetup4.TabIndex = 21;
            picSetup4.TabStop = false;
            picSetup4.Visible = false;
            picSetup4.Click += picSetup4_Click;
            // 
            // picSetup5
            // 
            picSetup5.BackColor = Color.Transparent;
            picSetup5.BackgroundImage = Properties.Resources.spW;
            picSetup5.BackgroundImageLayout = ImageLayout.Zoom;
            picSetup5.Cursor = Cursors.Hand;
            picSetup5.Location = new Point(937, 502);
            picSetup5.Name = "picSetup5";
            picSetup5.Size = new Size(127, 176);
            picSetup5.SizeMode = PictureBoxSizeMode.Zoom;
            picSetup5.TabIndex = 22;
            picSetup5.TabStop = false;
            picSetup5.Visible = false;
            picSetup5.Click += picSetup5_Click;
            // 
            // btnTogglePiecesZy
            // 
            btnTogglePiecesZy.BackColor = Color.Transparent;
            btnTogglePiecesZy.BackgroundImage = Properties.Resources.extraBtn;
            btnTogglePiecesZy.BackgroundImageLayout = ImageLayout.Stretch;
            btnTogglePiecesZy.Cursor = Cursors.Hand;
            btnTogglePiecesZy.FlatAppearance.BorderSize = 0;
            btnTogglePiecesZy.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnTogglePiecesZy.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnTogglePiecesZy.FlatStyle = FlatStyle.Flat;
            btnTogglePiecesZy.Location = new Point(812, 293);
            btnTogglePiecesZy.Name = "btnTogglePiecesZy";
            btnTogglePiecesZy.Size = new Size(41, 121);
            btnTogglePiecesZy.TabIndex = 23;
            btnTogglePiecesZy.UseVisualStyleBackColor = false;
            btnTogglePiecesZy.Click += btnTogglePiecesZy_Click;
            btnTogglePiecesZy.MouseEnter += btnTogglePiecesZy_MouseEnter;
            btnTogglePiecesZy.MouseLeave += btnTogglePiecesZy_MouseLeave;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.background;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1145, 780);
            Controls.Add(picLampGoldZy);
            Controls.Add(btnTogglePiecesZy);
            Controls.Add(picSetup5);
            Controls.Add(picSetup4);
            Controls.Add(pictureBox1);
            Controls.Add(videoPlayerZy);
            Controls.Add(btnSilverSetupZy);
            Controls.Add(btnGoldSetupZy);
            Controls.Add(picSetup3);
            Controls.Add(picSetup2);
            Controls.Add(picSetup1);
            Controls.Add(pbSilverZy);
            Controls.Add(pbGoldZy);
            Controls.Add(picLampSilverZy);
            Controls.Add(btnRestartZy);
            Controls.Add(lblStatusZy);
            Controls.Add(pbxTagZy);
            Controls.Add(pnlBoardZy);
            Controls.Add(btnZeroRobotZy);
            Name = "Form1";
            Text = "Form1";
            pnlBoardZy.ResumeLayout(false);
            tblBoardZy.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)picLampGoldZy).EndInit();
            ((System.ComponentModel.ISupportInitialize)picLampSilverZy).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbGoldZy).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbSilverZy).EndInit();
            ((System.ComponentModel.ISupportInitialize)picSetup1).EndInit();
            ((System.ComponentModel.ISupportInitialize)picSetup2).EndInit();
            ((System.ComponentModel.ISupportInitialize)picSetup3).EndInit();
            ((System.ComponentModel.ISupportInitialize)videoPlayerZy).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbxTagZy).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)picSetup4).EndInit();
            ((System.ComponentModel.ISupportInitialize)picSetup5).EndInit();
            ResumeLayout(false);
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
        private Button btnRestartZy;
        private PictureBox picLampGoldZy;
        private PictureBox picLampSilverZy;
        private PictureBox pbGoldZy;
        private PictureBox pbSilverZy;
        private PictureBox picSetup1;
        private PictureBox picSetup2;
        private PictureBox picSetup3;
        private AxWMPLib.AxWindowsMediaPlayer videoPlayerZy;
        private System.Windows.Forms.Timer videoHideTimerZy;
        private EventHandler videoPlayerZy_PlayerDockedStateChange;
        private PictureBox pbxTagZy;
        private Label lblStatusZy;
        private Button btnGoldSetupZy;
        private Button btnSilverSetupZy;
        private Button btnZeroRobotZy;
        private PictureBox pictureBox1;
        private PictureBox picSetup4;
        private PictureBox picSetup5;
        private Button btnTogglePiecesZy;
    }
}
