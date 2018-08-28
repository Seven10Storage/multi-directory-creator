namespace MultiDirectoryCreator
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
            this.threads = new System.Windows.Forms.NumericUpDown();
            this.btnStart = new System.Windows.Forms.Button();
            this.BtnBrowse = new System.Windows.Forms.Button();
            this.BaseDir = new System.Windows.Forms.FolderBrowserDialog();
            this.rootPath = new System.Windows.Forms.TextBox();
            this.labelThreads = new System.Windows.Forms.Label();
            this.btnStop = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.threads)).BeginInit();
            this.SuspendLayout();
            // 
            // threads
            // 
            this.threads.Location = new System.Drawing.Point(103, 27);
            this.threads.Name = "threads";
            this.threads.Size = new System.Drawing.Size(61, 20);
            this.threads.TabIndex = 0;
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(12, 129);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 1;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.Start_Click);
            // 
            // BtnBrowse
            // 
            this.BtnBrowse.Location = new System.Drawing.Point(12, 61);
            this.BtnBrowse.Name = "BtnBrowse";
            this.BtnBrowse.Size = new System.Drawing.Size(75, 23);
            this.BtnBrowse.TabIndex = 2;
            this.BtnBrowse.Text = "Browse";
            this.BtnBrowse.UseVisualStyleBackColor = true;
            this.BtnBrowse.Click += new System.EventHandler(this.Browse_Click);
            // 
            // rootPath
            // 
            this.rootPath.Location = new System.Drawing.Point(103, 63);
            this.rootPath.Name = "rootPath";
            this.rootPath.Size = new System.Drawing.Size(172, 20);
            this.rootPath.TabIndex = 3;
            this.rootPath.TextChanged += new System.EventHandler(this.TextChanged_Click);
            // 
            // labelThreads
            // 
            this.labelThreads.AutoSize = true;
            this.labelThreads.Location = new System.Drawing.Point(13, 33);
            this.labelThreads.Name = "labelThreads";
            this.labelThreads.Size = new System.Drawing.Size(46, 13);
            this.labelThreads.TabIndex = 4;
            this.labelThreads.Text = "Threads";
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(103, 129);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 5;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.Stop_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(404, 414);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.labelThreads);
            this.Controls.Add(this.rootPath);
            this.Controls.Add(this.BtnBrowse);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.threads);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.threads)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown threads;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button BtnBrowse;
        private System.Windows.Forms.FolderBrowserDialog BaseDir;
        private System.Windows.Forms.TextBox rootPath;
        private System.Windows.Forms.Label labelThreads;
        private System.Windows.Forms.Button btnStop;
    }
}

