﻿
namespace GameLauncher.App
{
    partial class SettingsScreen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsScreen));
            this.SettingsClearServerModCacheButton = new System.Windows.Forms.Button();
            this.SettingsLauncherVersion = new System.Windows.Forms.Label();
            this.SettingsAboutButton = new System.Windows.Forms.Button();
            this.SettingsGameFiles = new System.Windows.Forms.Button();
            this.SettingsClearCommunicationLogButton = new System.Windows.Forms.Button();
            this.SettingsClearCrashLogsButton = new System.Windows.Forms.Button();
            this.SettingsVFilesButton = new System.Windows.Forms.Button();
            this.SettingsGamePathText = new System.Windows.Forms.Label();
            this.SettingsCDNText = new System.Windows.Forms.Label();
            this.SettingsCDNPick = new System.Windows.Forms.ComboBox();
            this.SettingsLanguageText = new System.Windows.Forms.Label();
            this.SettingsUEditorButton = new System.Windows.Forms.Button();
            this.SettingsWordFilterCheck = new System.Windows.Forms.CheckBox();
            this.SettingsProxyCheckbox = new System.Windows.Forms.CheckBox();
            this.SettingsDiscordRPCCheckbox = new System.Windows.Forms.CheckBox();
            this.SettingsGameFilesCurrentText = new System.Windows.Forms.Label();
            this.SettingsGameFilesCurrent = new System.Windows.Forms.LinkLabel();
            this.SettingsCDNCurrentText = new System.Windows.Forms.Label();
            this.SettingsCDNCurrent = new System.Windows.Forms.LinkLabel();
            this.SettingsLauncherPathText = new System.Windows.Forms.Label();
            this.SettingsLauncherPathCurrent = new System.Windows.Forms.LinkLabel();
            this.SettingsNetworkText = new System.Windows.Forms.Label();
            this.SettingsMainSrvText = new System.Windows.Forms.Label();
            this.SettingsMainCDNText = new System.Windows.Forms.Label();
            this.SettingsBkupSrvText = new System.Windows.Forms.Label();
            this.SettingsBkupCDNText = new System.Windows.Forms.Label();
            this.ThemeAuthor = new System.Windows.Forms.Label();
            this.ThemeName = new System.Windows.Forms.Label();
            this.SettingsLanguage = new System.Windows.Forms.ComboBox();
            this.SettingsSave = new System.Windows.Forms.Button();
            this.SettingsCancel = new System.Windows.Forms.Button();
            this.SettingsModNetZipDownload = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // SettingsClearServerModCacheButton
            // 
            this.SettingsClearServerModCacheButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(29)))), ((int)(((byte)(38)))));
            this.SettingsClearServerModCacheButton.Enabled = false;
            this.SettingsClearServerModCacheButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(181)))), ((int)(((byte)(191)))));
            this.SettingsClearServerModCacheButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(58)))), ((int)(((byte)(76)))));
            this.SettingsClearServerModCacheButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SettingsClearServerModCacheButton.Font = new System.Drawing.Font("DejaVu Sans", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SettingsClearServerModCacheButton.ForeColor = System.Drawing.Color.Silver;
            this.SettingsClearServerModCacheButton.Location = new System.Drawing.Point(38, 287);
            this.SettingsClearServerModCacheButton.Name = "SettingsClearServerModCacheButton";
            this.SettingsClearServerModCacheButton.Size = new System.Drawing.Size(135, 25);
            this.SettingsClearServerModCacheButton.TabIndex = 107;
            this.SettingsClearServerModCacheButton.Text = "Clear Server Mods";
            this.SettingsClearServerModCacheButton.UseVisualStyleBackColor = false;
            this.SettingsClearServerModCacheButton.Click += new System.EventHandler(this.SettingsClearServerModCacheButton_Click);
            // 
            // SettingsLauncherVersion
            // 
            this.SettingsLauncherVersion.BackColor = System.Drawing.Color.Transparent;
            this.SettingsLauncherVersion.Font = new System.Drawing.Font("DejaVu Sans", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SettingsLauncherVersion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.SettingsLauncherVersion.Location = new System.Drawing.Point(36, 480);
            this.SettingsLauncherVersion.Name = "SettingsLauncherVersion";
            this.SettingsLauncherVersion.Size = new System.Drawing.Size(114, 14);
            this.SettingsLauncherVersion.TabIndex = 118;
            this.SettingsLauncherVersion.Text = "Version: vX.X.X.X";
            this.SettingsLauncherVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.SettingsLauncherVersion.Click += new System.EventHandler(this.SettingsLauncherVersion_Click);
            // 
            // SettingsAboutButton
            // 
            this.SettingsAboutButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(29)))), ((int)(((byte)(38)))));
            this.SettingsAboutButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(181)))), ((int)(((byte)(191)))));
            this.SettingsAboutButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(58)))), ((int)(((byte)(76)))));
            this.SettingsAboutButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SettingsAboutButton.Font = new System.Drawing.Font("DejaVu Sans", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SettingsAboutButton.ForeColor = System.Drawing.Color.Silver;
            this.SettingsAboutButton.Location = new System.Drawing.Point(780, 58);
            this.SettingsAboutButton.Name = "SettingsAboutButton";
            this.SettingsAboutButton.Size = new System.Drawing.Size(75, 23);
            this.SettingsAboutButton.TabIndex = 117;
            this.SettingsAboutButton.Text = "About";
            this.SettingsAboutButton.UseVisualStyleBackColor = true;
            this.SettingsAboutButton.Click += new System.EventHandler(this.SettingsAboutButton_Click);
            // 
            // SettingsGameFiles
            // 
            this.SettingsGameFiles.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(29)))), ((int)(((byte)(38)))));
            this.SettingsGameFiles.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(181)))), ((int)(((byte)(191)))));
            this.SettingsGameFiles.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(58)))), ((int)(((byte)(76)))));
            this.SettingsGameFiles.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SettingsGameFiles.Font = new System.Drawing.Font("DejaVu Sans", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SettingsGameFiles.ForeColor = System.Drawing.Color.Silver;
            this.SettingsGameFiles.Location = new System.Drawing.Point(38, 80);
            this.SettingsGameFiles.Name = "SettingsGameFiles";
            this.SettingsGameFiles.Size = new System.Drawing.Size(190, 23);
            this.SettingsGameFiles.TabIndex = 104;
            this.SettingsGameFiles.Text = "Change GameFiles Path";
            this.SettingsGameFiles.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.SettingsGameFiles.UseVisualStyleBackColor = false;
            this.SettingsGameFiles.Click += new System.EventHandler(this.SettingsGameFiles_Click);
            // 
            // SettingsClearCommunicationLogButton
            // 
            this.SettingsClearCommunicationLogButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(29)))), ((int)(((byte)(38)))));
            this.SettingsClearCommunicationLogButton.Enabled = false;
            this.SettingsClearCommunicationLogButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(181)))), ((int)(((byte)(191)))));
            this.SettingsClearCommunicationLogButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(58)))), ((int)(((byte)(76)))));
            this.SettingsClearCommunicationLogButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SettingsClearCommunicationLogButton.Font = new System.Drawing.Font("DejaVu Sans", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SettingsClearCommunicationLogButton.ForeColor = System.Drawing.Color.Silver;
            this.SettingsClearCommunicationLogButton.Location = new System.Drawing.Point(38, 250);
            this.SettingsClearCommunicationLogButton.Name = "SettingsClearCommunicationLogButton";
            this.SettingsClearCommunicationLogButton.Size = new System.Drawing.Size(135, 25);
            this.SettingsClearCommunicationLogButton.TabIndex = 106;
            this.SettingsClearCommunicationLogButton.Text = "Clear NFSWO Log";
            this.SettingsClearCommunicationLogButton.UseVisualStyleBackColor = false;
            this.SettingsClearCommunicationLogButton.Click += new System.EventHandler(this.SettingsClearCommunicationLogButton_Click);
            // 
            // SettingsClearCrashLogsButton
            // 
            this.SettingsClearCrashLogsButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(29)))), ((int)(((byte)(38)))));
            this.SettingsClearCrashLogsButton.Enabled = false;
            this.SettingsClearCrashLogsButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(181)))), ((int)(((byte)(191)))));
            this.SettingsClearCrashLogsButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(58)))), ((int)(((byte)(76)))));
            this.SettingsClearCrashLogsButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SettingsClearCrashLogsButton.Font = new System.Drawing.Font("DejaVu Sans", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SettingsClearCrashLogsButton.ForeColor = System.Drawing.Color.Silver;
            this.SettingsClearCrashLogsButton.Location = new System.Drawing.Point(38, 213);
            this.SettingsClearCrashLogsButton.Name = "SettingsClearCrashLogsButton";
            this.SettingsClearCrashLogsButton.Size = new System.Drawing.Size(135, 25);
            this.SettingsClearCrashLogsButton.TabIndex = 105;
            this.SettingsClearCrashLogsButton.Text = "Clear Crash Logs";
            this.SettingsClearCrashLogsButton.UseVisualStyleBackColor = false;
            this.SettingsClearCrashLogsButton.Click += new System.EventHandler(this.SettingsClearCrashLogsButton_Click);
            // 
            // SettingsVFilesButton
            // 
            this.SettingsVFilesButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(29)))), ((int)(((byte)(38)))));
            this.SettingsVFilesButton.FlatAppearance.BorderColor = System.Drawing.Color.DarkGoldenrod;
            this.SettingsVFilesButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(58)))), ((int)(((byte)(76)))));
            this.SettingsVFilesButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SettingsVFilesButton.Font = new System.Drawing.Font("DejaVu Sans", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SettingsVFilesButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(200)))), ((int)(((byte)(0)))));
            this.SettingsVFilesButton.Location = new System.Drawing.Point(234, 80);
            this.SettingsVFilesButton.Name = "SettingsVFilesButton";
            this.SettingsVFilesButton.Size = new System.Drawing.Size(124, 23);
            this.SettingsVFilesButton.TabIndex = 100;
            this.SettingsVFilesButton.Text = "Verify GameFiles";
            this.SettingsVFilesButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.SettingsVFilesButton.UseVisualStyleBackColor = false;
            this.SettingsVFilesButton.Click += new System.EventHandler(this.SettingsVFilesButton_Click);
            // 
            // SettingsGamePathText
            // 
            this.SettingsGamePathText.BackColor = System.Drawing.Color.Transparent;
            this.SettingsGamePathText.Font = new System.Drawing.Font("DejaVu Sans", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SettingsGamePathText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.SettingsGamePathText.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.SettingsGamePathText.Location = new System.Drawing.Point(36, 61);
            this.SettingsGamePathText.Name = "SettingsGamePathText";
            this.SettingsGamePathText.Size = new System.Drawing.Size(100, 14);
            this.SettingsGamePathText.TabIndex = 169;
            this.SettingsGamePathText.Text = "GAMEFILES:";
            this.SettingsGamePathText.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // SettingsCDNText
            // 
            this.SettingsCDNText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SettingsCDNText.BackColor = System.Drawing.Color.Transparent;
            this.SettingsCDNText.Font = new System.Drawing.Font("DejaVu Sans", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SettingsCDNText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.SettingsCDNText.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.SettingsCDNText.Location = new System.Drawing.Point(36, 112);
            this.SettingsCDNText.Name = "SettingsCDNText";
            this.SettingsCDNText.Size = new System.Drawing.Size(124, 14);
            this.SettingsCDNText.TabIndex = 172;
            this.SettingsCDNText.Text = "CDN: PINGING";
            this.SettingsCDNText.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // SettingsCDNPick
            // 
            this.SettingsCDNPick.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(58)))), ((int)(((byte)(76)))));
            this.SettingsCDNPick.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.SettingsCDNPick.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SettingsCDNPick.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SettingsCDNPick.Font = new System.Drawing.Font("DejaVu Sans", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SettingsCDNPick.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.SettingsCDNPick.FormattingEnabled = true;
            this.SettingsCDNPick.Location = new System.Drawing.Point(38, 131);
            this.SettingsCDNPick.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.SettingsCDNPick.Name = "SettingsCDNPick";
            this.SettingsCDNPick.Size = new System.Drawing.Size(287, 21);
            this.SettingsCDNPick.TabIndex = 101;
            this.SettingsCDNPick.SelectedIndexChanged += new System.EventHandler(this.SettingsCDNPick_SelectedIndexChanged);
            // 
            // SettingsLanguageText
            // 
            this.SettingsLanguageText.BackColor = System.Drawing.Color.Transparent;
            this.SettingsLanguageText.Font = new System.Drawing.Font("DejaVu Sans", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SettingsLanguageText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.SettingsLanguageText.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.SettingsLanguageText.Location = new System.Drawing.Point(36, 161);
            this.SettingsLanguageText.Name = "SettingsLanguageText";
            this.SettingsLanguageText.Size = new System.Drawing.Size(135, 14);
            this.SettingsLanguageText.TabIndex = 163;
            this.SettingsLanguageText.Text = "GAME LANGUAGE";
            this.SettingsLanguageText.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // SettingsUEditorButton
            // 
            this.SettingsUEditorButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(29)))), ((int)(((byte)(38)))));
            this.SettingsUEditorButton.FlatAppearance.BorderColor = System.Drawing.Color.DarkGoldenrod;
            this.SettingsUEditorButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(58)))), ((int)(((byte)(76)))));
            this.SettingsUEditorButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SettingsUEditorButton.Font = new System.Drawing.Font("DejaVu Sans", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SettingsUEditorButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(200)))), ((int)(((byte)(0)))));
            this.SettingsUEditorButton.Location = new System.Drawing.Point(213, 179);
            this.SettingsUEditorButton.Name = "SettingsUEditorButton";
            this.SettingsUEditorButton.Size = new System.Drawing.Size(145, 23);
            this.SettingsUEditorButton.TabIndex = 103;
            this.SettingsUEditorButton.Text = "Edit UserSettings";
            this.SettingsUEditorButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.SettingsUEditorButton.UseVisualStyleBackColor = false;
            this.SettingsUEditorButton.Click += new System.EventHandler(this.SettingsUEditorButton_Click);
            // 
            // SettingsWordFilterCheck
            // 
            this.SettingsWordFilterCheck.BackColor = System.Drawing.Color.Transparent;
            this.SettingsWordFilterCheck.Font = new System.Drawing.Font("DejaVu Sans", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SettingsWordFilterCheck.ForeColor = System.Drawing.Color.DarkGoldenrod;
            this.SettingsWordFilterCheck.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.SettingsWordFilterCheck.Location = new System.Drawing.Point(38, 325);
            this.SettingsWordFilterCheck.Name = "SettingsWordFilterCheck";
            this.SettingsWordFilterCheck.Size = new System.Drawing.Size(264, 18);
            this.SettingsWordFilterCheck.TabIndex = 108;
            this.SettingsWordFilterCheck.Text = "Disable Word Filtering on Game Chat";
            this.SettingsWordFilterCheck.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.SettingsWordFilterCheck.UseVisualStyleBackColor = false;
            // 
            // SettingsProxyCheckbox
            // 
            this.SettingsProxyCheckbox.BackColor = System.Drawing.Color.Transparent;
            this.SettingsProxyCheckbox.Font = new System.Drawing.Font("DejaVu Sans", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SettingsProxyCheckbox.ForeColor = System.Drawing.Color.DarkGoldenrod;
            this.SettingsProxyCheckbox.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.SettingsProxyCheckbox.Location = new System.Drawing.Point(38, 347);
            this.SettingsProxyCheckbox.Name = "SettingsProxyCheckbox";
            this.SettingsProxyCheckbox.Size = new System.Drawing.Size(112, 18);
            this.SettingsProxyCheckbox.TabIndex = 109;
            this.SettingsProxyCheckbox.Text = "Disable Proxy";
            this.SettingsProxyCheckbox.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.SettingsProxyCheckbox.UseVisualStyleBackColor = false;
            // 
            // SettingsDiscordRPCCheckbox
            // 
            this.SettingsDiscordRPCCheckbox.BackColor = System.Drawing.Color.Transparent;
            this.SettingsDiscordRPCCheckbox.Font = new System.Drawing.Font("DejaVu Sans", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SettingsDiscordRPCCheckbox.ForeColor = System.Drawing.Color.DarkGoldenrod;
            this.SettingsDiscordRPCCheckbox.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.SettingsDiscordRPCCheckbox.Location = new System.Drawing.Point(38, 369);
            this.SettingsDiscordRPCCheckbox.Name = "SettingsDiscordRPCCheckbox";
            this.SettingsDiscordRPCCheckbox.Size = new System.Drawing.Size(155, 18);
            this.SettingsDiscordRPCCheckbox.TabIndex = 110;
            this.SettingsDiscordRPCCheckbox.Text = "Disable Discord RPC";
            this.SettingsDiscordRPCCheckbox.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.SettingsDiscordRPCCheckbox.UseVisualStyleBackColor = false;
            // 
            // SettingsGameFilesCurrentText
            // 
            this.SettingsGameFilesCurrentText.BackColor = System.Drawing.Color.Transparent;
            this.SettingsGameFilesCurrentText.Font = new System.Drawing.Font("DejaVu Sans", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SettingsGameFilesCurrentText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.SettingsGameFilesCurrentText.Location = new System.Drawing.Point(370, 73);
            this.SettingsGameFilesCurrentText.Name = "SettingsGameFilesCurrentText";
            this.SettingsGameFilesCurrentText.Size = new System.Drawing.Size(164, 14);
            this.SettingsGameFilesCurrentText.TabIndex = 183;
            this.SettingsGameFilesCurrentText.Text = "CURRENT DIRECTORY:";
            this.SettingsGameFilesCurrentText.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // SettingsGameFilesCurrent
            // 
            this.SettingsGameFilesCurrent.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.SettingsGameFilesCurrent.BackColor = System.Drawing.Color.Transparent;
            this.SettingsGameFilesCurrent.Font = new System.Drawing.Font("DejaVu Sans", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SettingsGameFilesCurrent.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.SettingsGameFilesCurrent.LinkColor = System.Drawing.Color.LawnGreen;
            this.SettingsGameFilesCurrent.Location = new System.Drawing.Point(371, 88);
            this.SettingsGameFilesCurrent.Name = "SettingsGameFilesCurrent";
            this.SettingsGameFilesCurrent.Size = new System.Drawing.Size(360, 30);
            this.SettingsGameFilesCurrent.TabIndex = 111;
            this.SettingsGameFilesCurrent.TabStop = true;
            this.SettingsGameFilesCurrent.Text = "C:\\Soapbox Race World\\Game Files";
            this.SettingsGameFilesCurrent.VisitedLinkColor = System.Drawing.Color.White;
            this.SettingsGameFilesCurrent.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.SettingsGameFilesCurrent_LinkClicked);
            // 
            // SettingsCDNCurrentText
            // 
            this.SettingsCDNCurrentText.BackColor = System.Drawing.Color.Transparent;
            this.SettingsCDNCurrentText.Font = new System.Drawing.Font("DejaVu Sans", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SettingsCDNCurrentText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.SettingsCDNCurrentText.Location = new System.Drawing.Point(370, 123);
            this.SettingsCDNCurrentText.Name = "SettingsCDNCurrentText";
            this.SettingsCDNCurrentText.Size = new System.Drawing.Size(112, 14);
            this.SettingsCDNCurrentText.TabIndex = 184;
            this.SettingsCDNCurrentText.Text = "CURRENT CDN:";
            this.SettingsCDNCurrentText.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // SettingsCDNCurrent
            // 
            this.SettingsCDNCurrent.ActiveLinkColor = System.Drawing.Color.Transparent;
            this.SettingsCDNCurrent.BackColor = System.Drawing.Color.Transparent;
            this.SettingsCDNCurrent.Font = new System.Drawing.Font("DejaVu Sans", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SettingsCDNCurrent.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.SettingsCDNCurrent.LinkColor = System.Drawing.Color.LawnGreen;
            this.SettingsCDNCurrent.Location = new System.Drawing.Point(371, 139);
            this.SettingsCDNCurrent.Name = "SettingsCDNCurrent";
            this.SettingsCDNCurrent.Size = new System.Drawing.Size(360, 14);
            this.SettingsCDNCurrent.TabIndex = 112;
            this.SettingsCDNCurrent.TabStop = true;
            this.SettingsCDNCurrent.Text = "http://localhost";
            this.SettingsCDNCurrent.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.SettingsCDNCurrent.VisitedLinkColor = System.Drawing.Color.White;
            this.SettingsCDNCurrent.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.SettingsCDNCurrent_LinkClicked);
            // 
            // SettingsLauncherPathText
            // 
            this.SettingsLauncherPathText.BackColor = System.Drawing.Color.Transparent;
            this.SettingsLauncherPathText.Font = new System.Drawing.Font("DejaVu Sans", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SettingsLauncherPathText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.SettingsLauncherPathText.Location = new System.Drawing.Point(370, 164);
            this.SettingsLauncherPathText.Name = "SettingsLauncherPathText";
            this.SettingsLauncherPathText.Size = new System.Drawing.Size(150, 14);
            this.SettingsLauncherPathText.TabIndex = 171;
            this.SettingsLauncherPathText.Text = "LAUNCHER FOLDER:";
            this.SettingsLauncherPathText.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // SettingsLauncherPathCurrent
            // 
            this.SettingsLauncherPathCurrent.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.SettingsLauncherPathCurrent.BackColor = System.Drawing.Color.Transparent;
            this.SettingsLauncherPathCurrent.Font = new System.Drawing.Font("DejaVu Sans", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SettingsLauncherPathCurrent.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.SettingsLauncherPathCurrent.LinkColor = System.Drawing.Color.LawnGreen;
            this.SettingsLauncherPathCurrent.Location = new System.Drawing.Point(371, 179);
            this.SettingsLauncherPathCurrent.Name = "SettingsLauncherPathCurrent";
            this.SettingsLauncherPathCurrent.Size = new System.Drawing.Size(360, 30);
            this.SettingsLauncherPathCurrent.TabIndex = 113;
            this.SettingsLauncherPathCurrent.TabStop = true;
            this.SettingsLauncherPathCurrent.Text = "C:\\Soapbox Race World\\Launcher";
            this.SettingsLauncherPathCurrent.VisitedLinkColor = System.Drawing.Color.White;
            this.SettingsLauncherPathCurrent.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.SettingsLauncherPathCurrent_LinkClicked);
            // 
            // SettingsNetworkText
            // 
            this.SettingsNetworkText.BackColor = System.Drawing.Color.Transparent;
            this.SettingsNetworkText.Font = new System.Drawing.Font("DejaVu Sans", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SettingsNetworkText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.SettingsNetworkText.Location = new System.Drawing.Point(370, 214);
            this.SettingsNetworkText.Name = "SettingsNetworkText";
            this.SettingsNetworkText.Size = new System.Drawing.Size(164, 14);
            this.SettingsNetworkText.TabIndex = 178;
            this.SettingsNetworkText.Text = "CONNECTION STATUS:";
            this.SettingsNetworkText.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // SettingsMainSrvText
            // 
            this.SettingsMainSrvText.BackColor = System.Drawing.Color.Transparent;
            this.SettingsMainSrvText.Font = new System.Drawing.Font("DejaVu Sans", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SettingsMainSrvText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(179)))), ((int)(((byte)(189)))));
            this.SettingsMainSrvText.Location = new System.Drawing.Point(370, 230);
            this.SettingsMainSrvText.Name = "SettingsMainSrvText";
            this.SettingsMainSrvText.Size = new System.Drawing.Size(210, 14);
            this.SettingsMainSrvText.TabIndex = 179;
            this.SettingsMainSrvText.Text = "Main Server List API: PINGING";
            // 
            // SettingsMainCDNText
            // 
            this.SettingsMainCDNText.BackColor = System.Drawing.Color.Transparent;
            this.SettingsMainCDNText.Font = new System.Drawing.Font("DejaVu Sans", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SettingsMainCDNText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(179)))), ((int)(((byte)(189)))));
            this.SettingsMainCDNText.Location = new System.Drawing.Point(370, 250);
            this.SettingsMainCDNText.Name = "SettingsMainCDNText";
            this.SettingsMainCDNText.Size = new System.Drawing.Size(210, 14);
            this.SettingsMainCDNText.TabIndex = 182;
            this.SettingsMainCDNText.Text = "Main CDN List API: PINGING";
            this.SettingsMainCDNText.Visible = false;
            // 
            // SettingsBkupSrvText
            // 
            this.SettingsBkupSrvText.BackColor = System.Drawing.Color.Transparent;
            this.SettingsBkupSrvText.Font = new System.Drawing.Font("DejaVu Sans", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SettingsBkupSrvText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(179)))), ((int)(((byte)(189)))));
            this.SettingsBkupSrvText.Location = new System.Drawing.Point(370, 270);
            this.SettingsBkupSrvText.Name = "SettingsBkupSrvText";
            this.SettingsBkupSrvText.Size = new System.Drawing.Size(210, 14);
            this.SettingsBkupSrvText.TabIndex = 180;
            this.SettingsBkupSrvText.Text = "Backup Server List API: PINGING";
            this.SettingsBkupSrvText.Visible = false;
            // 
            // SettingsBkupCDNText
            // 
            this.SettingsBkupCDNText.BackColor = System.Drawing.Color.Transparent;
            this.SettingsBkupCDNText.Font = new System.Drawing.Font("DejaVu Sans", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SettingsBkupCDNText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(179)))), ((int)(((byte)(189)))));
            this.SettingsBkupCDNText.Location = new System.Drawing.Point(370, 290);
            this.SettingsBkupCDNText.Name = "SettingsBkupCDNText";
            this.SettingsBkupCDNText.Size = new System.Drawing.Size(210, 14);
            this.SettingsBkupCDNText.TabIndex = 181;
            this.SettingsBkupCDNText.Text = "Backup CDN List API: PINGING";
            this.SettingsBkupCDNText.Visible = false;
            // 
            // ThemeAuthor
            // 
            this.ThemeAuthor.BackColor = System.Drawing.Color.Transparent;
            this.ThemeAuthor.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.ThemeAuthor.Location = new System.Drawing.Point(35, 454);
            this.ThemeAuthor.Name = "ThemeAuthor";
            this.ThemeAuthor.Size = new System.Drawing.Size(232, 14);
            this.ThemeAuthor.TabIndex = 190;
            this.ThemeAuthor.Text = "Theme Author: Launcher - Division";
            this.ThemeAuthor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ThemeName
            // 
            this.ThemeName.BackColor = System.Drawing.Color.Transparent;
            this.ThemeName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.ThemeName.Location = new System.Drawing.Point(35, 428);
            this.ThemeName.Name = "ThemeName";
            this.ThemeName.Size = new System.Drawing.Size(148, 14);
            this.ThemeName.TabIndex = 191;
            this.ThemeName.Text = "Theme Name: Default";
            this.ThemeName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // SettingsLanguage
            // 
            this.SettingsLanguage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(58)))), ((int)(((byte)(76)))));
            this.SettingsLanguage.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.SettingsLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SettingsLanguage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SettingsLanguage.Font = new System.Drawing.Font("DejaVu Sans", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SettingsLanguage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.SettingsLanguage.FormattingEnabled = true;
            this.SettingsLanguage.Location = new System.Drawing.Point(38, 179);
            this.SettingsLanguage.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.SettingsLanguage.Name = "SettingsLanguage";
            this.SettingsLanguage.Size = new System.Drawing.Size(164, 21);
            this.SettingsLanguage.TabIndex = 102;
            this.SettingsLanguage.SelectedIndexChanged += new System.EventHandler(this.SettingsLanguage_SelectedIndexChanged);
            // 
            // SettingsSave
            // 
            this.SettingsSave.BackColor = System.Drawing.Color.Transparent;
            this.SettingsSave.FlatAppearance.BorderSize = 0;
            this.SettingsSave.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.SettingsSave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.SettingsSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SettingsSave.Font = new System.Drawing.Font("DejaVu Sans", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SettingsSave.ForeColor = System.Drawing.Color.White;
            this.SettingsSave.Image = global::GameLauncher.Properties.Resources.greenbutton;
            this.SettingsSave.Location = new System.Drawing.Point(645, 454);
            this.SettingsSave.Name = "SettingsSave";
            this.SettingsSave.Size = new System.Drawing.Size(100, 42);
            this.SettingsSave.TabIndex = 114;
            this.SettingsSave.Text = "SAVE";
            this.SettingsSave.UseVisualStyleBackColor = false;
            this.SettingsSave.Click += new System.EventHandler(this.SettingsSave_Click);
            // 
            // SettingsCancel
            // 
            this.SettingsCancel.BackColor = System.Drawing.Color.Transparent;
            this.SettingsCancel.FlatAppearance.BorderSize = 0;
            this.SettingsCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.SettingsCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.SettingsCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SettingsCancel.Font = new System.Drawing.Font("DejaVu Sans", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SettingsCancel.ForeColor = System.Drawing.Color.White;
            this.SettingsCancel.Image = global::GameLauncher.Properties.Resources.graybutton;
            this.SettingsCancel.Location = new System.Drawing.Point(755, 454);
            this.SettingsCancel.Name = "SettingsCancel";
            this.SettingsCancel.Size = new System.Drawing.Size(100, 42);
            this.SettingsCancel.TabIndex = 116;
            this.SettingsCancel.Text = "EXIT";
            this.SettingsCancel.UseVisualStyleBackColor = false;
            this.SettingsCancel.Click += new System.EventHandler(this.SettingsCancel_Click);
            // 
            // SettingsModNetZipDownload
            // 
            this.SettingsModNetZipDownload.BackColor = System.Drawing.Color.Transparent;
            this.SettingsModNetZipDownload.Font = new System.Drawing.Font("DejaVu Sans", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SettingsModNetZipDownload.ForeColor = System.Drawing.Color.DarkGoldenrod;
            this.SettingsModNetZipDownload.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.SettingsModNetZipDownload.Location = new System.Drawing.Point(38, 391);
            this.SettingsModNetZipDownload.Name = "SettingsModNetZipDownload";
            this.SettingsModNetZipDownload.Size = new System.Drawing.Size(229, 18);
            this.SettingsModNetZipDownload.TabIndex = 192;
            this.SettingsModNetZipDownload.Text = "Enable ModNet ZIP Download";
            this.SettingsModNetZipDownload.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.SettingsModNetZipDownload.UseVisualStyleBackColor = false;
            // 
            // SettingsScreen
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Desktop;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(891, 529);
            this.Controls.Add(this.SettingsModNetZipDownload);
            this.Controls.Add(this.SettingsLanguage);
            this.Controls.Add(this.ThemeName);
            this.Controls.Add(this.ThemeAuthor);
            this.Controls.Add(this.SettingsClearServerModCacheButton);
            this.Controls.Add(this.SettingsLauncherVersion);
            this.Controls.Add(this.SettingsAboutButton);
            this.Controls.Add(this.SettingsGameFiles);
            this.Controls.Add(this.SettingsClearCommunicationLogButton);
            this.Controls.Add(this.SettingsClearCrashLogsButton);
            this.Controls.Add(this.SettingsVFilesButton);
            this.Controls.Add(this.SettingsGamePathText);
            this.Controls.Add(this.SettingsSave);
            this.Controls.Add(this.SettingsCancel);
            this.Controls.Add(this.SettingsCDNText);
            this.Controls.Add(this.SettingsCDNPick);
            this.Controls.Add(this.SettingsLanguageText);
            this.Controls.Add(this.SettingsUEditorButton);
            this.Controls.Add(this.SettingsWordFilterCheck);
            this.Controls.Add(this.SettingsProxyCheckbox);
            this.Controls.Add(this.SettingsDiscordRPCCheckbox);
            this.Controls.Add(this.SettingsGameFilesCurrentText);
            this.Controls.Add(this.SettingsGameFilesCurrent);
            this.Controls.Add(this.SettingsCDNCurrentText);
            this.Controls.Add(this.SettingsCDNCurrent);
            this.Controls.Add(this.SettingsLauncherPathText);
            this.Controls.Add(this.SettingsLauncherPathCurrent);
            this.Controls.Add(this.SettingsNetworkText);
            this.Controls.Add(this.SettingsMainSrvText);
            this.Controls.Add(this.SettingsMainCDNText);
            this.Controls.Add(this.SettingsBkupSrvText);
            this.Controls.Add(this.SettingsBkupCDNText);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("DejaVu Sans", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "SettingsScreen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Settings - SBRW Launcher";
            this.TransparencyKey = System.Drawing.Color.Fuchsia;
            this.Load += new System.EventHandler(this.SettingsScreen_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button SettingsClearServerModCacheButton;
        private System.Windows.Forms.Label SettingsLauncherVersion;
        private System.Windows.Forms.Button SettingsAboutButton;
        private System.Windows.Forms.Button SettingsGameFiles;
        private System.Windows.Forms.Button SettingsClearCommunicationLogButton;
        private System.Windows.Forms.Button SettingsClearCrashLogsButton;
        private System.Windows.Forms.Button SettingsVFilesButton;
        private System.Windows.Forms.Label SettingsGamePathText;
        private System.Windows.Forms.Button SettingsSave;
        private System.Windows.Forms.Button SettingsCancel;
        private System.Windows.Forms.Label SettingsCDNText;
        private System.Windows.Forms.ComboBox SettingsCDNPick;
        private System.Windows.Forms.Label SettingsLanguageText;
        private System.Windows.Forms.Button SettingsUEditorButton;
        private System.Windows.Forms.CheckBox SettingsWordFilterCheck;
        private System.Windows.Forms.CheckBox SettingsProxyCheckbox;
        private System.Windows.Forms.CheckBox SettingsDiscordRPCCheckbox;
        private System.Windows.Forms.Label SettingsGameFilesCurrentText;
        private System.Windows.Forms.LinkLabel SettingsGameFilesCurrent;
        private System.Windows.Forms.Label SettingsCDNCurrentText;
        private System.Windows.Forms.LinkLabel SettingsCDNCurrent;
        private System.Windows.Forms.Label SettingsLauncherPathText;
        private System.Windows.Forms.LinkLabel SettingsLauncherPathCurrent;
        private System.Windows.Forms.Label SettingsNetworkText;
        private System.Windows.Forms.Label SettingsMainSrvText;
        private System.Windows.Forms.Label SettingsMainCDNText;
        private System.Windows.Forms.Label SettingsBkupSrvText;
        private System.Windows.Forms.Label SettingsBkupCDNText;
        private System.Windows.Forms.Label ThemeAuthor;
        private System.Windows.Forms.Label ThemeName;
        private System.Windows.Forms.ComboBox SettingsLanguage;
        private System.Windows.Forms.CheckBox SettingsModNetZipDownload;
    }
}