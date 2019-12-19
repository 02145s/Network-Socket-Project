namespace MultiChatServer {
    partial class ChatForm_S {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent() {
            this.tblMainLayout = new System.Windows.Forms.TableLayoutPanel();
            this.txtTTS = new System.Windows.Forms.TextBox();
            this.lblTTS = new System.Windows.Forms.Label();
            this.lblAddress = new System.Windows.Forms.Label();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnSend = new System.Windows.Forms.Button();
            this.lblPort = new System.Windows.Forms.Label();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.txtHistory = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.button1 = new System.Windows.Forms.Button();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton5 = new System.Windows.Forms.RadioButton();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.tblMainLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tblMainLayout
            // 
            this.tblMainLayout.ColumnCount = 5;
            this.tblMainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tblMainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblMainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tblMainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tblMainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tblMainLayout.Controls.Add(this.txtTTS, 1, 2);
            this.tblMainLayout.Controls.Add(this.lblTTS, 0, 2);
            this.tblMainLayout.Controls.Add(this.lblAddress, 0, 0);
            this.tblMainLayout.Controls.Add(this.txtAddress, 1, 0);
            this.tblMainLayout.Controls.Add(this.btnStart, 4, 0);
            this.tblMainLayout.Controls.Add(this.btnSend, 4, 2);
            this.tblMainLayout.Controls.Add(this.lblPort, 2, 0);
            this.tblMainLayout.Controls.Add(this.txtPort, 3, 0);
            this.tblMainLayout.Controls.Add(this.txtHistory);
            this.tblMainLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblMainLayout.Location = new System.Drawing.Point(0, 0);
            this.tblMainLayout.Name = "tblMainLayout";
            this.tblMainLayout.Padding = new System.Windows.Forms.Padding(8);
            this.tblMainLayout.RowCount = 3;
            this.tblMainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tblMainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblMainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tblMainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblMainLayout.Size = new System.Drawing.Size(951, 583);
            this.tblMainLayout.TabIndex = 1;
            // 
            // txtTTS
            // 
            this.tblMainLayout.SetColumnSpan(this.txtTTS, 3);
            this.txtTTS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtTTS.Location = new System.Drawing.Point(112, 545);
            this.txtTTS.Margin = new System.Windows.Forms.Padding(4, 2, 3, 3);
            this.txtTTS.MaxLength = 260;
            this.txtTTS.Name = "txtTTS";
            this.txtTTS.Size = new System.Drawing.Size(728, 27);
            this.txtTTS.TabIndex = 7;
            this.txtTTS.Enter += new System.EventHandler(this.RadioButton2_CheckedChanged);
            this.txtTTS.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTTs_keyup);
            this.txtTTS.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtTTs_keyup);
            // 
            // lblTTS
            // 
            this.lblTTS.AutoSize = true;
            this.lblTTS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTTS.Location = new System.Drawing.Point(9, 544);
            this.lblTTS.Margin = new System.Windows.Forms.Padding(1);
            this.lblTTS.Name = "lblTTS";
            this.lblTTS.Size = new System.Drawing.Size(98, 30);
            this.lblTTS.TabIndex = 6;
            this.lblTTS.Text = "보낼 텍스트";
            this.lblTTS.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblAddress
            // 
            this.lblAddress.AutoSize = true;
            this.lblAddress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAddress.Location = new System.Drawing.Point(9, 9);
            this.lblAddress.Margin = new System.Windows.Forms.Padding(1);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new System.Drawing.Size(98, 30);
            this.lblAddress.TabIndex = 0;
            this.lblAddress.Text = "서버 주소";
            this.lblAddress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtAddress
            // 
            this.txtAddress.BackColor = System.Drawing.Color.White;
            this.txtAddress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtAddress.Location = new System.Drawing.Point(112, 10);
            this.txtAddress.Margin = new System.Windows.Forms.Padding(4, 2, 3, 3);
            this.txtAddress.MaxLength = 260;
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(528, 27);
            this.txtAddress.TabIndex = 1;
            // 
            // btnStart
            // 
            this.btnStart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnStart.Location = new System.Drawing.Point(844, 9);
            this.btnStart.Margin = new System.Windows.Forms.Padding(1);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(98, 30);
            this.btnStart.TabIndex = 4;
            this.btnStart.Text = "시작";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.BeginStartServer);
            // 
            // btnSend
            // 
            this.btnSend.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSend.Location = new System.Drawing.Point(844, 544);
            this.btnSend.Margin = new System.Windows.Forms.Padding(1);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(98, 30);
            this.btnSend.TabIndex = 8;
            this.btnSend.Text = "보내기";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.OnSendData);
            this.btnSend.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtTTs_keyup);
            // 
            // lblPort
            // 
            this.lblPort.AutoSize = true;
            this.lblPort.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPort.Location = new System.Drawing.Point(644, 9);
            this.lblPort.Margin = new System.Windows.Forms.Padding(1);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(98, 30);
            this.lblPort.TabIndex = 2;
            this.lblPort.Text = "포트 번호";
            this.lblPort.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtPort
            // 
            this.txtPort.Dock = System.Windows.Forms.DockStyle.Right;
            this.txtPort.Location = new System.Drawing.Point(747, 10);
            this.txtPort.Margin = new System.Windows.Forms.Padding(4, 2, 3, 3);
            this.txtPort.MaxLength = 5;
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(93, 27);
            this.txtPort.TabIndex = 3;
            this.txtPort.Text = "15000";
            // 
            // txtHistory
            // 
            this.txtHistory.AllowDrop = true;
            this.txtHistory.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtHistory.BackColor = System.Drawing.Color.White;
            this.tblMainLayout.SetColumnSpan(this.txtHistory, 5);
            this.txtHistory.Location = new System.Drawing.Point(12, 53);
            this.txtHistory.Margin = new System.Windows.Forms.Padding(4, 3, 2, 3);
            this.txtHistory.MaximumSize = new System.Drawing.Size(100, 0);
            this.txtHistory.MinimumSize = new System.Drawing.Size(600, 400);
            this.txtHistory.Multiline = true;
            this.txtHistory.Name = "txtHistory";
            this.txtHistory.ReadOnly = true;
            this.txtHistory.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtHistory.Size = new System.Drawing.Size(600, 476);
            this.txtHistory.TabIndex = 5;
            this.txtHistory.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BackColor = System.Drawing.SystemColors.Window;
            this.pictureBox1.Location = new System.Drawing.Point(3, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(306, 309);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.PictureBox1_Click);
            this.pictureBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.pictureBox1);
            this.flowLayoutPanel1.Controls.Add(this.button1);
            this.flowLayoutPanel1.Controls.Add(this.radioButton1);
            this.flowLayoutPanel1.Controls.Add(this.radioButton2);
            this.flowLayoutPanel1.Controls.Add(this.radioButton3);
            this.flowLayoutPanel1.Controls.Add(this.radioButton5);
            this.flowLayoutPanel1.Controls.Add(this.button4);
            this.flowLayoutPanel1.Controls.Add(this.button5);
            this.flowLayoutPanel1.Controls.Add(this.button6);
            this.flowLayoutPanel1.Controls.Add(this.button2);
            this.flowLayoutPanel1.Controls.Add(this.button3);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(617, 43);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(320, 491);
            this.flowLayoutPanel1.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.Location = new System.Drawing.Point(3, 320);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this.button1.Name = "button1";
            this.button1.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.button1.Size = new System.Drawing.Size(306, 34);
            this.button1.TabIndex = 3;
            this.button1.Text = "모두 지우기";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(3, 360);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(72, 24);
            this.radioButton1.TabIndex = 4;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "검은색";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.RadioButton1_CheckedChanged);
            this.radioButton1.MouseEnter += new System.EventHandler(this.RadioButton1_CheckedChanged);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(81, 360);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(72, 24);
            this.radioButton2.TabIndex = 5;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "빨간색";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.RadioButton2_CheckedChanged);
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(159, 360);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(72, 24);
            this.radioButton3.TabIndex = 6;
            this.radioButton3.TabStop = true;
            this.radioButton3.Text = "파란색";
            this.radioButton3.UseVisualStyleBackColor = true;
            this.radioButton3.CheckedChanged += new System.EventHandler(this.RadioButton3_CheckedChanged);
            // 
            // radioButton5
            // 
            this.radioButton5.AutoSize = true;
            this.radioButton5.Location = new System.Drawing.Point(237, 360);
            this.radioButton5.Name = "radioButton5";
            this.radioButton5.Size = new System.Drawing.Size(72, 24);
            this.radioButton5.TabIndex = 8;
            this.radioButton5.TabStop = true;
            this.radioButton5.Text = "하얀색";
            this.radioButton5.UseVisualStyleBackColor = true;
            this.radioButton5.CheckedChanged += new System.EventHandler(this.RadioButton5_CheckedChanged);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(3, 390);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(72, 27);
            this.button4.TabIndex = 15;
            this.button4.Text = "사각형";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(81, 390);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(51, 27);
            this.button5.TabIndex = 16;
            this.button5.Text = "원";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(138, 390);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(71, 27);
            this.button6.TabIndex = 17;
            this.button6.Text = "직선";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(3, 423);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(129, 25);
            this.button2.TabIndex = 13;
            this.button2.Text = "사각형 받아오기";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(138, 423);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(104, 25);
            this.button3.TabIndex = 14;
            this.button3.Text = "원 받아오기";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // ChatForm_S
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(951, 583);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.tblMainLayout);
            this.Font = new System.Drawing.Font("맑은 고딕", 11.25F);
            this.Name = "ChatForm_S";
            this.Text = "Multi Chat Server";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ChatForm_FormClosing);
            this.Load += new System.EventHandler(this.OnFormLoaded);
            this.tblMainLayout.ResumeLayout(false);
            this.tblMainLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tblMainLayout;
        private System.Windows.Forms.TextBox txtTTS;
        private System.Windows.Forms.Label lblTTS;
        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.TextBox txtHistory;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Label lblPort;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        public System.Windows.Forms.Button button1;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.RadioButton radioButton5;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
    }
}

