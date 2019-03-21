namespace Chrome_Cache_Extract
{
    partial class MainWindow
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
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.clb_kullaniciListesi = new System.Windows.Forms.CheckedListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.checkBox_normal = new System.Windows.Forms.CheckBox();
            this.checkBox_multiMedia = new System.Windows.Forms.CheckBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.button_select_folder = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtBox_savePath = new System.Windows.Forms.TextBox();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.button1.Location = new System.Drawing.Point(304, 243);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Çıkar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Kullanıcılar";
            // 
            // clb_kullaniciListesi
            // 
            this.clb_kullaniciListesi.CheckOnClick = true;
            this.clb_kullaniciListesi.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.clb_kullaniciListesi.FormattingEnabled = true;
            this.clb_kullaniciListesi.Location = new System.Drawing.Point(15, 38);
            this.clb_kullaniciListesi.Name = "clb_kullaniciListesi";
            this.clb_kullaniciListesi.Size = new System.Drawing.Size(167, 148);
            this.clb_kullaniciListesi.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.Location = new System.Drawing.Point(188, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(128, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Çıkarılacak Çerezler";
            // 
            // checkBox_normal
            // 
            this.checkBox_normal.AutoSize = true;
            this.checkBox_normal.Location = new System.Drawing.Point(191, 38);
            this.checkBox_normal.Name = "checkBox_normal";
            this.checkBox_normal.Size = new System.Drawing.Size(100, 17);
            this.checkBox_normal.TabIndex = 3;
            this.checkBox_normal.Text = "Normal Çerezler";
            this.checkBox_normal.UseVisualStyleBackColor = true;
            // 
            // checkBox_multiMedia
            // 
            this.checkBox_multiMedia.AutoSize = true;
            this.checkBox_multiMedia.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.checkBox_multiMedia.Location = new System.Drawing.Point(191, 74);
            this.checkBox_multiMedia.Name = "checkBox_multiMedia";
            this.checkBox_multiMedia.Size = new System.Drawing.Size(122, 17);
            this.checkBox_multiMedia.TabIndex = 3;
            this.checkBox_multiMedia.Text = "Multimedya Çerezleri";
            this.checkBox_multiMedia.UseVisualStyleBackColor = true;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.Calistir);
            // 
            // button_select_folder
            // 
            this.button_select_folder.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button_select_folder.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.button_select_folder.Location = new System.Drawing.Point(191, 243);
            this.button_select_folder.Name = "button_select_folder";
            this.button_select_folder.Size = new System.Drawing.Size(107, 23);
            this.button_select_folder.TabIndex = 0;
            this.button_select_folder.Text = "Klasör Seç";
            this.button_select_folder.UseVisualStyleBackColor = true;
            this.button_select_folder.Click += new System.EventHandler(this.button_select_folder_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 217);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Kayıt Yeri:";
            // 
            // txtBox_savePath
            // 
            this.txtBox_savePath.Location = new System.Drawing.Point(72, 214);
            this.txtBox_savePath.Name = "txtBox_savePath";
            this.txtBox_savePath.ReadOnly = true;
            this.txtBox_savePath.Size = new System.Drawing.Size(226, 20);
            this.txtBox_savePath.TabIndex = 5;
            this.txtBox_savePath.WordWrap = false;
            // 
            // progressBar
            // 
            this.progressBar.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.progressBar.Location = new System.Drawing.Point(15, 254);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(100, 12);
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar.TabIndex = 6;
            this.progressBar.Visible = false;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(196, 97);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(120, 95);
            this.listBox1.TabIndex = 7;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(394, 278);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.txtBox_savePath);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.checkBox_multiMedia);
            this.Controls.Add(this.checkBox_normal);
            this.Controls.Add(this.clb_kullaniciListesi);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button_select_folder);
            this.Controls.Add(this.button1);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "MainWindow";
            this.Text = "Chrome Cache Export";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckedListBox clb_kullaniciListesi;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkBox_normal;
        private System.Windows.Forms.CheckBox checkBox_multiMedia;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button button_select_folder;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtBox_savePath;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.ListBox listBox1;
    }
}

