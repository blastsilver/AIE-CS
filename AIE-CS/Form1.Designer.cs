namespace AIE_CS
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
            this.components = new System.ComponentModel.Container();
            this.form1_Timer = new System.Windows.Forms.Timer(this.components);
            this.form1_SaveFile = new System.Windows.Forms.SaveFileDialog();
            this.form1_OpenFile = new System.Windows.Forms.OpenFileDialog();
            this.form1_AppClose = new System.Windows.Forms.PictureBox();
            this.form1_AppTitleName = new System.Windows.Forms.Label();
            this.form1_AppTitleBar = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.form1_AppClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.form1_AppTitleBar)).BeginInit();
            this.SuspendLayout();
            // 
            // form1_OpenFile
            // 
            this.form1_OpenFile.FileName = "form1_OpenFile";
            // 
            // form1_AppClose
            // 
            this.form1_AppClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.form1_AppClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.form1_AppClose.BackgroundImage = global::AIE_CS.Properties.Resources.ic_clear;
            this.form1_AppClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.form1_AppClose.Location = new System.Drawing.Point(649, 12);
            this.form1_AppClose.Name = "form1_AppClose";
            this.form1_AppClose.Size = new System.Drawing.Size(32, 24);
            this.form1_AppClose.TabIndex = 0;
            this.form1_AppClose.TabStop = false;
            this.form1_AppClose.MouseEnter += new System.EventHandler(this.form1_AppClose_MouseEnter);
            this.form1_AppClose.MouseLeave += new System.EventHandler(this.form1_AppClose_MouseLeave);
            this.form1_AppClose.MouseUp += new System.Windows.Forms.MouseEventHandler(this.form1_AppClose_MouseUp);
            // 
            // form1_AppTitleName
            // 
            this.form1_AppTitleName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.form1_AppTitleName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.form1_AppTitleName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.form1_AppTitleName.ForeColor = System.Drawing.Color.White;
            this.form1_AppTitleName.Location = new System.Drawing.Point(12, 12);
            this.form1_AppTitleName.Name = "form1_AppTitleName";
            this.form1_AppTitleName.Size = new System.Drawing.Size(631, 24);
            this.form1_AppTitleName.TabIndex = 1;
            this.form1_AppTitleName.Text = "Form 1";
            this.form1_AppTitleName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.form1_AppTitleName.MouseDown += new System.Windows.Forms.MouseEventHandler(this.form1_AppTitleName_MouseDown);
            // 
            // form1_AppTitleBar
            // 
            this.form1_AppTitleBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.form1_AppTitleBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.form1_AppTitleBar.Location = new System.Drawing.Point(0, 0);
            this.form1_AppTitleBar.Name = "form1_AppTitleBar";
            this.form1_AppTitleBar.Size = new System.Drawing.Size(693, 48);
            this.form1_AppTitleBar.TabIndex = 2;
            this.form1_AppTitleBar.TabStop = false;
            this.form1_AppTitleBar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.form1_AppTitleBar_MouseDown);
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(693, 500);
            this.Controls.Add(this.form1_AppTitleName);
            this.Controls.Add(this.form1_AppClose);
            this.Controls.Add(this.form1_AppTitleBar);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            ((System.ComponentModel.ISupportInitialize)(this.form1_AppClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.form1_AppTitleBar)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer form1_Timer;
        private System.Windows.Forms.SaveFileDialog form1_SaveFile;
        private System.Windows.Forms.OpenFileDialog form1_OpenFile;
        private System.Windows.Forms.PictureBox form1_AppClose;
        private System.Windows.Forms.Label form1_AppTitleName;
        private System.Windows.Forms.PictureBox form1_AppTitleBar;
    }
}

