﻿namespace MCSync
{
    partial class frmConfig
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmConfig));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtModsDirectory = new System.Windows.Forms.TextBox();
            this.txtResourcePacksDirectory = new System.Windows.Forms.TextBox();
            this.cmdBrowseMods = new System.Windows.Forms.Button();
            this.cmdBrowseResourcePacks = new System.Windows.Forms.Button();
            this.cmdSave = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.rbClient = new System.Windows.Forms.RadioButton();
            this.rbServer = new System.Windows.Forms.RadioButton();
            this.cmdBrowseConfig = new System.Windows.Forms.Button();
            this.txtConfigDirectory = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmdModsWhitelist = new System.Windows.Forms.Button();
            this.cmdResourcePacksWhitelist = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(65, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Mods Directory:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(134, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Resource Packs Directory:";
            // 
            // txtModsDirectory
            // 
            this.txtModsDirectory.Location = new System.Drawing.Point(152, 35);
            this.txtModsDirectory.Name = "txtModsDirectory";
            this.txtModsDirectory.ReadOnly = true;
            this.txtModsDirectory.Size = new System.Drawing.Size(241, 20);
            this.txtModsDirectory.TabIndex = 0;
            // 
            // txtResourcePacksDirectory
            // 
            this.txtResourcePacksDirectory.Location = new System.Drawing.Point(152, 61);
            this.txtResourcePacksDirectory.Name = "txtResourcePacksDirectory";
            this.txtResourcePacksDirectory.ReadOnly = true;
            this.txtResourcePacksDirectory.Size = new System.Drawing.Size(241, 20);
            this.txtResourcePacksDirectory.TabIndex = 2;
            // 
            // cmdBrowseMods
            // 
            this.cmdBrowseMods.Location = new System.Drawing.Point(402, 33);
            this.cmdBrowseMods.Name = "cmdBrowseMods";
            this.cmdBrowseMods.Size = new System.Drawing.Size(75, 23);
            this.cmdBrowseMods.TabIndex = 1;
            this.cmdBrowseMods.Text = "Browse...";
            this.cmdBrowseMods.UseVisualStyleBackColor = true;
            this.cmdBrowseMods.Click += new System.EventHandler(this.cmdBrowseMods_Click);
            // 
            // cmdBrowseResourcePacks
            // 
            this.cmdBrowseResourcePacks.Location = new System.Drawing.Point(402, 59);
            this.cmdBrowseResourcePacks.Name = "cmdBrowseResourcePacks";
            this.cmdBrowseResourcePacks.Size = new System.Drawing.Size(75, 23);
            this.cmdBrowseResourcePacks.TabIndex = 3;
            this.cmdBrowseResourcePacks.Text = "Browse...";
            this.cmdBrowseResourcePacks.UseVisualStyleBackColor = true;
            this.cmdBrowseResourcePacks.Click += new System.EventHandler(this.cmdBrowseResourcePacks_Click);
            // 
            // cmdSave
            // 
            this.cmdSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSave.Location = new System.Drawing.Point(420, 331);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(75, 23);
            this.cmdSave.TabIndex = 6;
            this.cmdSave.Text = "Save";
            this.cmdSave.UseVisualStyleBackColor = true;
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // cmdCancel
            // 
            this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Location = new System.Drawing.Point(339, 331);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(75, 23);
            this.cmdCancel.TabIndex = 7;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(109, 116);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Mode:";
            // 
            // rbClient
            // 
            this.rbClient.AutoSize = true;
            this.rbClient.Location = new System.Drawing.Point(152, 113);
            this.rbClient.Name = "rbClient";
            this.rbClient.Size = new System.Drawing.Size(54, 17);
            this.rbClient.TabIndex = 4;
            this.rbClient.TabStop = true;
            this.rbClient.Text = "Player";
            this.rbClient.UseVisualStyleBackColor = true;
            // 
            // rbServer
            // 
            this.rbServer.AutoSize = true;
            this.rbServer.Location = new System.Drawing.Point(211, 113);
            this.rbServer.Name = "rbServer";
            this.rbServer.Size = new System.Drawing.Size(56, 17);
            this.rbServer.TabIndex = 5;
            this.rbServer.TabStop = true;
            this.rbServer.Text = "Server";
            this.rbServer.UseVisualStyleBackColor = true;
            // 
            // cmdBrowseConfig
            // 
            this.cmdBrowseConfig.Location = new System.Drawing.Point(402, 85);
            this.cmdBrowseConfig.Name = "cmdBrowseConfig";
            this.cmdBrowseConfig.Size = new System.Drawing.Size(75, 23);
            this.cmdBrowseConfig.TabIndex = 10;
            this.cmdBrowseConfig.Text = "Browse...";
            this.cmdBrowseConfig.UseVisualStyleBackColor = true;
            this.cmdBrowseConfig.Click += new System.EventHandler(this.cmdBrowseConfig_Click);
            // 
            // txtConfigDirectory
            // 
            this.txtConfigDirectory.Location = new System.Drawing.Point(152, 87);
            this.txtConfigDirectory.Name = "txtConfigDirectory";
            this.txtConfigDirectory.ReadOnly = true;
            this.txtConfigDirectory.Size = new System.Drawing.Size(241, 20);
            this.txtConfigDirectory.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(60, 90);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Config Directory:";
            // 
            // cmdModsWhitelist
            // 
            this.cmdModsWhitelist.Location = new System.Drawing.Point(123, 88);
            this.cmdModsWhitelist.Name = "cmdModsWhitelist";
            this.cmdModsWhitelist.Size = new System.Drawing.Size(115, 23);
            this.cmdModsWhitelist.TabIndex = 11;
            this.cmdModsWhitelist.Text = "Mods";
            this.cmdModsWhitelist.UseVisualStyleBackColor = true;
            this.cmdModsWhitelist.Click += new System.EventHandler(this.cmdModsWhitelist_Click);
            // 
            // cmdResourcePacksWhitelist
            // 
            this.cmdResourcePacksWhitelist.Location = new System.Drawing.Point(244, 88);
            this.cmdResourcePacksWhitelist.Name = "cmdResourcePacksWhitelist";
            this.cmdResourcePacksWhitelist.Size = new System.Drawing.Size(118, 23);
            this.cmdResourcePacksWhitelist.TabIndex = 11;
            this.cmdResourcePacksWhitelist.Text = "Resource Packs";
            this.cmdResourcePacksWhitelist.UseVisualStyleBackColor = true;
            this.cmdResourcePacksWhitelist.Click += new System.EventHandler(this.cmdResourcePacksWhitelist_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtModsDirectory);
            this.groupBox1.Controls.Add(this.cmdBrowseConfig);
            this.groupBox1.Controls.Add(this.txtResourcePacksDirectory);
            this.groupBox1.Controls.Add(this.txtConfigDirectory);
            this.groupBox1.Controls.Add(this.cmdBrowseMods);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cmdBrowseResourcePacks);
            this.groupBox1.Controls.Add(this.rbServer);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.rbClient);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(483, 155);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Folder Settings";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.cmdResourcePacksWhitelist);
            this.groupBox2.Controls.Add(this.cmdModsWhitelist);
            this.groupBox2.Location = new System.Drawing.Point(12, 173);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(483, 151);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Manage Whitelists";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(120, 35);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(242, 39);
            this.label5.TabIndex = 12;
            this.label5.Text = "Mods and Resource Packs not whitelisted will be\r\ndeleted when you update. You can" +
    " white list them\r\nbelow.\r\n";
            // 
            // frmConfig
            // 
            this.AcceptButton = this.cmdSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.CancelButton = this.cmdCancel;
            this.ClientSize = new System.Drawing.Size(507, 366);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdSave);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmConfig";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MCSync Settings";
            this.Load += new System.EventHandler(this.frmConfig_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtModsDirectory;
        private System.Windows.Forms.TextBox txtResourcePacksDirectory;
        private System.Windows.Forms.Button cmdBrowseMods;
        private System.Windows.Forms.Button cmdBrowseResourcePacks;
        private System.Windows.Forms.Button cmdSave;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton rbClient;
        private System.Windows.Forms.RadioButton rbServer;
        private System.Windows.Forms.Button cmdBrowseConfig;
        private System.Windows.Forms.TextBox txtConfigDirectory;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button cmdModsWhitelist;
        private System.Windows.Forms.Button cmdResourcePacksWhitelist;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label5;
    }
}