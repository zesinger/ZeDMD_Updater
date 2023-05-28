using System.Dynamic;

namespace ZeDMD_Updater
{
    partial class Form1
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        private const int MAJ_VERSION=1;
        private const int MIN_VERSION=1;

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.Start = new System.Windows.Forms.Button();
            this.SD = new System.Windows.Forms.RadioButton();
            this.HD = new System.Windows.Forms.RadioButton();
            this.LatestVersion = new System.Windows.Forms.TextBox();
            this.ESP32List = new System.Windows.Forms.ListBox();
            this.ZeDMDList = new System.Windows.Forms.ListBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.versionList = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Enabled = false;
            this.textBox1.Location = new System.Drawing.Point(219, 44);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(187, 20);
            this.textBox1.TabIndex = 0;
            this.textBox1.Text = "ZeDMD found:";
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox2
            // 
            this.textBox2.Enabled = false;
            this.textBox2.Location = new System.Drawing.Point(12, 44);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(186, 20);
            this.textBox2.TabIndex = 2;
            this.textBox2.Text = "Non-ZeDMD ESP32 found:";
            this.textBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Start
            // 
            this.Start.Enabled = false;
            this.Start.Location = new System.Drawing.Point(219, 210);
            this.Start.Name = "Start";
            this.Start.Size = new System.Drawing.Size(187, 23);
            this.Start.TabIndex = 4;
            this.Start.Text = "Install/Update";
            this.Start.UseVisualStyleBackColor = true;
            this.Start.Click += new System.EventHandler(this.Start_Click);
            // 
            // SD
            // 
            this.SD.AutoSize = true;
            this.SD.Location = new System.Drawing.Point(13, 213);
            this.SD.Name = "SD";
            this.SD.Size = new System.Drawing.Size(63, 17);
            this.SD.TabIndex = 8;
            this.SD.TabStop = true;
            this.SD.Text = "ZeDMD";
            this.SD.UseVisualStyleBackColor = true;
            this.SD.CheckedChanged += new System.EventHandler(this.SD_CheckedChanged);
            // 
            // HD
            // 
            this.HD.AutoSize = true;
            this.HD.Location = new System.Drawing.Point(81, 213);
            this.HD.Name = "HD";
            this.HD.Size = new System.Drawing.Size(82, 17);
            this.HD.TabIndex = 9;
            this.HD.TabStop = true;
            this.HD.Text = "ZeDMD HD";
            this.HD.UseVisualStyleBackColor = true;
            this.HD.CheckedChanged += new System.EventHandler(this.HD_CheckedChanged);
            // 
            // LatestVersion
            // 
            this.LatestVersion.Enabled = false;
            this.LatestVersion.Location = new System.Drawing.Point(13, 13);
            this.LatestVersion.Name = "LatestVersion";
            this.LatestVersion.ReadOnly = true;
            this.LatestVersion.Size = new System.Drawing.Size(392, 20);
            this.LatestVersion.TabIndex = 10;
            this.LatestVersion.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ESP32List
            // 
            this.ESP32List.FormattingEnabled = true;
            this.ESP32List.Location = new System.Drawing.Point(12, 73);
            this.ESP32List.Name = "ESP32List";
            this.ESP32List.Size = new System.Drawing.Size(186, 134);
            this.ESP32List.TabIndex = 11;
            this.ESP32List.SelectedIndexChanged += new System.EventHandler(this.ESP32List_SelectedIndexChanged);
            // 
            // ZeDMDList
            // 
            this.ZeDMDList.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.ZeDMDList.FormattingEnabled = true;
            this.ZeDMDList.Location = new System.Drawing.Point(219, 73);
            this.ZeDMDList.Name = "ZeDMDList";
            this.ZeDMDList.Size = new System.Drawing.Size(186, 134);
            this.ZeDMDList.TabIndex = 12;
            this.ZeDMDList.SelectedIndexChanged += new System.EventHandler(this.ZeDMDList_SelectedIndexChanged);
            // 
            // textBox3
            // 
            this.textBox3.Enabled = false;
            this.textBox3.Location = new System.Drawing.Point(423, 13);
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(95, 20);
            this.textBox3.TabIndex = 13;
            this.textBox3.Text = "Available versions:";
            this.textBox3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // versionList
            // 
            this.versionList.FormattingEnabled = true;
            this.versionList.Location = new System.Drawing.Point(423, 44);
            this.versionList.Name = "versionList";
            this.versionList.Size = new System.Drawing.Size(95, 186);
            this.versionList.TabIndex = 14;
            this.versionList.SelectedIndexChanged += new System.EventHandler(this.versionList_SelectedIndexChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(531, 246);
            this.Controls.Add(this.versionList);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.ZeDMDList);
            this.Controls.Add(this.ESP32List);
            this.Controls.Add(this.LatestVersion);
            this.Controls.Add(this.HD);
            this.Controls.Add(this.SD);
            this.Controls.Add(this.Start);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Name = "Form1";
            this.Text = "ZeDMD Installer/Updater v"+Form1.MAJ_VERSION.ToString()+"."+Form1.MIN_VERSION.ToString()+" by Zedrummer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button Start;
        private System.Windows.Forms.RadioButton SD;
        private System.Windows.Forms.RadioButton HD;
        private System.Windows.Forms.TextBox LatestVersion;
        private System.Windows.Forms.ListBox ESP32List;
        public System.Windows.Forms.ListBox ZeDMDList;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.ListBox versionList;
    }
}

