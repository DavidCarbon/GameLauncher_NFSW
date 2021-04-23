﻿namespace GameLauncher
{
    sealed partial class MainScreen
    {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Wymagana metoda obsługi projektanta — nie należy modyfikować 
        /// zawartość tej metody z edytorem kodu.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainScreen));
            this.Timeout = new System.Windows.Forms.Timer(this.components);
            this.SelectServerBtn = new System.Windows.Forms.Button();
            this.translatedBy = new System.Windows.Forms.Label();
            this.ServerPick = new System.Windows.Forms.ComboBox();
            this.AddServer = new System.Windows.Forms.Button();
            this.PlayProgressText = new System.Windows.Forms.Label();
            this.LauncherStatusText = new System.Windows.Forms.Label();
            this.LauncherStatusDesc = new System.Windows.Forms.Label();
            this.ServerStatusText = new System.Windows.Forms.Label();
            this.ServerStatusDesc = new System.Windows.Forms.Label();
            this.APIStatusText = new System.Windows.Forms.Label();
            this.APIStatusDesc = new System.Windows.Forms.Label();
            this.CurrentWindowInfo = new System.Windows.Forms.Label();
            this.MainEmail = new System.Windows.Forms.TextBox();
            this.MainPassword = new System.Windows.Forms.TextBox();
            this.RememberMe = new System.Windows.Forms.CheckBox();
            this.ForgotPassword = new System.Windows.Forms.LinkLabel();
            this.LoginButton = new System.Windows.Forms.Button();
            this.RegisterText = new System.Windows.Forms.Button();
            this.ServerPingStatusText = new System.Windows.Forms.Label();
            this.LogoutButton = new System.Windows.Forms.Button();
            this.PlayButton = new System.Windows.Forms.Button();
            this.PlayProgressTextTimer = new System.Windows.Forms.Label();
            this.ShowPlayPanel = new System.Windows.Forms.Panel();
            this.InsiderBuildNumberText = new System.Windows.Forms.Label();
            this.logo = new System.Windows.Forms.PictureBox();
            this.SettingsButton = new System.Windows.Forms.PictureBox();
            this.CloseBTN = new System.Windows.Forms.PictureBox();
            this.ServerInfoPanel = new System.Windows.Forms.Panel();
            this.HomePageIcon = new System.Windows.Forms.PictureBox();
            this.DiscordIcon = new System.Windows.Forms.PictureBox();
            this.FacebookIcon = new System.Windows.Forms.PictureBox();
            this.TwitterAccountLink = new System.Windows.Forms.LinkLabel();
            this.TwitterIcon = new System.Windows.Forms.PictureBox();
            this.FacebookGroupLink = new System.Windows.Forms.LinkLabel();
            this.HomePageLink = new System.Windows.Forms.LinkLabel();
            this.DiscordInviteLink = new System.Windows.Forms.LinkLabel();
            this.ServerShutDown = new System.Windows.Forms.Label();
            this.SceneryGroupText = new System.Windows.Forms.Label();
            this.LauncherIconStatus = new System.Windows.Forms.PictureBox();
            this.APIStatusIcon = new System.Windows.Forms.PictureBox();
            this.ServerStatusIcon = new System.Windows.Forms.PictureBox();
            this.MainEmailBorder = new System.Windows.Forms.PictureBox();
            this.MainPasswordBorder = new System.Windows.Forms.PictureBox();
            this.ProgressBarOutline = new System.Windows.Forms.PictureBox();
            this.VerticalBanner = new System.Windows.Forms.PictureBox();
            this.ExtractingProgress = new GameLauncher.App.Classes.LauncherCore.Visuals.ProgressBarEx();
            this.PlayProgress = new GameLauncher.App.Classes.LauncherCore.Visuals.ProgressBarEx();
            this.Notification = new System.Windows.Forms.NotifyIcon(this.components);
            this.ShowPlayPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SettingsButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CloseBTN)).BeginInit();
            this.ServerInfoPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.HomePageIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DiscordIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FacebookIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TwitterIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LauncherIconStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.APIStatusIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ServerStatusIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MainEmailBorder)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MainPasswordBorder)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ProgressBarOutline)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.VerticalBanner)).BeginInit();
            this.SuspendLayout();
            // 
            // Timeout
            // 
            this.Timeout.Interval = 3000;
            // 
            // SelectServerBtn
            // 
            this.SelectServerBtn.Font = new System.Drawing.Font("DejaVu Sans", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SelectServerBtn.Location = new System.Drawing.Point(888, 15);
            this.SelectServerBtn.Name = "SelectServerBtn";
            this.SelectServerBtn.Size = new System.Drawing.Size(228, 24);
            this.SelectServerBtn.TabIndex = 1;
            this.SelectServerBtn.Text = "Select Server";
            this.SelectServerBtn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.SelectServerBtn.UseVisualStyleBackColor = true;
            // 
            // translatedBy
            // 
            this.translatedBy.BackColor = System.Drawing.Color.Transparent;
            this.translatedBy.Font = new System.Drawing.Font("DejaVu Sans", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.translatedBy.ForeColor = System.Drawing.Color.DarkGray;
            this.translatedBy.Location = new System.Drawing.Point(12, -1);
            this.translatedBy.Name = "translatedBy";
            this.translatedBy.Size = new System.Drawing.Size(126, 13);
            this.translatedBy.TabIndex = 55;
            this.translatedBy.Text = "Translated by: meme";
            // 
            // ServerPick
            // 
            this.ServerPick.BackColor = System.Drawing.Color.White;
            this.ServerPick.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.ServerPick.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ServerPick.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ServerPick.Font = new System.Drawing.Font("DejaVu Sans", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ServerPick.ForeColor = System.Drawing.Color.Black;
            this.ServerPick.FormattingEnabled = true;
            this.ServerPick.Location = new System.Drawing.Point(586, 50);
            this.ServerPick.Name = "ServerPick";
            this.ServerPick.Size = new System.Drawing.Size(241, 22);
            this.ServerPick.TabIndex = 2;
            // 
            // AddServer
            // 
            this.AddServer.BackColor = System.Drawing.SystemColors.Control;
            this.AddServer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.AddServer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AddServer.Font = new System.Drawing.Font("DejaVu Sans", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AddServer.ForeColor = System.Drawing.Color.Black;
            this.AddServer.Location = new System.Drawing.Point(833, 49);
            this.AddServer.Name = "AddServer";
            this.AddServer.Size = new System.Drawing.Size(24, 24);
            this.AddServer.TabIndex = 3;
            this.AddServer.Text = "+";
            this.AddServer.UseVisualStyleBackColor = false;
            // 
            // PlayProgressText
            // 
            this.PlayProgressText.BackColor = System.Drawing.Color.Transparent;
            this.PlayProgressText.Font = new System.Drawing.Font("DejaVu Sans", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PlayProgressText.ForeColor = System.Drawing.Color.White;
            this.PlayProgressText.Location = new System.Drawing.Point(42, 404);
            this.PlayProgressText.Name = "PlayProgressText";
            this.PlayProgressText.Size = new System.Drawing.Size(510, 14);
            this.PlayProgressText.TabIndex = 10;
            this.PlayProgressText.Text = "PLEASE WAIT";
            this.PlayProgressText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LauncherStatusText
            // 
            this.LauncherStatusText.BackColor = System.Drawing.Color.Transparent;
            this.LauncherStatusText.Font = new System.Drawing.Font("DejaVu Sans", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LauncherStatusText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(132)))), ((int)(((byte)(132)))), ((int)(((byte)(132)))));
            this.LauncherStatusText.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.LauncherStatusText.Location = new System.Drawing.Point(53, 453);
            this.LauncherStatusText.Name = "LauncherStatusText";
            this.LauncherStatusText.Size = new System.Drawing.Size(152, 30);
            this.LauncherStatusText.TabIndex = 4;
            this.LauncherStatusText.Text = "Launcher\n - Checking";
            this.LauncherStatusText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.LauncherStatusText.UseCompatibleTextRendering = true;
            // 
            // LauncherStatusDesc
            // 
            this.LauncherStatusDesc.BackColor = System.Drawing.Color.Transparent;
            this.LauncherStatusDesc.Font = new System.Drawing.Font("DejaVu Sans", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LauncherStatusDesc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.LauncherStatusDesc.Location = new System.Drawing.Point(53, 486);
            this.LauncherStatusDesc.Name = "LauncherStatusDesc";
            this.LauncherStatusDesc.Size = new System.Drawing.Size(152, 14);
            this.LauncherStatusDesc.TabIndex = 5;
            this.LauncherStatusDesc.Text = "Version: vX.X.X.X";
            this.LauncherStatusDesc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ServerStatusText
            // 
            this.ServerStatusText.BackColor = System.Drawing.Color.Transparent;
            this.ServerStatusText.Font = new System.Drawing.Font("DejaVu Sans", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ServerStatusText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(179)))), ((int)(((byte)(189)))));
            this.ServerStatusText.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ServerStatusText.Location = new System.Drawing.Point(242, 453);
            this.ServerStatusText.Name = "ServerStatusText";
            this.ServerStatusText.Size = new System.Drawing.Size(150, 30);
            this.ServerStatusText.TabIndex = 7;
            this.ServerStatusText.Text = "Server Status\n - Pinging";
            this.ServerStatusText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ServerStatusDesc
            // 
            this.ServerStatusDesc.BackColor = System.Drawing.Color.Transparent;
            this.ServerStatusDesc.Font = new System.Drawing.Font("DejaVu Sans", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ServerStatusDesc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ServerStatusDesc.Location = new System.Drawing.Point(242, 486);
            this.ServerStatusDesc.Name = "ServerStatusDesc";
            this.ServerStatusDesc.Size = new System.Drawing.Size(150, 30);
            this.ServerStatusDesc.TabIndex = 8;
            this.ServerStatusDesc.Text = "Players Online: ###\r\nRegistered: ###";
            this.ServerStatusDesc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // APIStatusText
            // 
            this.APIStatusText.BackColor = System.Drawing.Color.Transparent;
            this.APIStatusText.Font = new System.Drawing.Font("DejaVu Sans", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.APIStatusText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(179)))), ((int)(((byte)(189)))));
            this.APIStatusText.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.APIStatusText.Location = new System.Drawing.Point(426, 453);
            this.APIStatusText.Name = "APIStatusText";
            this.APIStatusText.Size = new System.Drawing.Size(130, 30);
            this.APIStatusText.TabIndex = 116;
            this.APIStatusText.Text = "Main API\n - Pinging";
            this.APIStatusText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // APIStatusDesc
            // 
            this.APIStatusDesc.BackColor = System.Drawing.Color.Transparent;
            this.APIStatusDesc.Font = new System.Drawing.Font("DejaVu Sans", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.APIStatusDesc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.APIStatusDesc.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.APIStatusDesc.Location = new System.Drawing.Point(426, 486);
            this.APIStatusDesc.Name = "APIStatusDesc";
            this.APIStatusDesc.Size = new System.Drawing.Size(130, 14);
            this.APIStatusDesc.TabIndex = 120;
            this.APIStatusDesc.Text = "Checking Status";
            this.APIStatusDesc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // CurrentWindowInfo
            // 
            this.CurrentWindowInfo.BackColor = System.Drawing.Color.Transparent;
            this.CurrentWindowInfo.Cursor = System.Windows.Forms.Cursors.Default;
            this.CurrentWindowInfo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CurrentWindowInfo.Font = new System.Drawing.Font("DejaVu Sans", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CurrentWindowInfo.ForeColor = System.Drawing.Color.White;
            this.CurrentWindowInfo.Location = new System.Drawing.Point(625, 82);
            this.CurrentWindowInfo.Name = "CurrentWindowInfo";
            this.CurrentWindowInfo.Size = new System.Drawing.Size(211, 60);
            this.CurrentWindowInfo.TabIndex = 16;
            this.CurrentWindowInfo.Text = "ENTER YOUR ACCOUNT INFORMATION\n TO LOG IN";
            this.CurrentWindowInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.CurrentWindowInfo.UseCompatibleTextRendering = true;
            this.CurrentWindowInfo.UseMnemonic = false;
            // 
            // MainEmail
            // 
            this.MainEmail.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(32)))), ((int)(((byte)(42)))));
            this.MainEmail.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.MainEmail.Font = new System.Drawing.Font("DejaVu Sans", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainEmail.ForeColor = System.Drawing.Color.White;
            this.MainEmail.Location = new System.Drawing.Point(645, 173);
            this.MainEmail.Name = "MainEmail";
            this.MainEmail.Size = new System.Drawing.Size(180, 14);
            this.MainEmail.TabIndex = 4;
            this.MainEmail.TextChanged += new System.EventHandler(this.Email_TextChanged);
            // 
            // MainPassword
            // 
            this.MainPassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(32)))), ((int)(((byte)(42)))));
            this.MainPassword.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.MainPassword.Font = new System.Drawing.Font("DejaVu Sans", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainPassword.ForeColor = System.Drawing.Color.White;
            this.MainPassword.Location = new System.Drawing.Point(645, 221);
            this.MainPassword.Name = "MainPassword";
            this.MainPassword.Size = new System.Drawing.Size(180, 14);
            this.MainPassword.TabIndex = 5;
            this.MainPassword.UseSystemPasswordChar = true;
            this.MainPassword.WordWrap = false;
            this.MainPassword.TextChanged += new System.EventHandler(this.Password_TextChanged);
            // 
            // RememberMe
            // 
            this.RememberMe.BackColor = System.Drawing.Color.Transparent;
            this.RememberMe.Font = new System.Drawing.Font("DejaVu Sans", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RememberMe.ForeColor = System.Drawing.Color.White;
            this.RememberMe.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.RememberMe.Location = new System.Drawing.Point(645, 260);
            this.RememberMe.Name = "RememberMe";
            this.RememberMe.Size = new System.Drawing.Size(190, 18);
            this.RememberMe.TabIndex = 6;
            this.RememberMe.Text = "REMEMBER MY LOGIN";
            this.RememberMe.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.RememberMe.UseVisualStyleBackColor = false;
            // 
            // ForgotPassword
            // 
            this.ForgotPassword.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(228)))), ((int)(((byte)(0)))));
            this.ForgotPassword.BackColor = System.Drawing.Color.Transparent;
            this.ForgotPassword.Font = new System.Drawing.Font("DejaVu Sans", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForgotPassword.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(200)))), ((int)(((byte)(0)))));
            this.ForgotPassword.Location = new System.Drawing.Point(659, 281);
            this.ForgotPassword.Name = "ForgotPassword";
            this.ForgotPassword.Size = new System.Drawing.Size(180, 14);
            this.ForgotPassword.TabIndex = 7;
            this.ForgotPassword.TabStop = true;
            this.ForgotPassword.Text = "I FORGOT MY PASSWORD";
            this.ForgotPassword.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ForgotPassword.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(228)))), ((int)(((byte)(0)))));
            // 
            // LoginButton
            // 
            this.LoginButton.BackColor = System.Drawing.Color.Transparent;
            this.LoginButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.LoginButton.FlatAppearance.BorderSize = 0;
            this.LoginButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.LoginButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.LoginButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LoginButton.Font = new System.Drawing.Font("DejaVu Sans", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LoginButton.ForeColor = System.Drawing.Color.White;
            this.LoginButton.Location = new System.Drawing.Point(605, 362);
            this.LoginButton.Name = "LoginButton";
            this.LoginButton.Size = new System.Drawing.Size(231, 35);
            this.LoginButton.TabIndex = 8;
            this.LoginButton.Text = "LOG ON";
            this.LoginButton.UseVisualStyleBackColor = false;
            // 
            // RegisterText
            // 
            this.RegisterText.BackColor = System.Drawing.Color.Transparent;
            this.RegisterText.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.RegisterText.FlatAppearance.BorderSize = 0;
            this.RegisterText.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.RegisterText.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.RegisterText.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RegisterText.Font = new System.Drawing.Font("DejaVu Sans", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RegisterText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(159)))), ((int)(((byte)(193)))), ((int)(((byte)(32)))));
            this.RegisterText.Location = new System.Drawing.Point(605, 409);
            this.RegisterText.Name = "RegisterText";
            this.RegisterText.Size = new System.Drawing.Size(231, 35);
            this.RegisterText.TabIndex = 10;
            this.RegisterText.Text = "REGISTER";
            this.RegisterText.UseVisualStyleBackColor = false;
            // 
            // ServerPingStatusText
            // 
            this.ServerPingStatusText.BackColor = System.Drawing.Color.Transparent;
            this.ServerPingStatusText.Cursor = System.Windows.Forms.Cursors.Default;
            this.ServerPingStatusText.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ServerPingStatusText.Font = new System.Drawing.Font("DejaVu Sans", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ServerPingStatusText.ForeColor = System.Drawing.Color.White;
            this.ServerPingStatusText.Location = new System.Drawing.Point(5, 0);
            this.ServerPingStatusText.Name = "ServerPingStatusText";
            this.ServerPingStatusText.Size = new System.Drawing.Size(230, 61);
            this.ServerPingStatusText.TabIndex = 148;
            this.ServerPingStatusText.Text = "Your Ping to the Server";
            this.ServerPingStatusText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LogoutButton
            // 
            this.LogoutButton.BackColor = System.Drawing.Color.Transparent;
            this.LogoutButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.LogoutButton.FlatAppearance.BorderSize = 0;
            this.LogoutButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.LogoutButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.LogoutButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LogoutButton.Font = new System.Drawing.Font("DejaVu Sans", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LogoutButton.ForeColor = System.Drawing.Color.White;
            this.LogoutButton.Location = new System.Drawing.Point(6, 64);
            this.LogoutButton.Name = "LogoutButton";
            this.LogoutButton.Size = new System.Drawing.Size(231, 35);
            this.LogoutButton.TabIndex = 9;
            this.LogoutButton.Text = "LOG OUT";
            this.LogoutButton.UseVisualStyleBackColor = false;
            // 
            // PlayButton
            // 
            this.PlayButton.BackColor = System.Drawing.Color.Transparent;
            this.PlayButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.PlayButton.FlatAppearance.BorderSize = 0;
            this.PlayButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.PlayButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.PlayButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PlayButton.Font = new System.Drawing.Font("DejaVu Sans", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PlayButton.ForeColor = System.Drawing.Color.Transparent;
            this.PlayButton.Location = new System.Drawing.Point(7, 104);
            this.PlayButton.Name = "PlayButton";
            this.PlayButton.Size = new System.Drawing.Size(230, 63);
            this.PlayButton.TabIndex = 15;
            this.PlayButton.Text = "PLAY NOW";
            this.PlayButton.UseVisualStyleBackColor = false;
            // 
            // PlayProgressTextTimer
            // 
            this.PlayProgressTextTimer.BackColor = System.Drawing.Color.Transparent;
            this.PlayProgressTextTimer.Font = new System.Drawing.Font("DejaVu Sans", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PlayProgressTextTimer.ForeColor = System.Drawing.Color.White;
            this.PlayProgressTextTimer.Location = new System.Drawing.Point(25, 385);
            this.PlayProgressTextTimer.Name = "PlayProgressTextTimer";
            this.PlayProgressTextTimer.Size = new System.Drawing.Size(529, 14);
            this.PlayProgressTextTimer.TabIndex = 135;
            this.PlayProgressTextTimer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ShowPlayPanel
            // 
            this.ShowPlayPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ShowPlayPanel.BackColor = System.Drawing.Color.Transparent;
            this.ShowPlayPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ShowPlayPanel.Controls.Add(this.ServerPingStatusText);
            this.ShowPlayPanel.Controls.Add(this.LogoutButton);
            this.ShowPlayPanel.Controls.Add(this.PlayButton);
            this.ShowPlayPanel.Font = new System.Drawing.Font("DejaVu Sans", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ShowPlayPanel.ForeColor = System.Drawing.Color.Transparent;
            this.ShowPlayPanel.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.ShowPlayPanel.Location = new System.Drawing.Point(600, 281);
            this.ShowPlayPanel.Name = "ShowPlayPanel";
            this.ShowPlayPanel.Size = new System.Drawing.Size(237, 173);
            this.ShowPlayPanel.TabIndex = 153;
            this.ShowPlayPanel.Visible = false;
            // 
            // InsiderBuildNumberText
            // 
            this.InsiderBuildNumberText.BackColor = System.Drawing.Color.Transparent;
            this.InsiderBuildNumberText.Font = new System.Drawing.Font("DejaVu Sans", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InsiderBuildNumberText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(179)))), ((int)(((byte)(189)))));
            this.InsiderBuildNumberText.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.InsiderBuildNumberText.Location = new System.Drawing.Point(352, 65);
            this.InsiderBuildNumberText.Margin = new System.Windows.Forms.Padding(0);
            this.InsiderBuildNumberText.Name = "InsiderBuildNumberText";
            this.InsiderBuildNumberText.Size = new System.Drawing.Size(208, 14);
            this.InsiderBuildNumberText.TabIndex = 173;
            this.InsiderBuildNumberText.Text = "Insider Build Date: ##-##-##-X";
            this.InsiderBuildNumberText.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // logo
            // 
            this.logo.BackColor = System.Drawing.Color.Transparent;
            this.logo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.logo.InitialImage = null;
            this.logo.Location = new System.Drawing.Point(17, 10);
            this.logo.Name = "logo";
            this.logo.Size = new System.Drawing.Size(215, 71);
            this.logo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.logo.TabIndex = 0;
            this.logo.TabStop = false;
            // 
            // SettingsButton
            // 
            this.SettingsButton.BackColor = System.Drawing.Color.Transparent;
            this.SettingsButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.SettingsButton.InitialImage = null;
            this.SettingsButton.Location = new System.Drawing.Point(806, 15);
            this.SettingsButton.Name = "SettingsButton";
            this.SettingsButton.Size = new System.Drawing.Size(25, 25);
            this.SettingsButton.TabIndex = 21;
            this.SettingsButton.TabStop = false;
            // 
            // CloseBTN
            // 
            this.CloseBTN.BackColor = System.Drawing.Color.Transparent;
            this.CloseBTN.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.CloseBTN.InitialImage = null;
            this.CloseBTN.Location = new System.Drawing.Point(841, 15);
            this.CloseBTN.Name = "CloseBTN";
            this.CloseBTN.Size = new System.Drawing.Size(25, 25);
            this.CloseBTN.TabIndex = 0;
            this.CloseBTN.TabStop = false;
            this.CloseBTN.Click += new System.EventHandler(this.CloseBTN_Click);
            // 
            // ServerInfoPanel
            // 
            this.ServerInfoPanel.BackColor = System.Drawing.Color.Transparent;
            this.ServerInfoPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ServerInfoPanel.Controls.Add(this.HomePageIcon);
            this.ServerInfoPanel.Controls.Add(this.DiscordIcon);
            this.ServerInfoPanel.Controls.Add(this.FacebookIcon);
            this.ServerInfoPanel.Controls.Add(this.TwitterAccountLink);
            this.ServerInfoPanel.Controls.Add(this.TwitterIcon);
            this.ServerInfoPanel.Controls.Add(this.FacebookGroupLink);
            this.ServerInfoPanel.Controls.Add(this.HomePageLink);
            this.ServerInfoPanel.Controls.Add(this.DiscordInviteLink);
            this.ServerInfoPanel.Controls.Add(this.ServerShutDown);
            this.ServerInfoPanel.Controls.Add(this.SceneryGroupText);
            this.ServerInfoPanel.Font = new System.Drawing.Font("DejaVu Sans", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ServerInfoPanel.ForeColor = System.Drawing.Color.Transparent;
            this.ServerInfoPanel.Location = new System.Drawing.Point(27, 305);
            this.ServerInfoPanel.Name = "ServerInfoPanel";
            this.ServerInfoPanel.Size = new System.Drawing.Size(525, 68);
            this.ServerInfoPanel.TabIndex = 172;
            // 
            // HomePageIcon
            // 
            this.HomePageIcon.BackColor = System.Drawing.Color.Transparent;
            this.HomePageIcon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.HomePageIcon.InitialImage = null;
            this.HomePageIcon.Location = new System.Drawing.Point(7, 8);
            this.HomePageIcon.Name = "HomePageIcon";
            this.HomePageIcon.Size = new System.Drawing.Size(25, 25);
            this.HomePageIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.HomePageIcon.TabIndex = 178;
            this.HomePageIcon.TabStop = false;
            // 
            // DiscordIcon
            // 
            this.DiscordIcon.BackColor = System.Drawing.Color.Transparent;
            this.DiscordIcon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.DiscordIcon.InitialImage = null;
            this.DiscordIcon.Location = new System.Drawing.Point(127, 8);
            this.DiscordIcon.Name = "DiscordIcon";
            this.DiscordIcon.Size = new System.Drawing.Size(25, 25);
            this.DiscordIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.DiscordIcon.TabIndex = 177;
            this.DiscordIcon.TabStop = false;
            // 
            // FacebookIcon
            // 
            this.FacebookIcon.BackColor = System.Drawing.Color.Transparent;
            this.FacebookIcon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.FacebookIcon.InitialImage = null;
            this.FacebookIcon.Location = new System.Drawing.Point(263, 8);
            this.FacebookIcon.Name = "FacebookIcon";
            this.FacebookIcon.Size = new System.Drawing.Size(25, 25);
            this.FacebookIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.FacebookIcon.TabIndex = 176;
            this.FacebookIcon.TabStop = false;
            // 
            // TwitterAccountLink
            // 
            this.TwitterAccountLink.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.TwitterAccountLink.BackColor = System.Drawing.Color.Transparent;
            this.TwitterAccountLink.Font = new System.Drawing.Font("DejaVu Sans", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TwitterAccountLink.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(179)))), ((int)(((byte)(189)))));
            this.TwitterAccountLink.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.TwitterAccountLink.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(179)))), ((int)(((byte)(189)))));
            this.TwitterAccountLink.Location = new System.Drawing.Point(437, 12);
            this.TwitterAccountLink.Name = "TwitterAccountLink";
            this.TwitterAccountLink.Size = new System.Drawing.Size(88, 14);
            this.TwitterAccountLink.TabIndex = 174;
            this.TwitterAccountLink.TabStop = true;
            this.TwitterAccountLink.Text = "Twitter Feed";
            this.TwitterAccountLink.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TwitterIcon
            // 
            this.TwitterIcon.BackColor = System.Drawing.Color.Transparent;
            this.TwitterIcon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.TwitterIcon.InitialImage = null;
            this.TwitterIcon.Location = new System.Drawing.Point(407, 8);
            this.TwitterIcon.Name = "TwitterIcon";
            this.TwitterIcon.Size = new System.Drawing.Size(25, 25);
            this.TwitterIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.TwitterIcon.TabIndex = 174;
            this.TwitterIcon.TabStop = false;
            // 
            // FacebookGroupLink
            // 
            this.FacebookGroupLink.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.FacebookGroupLink.BackColor = System.Drawing.Color.Transparent;
            this.FacebookGroupLink.Font = new System.Drawing.Font("DejaVu Sans", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FacebookGroupLink.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(179)))), ((int)(((byte)(189)))));
            this.FacebookGroupLink.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.FacebookGroupLink.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(179)))), ((int)(((byte)(189)))));
            this.FacebookGroupLink.Location = new System.Drawing.Point(292, 12);
            this.FacebookGroupLink.Name = "FacebookGroupLink";
            this.FacebookGroupLink.Size = new System.Drawing.Size(110, 14);
            this.FacebookGroupLink.TabIndex = 173;
            this.FacebookGroupLink.TabStop = true;
            this.FacebookGroupLink.Text = "Facebook Group";
            this.FacebookGroupLink.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // HomePageLink
            // 
            this.HomePageLink.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.HomePageLink.BackColor = System.Drawing.Color.Transparent;
            this.HomePageLink.Font = new System.Drawing.Font("DejaVu Sans", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HomePageLink.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(179)))), ((int)(((byte)(189)))));
            this.HomePageLink.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.HomePageLink.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(179)))), ((int)(((byte)(189)))));
            this.HomePageLink.Location = new System.Drawing.Point(36, 12);
            this.HomePageLink.Name = "HomePageLink";
            this.HomePageLink.Size = new System.Drawing.Size(79, 14);
            this.HomePageLink.TabIndex = 171;
            this.HomePageLink.TabStop = true;
            this.HomePageLink.Text = "Home Page";
            this.HomePageLink.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // DiscordInviteLink
            // 
            this.DiscordInviteLink.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.DiscordInviteLink.BackColor = System.Drawing.Color.Transparent;
            this.DiscordInviteLink.Font = new System.Drawing.Font("DejaVu Sans", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DiscordInviteLink.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(179)))), ((int)(((byte)(189)))));
            this.DiscordInviteLink.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.DiscordInviteLink.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(179)))), ((int)(((byte)(189)))));
            this.DiscordInviteLink.Location = new System.Drawing.Point(154, 12);
            this.DiscordInviteLink.Name = "DiscordInviteLink";
            this.DiscordInviteLink.Size = new System.Drawing.Size(95, 14);
            this.DiscordInviteLink.TabIndex = 172;
            this.DiscordInviteLink.TabStop = true;
            this.DiscordInviteLink.Text = "Discord Invite";
            this.DiscordInviteLink.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ServerShutDown
            // 
            this.ServerShutDown.BackColor = System.Drawing.Color.Transparent;
            this.ServerShutDown.Font = new System.Drawing.Font("DejaVu Sans", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ServerShutDown.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(179)))), ((int)(((byte)(189)))));
            this.ServerShutDown.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ServerShutDown.Location = new System.Drawing.Point(290, 42);
            this.ServerShutDown.Name = "ServerShutDown";
            this.ServerShutDown.Size = new System.Drawing.Size(220, 15);
            this.ServerShutDown.TabIndex = 169;
            this.ServerShutDown.Text = "Restart Timer: 00.00 Hours";
            this.ServerShutDown.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SceneryGroupText
            // 
            this.SceneryGroupText.BackColor = System.Drawing.Color.Transparent;
            this.SceneryGroupText.Font = new System.Drawing.Font("DejaVu Sans", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SceneryGroupText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(179)))), ((int)(((byte)(189)))));
            this.SceneryGroupText.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.SceneryGroupText.Location = new System.Drawing.Point(64, 43);
            this.SceneryGroupText.Name = "SceneryGroupText";
            this.SceneryGroupText.Size = new System.Drawing.Size(220, 15);
            this.SceneryGroupText.TabIndex = 179;
            this.SceneryGroupText.Text = "Scenery: SetByServInfo";
            this.SceneryGroupText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LauncherIconStatus
            // 
            this.LauncherIconStatus.BackColor = System.Drawing.Color.Transparent;
            this.LauncherIconStatus.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.LauncherIconStatus.InitialImage = null;
            this.LauncherIconStatus.Location = new System.Drawing.Point(25, 456);
            this.LauncherIconStatus.Name = "LauncherIconStatus";
            this.LauncherIconStatus.Size = new System.Drawing.Size(25, 25);
            this.LauncherIconStatus.TabIndex = 79;
            this.LauncherIconStatus.TabStop = false;
            // 
            // APIStatusIcon
            // 
            this.APIStatusIcon.BackColor = System.Drawing.Color.Transparent;
            this.APIStatusIcon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.APIStatusIcon.InitialImage = null;
            this.APIStatusIcon.Location = new System.Drawing.Point(397, 456);
            this.APIStatusIcon.Name = "APIStatusIcon";
            this.APIStatusIcon.Size = new System.Drawing.Size(25, 25);
            this.APIStatusIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.APIStatusIcon.TabIndex = 113;
            this.APIStatusIcon.TabStop = false;
            // 
            // ServerStatusIcon
            // 
            this.ServerStatusIcon.BackColor = System.Drawing.Color.Transparent;
            this.ServerStatusIcon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ServerStatusIcon.InitialImage = null;
            this.ServerStatusIcon.Location = new System.Drawing.Point(211, 456);
            this.ServerStatusIcon.Name = "ServerStatusIcon";
            this.ServerStatusIcon.Size = new System.Drawing.Size(25, 25);
            this.ServerStatusIcon.TabIndex = 6;
            this.ServerStatusIcon.TabStop = false;
            // 
            // MainEmailBorder
            // 
            this.MainEmailBorder.BackColor = System.Drawing.Color.Transparent;
            this.MainEmailBorder.ErrorImage = null;
            this.MainEmailBorder.InitialImage = null;
            this.MainEmailBorder.Location = new System.Drawing.Point(606, 161);
            this.MainEmailBorder.Name = "MainEmailBorder";
            this.MainEmailBorder.Size = new System.Drawing.Size(231, 37);
            this.MainEmailBorder.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.MainEmailBorder.TabIndex = 144;
            this.MainEmailBorder.TabStop = false;
            // 
            // MainPasswordBorder
            // 
            this.MainPasswordBorder.BackColor = System.Drawing.Color.Transparent;
            this.MainPasswordBorder.ErrorImage = null;
            this.MainPasswordBorder.InitialImage = null;
            this.MainPasswordBorder.Location = new System.Drawing.Point(606, 210);
            this.MainPasswordBorder.Name = "MainPasswordBorder";
            this.MainPasswordBorder.Size = new System.Drawing.Size(231, 37);
            this.MainPasswordBorder.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.MainPasswordBorder.TabIndex = 145;
            this.MainPasswordBorder.TabStop = false;
            // 
            // ProgressBarOutline
            // 
            this.ProgressBarOutline.BackColor = System.Drawing.Color.Transparent;
            this.ProgressBarOutline.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ProgressBarOutline.ErrorImage = null;
            this.ProgressBarOutline.InitialImage = null;
            this.ProgressBarOutline.Location = new System.Drawing.Point(25, 405);
            this.ProgressBarOutline.Name = "ProgressBarOutline";
            this.ProgressBarOutline.Size = new System.Drawing.Size(529, 42);
            this.ProgressBarOutline.TabIndex = 174;
            this.ProgressBarOutline.TabStop = false;
            // 
            // VerticalBanner
            // 
            this.VerticalBanner.BackColor = System.Drawing.Color.Transparent;
            this.VerticalBanner.InitialImage = null;
            this.VerticalBanner.Location = new System.Drawing.Point(28, 81);
            this.VerticalBanner.Name = "VerticalBanner";
            this.VerticalBanner.Size = new System.Drawing.Size(523, 223);
            this.VerticalBanner.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.VerticalBanner.TabIndex = 22;
            this.VerticalBanner.TabStop = false;
            // 
            // ExtractingProgress
            // 
            this.ExtractingProgress.BackColor = System.Drawing.Color.Transparent;
            this.ExtractingProgress.BackgroundColor = System.Drawing.Color.Black;
            this.ExtractingProgress.Border = false;
            this.ExtractingProgress.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(179)))), ((int)(((byte)(189)))));
            this.ExtractingProgress.Font = new System.Drawing.Font("DejaVu Sans", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ExtractingProgress.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(179)))), ((int)(((byte)(189)))));
            this.ExtractingProgress.GradiantColor = System.Drawing.Color.Transparent;
            this.ExtractingProgress.Image = global::GameLauncher.Properties.Resources.progress_success;
            this.ExtractingProgress.Location = new System.Drawing.Point(30, 430);
            this.ExtractingProgress.Name = "ExtractingProgress";
            this.ExtractingProgress.ProgressColor = System.Drawing.Color.Green;
            this.ExtractingProgress.RoundedCorners = false;
            this.ExtractingProgress.Size = new System.Drawing.Size(519, 13);
            this.ExtractingProgress.Text = "downloadProgress";
            // 
            // PlayProgress
            // 
            this.PlayProgress.BackColor = System.Drawing.Color.Transparent;
            this.PlayProgress.BackgroundColor = System.Drawing.Color.Black;
            this.PlayProgress.Border = false;
            this.PlayProgress.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(179)))), ((int)(((byte)(189)))));
            this.PlayProgress.Cursor = System.Windows.Forms.Cursors.Default;
            this.PlayProgress.Font = new System.Drawing.Font("DejaVu Sans", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PlayProgress.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(179)))), ((int)(((byte)(189)))));
            this.PlayProgress.GradiantColor = System.Drawing.Color.Transparent;
            this.PlayProgress.Image = global::GameLauncher.Properties.Resources.progress_preload;
            this.PlayProgress.Location = new System.Drawing.Point(30, 430);
            this.PlayProgress.Name = "PlayProgress";
            this.PlayProgress.ProgressColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(84)))), ((int)(((byte)(92)))));
            this.PlayProgress.RoundedCorners = false;
            this.PlayProgress.Size = new System.Drawing.Size(519, 13);
            this.PlayProgress.Text = "downloadProgress";
            // 
            // Notification
            // 
            this.Notification.Text = "notifyIcon1";
            this.Notification.Visible = true;
            // 
            // MainScreen
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Black;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(891, 529);
            this.Controls.Add(this.InsiderBuildNumberText);
            this.Controls.Add(this.CurrentWindowInfo);
            this.Controls.Add(this.logo);
            this.Controls.Add(this.SettingsButton);
            this.Controls.Add(this.ShowPlayPanel);
            this.Controls.Add(this.CloseBTN);
            this.Controls.Add(this.SelectServerBtn);
            this.Controls.Add(this.ServerInfoPanel);
            this.Controls.Add(this.translatedBy);
            this.Controls.Add(this.ServerPick);
            this.Controls.Add(this.AddServer);
            this.Controls.Add(this.LauncherIconStatus);
            this.Controls.Add(this.APIStatusIcon);
            this.Controls.Add(this.ServerStatusIcon);
            this.Controls.Add(this.LauncherStatusText);
            this.Controls.Add(this.LauncherStatusDesc);
            this.Controls.Add(this.ServerStatusText);
            this.Controls.Add(this.ServerStatusDesc);
            this.Controls.Add(this.APIStatusText);
            this.Controls.Add(this.APIStatusDesc);
            this.Controls.Add(this.MainEmail);
            this.Controls.Add(this.MainEmailBorder);
            this.Controls.Add(this.MainPassword);
            this.Controls.Add(this.MainPasswordBorder);
            this.Controls.Add(this.RememberMe);
            this.Controls.Add(this.ForgotPassword);
            this.Controls.Add(this.LoginButton);
            this.Controls.Add(this.RegisterText);
            this.Controls.Add(this.PlayProgressTextTimer);
            this.Controls.Add(this.PlayProgressText);
            this.Controls.Add(this.ExtractingProgress);
            this.Controls.Add(this.PlayProgress);
            this.Controls.Add(this.ProgressBarOutline);
            this.Controls.Add(this.VerticalBanner);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("DejaVu Sans", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainScreen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GameLauncher";
            this.TransparencyKey = System.Drawing.Color.Fuchsia;
            this.ShowPlayPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.logo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SettingsButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CloseBTN)).EndInit();
            this.ServerInfoPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.HomePageIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DiscordIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FacebookIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TwitterIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LauncherIconStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.APIStatusIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ServerStatusIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MainEmailBorder)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MainPasswordBorder)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ProgressBarOutline)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.VerticalBanner)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer Timeout;
        private System.Windows.Forms.PictureBox logo;
        private System.Windows.Forms.PictureBox CloseBTN;
        private System.Windows.Forms.Button SelectServerBtn;
        private System.Windows.Forms.Label translatedBy;
        private System.Windows.Forms.PictureBox SettingsButton;
        private System.Windows.Forms.ComboBox ServerPick;
        private System.Windows.Forms.Button AddServer;
        private System.Windows.Forms.PictureBox VerticalBanner;
        internal System.Windows.Forms.Label PlayProgressText;
        private GameLauncher.App.Classes.LauncherCore.Visuals.ProgressBarEx ExtractingProgress;
        private GameLauncher.App.Classes.LauncherCore.Visuals.ProgressBarEx PlayProgress;
        private System.Windows.Forms.PictureBox LauncherIconStatus;
        private System.Windows.Forms.Label LauncherStatusText;
        private System.Windows.Forms.Label LauncherStatusDesc;
        private System.Windows.Forms.PictureBox ServerStatusIcon;
        private System.Windows.Forms.Label ServerStatusText;
        private System.Windows.Forms.Label ServerStatusDesc;
        private System.Windows.Forms.PictureBox APIStatusIcon;
        private System.Windows.Forms.Label APIStatusText;
        private System.Windows.Forms.Label APIStatusDesc;
        private System.Windows.Forms.Label CurrentWindowInfo;
        private System.Windows.Forms.TextBox MainEmail;
        private System.Windows.Forms.PictureBox MainEmailBorder;
        private System.Windows.Forms.TextBox MainPassword;
        private System.Windows.Forms.PictureBox MainPasswordBorder;
        private System.Windows.Forms.CheckBox RememberMe;
        private System.Windows.Forms.LinkLabel ForgotPassword;
        private System.Windows.Forms.Button LoginButton;
        private System.Windows.Forms.Button RegisterText;
        private System.Windows.Forms.Label ServerPingStatusText;
        private System.Windows.Forms.Button LogoutButton;
        private System.Windows.Forms.Button PlayButton;
        internal System.Windows.Forms.Label PlayProgressTextTimer;
        private System.Windows.Forms.Panel ShowPlayPanel;
        private System.Windows.Forms.LinkLabel DiscordInviteLink;
        private System.Windows.Forms.Label ServerShutDown;
        private System.Windows.Forms.Panel ServerInfoPanel;
        private System.Windows.Forms.LinkLabel TwitterAccountLink;
        private System.Windows.Forms.PictureBox TwitterIcon;
        private System.Windows.Forms.LinkLabel FacebookGroupLink;
        private System.Windows.Forms.LinkLabel HomePageLink;
        private System.Windows.Forms.PictureBox HomePageIcon;
        private System.Windows.Forms.PictureBox DiscordIcon;
        private System.Windows.Forms.PictureBox FacebookIcon;
        private System.Windows.Forms.Label SceneryGroupText;
        private System.Windows.Forms.Label InsiderBuildNumberText;
        private System.Windows.Forms.PictureBox ProgressBarOutline;
        private System.Windows.Forms.NotifyIcon Notification;
    }
}
