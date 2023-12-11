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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.BVal = new System.Windows.Forms.ComboBox();
            this.BSet = new System.Windows.Forms.RadioButton();
            this.BKeep = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.OVal = new System.Windows.Forms.ComboBox();
            this.OSet = new System.Windows.Forms.RadioButton();
            this.OKeep = new System.Windows.Forms.RadioButton();
            this.LEDTest = new System.Windows.Forms.Button();
            this.wifi = new System.Windows.Forms.CheckBox();
            this.esps3 = new System.Windows.Forms.CheckBox();
            this.sevenbit = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
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
            this.Start.Location = new System.Drawing.Point(408, 284);
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
            this.SD.Location = new System.Drawing.Point(6, 19);
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
            this.HD.Location = new System.Drawing.Point(75, 19);
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
            this.textBox3.Size = new System.Drawing.Size(172, 20);
            this.textBox3.TabIndex = 13;
            this.textBox3.Text = "Available versions:";
            this.textBox3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // versionList
            // 
            this.versionList.FormattingEnabled = true;
            this.versionList.Location = new System.Drawing.Point(423, 44);
            this.versionList.Name = "versionList";
            this.versionList.Size = new System.Drawing.Size(95, 160);
            this.versionList.TabIndex = 14;
            this.versionList.SelectedIndexChanged += new System.EventHandler(this.versionList_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.esps3);
            this.groupBox1.Controls.Add(this.HD);
            this.groupBox1.Controls.Add(this.SD);
            this.groupBox1.Location = new System.Drawing.Point(10, 213);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(163, 65);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Panel resolution";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.BVal);
            this.groupBox2.Controls.Add(this.BSet);
            this.groupBox2.Controls.Add(this.BKeep);
            this.groupBox2.Location = new System.Drawing.Point(230, 215);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(149, 44);
            this.groupBox2.TabIndex = 16;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Brightness";
            // 
            // BVal
            // 
            this.BVal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.BVal.FormattingEnabled = true;
            this.BVal.Location = new System.Drawing.Point(103, 17);
            this.BVal.Name = "BVal";
            this.BVal.Size = new System.Drawing.Size(40, 21);
            this.BVal.TabIndex = 10;
            // 
            // BSet
            // 
            this.BSet.AutoSize = true;
            this.BSet.Location = new System.Drawing.Point(65, 19);
            this.BSet.Name = "BSet";
            this.BSet.Size = new System.Drawing.Size(41, 17);
            this.BSet.TabIndex = 9;
            this.BSet.TabStop = true;
            this.BSet.Text = "Set";
            this.BSet.UseVisualStyleBackColor = true;
            this.BSet.CheckedChanged += new System.EventHandler(this.BSet_CheckedChanged);
            // 
            // BKeep
            // 
            this.BKeep.AutoSize = true;
            this.BKeep.Location = new System.Drawing.Point(9, 20);
            this.BKeep.Name = "BKeep";
            this.BKeep.Size = new System.Drawing.Size(50, 17);
            this.BKeep.TabIndex = 8;
            this.BKeep.TabStop = true;
            this.BKeep.Text = "Keep";
            this.BKeep.UseVisualStyleBackColor = true;
            this.BKeep.CheckedChanged += new System.EventHandler(this.BKeep_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.OVal);
            this.groupBox3.Controls.Add(this.OSet);
            this.groupBox3.Controls.Add(this.OKeep);
            this.groupBox3.Location = new System.Drawing.Point(445, 215);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(149, 44);
            this.groupBox3.TabIndex = 17;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "RGB order";
            // 
            // OVal
            // 
            this.OVal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.OVal.FormattingEnabled = true;
            this.OVal.Location = new System.Drawing.Point(103, 17);
            this.OVal.Name = "OVal";
            this.OVal.Size = new System.Drawing.Size(40, 21);
            this.OVal.TabIndex = 10;
            // 
            // OSet
            // 
            this.OSet.AutoSize = true;
            this.OSet.Location = new System.Drawing.Point(65, 19);
            this.OSet.Name = "OSet";
            this.OSet.Size = new System.Drawing.Size(41, 17);
            this.OSet.TabIndex = 9;
            this.OSet.TabStop = true;
            this.OSet.Text = "Set";
            this.OSet.UseVisualStyleBackColor = true;
            this.OSet.CheckedChanged += new System.EventHandler(this.OSet_CheckedChanged);
            // 
            // OKeep
            // 
            this.OKeep.AutoSize = true;
            this.OKeep.Location = new System.Drawing.Point(6, 19);
            this.OKeep.Name = "OKeep";
            this.OKeep.Size = new System.Drawing.Size(50, 17);
            this.OKeep.TabIndex = 8;
            this.OKeep.TabStop = true;
            this.OKeep.Text = "Keep";
            this.OKeep.UseVisualStyleBackColor = true;
            this.OKeep.CheckedChanged += new System.EventHandler(this.OKeep_CheckedChanged);
            // 
            // LEDTest
            // 
            this.LEDTest.Enabled = false;
            this.LEDTest.Location = new System.Drawing.Point(10, 284);
            this.LEDTest.Name = "LEDTest";
            this.LEDTest.Size = new System.Drawing.Size(187, 23);
            this.LEDTest.TabIndex = 18;
            this.LEDTest.Text = "LED test";
            this.LEDTest.UseVisualStyleBackColor = true;
            this.LEDTest.Click += new System.EventHandler(this.LEDTest_Click);
            // 
            // wifi
            // 
            this.wifi.AutoSize = true;
            this.wifi.Location = new System.Drawing.Point(526, 48);
            this.wifi.Name = "wifi";
            this.wifi.Size = new System.Drawing.Size(47, 17);
            this.wifi.TabIndex = 19;
            this.wifi.Text = "WiFi";
            this.wifi.UseVisualStyleBackColor = true;
            this.wifi.CheckedChanged += new System.EventHandler(this.wifi_CheckedChanged);
            // 
            // esps3
            // 
            this.esps3.AutoSize = true;
            this.esps3.Location = new System.Drawing.Point(39, 42);
            this.esps3.Name = "esps3";
            this.esps3.Size = new System.Drawing.Size(75, 17);
            this.esps3.TabIndex = 21;
            this.esps3.Text = "ESP32 S3";
            this.esps3.UseVisualStyleBackColor = true;
            this.esps3.CheckedChanged += new System.EventHandler(this.esps3_CheckedChanged);
            // 
            // sevenbit
            // 
            this.sevenbit.AutoSize = true;
            this.sevenbit.Location = new System.Drawing.Point(521, 73);
            this.sevenbit.Name = "sevenbit";
            this.sevenbit.Size = new System.Drawing.Size(74, 17);
            this.sevenbit.TabIndex = 20;
            this.sevenbit.Text = "7bit colors";
            this.sevenbit.UseVisualStyleBackColor = true;
            this.sevenbit.CheckedChanged += new System.EventHandler(this.sevenbit_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(606, 328);
            this.Controls.Add(this.sevenbit);
            this.Controls.Add(this.wifi);
            this.Controls.Add(this.LEDTest);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.versionList);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.ZeDMDList);
            this.Controls.Add(this.ESP32List);
            this.Controls.Add(this.LatestVersion);
            this.Controls.Add(this.Start);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Name = "Form1";
            this.Text = "ZeDMD Installer/Updater v1.0 by Zedrummer";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
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
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox BVal;
        private System.Windows.Forms.RadioButton BSet;
        private System.Windows.Forms.RadioButton BKeep;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox OVal;
        private System.Windows.Forms.RadioButton OSet;
        private System.Windows.Forms.RadioButton OKeep;
        private System.Windows.Forms.Button LEDTest;
        private System.Windows.Forms.CheckBox wifi;
        private System.Windows.Forms.CheckBox esps3;
        private System.Windows.Forms.CheckBox sevenbit;
    }
}

