using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Security.Cryptography;
using System.IO;
using System.Xml;
using System.Diagnostics;
using System.Threading;
using System.Reflection;
using System.Net.NetworkInformation;
using GameLauncher.Resources;
using GameLauncher.App.Classes;
using Newtonsoft.Json;
using SoapBox.JsonScheme;
using GameLauncher.App.Classes.Events;
using GameLauncherReborn;
using Microsoft.Win32;
using GameLauncher.App;
using GameLauncher.HashPassword;
using System.Linq;
using System.Text.RegularExpressions;
using GameLauncher.App.Classes.Logger;
using System.IO.Compression;
using GameLauncher.App.Classes.Auth;
using DiscordRPC;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.Threading.Tasks;
using GameLauncher.App.Classes.ModNetReloaded;
using GameLauncher.App.Classes.HashPassword;
using GameLauncher.App.Classes.RPC;
using GameLauncher.App.Classes.GPU;
using GameLauncher.Properties;
using GameLauncher.App.Classes.SystemPlatform.Windows;
using System.Management.Automation;

namespace GameLauncher
{
    public sealed partial class MainScreen : Form {
        private Point _mouseDownPoint = Point.Empty;
        private bool _loginEnabled;
        private bool _serverEnabled;
        private bool _builtinserver;
        private bool _useSavedPassword;
        private bool _skipServerTrigger;
        private bool _ticketRequired;
        private bool _windowMoved;
        private bool _playenabled;
        private bool _loggedIn;
        private bool _restartRequired;
        private bool _allowRegistration;
        private bool _isDownloading = true;
        private bool _modernAuthSupport = false;
        private bool _gameKilledBySpeedBugCheck = false;
        private bool _disableLogout = false;

        public static String getTempNa = Path.GetTempFileName();

        //private bool _disableChecks;
        private bool _disableProxy;
        private bool _disableDiscordRPC;

        private int _lastSelectedServerId;
        private int _nfswPid;
        private Thread _nfswstarted;


        private DateTime _downloadStartTime;
        private readonly Downloader _downloader;

        private string _serverWebsiteLink = "";
        private string _serverFacebookLink = "";
        private string _serverDiscordLink = "";
        private string _serverTwitterLink = "";
        private string _loginWelcomeTime = "";
        private string _loginToken = "";
        private string _userId = "";
        private string _serverIp = "";
        private string _langInfo;
        private string _newLauncherPath;
        private string _newGameFilesPath;
        private readonly float _dpiDefaultScale = 96f;

        private readonly RichPresence _presence = new RichPresence();

        //private readonly Pen _colorOffline = new Pen(Color.FromArgb(128, 0, 0));
        //private readonly Pen _colorOnline = new Pen(Color.FromArgb(0, 128, 0));
        //private readonly Pen _colorLoading = new Pen(Color.FromArgb(0, 0, 0));
        
        private readonly IniFile _settingFile = new IniFile("Settings.ini");
        private readonly string _userSettings = Environment.GetEnvironmentVariable("AppData") + "/Need for Speed World/Settings/UserSettings.xml";
        private string _presenceImageKey;
        private string _NFSW_Installation_Source;
        private string _realServername;
        private string _realServernameBanner;
        private string _OS;

        public static String ModNetFileNameInUse = String.Empty;
        Queue<Uri> modFilesDownloadUrls = new Queue<Uri>();
        bool isDownloadingModNetFiles = false;
        int CurrentModFileCount = 0;
        int TotalModFileCount = 0;

        //private Point _startPoint = new Point(28, 308);
        //private Point _endPoint = new Point(549, 308);

        ServerInfo _serverInfo = null;
        GetServerInformation json = new GetServerInformation();

        public static DiscordRpcClient discordRpcClient;

        List<ServerInfo> finalItems = new List<ServerInfo>();
        List<CDNInfo> finalCDNItems = new List<CDNInfo>();
        Dictionary<string, int> serverStatusDictionary = new Dictionary<string, int>();

        String filename_pack = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "GameFiles.sbrwpack");

        //UltimateLauncherFunction: SelectServer
        private static ServerInfo _ServerList;
        public static ServerInfo ServerName {
            get { return _ServerList; }
            set { _ServerList = value; }
        }

        //UltimateLauncherFunction: SelectCDN
        private static CDNInfo _CDNList;
        public static CDNInfo CDNName
        {
            get { return _CDNList; }
            set { _CDNList = value; }
        }

        private static Random random = new Random();

        public static string RandomString(int length) {
			const string chars = "qwertyuiopasdfghjklzxcvbnm1234567890_";
			return new string(Enumerable.Repeat(chars, length)
			  .Select(s => s[random.Next(s.Length)]).ToArray());
		}

        private void MoveWindow_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Y <= 90) _mouseDownPoint = new Point(e.X, e.Y);
        }

        private void MoveWindow_MouseUp(object sender, MouseEventArgs e)
        {
            _mouseDownPoint = Point.Empty;
            Opacity = 1;
        }

        private void MoveWindow_MouseMove(object sender, MouseEventArgs e)
        {
            if (_mouseDownPoint.IsEmpty) { return; }
            var f = this as Form;
            f.Location = new Point(f.Location.X + (e.X - _mouseDownPoint.X), f.Location.Y + (e.Y - _mouseDownPoint.Y));
            _windowMoved = true;
            Opacity = 0.9;
        }

        public MainScreen() {

            ParseUri uri = new ParseUri(Environment.GetCommandLineArgs());

            if (uri.IsDiscordPresent()) {
                Notification.Visible = true;
                Notification.BalloonTipIcon = ToolTipIcon.Info;
                Notification.BalloonTipTitle = "GameLauncherReborn";
                Notification.BalloonTipText = "Discord features are not yet completed.";
                Notification.ShowBalloonTip(5000);
                Notification.Dispose();
            }

            Log.Debug("CORE: Entered mainScreen");

            Random rnd;
            rnd = new Random(Environment.TickCount);

            discordRpcClient = new DiscordRpcClient(Self.DiscordRPCID);

            discordRpcClient.OnReady += (sender, e) => {
                Log.Debug("DISCORD: Discord ready. Detected user: " + e.User.Username + ". Discord version: " + e.Version);
                Self.discordid = e.User.ID.ToString();
            };

            discordRpcClient.OnError += (sender, e) => {
                Log.Error($"DISCORD: Discord Error\n{e.Message}");
            };

            discordRpcClient.Initialize();

            
            Log.Debug("CORE: Setting SSL Protocol");
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

            if (DetectLinux.LinuxDetected()) {
                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            }

            Log.Debug("LAUNCHER: Detecting OS");
            if (DetectLinux.LinuxDetected()) {
                _OS = DetectLinux.Distro();
                Log.Debug("SYSTEM: Detected OS: " + _OS);
            } else {
                _OS = (string)Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\Windows NT\\CurrentVersion").GetValue("productName");
                Log.Debug("SYSTEM: Detected OS: " + _OS);
                if (Environment.Is64BitOperatingSystem == true) {
                    Log.Debug("SYSTEM: OS Type: 64 Bit");
                }
                Log.Debug("SYSTEM: OS Details: " + Environment.OSVersion);
                Log.Debug("SYSTEM: Video Card: " + GPUHelper.CardName());
                Log.Debug("SYSTEM: Driver Version: " + GPUHelper.DriverVersion());
            }

            _downloader = new Downloader(this, 3, 2, 16) {
                ProgressUpdated = new ProgressUpdated(OnDownloadProgress),
                DownloadFinished = new DownloadFinished(DownloadTracksFiles),
                DownloadFailed = new DownloadFailed(OnDownloadFailed),
                ShowMessage = new ShowMessage(OnShowMessage),
				ShowExtract = new ShowExtract(OnShowExtract)
            };

            Log.Debug("CORE: InitializeComponent");
            InitializeComponent();

            Log.Debug("CORE: Applying Fonts");
            ApplyEmbeddedFonts();

            _disableProxy = (_settingFile.KeyExists("DisableProxy") && _settingFile.Read("DisableProxy") == "1") ? true : false;
            _disableDiscordRPC = (_settingFile.KeyExists("DisableRPC") && _settingFile.Read("DisableRPC") == "1") ? true : false;
            Log.Debug("PROXY: Checking if Proxy Is Disabled from User Settings! It's value is " + _disableProxy);

            Self.CenterScreen(this);

            Log.Debug("CORE: Disabling MaximizeBox");
            MaximizeBox = false;
            Log.Debug("CORE: Setting Styles");
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer | ControlStyles.OptimizedDoubleBuffer, true);

            Log.Debug("CORE: Applying EventHandlers");
            closebtn.MouseEnter += new EventHandler(Closebtn_MouseEnter);
            closebtn.MouseLeave += new EventHandler(Closebtn_MouseLeave);
            closebtn.Click += new EventHandler(Closebtn_Click);

            settingsButton.MouseEnter += new EventHandler(SettingsButton_MouseEnter);
            settingsButton.MouseLeave += new EventHandler(SettingsButton_MouseLeave);
            settingsButton.Click += new EventHandler(SettingsButton_Click);

            loginButton.MouseEnter += new EventHandler(LoginButton_MouseEnter);
            loginButton.MouseLeave += new EventHandler(LoginButton_MouseLeave);
            loginButton.MouseUp += new MouseEventHandler(LoginButton_MouseUp);
            loginButton.MouseDown += new MouseEventHandler(LoginButton_MouseDown);
            loginButton.Click += new EventHandler(LoginButton_Click);

            registerButton.MouseEnter += Greenbutton_hover_MouseEnter;
            registerButton.MouseLeave += Greenbutton_MouseLeave;
            registerButton.MouseUp += Greenbutton_hover_MouseUp;
            registerButton.MouseDown += Greenbutton_click_MouseDown;
            registerButton.Click += RegisterButton_Click;

            registerCancel.MouseEnter += new EventHandler(Graybutton_hover_MouseEnter);
            registerCancel.MouseLeave += new EventHandler(Graybutton_MouseLeave);
            registerCancel.MouseUp += new MouseEventHandler(Graybutton_hover_MouseUp);
            registerCancel.MouseDown += new MouseEventHandler(Graybutton_click_MouseDown);
            registerCancel.Click += new EventHandler(RegisterCancel_Click);

            logoutButton.MouseEnter += new EventHandler(Graybutton_hover_MouseEnter);
            logoutButton.MouseLeave += new EventHandler(Graybutton_MouseLeave);
            logoutButton.MouseUp += new MouseEventHandler(Graybutton_hover_MouseUp);
            logoutButton.MouseDown += new MouseEventHandler(Graybutton_click_MouseDown);
            logoutButton.Click += new EventHandler(LogoutButton_Click);

            settingsSave.MouseEnter += new EventHandler(Greenbutton_hover_MouseEnter);
            settingsSave.MouseLeave += new EventHandler(Greenbutton_MouseLeave);
            settingsSave.MouseUp += new MouseEventHandler(Greenbutton_hover_MouseUp);
            settingsSave.MouseDown += new MouseEventHandler(Greenbutton_click_MouseDown);
            settingsSave.Click += new EventHandler(SettingsSave_Click);

            settingsCancel.MouseEnter += new EventHandler(Graybutton_hover_MouseEnter);
            settingsCancel.MouseLeave += new EventHandler(Graybutton_MouseLeave);
            settingsCancel.MouseUp += new MouseEventHandler(Graybutton_hover_MouseUp);
            settingsCancel.MouseDown += new MouseEventHandler(Graybutton_click_MouseDown);
            settingsCancel.Click += new EventHandler(SettingsCancel_Click);

            settingsLauncherPathCurrent.Click += new EventHandler(SettingsLauncherPathCurrent_Click);
            settingsGameFiles.Click += new EventHandler(SettingsGameFiles_Click);
            settingsGameFilesCurrent.Click += new EventHandler(SettingsGameFilesCurrent_Click);

            addServer.Click += new EventHandler(AddServer_Click);
            settingsLauncherVersion.Click += new EventHandler(OpenDebugWindow);

            email.KeyUp += new KeyEventHandler(Loginbuttonenabler);
            email.KeyDown += new KeyEventHandler(LoginEnter);
            password.KeyUp += new KeyEventHandler(Loginbuttonenabler);
            password.KeyDown += new KeyEventHandler(LoginEnter);

            serverPick.SelectedIndexChanged += new EventHandler(ServerPick_SelectedIndexChanged);
            serverPick.DrawItem += new DrawItemEventHandler(ComboBox1_DrawItem);

            forgotPassword.LinkClicked += new LinkLabelLinkClickedEventHandler(ForgotPassword_LinkClicked);

            MouseMove += new MouseEventHandler(MoveWindow_MouseMove);
            MouseUp += new MouseEventHandler(MoveWindow_MouseUp);
            MouseDown += new MouseEventHandler(MoveWindow_MouseDown);

            logo.MouseEnter += new EventHandler(Logo_MouseEnter);
            logo.MouseLeave += new EventHandler(Logo_MouseLeave);
            logo.MouseMove += new MouseEventHandler(MoveWindow_MouseMove);
            logo.MouseUp += new MouseEventHandler(MoveWindow_MouseUp);
            logo.MouseDown += new MouseEventHandler(MoveWindow_MouseDown);

            playButton.MouseEnter += new EventHandler(PlayButton_MouseEnter);
            playButton.MouseLeave += new EventHandler(PlayButton_MouseLeave);
            playButton.MouseUp += new MouseEventHandler(PlayButton_MouseUp);
            playButton.MouseDown += new MouseEventHandler(PlayButton_MouseDown);
            playButton.Click += new EventHandler(PlayButton_Click);

            registerText.MouseEnter += new EventHandler(Greenbutton_hover_MouseEnter);
            registerText.MouseLeave += new EventHandler(Greenbutton_MouseLeave);
            registerText.MouseUp += new MouseEventHandler(Greenbutton_hover_MouseUp);
            registerText.MouseDown += new MouseEventHandler(Greenbutton_click_MouseDown);
            registerText.Click += new EventHandler(RegisterText_LinkClicked);

            this.Load += new EventHandler(MainScreen_Load);

            this.Shown += (x,y) => {
                if(UriScheme.ForceGame == true) {
                    PlayButton_Click(x, y);
                }

                new Thread(() => {
                    discordRpcClient.Invoke();

                    //Let's fetch all servers
                    List<ServerInfo> allServs = finalItems.FindAll(i => string.Equals(i.IsSpecial, false));
                    allServs.ForEach(delegate(ServerInfo server) {
                        try { 
                            WebClient pingServer = new WebClient();
                            pingServer.DownloadString(server.IpAddress + "/GetServerInformation");

                            if(!serverStatusDictionary.ContainsKey(server.Id))
                                serverStatusDictionary.Add(server.Id, 1);
                        } catch {
                            if (!serverStatusDictionary.ContainsKey(server.Id))
                                serverStatusDictionary.Add(server.Id, 0);
                        }
                    });
                }).Start();
            };

            if (WindowsProductVersion.GetWindowsNumber() >= 10.0 && (_settingFile.Read("WindowsDefender") == "Not Excluded"))
            {
                Log.Debug("WINDOWS DEFENDER: Windows 10 Detected! Running Exclusions for Core Folders");
                WindowsDefenderFirstRun();
            }
            else if (WindowsProductVersion.GetWindowsNumber() >= 10.0 && _settingFile.KeyExists("WindowsDefender"))
            {
                Log.Debug("WINDOWS DEFENDER: Found 'WindowsDefender' key! Its value is " + _settingFile.Read("WindowsDefender"));
            }

            Log.Debug("CORE: Checking permissions");
            if (!Self.HasWriteAccessToFolder(Directory.GetCurrentDirectory())) {
                Log.Error("CORE: Check Permission Failed.");
                MessageBox.Show(null, "Failed to write the test file. Make sure you're running the launcher with administrative privileges.", "GameLauncher", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            Log.Debug("LAUNCHER: Checking InstallationDirectory: " + _settingFile.Read("InstallationDirectory"));
            if (string.IsNullOrEmpty(_settingFile.Read("InstallationDirectory"))) {
                Log.Debug("LAUNCHER: First run!");

                try { 
                    Form welcome = new WelcomeScreen();
                    DialogResult welcomereply = welcome.ShowDialog();

                    if(welcomereply != DialogResult.OK) {
                        Process.GetProcessById(Process.GetCurrentProcess().Id).Kill();
                    } else {
                        _settingFile.Write("CDN", CDN.CDNUrl);
                        _NFSW_Installation_Source = CDN.CDNUrl;
                    }
                } catch {
                    _settingFile.Write("CDN", "http://cdn.worldunited.gg/gamefiles/packed/");
                    _NFSW_Installation_Source = "http://cdn.worldunited.gg/gamefiles/packed/";
                }

                var fbd = new CommonOpenFileDialog {
                    EnsurePathExists = true,
                    EnsureFileExists = false,
                    AllowNonFileSystemItems = false,
                    Title = "Select the location to Find or Download NFS:W",
                    IsFolderPicker = true
                };

                if (fbd.ShowDialog() == CommonFileDialogResult.Ok) {
                    if (!Self.HasWriteAccessToFolder(fbd.FileName)) {
                        Log.Error("LAUNCHER: Not enough permissions. Exiting.");
                        MessageBox.Show(null, "You don't have enough permission to select this path as installation folder. Please select another directory.", "GameLauncher", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Environment.Exit(Environment.ExitCode);
                    }

                    if (fbd.DefaultFileName == Environment.CurrentDirectory) {
                        Directory.CreateDirectory("GameFiles");
                        Log.Debug("LAUNCHER: Installing NFSW in same directory where the launcher resides is disadvised.");
                        MessageBox.Show(null, string.Format("Installing NFSW in same directory where the launcher resides is disadvised. Instead, we will install it on {0}.", Environment.CurrentDirectory + "\\GameFiles"), "GameLauncher", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        _settingFile.Write("InstallationDirectory", Environment.CurrentDirectory + "\\GameFiles");
                    } else {
                        Log.Debug("LAUNCHER: Directory Set: " + fbd.FileName);
                        _settingFile.Write("InstallationDirectory", fbd.FileName);
                    }
                } else {
                    Log.Debug("LAUNCHER: Exiting");
                    Environment.Exit(Environment.ExitCode);
                }
                fbd.Dispose();
            }

            if (!DetectLinux.LinuxDetected()) {
                Log.Debug("CORE: Setting cursor.");
                string temporaryFile = Path.GetTempFileName();
                File.WriteAllBytes(temporaryFile, ExtractResource.AsByte("GameLauncher.SoapBoxModules.cursor.ani"));
                Cursor mycursor = new Cursor(Cursor.Current.Handle);
                IntPtr colorcursorhandle = User32.LoadCursorFromFile(temporaryFile);
                mycursor.GetType().InvokeMember("handle", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetField, null, mycursor, new object[] { colorcursorhandle });
                Cursor = mycursor;
                File.Delete(temporaryFile);
            }

            Log.Debug("CORE: Doing magic with imageServerName");
            var pos = PointToScreen(imageServerName.Location);
            pos = verticalBanner.PointToClient(pos);
            imageServerName.Parent = verticalBanner;
            imageServerName.Location = pos;
            imageServerName.BackColor = Color.Transparent;

            //Log.Debug("CORE: Setting ServerStatusBar");
            //ServerStatusBar(_colorLoading, _startPoint, _endPoint);

            Log.Debug("CORE: Loading ModManager Cache");
            ModManager.LoadModCache();
        }

        private void ComboBox1_DrawItem(object sender, DrawItemEventArgs e) {
            var font = (sender as ComboBox).Font;
            Brush backgroundColor;
            Brush textColor;

            var serverListText = "";
            int onlineStatus = 2; //0 = offline | 1 = online | 2 = checking

            if (sender is ComboBox cb) {
                if (cb.Items[e.Index] is ServerInfo si) {
                    serverListText = si.Name;
                    onlineStatus = serverStatusDictionary.ContainsKey(si.Id) ? serverStatusDictionary[si.Id] : 2;
                }
            }

            if (serverListText.StartsWith("<GROUP>")) {
                font = new Font(font, FontStyle.Bold);
                e.Graphics.FillRectangle(Brushes.White, e.Bounds);
                e.Graphics.DrawString(serverListText.Replace("<GROUP>", string.Empty), font, Brushes.Black, e.Bounds);
            } else {
                font = new Font(font, FontStyle.Regular);
                if ((e.State & DrawItemState.Selected) == DrawItemState.Selected && e.State != DrawItemState.ComboBoxEdit) {
                    backgroundColor = SystemBrushes.Highlight;
                    textColor = SystemBrushes.HighlightText;
                } else {
                    if(onlineStatus == 2) {
                        //CHECKING
                        backgroundColor = Brushes.Khaki;
                    } else if(onlineStatus == 1) {
                        //ONLINE
                        backgroundColor = Brushes.PaleGreen;
                    } else {
                        //OFFLINE
                        backgroundColor = Brushes.LightCoral;
                    }

                    textColor = Brushes.Black;
                }

                e.Graphics.FillRectangle(backgroundColor, e.Bounds);
                e.Graphics.DrawString("    " + serverListText, font, textColor, e.Bounds);
            }
        }

        private void MainScreen_Load(object sender, EventArgs e) {
            Log.Debug("CORE: Entering mainScreen_Load");

            Log.Debug("LAUNCHER: Updating server list");
            ServerListUpdater.UpdateList();

            Log.Debug("CORE: Setting WindowName");
            Text = "GameLauncherReborn v" + Application.ProductVersion;

            Log.Debug("CORE: Checking window location");
            if (Location.X >= Screen.PrimaryScreen.Bounds.Width || Location.Y >= Screen.PrimaryScreen.Bounds.Height || Location.X <= 0 || Location.Y <= 0) {
                Log.Debug("CORE: Window location restored to centerScreen");

                Self.CenterScreen(this);
                _windowMoved = true;
            }

            _NFSW_Installation_Source = !string.IsNullOrEmpty(_settingFile.Read("CDN")) ? _settingFile.Read("CDN") : "http://cdn.worldunited.gg/gamefiles/packed/";
            Log.Debug("LAUNCHER: NFSW Download Source is now: " + _NFSW_Installation_Source);

            Log.Debug("CORE: Applyinng ContextMenu");
            translatedBy.Text = "";
            ContextMenu = new ContextMenu();

            ContextMenu.MenuItems.Add(new MenuItem("Donate", (b,n) => { Process.Start("https://paypal.me/metonator95"); }));
            ContextMenu.MenuItems.Add("-");
            ContextMenu.MenuItems.Add(new MenuItem("Settings", SettingsButton_Click));
            ContextMenu.MenuItems.Add(new MenuItem("Add Server", AddServer_Click));
            ContextMenu.MenuItems.Add("-");
            ContextMenu.MenuItems.Add(new MenuItem("Close launcher", Closebtn_Click));

            Notification.ContextMenu = ContextMenu;
            Notification.Icon = new Icon(Icon, Icon.Width, Icon.Height);
            Notification.Text = "GameLauncher";
            Notification.Visible = true;

            ContextMenu = null;

            email.Text = _settingFile.Read("AccountEmail");
            password.Text = Properties.Settings.Default.PasswordDecoded;
            if (!string.IsNullOrEmpty(_settingFile.Read("AccountEmail")) && !string.IsNullOrEmpty(_settingFile.Read("Password"))) {
                Log.Debug("LAUNCHER: Restoring last saved email and password");
                rememberMe.Checked = true;
            }

            //DavidCarbonGaming
            CDNListUpdater.UpdateCDNList();

            settingsCDNPick.DisplayMember = "Name";

            Log.Debug("LAUNCHER: Setting cdn list");
            finalCDNItems = CDNListUpdater.GetCDNList();
            settingsCDNPick.DataSource = finalCDNItems;
            Log.Debug("Final List of CDN " + finalCDNItems);

            //DavidCarbonGaming
            Log.Debug("CDNLIST: Checking...");
            Log.Debug("CDNLIST: Setting first server in list");
            Log.Debug("CDNLIST: Checking if server is set on INI File");
            try
            {
                if (string.IsNullOrEmpty(_settingFile.Read("CDN")))
                {
                    Log.Debug("CDNLIST: Failed to find anything... assuming " + ((CDNInfo)settingsCDNPick.SelectedItem).Url);
                    _settingFile.Write("CDN", ((CDNInfo)settingsCDNPick.SelectedItem).Url);
                }
            }
            catch
            {
                Log.Debug("CDNLIST: Failed to write anything...");
                _settingFile.Write("CDN", "");
            }

            Log.Debug("CDNLIST: Re-Checking if server is set on INI File");
            if (_settingFile.KeyExists("CDN"))
            {
                Log.Debug("CDNLIST: Found something!");

                Log.Debug("CDNLIST: Checking if server exists on our database");

                if (finalCDNItems.FindIndex(i => string.Equals(i.Url, _settingFile.Read("CDN"))) != 0)
                {
                    Log.Debug("CDNLIST: Server found! Checking ID");
                    var index = finalCDNItems.FindIndex(i => string.Equals(i.Url, _settingFile.Read("CDN")));

                    Log.Debug("CDNLIST: ID is " + index);
                    if (index >= 0)
                    {
                        Log.Debug("CDNLIST: ID set correctly");
                        settingsCDNPick.SelectedIndex = index;
                    }
                }
                else
                {
                    Log.Debug("CDNLIST: Unable to find anything, assuming default");
                    settingsCDNPick.SelectedIndex = 1;
                }

                Log.Debug("CDNLIST: Triggering server change");
                if (settingsCDNPick.SelectedIndex == 1)
                {
                    SettingsCDNPick_SelectedIndexChanged(sender, e);
                }
                Log.Debug("CDNLIST: All done");
            }

            serverPick.DisplayMember = "Name";

            Log.Debug("LAUNCHER: Setting server list");
            finalItems = ServerListUpdater.GetList();
            serverPick.DataSource = finalItems;
            Log.Debug("Final List of Servers " + finalItems);

            //ForceSelectServer
            if (string.IsNullOrEmpty(_settingFile.Read("Server"))) {
                //SelectServerBtn_Click(null, null);
                new SelectServer().ShowDialog();

                if (ServerName != null)  {
                    this.SelectServerBtn.Text = "[...] " + ServerName.Name;
                    _settingFile.Write("Server", ServerName.IpAddress);
                } else {
                    Process.GetProcessById(Process.GetCurrentProcess().Id).Kill();
                }
            } //else {
                Log.Debug("SERVERLIST: Checking...");
                Log.Debug("SERVERLIST: Setting first server in list");
                Log.Debug("SERVERLIST: Checking if server is set on INI File");
                try { 
                    if (string.IsNullOrEmpty(_settingFile.Read("Server"))) {
                        Log.Debug("SERVERLIST: Failed to find anything... assuming " + ((ServerInfo)serverPick.SelectedItem).IpAddress);
                        _settingFile.Write("Server", ((ServerInfo)serverPick.SelectedItem).IpAddress);
                    }
                } catch {
                    Log.Debug("SERVERLIST: Failed to write anything...");
                    _settingFile.Write("Server", "");
                }

                Log.Debug("SERVERLIST: Re-Checking if server is set on INI File");
                if (_settingFile.KeyExists("Server")) {
                    Log.Debug("SERVERLIST: Found something!");
                    _skipServerTrigger = true;

                    Log.Debug("SERVERLIST: Checking if server exists on our database");

                    if ( finalItems.FindIndex(i => string.Equals(i.IpAddress, _settingFile.Read("Server"))) != 0 /*_slresponse.Contains(_settingFile.Read("Server"))*/) {
                        Log.Debug("SERVERLIST: Server found! Checking ID");
                        var index = finalItems.FindIndex(i => string.Equals(i.IpAddress, _settingFile.Read("Server")));

                        Log.Debug("SERVERLIST: ID is " + index);
                        if (index >= 0) {
                            Log.Debug("SERVERLIST: ID set correctly");
                            serverPick.SelectedIndex = index;
                        }
					} else {
                        Log.Debug("SERVERLIST: Unable to find anything, assuming default");
                        serverPick.SelectedIndex = 1;
                        Log.Debug("SERVERLIST: Deleting unknown entry");
                        _settingFile.DeleteKey("Server");
                    }

                    Log.Debug("SERVERLIST: Triggering server change");
                    if (serverPick.SelectedIndex == 1) {
                        ServerPick_SelectedIndexChanged(sender, e);
                    }
                    Log.Debug("SERVERLIST: All done");
                }
            //}

            Log.Debug("LAUNCHER: Checking for password");
            if (_settingFile.KeyExists("Password"))
            {
                _loginEnabled = true;
                _serverEnabled = true;
                _useSavedPassword = true;
                loginButton.Image = Properties.Resources.graybutton;
                loginButton.ForeColor = Color.White;
            }
            else
            {
                _loginEnabled = false;
                _serverEnabled = false;
                _useSavedPassword = false;
                loginButton.Image = Properties.Resources.graybutton;
                loginButton.ForeColor = Color.Gray;
            }

            Log.Debug("LAUNCHER: Setting game language");

            settingsLanguage.DisplayMember = "Text";
            settingsLanguage.ValueMember = "Value";

            var languages = new[] {
                new { Text = "English", Value = "EN" },
                new { Text = "Deutsch", Value = "DE" },
                new { Text = "Español", Value = "ES" },
                new { Text = "Français", Value = "FR" },
                new { Text = "Polski", Value = "PL" },
                new { Text = "Русский", Value = "RU" },
                new { Text = "Português (Brasil)", Value = "PT" },
                new { Text = "繁體中文", Value = "TC" },
                new { Text = "简体中文", Value = "SC" },
                new { Text = "ภาษาไทย", Value = "TH" },
                new { Text = "Türkçe", Value = "TR" },
            };

            settingsLanguage.DataSource = languages;

            if (_settingFile.KeyExists("Language"))
            {
                settingsLanguage.SelectedValue = _settingFile.Read("Language");
            }

            /*
            Task.Run(() => {
                String _slresponse2 = string.Empty;
                try {
                    WebClient wc = new WebClient();
                    try {
                        _slresponse2 = wc.DownloadString(Self.CDNUrlList);
                    } catch {
                        _slresponse2 = wc.DownloadString(Self.CDNUrlStaticList);
                    }
                } catch(Exception error) {
                    MessageBox.Show(error.Message, "An error occurred while loading CDN List");
                    _slresponse2 = JsonConvert.SerializeObject(new[] {
                        new CDNObject { Name = "[CF] WorldUnited.gg Mirror", Url = "http://cdn.worldunited.gg/gamefiles/packed/" },
                        new CDNObject { Name = "[CF] DavidCarbon Mirror", Url = "http://g-sbrw.davidcarbon.download"}
                    });
                }

                List<CDNObject> CDNList = JsonConvert.DeserializeObject<List<CDNObject>>(_slresponse2);

                settingsCDNPick.Invoke(new Action(() => 
                {
                    settingsCDNPick.DisplayMember = "name";
                    settingsCDNPick.DataSource = CDNList;
                }));
            });
            */

            Log.Debug("LAUNCHER: Re-checking InstallationDirectory: " + _settingFile.Read("InstallationDirectory"));

            var drive = Path.GetPathRoot(_settingFile.Read("InstallationDirectory"));
            if (!Directory.Exists(drive)) {
                if (!string.IsNullOrEmpty(drive)) {
                    var newdir = Directory.GetCurrentDirectory() + "\\GameFiles";
                    _settingFile.Write("InstallationDirectory", newdir);
                    Log.Debug(string.Format("LAUNCHER: Drive {0} was not found. Your actual installation directory is set to {1} now.", drive, newdir));

                    MessageBox.Show(null, string.Format("Drive {0} was not found. Your actual installation directory is set to {1} now.", drive, newdir), "GameLauncher", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            //vfilesCheck.Checked = _disableChecks;
            settingsProxyCheckbox.Checked = _disableProxy;
            settingsDiscordRPCCheckbox.Checked = _disableDiscordRPC;

            Log.Debug("CORE: Hiding RegisterFormElements"); RegisterFormElements(false);
            Log.Debug("CORE: Hiding SettingsFormElements"); SettingsFormElements(false);
            Log.Debug("CORE: Hiding LoggedInFormElements"); LoggedInFormElements(false);

            Log.Debug("CORE: Showing LoginFormElements"); LoginFormElements(true);

            Log.Debug("CORE: Setting Registry Options");
            try {
                var gameInstallDirValue = Registry.GetValue("HKEY_LOCAL_MACHINE\\software\\Electronic Arts\\Need For Speed World", "GameInstallDir", RegistryValueKind.String);
                if (gameInstallDirValue == null || gameInstallDirValue.ToString() != Path.GetFullPath(_settingFile.Read("InstallationDirectory"))) {
                    try {
                        Registry.SetValue("HKEY_LOCAL_MACHINE\\software\\Electronic Arts\\Need For Speed World", "GameInstallDir", Path.GetFullPath(_settingFile.Read("InstallationDirectory")));
                        Registry.SetValue("HKEY_LOCAL_MACHINE\\software\\Electronic Arts\\Need For Speed World", "LaunchInstallDir", Path.GetFullPath(Application.ExecutablePath));
                    } catch(Exception ex) {
                        Log.Error(ex.Message);
                    }
                }
            } catch(Exception ex) {
                Log.Error(ex.Message);
            }

            Log.Debug("LAUNCHER: Setting configurations");
            _newGameFilesPath = Path.GetFullPath(_settingFile.Read("InstallationDirectory"));
            settingsGameFilesCurrentText.Text = "CURRENT DIRECTORY:";
            settingsGameFilesCurrent.Text = _newGameFilesPath;
            //DavidCarbon
            settingsCDNCurrent.Text = _settingFile.Read("CDN");
            //Zacam
            _newLauncherPath = Path.GetDirectoryName(Application.ExecutablePath);
            settingsLauncherPathText.Text = "LAUNCHER FOLDER:";
            settingsLauncherPathCurrent.Text = _newLauncherPath;

            Log.Debug("DISCORD: Initializing DiscordRPC");
            Log.Debug("DISCORD: Checking if Discord RPC is Disabled from User Settings! It's value is " + _disableDiscordRPC);

            _presence.State = _OS;
            _presence.Details = "In-Launcher: " + Application.ProductVersion;
            _presence.Assets = new Assets
            {
                LargeImageText = "SBRW",
                LargeImageKey = "nfsw"
            };
            if(discordRpcClient != null) discordRpcClient.SetPresence(_presence);

            BeginInvoke((MethodInvoker)delegate {
                Log.Debug("CORE: 'GetServerInformation' from all Servers in Server List and Download Selected Server Banners");
                LaunchNfsw();
            });

            this.BringToFront();

            if(!DetectLinux.LinuxDetected()) {
                Log.Debug("LAUNCHER: Checking for update: " + Self.mainserver + "/update.php?version=" + Application.ProductVersion);
                new LauncherUpdateCheck(launcherIconStatus, launcherStatusText, launcherStatusDesc).CheckAvailability();
            } else {
                launcherIconStatus.Image = Properties.Resources.ac_success;
                launcherStatusText.ForeColor = Color.FromArgb(0x9fc120);
                launcherStatusText.Text = "Launcher Status - Linux";
            }
            settingsLauncherVersion.Text = launcherStatusDesc.Text;

            Self.gamedir = _settingFile.Read("InstallationDirectory");

            if(File.Exists(_settingFile.Read("InstallationDirectory") + "/profwords") || File.Exists(_settingFile.Read("InstallationDirectory") + "/profwords_dis")) { 
                try { 
                    settingsWordFilterCheck.Checked = File.Exists(_settingFile.Read("InstallationDirectory") + "/profwords") ? false : true;
                } catch {
                    settingsWordFilterCheck.Checked = false;
                }
            } else {
                settingsWordFilterCheck.Enabled = false;
            }

            /* Load Settings API Connection Status */
            Task.Delay(800);
            PingServerListAPIStatus();

            /* Remove TracksHigh Folder and Files */
            RemoveTracksHighFiles();

        }

        private void Closebtn_Click(object sender, EventArgs e) {
            //closebtn.BackgroundImage = Properties.Resources.close_click;

		    try {
                if (!(serverPick.SelectedItem is ServerInfo server)) return;
                _settingFile.Write("Server", server.IpAddress); 
            } catch {
                    
            }

            if (_windowMoved)
            {
                _settingFile.Write("LauncherPosX", Location.X.ToString());
                _settingFile.Write("LauncherPosY", Location.Y.ToString());
            }

            try { 
                _settingFile.Write("InstallationDirectory", Path.GetFullPath(_settingFile.Read("InstallationDirectory")));
            } catch {
                _settingFile.Write("InstallationDirectory", _settingFile.Read("InstallationDirectory"));
            }

            //DavidCarbon
            //This Saves the update the was skipped or to remind the user at next launch
            if (Settings.Default.IgnoreUpdateVersion != String.Empty)
            {
                _settingFile.Write("IgnoreUpdateVersion", Settings.Default.IgnoreUpdateVersion);
                Log.Debug("IGNOREUPDATEVERSION: Skipping Update " + Settings.Default.IgnoreUpdateVersion + " !");
            }
            else
            {
                if (_settingFile.Read("IgnoreUpdateVersion") != String.Empty)
                {
                    if (_settingFile.Read("IgnoreUpdateVersion") == Application.ProductVersion)
                    {
                        _settingFile.Write("IgnoreUpdateVersion", String.Empty);
                        Log.Debug("IGNOREUPDATEVERSION: Cleared OLD IgnoreUpdateVersion Build Number. You're now on the Latest Game Launcher!");
                    }
                    else
                    {
                        Log.Debug("IGNOREUPDATEVERSION: Manually Skipping Update " + _settingFile.Read("IgnoreUpdateVersion") + " !");
                    }
                }
                else
                {
                    Log.Debug("IGNOREUPDATEVERSION: Latest Game Launcher!");
                }
            }

            Process[] allOfThem = Process.GetProcessesByName("nfsw");
            foreach (var oneProcess in allOfThem) {
                Process.GetProcessById(oneProcess.Id).Kill();
            }

            //Kill DiscordRPC
            if(discordRpcClient != null) {
                discordRpcClient.Dispose();
            }

            ServerProxy.Instance.Stop();
            Notification.Dispose();

            var linksPath = Path.Combine(_settingFile.Read("InstallationDirectory"), ".links");
            if (File.Exists(linksPath))
            {
                Log.Debug("CLEANLINKS: Cleaning Up Mod Files {Exiting}");
                CleanLinks(linksPath);
            }

            //Leave this here. Its to properly close the launcher from Visual Studio (And Close the Launcher a well)
            try { this.Close(); } catch { }
        }

        private void AddServer_Click(object sender, EventArgs e)
        {
             new AddServer().Show();
        }

        private void OpenDebugWindow(object sender, EventArgs e)
        {
            if (!(serverPick.SelectedItem is ServerInfo server)) return;

            var form = new DebugWindow(server.IpAddress, server.Name);
            form.Show();
        }

        private void Closebtn_MouseEnter(object sender, EventArgs e)
        {
            //closebtn.BackgroundImage = Properties.Resources.close_hover;
        }

        private void Closebtn_MouseLeave(object sender, EventArgs e)
        {
            closebtn.BackgroundImage = Properties.Resources.close;
        }

        private void LoginEnter(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return && _loginEnabled)
            {
                LoginButton_Click(null, null);
                e.SuppressKeyPress = true;
            }
        }

        private void Loginbuttonenabler(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(email.Text) || string.IsNullOrEmpty(password.Text))
            {
                _loginEnabled = false;
                loginButton.Image = Properties.Resources.graybutton;
                loginButton.ForeColor = Color.Gray;
            }
            else
            {
                _loginEnabled = true;
                loginButton.Image = Properties.Resources.graybutton;
                loginButton.ForeColor = Color.White;
            }

            _useSavedPassword = false;
        }

        private void LoginButton_MouseUp(object sender, EventArgs e)
        {
            if (_loginEnabled || _builtinserver)
            {
                loginButton.Image = Properties.Resources.graybutton_hover;
            }
            else
            {
                loginButton.Image = Properties.Resources.graybutton;
            }
        }

        private void LoginButton_MouseDown(object sender, EventArgs e)
        {
            if (_loginEnabled || _builtinserver)
            {
                loginButton.Image = Properties.Resources.graybutton_click;
            }
            else
            {
                loginButton.Image = Properties.Resources.graybutton;
            }
        }

        private void LoginButton_Click(object sender, EventArgs e) {
            if ((_loginEnabled == false || _serverEnabled == false) && _builtinserver == false) {
                return;
            }

            if (_isDownloading) {
                MessageBox.Show(null, "Please wait while launcher is still downloading gamefiles.", "GameLauncher", MessageBoxButtons.OK);
                return;
            }

            Tokens.Clear();

            String username = email.Text.ToString();
            String pass = password.Text.ToString();
            String realpass;

            Tokens.IPAddress = _serverInfo.IpAddress;
            Tokens.ServerName = _serverInfo.Name;

            Self.userAgent = (_serverInfo.ForceUserAgent == null) ? null : _serverInfo.ForceUserAgent;

            if (_modernAuthSupport == false) {
                //ClassicAuth sends password in SHA1
                realpass = (_useSavedPassword) ? _settingFile.Read("Password") : SHA.HashPassword(password.Text.ToString()).ToLower();
                ClassicAuth.Login(username, realpass);
            } else {
                //ModernAuth sends passwords in plaintext, but is POST request
                realpass = (_useSavedPassword) ? _settingFile.Read("Password") : password.Text.ToString();
                ModernAuth.Login(username, realpass);
            }

            if (rememberMe.Checked) {
                _settingFile.Write("AccountEmail", username);
                _settingFile.Write("Password", realpass);
                Properties.Settings.Default.PasswordDecoded = password.Text.ToString();
            } else {
                _settingFile.DeleteKey("AccountEmail");
                _settingFile.DeleteKey("Password");
                Properties.Settings.Default.PasswordDecoded = String.Empty;
            }

            Properties.Settings.Default.Save();

            if (String.IsNullOrEmpty(Tokens.Error)) {
                _loggedIn = true;
                _userId = Tokens.UserId;
                _loginToken = Tokens.LoginToken;
                _serverIp = Tokens.IPAddress;

                if(!String.IsNullOrEmpty(Tokens.Warning)) {
                    MessageBox.Show(null, Tokens.Warning, "GameLauncher", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                LoginFormElements(false);
                LoggedInFormElements(true);
                settingsButton.Visible = false;
            } else {
                //Main Screen Login
                MainEmailBorder.Image = Properties.Resources.email_error_text_border;
                MainPasswordBorder.Image = Properties.Resources.password_error_text_border;
                MessageBox.Show(null, Tokens.Error, "GameLauncher", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoginButton_MouseEnter(object sender, EventArgs e)
        {
            if (_loginEnabled || _builtinserver)
            {
                loginButton.Image = Properties.Resources.graybutton_hover;
                loginButton.ForeColor = Color.White;
            }
            else
            {
                loginButton.Image = Properties.Resources.graybutton;
                loginButton.ForeColor = Color.Gray;
            }
        }

        private void LoginButton_MouseLeave(object sender, EventArgs e)
        {
            if (_loginEnabled || _builtinserver)
            {
                loginButton.Image = Properties.Resources.graybutton;
                loginButton.ForeColor = Color.White;
            }
            else
            {
                loginButton.Image = Properties.Resources.graybutton;
                loginButton.ForeColor = Color.Gray;
            }
        }

        private void ServerPick_SelectedIndexChanged(object sender, EventArgs e) {
            MainEmailBorder.Image = Properties.Resources.email_text_border;
            MainPasswordBorder.Image = Properties.Resources.password_text_border;

            //ServerStatusBar(_colorLoading, _startPoint, _endPoint);

            _serverInfo = (ServerInfo)serverPick.SelectedItem;
            _realServername = _serverInfo.Name;
            _realServernameBanner = _serverInfo.Name;
            _modernAuthSupport = false;

            if (_serverInfo.IsSpecial) {
                serverPick.SelectedIndex = _lastSelectedServerId;
                return;
            }

            if (!_skipServerTrigger) { return; }

            _lastSelectedServerId = serverPick.SelectedIndex;
            _allowRegistration = false;
            _loginEnabled = false;

            ServerStatusText.Text = "Server Status - Pinging";
            ServerStatusText.ForeColor = Color.FromArgb(66, 179, 189);
            ServerStatusDesc.Text = "";
            ServerStatusIcon.Image = Properties.Resources.server_checking;

            loginButton.ForeColor = Color.Gray;
            var verticalImageUrl = "";
            verticalBanner.Image = null;
            verticalBanner.BackColor = Color.Transparent;

            var serverIp = _serverInfo.IpAddress;
            string numPlayers;

            //Disable Social Panel when switching
            DisableSocialPanelandClearIt();

            imageServerName.Text = ((ServerInfo)serverPick.SelectedItem).Name;

            if (serverPick.GetItemText(serverPick.SelectedItem) == "Offline Built-In Server") {
                _builtinserver = true;
                loginButton.Image = Properties.Resources.graybutton;
                loginButton.Text = "Launch".ToUpper();
                loginButton.ForeColor = Color.White;
                ServerInfoPanel.Visible = false;
            } else {
                _builtinserver = false;
                loginButton.Image = Properties.Resources.graybutton;
                loginButton.Text = "Login".ToUpper();
                loginButton.ForeColor = Color.Gray;
                ServerInfoPanel.Visible = false;
            }

            WebClient client = new WebClient();
            var artificialPingStart = Self.GetTimestamp();
            verticalBanner.BackColor = Color.Transparent;

            var stringToUri = new Uri(serverIp + "/GetServerInformation");
            client.DownloadStringAsync(stringToUri);

            System.Timers.Timer aTimer = new System.Timers.Timer(10000);
            aTimer.Elapsed += (x, y) => { client.CancelAsync(); };
            aTimer.Enabled = true;

            client.DownloadStringCompleted += (sender2, e2) => {
                aTimer.Enabled = false;

                var artificialPingEnd = Self.GetTimestamp();

                if(e2.Cancelled) {
                    //ServerStatusBar(_colorOffline, _startPoint, _endPoint);

                    ServerStatusText.Text = "Server Status - Offline ( OFF )";
                    ServerStatusText.ForeColor = Color.FromArgb(254, 0, 0);
                    ServerStatusDesc.Text = "Failed to connect to server.";
                    ServerStatusIcon.Image = Properties.Resources.server_offline;
                    _serverEnabled = false;
                    _allowRegistration = false;
                    //Disable Login & Register Button
                    loginButton.Enabled = false;
                    registerText.Enabled = false;
                    //Disable Social Panel
                    DisableSocialPanelandClearIt();

                    if (!serverStatusDictionary.ContainsKey(_serverInfo.Id)) {
                        serverStatusDictionary.Add(_serverInfo.Id, 2);
                    } else { 
                        serverStatusDictionary[_serverInfo.Id] = 2; 
                    }
                } else if (e2.Error != null) {
                    //ServerStatusBar(_colorOffline, _startPoint, _endPoint);

                    ServerStatusText.Text = "Server Status - Offline ( OFF )";
                    ServerStatusText.ForeColor = Color.FromArgb(254, 0, 0);
                    ServerStatusDesc.Text = "Server seems to be offline.";
                    ServerStatusIcon.Image = Properties.Resources.server_offline;
                    _serverEnabled = false;
                    _allowRegistration = false;
                    //Disable Login & Register Button
                    loginButton.Enabled = false;
                    registerText.Enabled = false;
                    //Disable Social Panel
                    DisableSocialPanelandClearIt();

                    if (!serverStatusDictionary.ContainsKey(_serverInfo.Id)) {
                        serverStatusDictionary.Add(_serverInfo.Id, 0);
                    } else {
                        serverStatusDictionary[_serverInfo.Id] = 0;
                    }
                } else {
                    if (_realServername == "Offline Built-In Server") {
                        DisableSocialPanelandClearIt();
                        numPlayers = "∞";
                    } else {
                        if (!serverStatusDictionary.ContainsKey(_serverInfo.Id)) {
                            serverStatusDictionary.Add(_serverInfo.Id, 1);
                        } else {
                            serverStatusDictionary[_serverInfo.Id] = 1;
                        }
                        //Enable Social Panel
                        ServerInfoPanel.Visible = true;

                        String purejson = String.Empty;
                        purejson = e2.Result;
                        json = JsonConvert.DeserializeObject<GetServerInformation>(e2.Result);
                        Self.rememberjson = e2.Result;
                        try {
                            _realServernameBanner = json.ServerName;
                            if (!string.IsNullOrEmpty(json.BannerUrl)) {
                                bool result;

                                try {
                                    result = Uri.TryCreate(json.BannerUrl, UriKind.Absolute, out Uri uriResult) && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
                                } catch {
                                    result = false;
                                }

                                if (result) {
                                    verticalImageUrl = json.BannerUrl;
                                } else {
                                    verticalImageUrl = null;
                                }
                            } else {
                                verticalImageUrl = null;
                            }
                        } catch {
                            verticalImageUrl = null;
                        }

                        /* Social Panel Core */

                        //Discord Invite Display
                        try
                        {
                            if (string.IsNullOrEmpty(json.DiscordUrl))
                            {
                                DiscordIcon.BackgroundImage = Properties.Resources.social_discord_disabled;
                                DiscordInviteLink.Enabled = false;
                                _serverDiscordLink = null;
                            }
                            else
                            {
                                DiscordIcon.BackgroundImage = Properties.Resources.social_discord;
                                DiscordInviteLink.Enabled = true;
                                _serverDiscordLink = json.DiscordUrl;
                            }
                        }
                        catch
                        {
                            DiscordIcon.BackgroundImage = Properties.Resources.social_discord_disabled;
                            DiscordInviteLink.Enabled = false;
                            _serverDiscordLink = null;
                        }

                        //Homepage Display
                        try
                        {
                            if (string.IsNullOrEmpty(json.HomePageUrl))
                            {
                                HomePageIcon.BackgroundImage = Properties.Resources.social_home_page_disabled;
                                HomePageLink.Enabled = false;
                                _serverWebsiteLink = null;
                            }
                            else
                            {
                                HomePageIcon.BackgroundImage = Properties.Resources.social_home_page;
                                HomePageLink.Enabled = true;
                                _serverWebsiteLink = json.HomePageUrl;
                            }
                        }
                        catch
                        {
                            HomePageIcon.BackgroundImage = Properties.Resources.social_home_page_disabled;
                            HomePageLink.Enabled = false;
                            _serverWebsiteLink = null;
                        }

                        //Facebook Group Display
                        try
                        {
                            if (string.IsNullOrEmpty(json.FacebookUrl) || json.FacebookUrl == "Your facebook page url")
                            {
                                FacebookIcon.BackgroundImage = Properties.Resources.social_facebook_disabled;
                                FacebookGroupLink.Enabled = false;
                                _serverFacebookLink = null;
                            }
                            else
                            {
                                FacebookIcon.BackgroundImage = Properties.Resources.social_facebook;
                                FacebookGroupLink.Enabled = true;
                                _serverFacebookLink = json.FacebookUrl;
                            }
                        }
                        catch
                        {
                            FacebookIcon.BackgroundImage = Properties.Resources.social_facebook_disabled;
                            FacebookGroupLink.Enabled = false;
                            _serverFacebookLink = null;
                        }

                        //Twitter Account Display
                        try
                        {
                            if (string.IsNullOrEmpty(json.TwitterUrl))
                            {
                                TwitterIcon.BackgroundImage = Properties.Resources.social_twitter_disabled;
                                TwitterAccountLink.Enabled = false;
                                _serverTwitterLink = null;
                            }
                            else
                            {
                                TwitterIcon.BackgroundImage = Properties.Resources.social_twitter;
                                TwitterAccountLink.Enabled = true;
                                _serverTwitterLink = json.TwitterUrl;
                            }
                        }
                        catch
                        {
                            TwitterIcon.BackgroundImage = Properties.Resources.social_twitter_disabled;
                            TwitterAccountLink.Enabled = false;
                            _serverTwitterLink = null;
                        }

                        //Server Set Speedbug Timer Display
                        try
                        {
                            int serverSecondsToShutDown = (json.SecondsToShutDown != 0) ? json.SecondsToShutDown : 2 * 60 * 60;
                            TimeSpan t = TimeSpan.FromSeconds(serverSecondsToShutDown);
                            string serverSecondsToShutDownNamed = string.Format("Restart Timer: " + "{0} Hours", t.Hours);

                             this.ServerShutDown.Text = serverSecondsToShutDownNamed;
                        }
                        catch
                        {
                            this.ServerShutDown.Text = "∞ and Beyond";
                        }

                        //Scenery Group Display
                        switch (String.Join("", json.ActivatedHolidaySceneryGroups)) {
                            case "SCENERY_GROUP_NEWYEARS":
                                this.SceneryGroupText.Text = "Scenery: New Years";
                                break;
                            case "SCENERY_GROUP_OKTOBERFEST":
                                this.SceneryGroupText.Text = "Scenery: OKTOBERFEST";
                                break;
                            case "SCENERY_GROUP_HALLOWEEN":
                                this.SceneryGroupText.Text = "Scenery: Halloween";
                                break;
                            case "SCENERY_GROUP_CHRISTMAS":
                                this.SceneryGroupText.Text = "Scenery: Christmas";
                                break;
                            default:
                                this.SceneryGroupText.Text = "Scenery: Normal";
                                break;
                        }

                        try {
                            if (string.IsNullOrEmpty(json.RequireTicket)) {
                                _ticketRequired = true;
                            } else if (json.RequireTicket == "true") {
                                _ticketRequired = true;
                            } else {
                                _ticketRequired = false;
                            }
                        } catch {
                            _ticketRequired = false;
                        }

                        try {
                            if (string.IsNullOrEmpty(json.ModernAuthSupport)) {
                                _modernAuthSupport = false;
                            } else if (json.ModernAuthSupport == "true") {
                                if(stringToUri.Scheme == "https") {
                                    _modernAuthSupport = true;
                                } else {
                                    _modernAuthSupport = false;
                                }
                            } else {
                                _modernAuthSupport = false;
                            }
                        } catch {
                            _modernAuthSupport = false;
                        }

                        if (json.MaxUsersAllowed == 0) {
                            numPlayers = string.Format("{0}/{1}", json.OnlineNumber, json.NumberOfRegistered);
                        } else {
                            numPlayers = string.Format("{0}/{1}", json.OnlineNumber, json.MaxUsersAllowed.ToString());
                        }

                        _allowRegistration = true;

                        //ServerStatusBar(_colorOnline, _startPoint, _endPoint);
                    }

                    try { 
                        ServerStatusText.Text = "Server Status - Online ( ON )";
                        ServerStatusText.ForeColor = Color.FromArgb(159, 193, 32);
                        ServerStatusIcon.Image = Properties.Resources.server_online;
                        _loginEnabled = true;
                        //Enable Login & Register Button
                        loginButton.ForeColor = Color.White;
                        loginButton.Enabled = true;
                        registerText.Enabled = true;

                        if (((ServerInfo)serverPick.SelectedItem).Category == "DEV"){
                            //Disable Social Panel
                            DisableSocialPanelandClearIt();
                        }
                    }
                    catch {
                        //¯\_(ツ)_/¯
                    }

                    if (!DetectLinux.LinuxDetected()) {
                        Ping pingSender = new Ping();
                        pingSender.SendAsync(stringToUri.Host, 1000, new byte[1], new PingOptions(64, true), new AutoResetEvent(false));
                        pingSender.PingCompleted += (sender3, e3) => {
                            PingReply reply = e3.Reply;

                            if (reply.Status == IPStatus.Success && _realServername != "Offline Built-In Server")
                            {
                                if (this.ServerPingStatusText.InvokeRequired)
                                {
                                    ServerStatusDesc.Invoke(new Action(delegate () {
                                        ServerPingStatusText.Text = string.Format("Your Ping to the Server \n{0}".ToUpper(), reply.RoundtripTime + "ms");
                                    }));
                                }
                                else
                                {
                                    this.ServerPingStatusText.Text = string.Format("Your Ping to the Server \n{0}".ToUpper(), reply.RoundtripTime + "ms");
                                }
                            }
                            else
                            {
                                this.ServerPingStatusText.Text = string.Format("");
                            }
                        };
                    }
                    else
                    {
                        this.ServerPingStatusText.Text = string.Format("");
                    }

                    //for thread safety
                    if (this.ServerStatusDesc.InvokeRequired)
                    {
                        ServerStatusDesc.Invoke(new Action(delegate ()
                        {
                            ServerStatusDesc.Text = string.Format("Players in Game - {0}", numPlayers);
                        }));
                    }
                    else
                    {
                        this.ServerStatusDesc.Text = string.Format("Players in Game - {0}", numPlayers);
                    }

                    _serverEnabled = true;

                    if (!string.IsNullOrEmpty(verticalImageUrl)) {
                        WebClient client2 = new WebClient();
                        Uri stringToUri3 = new Uri(verticalImageUrl);
                        client2.DownloadDataAsync(stringToUri3);
                        client2.DownloadProgressChanged += (sender4, e4) => {
                            if (e4.TotalBytesToReceive > 2000000) {
                                client2.CancelAsync();
                            }
                        };

                        client2.DownloadDataCompleted += (sender4, e4) => {
                            if (e4.Cancelled) {
                                return;
                            } else if (e4.Error != null) {
                                return;
                            } else {
                                try {
                                    if(UriScheme.ForceGame != true) {
                                        Image image;
                                        var memoryStream = new MemoryStream(e4.Result);
                                        image = Image.FromStream(memoryStream);
                                        verticalBanner.Image = image;
                                        verticalBanner.BackColor = Color.Black;

                                        imageServerName.Text = String.Empty; //_realServernameBanner;
                                    } else {
                                        imageServerName.Text = "WebLogin";
                                        verticalBanner.Image = null;
                                        verticalBanner.BackColor = Color.Black;
                                    }
                                } catch(Exception ex) {
                                    Console.WriteLine(ex.Message);
                                    verticalBanner.Image = null;
                                }
                            }
                        };
                    }
                }
            };
        }

        private void ApplyEmbeddedFonts() {
            FontFamily AkrobatSemiBold = FontWrapper.Instance.GetFontFamily("Akrobat-SemiBold.ttf");
            FontFamily AkrobatRegular = FontWrapper.Instance.GetFontFamily("Akrobat-Regular.ttf");
        /* Front Screen */
            // serverPick -- Server List Text is not controlled here
            imageServerName.Font = new Font(AkrobatSemiBold, 25f * _dpiDefaultScale / CreateGraphics().DpiX, FontStyle.Bold);
            launcherStatusText.Font = new Font(AkrobatRegular, 10f * _dpiDefaultScale / CreateGraphics().DpiX, FontStyle.Regular);
            launcherStatusDesc.Font = new Font(AkrobatSemiBold, 10f * _dpiDefaultScale / CreateGraphics().DpiX, FontStyle.Bold);
            ServerStatusText.Font = new Font(AkrobatRegular, 10f * _dpiDefaultScale / CreateGraphics().DpiX, FontStyle.Regular);
            ServerStatusDesc.Font = new Font(AkrobatSemiBold, 10f * _dpiDefaultScale / CreateGraphics().DpiX, FontStyle.Bold);
            APIStatusText.Font = new Font(AkrobatRegular, 10f * _dpiDefaultScale / CreateGraphics().DpiX, FontStyle.Regular);
            APIStatusDesc.Font = new Font(AkrobatSemiBold, 10f * _dpiDefaultScale / CreateGraphics().DpiX, FontStyle.Bold);
        /* Social Panel */
            ServerShutDown.Font = new Font(AkrobatSemiBold, 10f * _dpiDefaultScale / CreateGraphics().DpiX, FontStyle.Bold);
            HomePageLink.Font = new Font(AkrobatSemiBold, 10f * _dpiDefaultScale / CreateGraphics().DpiX, FontStyle.Bold);
            DiscordInviteLink.Font = new Font(AkrobatSemiBold, 10f * _dpiDefaultScale / CreateGraphics().DpiX, FontStyle.Bold);
            FacebookGroupLink.Font = new Font(AkrobatSemiBold, 10f * _dpiDefaultScale / CreateGraphics().DpiX, FontStyle.Bold);
            TwitterAccountLink.Font = new Font(AkrobatSemiBold, 10f * _dpiDefaultScale / CreateGraphics().DpiX, FontStyle.Bold);
            SceneryGroupText.Font = new Font(AkrobatSemiBold, 10f * _dpiDefaultScale / CreateGraphics().DpiX, FontStyle.Bold);
        /* Settings */
            settingsGamePathText.Font = new Font(AkrobatSemiBold, 10f * _dpiDefaultScale / CreateGraphics().DpiX, FontStyle.Bold);
            // settingsGameFiles -- Change GameFiles Path button text is not controlled here
            settingsCDNText.Font = new Font(AkrobatSemiBold, 10f * _dpiDefaultScale / CreateGraphics().DpiX, FontStyle.Bold);
            // settingsCDNPick -- CDN Menu Dropdown text is not controlled here
            settingsLanguageText.Font = new Font(AkrobatSemiBold, 10f * _dpiDefaultScale / CreateGraphics().DpiX, FontStyle.Bold);
            // settingsLanguage -- Language Menu Dropdown text is not controlled here
            settingsWordFilterCheck.Font = new Font(AkrobatSemiBold, 10f * _dpiDefaultScale / CreateGraphics().DpiX, FontStyle.Bold);
            settingsProxyCheckbox.Font = new Font(AkrobatSemiBold, 10f * _dpiDefaultScale / CreateGraphics().DpiX, FontStyle.Bold);
            settingsDiscordRPCCheckbox.Font = new Font(AkrobatSemiBold, 10f * _dpiDefaultScale / CreateGraphics().DpiX, FontStyle.Bold);
            SettingsClearCrashLogsButton.Font = new Font(AkrobatSemiBold, 10f * _dpiDefaultScale / CreateGraphics().DpiX, FontStyle.Bold);
            settingsGameFilesCurrentText.Font = new Font(AkrobatSemiBold, 8f * _dpiDefaultScale / CreateGraphics().DpiX, FontStyle.Bold);
            settingsGameFilesCurrent.Font = new Font(AkrobatRegular, 9f * _dpiDefaultScale / CreateGraphics().DpiX, FontStyle.Regular);
            settingsCDNCurrentText.Font = new Font(AkrobatSemiBold, 8f * _dpiDefaultScale / CreateGraphics().DpiX, FontStyle.Bold);
            settingsCDNCurrent.Font = new Font(AkrobatRegular, 9f * _dpiDefaultScale / CreateGraphics().DpiX, FontStyle.Regular);
            settingsLauncherPathText.Font = new Font(AkrobatSemiBold, 10f * _dpiDefaultScale / CreateGraphics().DpiX, FontStyle.Bold);
            settingsLauncherPathCurrent.Font = new Font(AkrobatRegular, 9f * _dpiDefaultScale / CreateGraphics().DpiX, FontStyle.Regular);
            settingsNetworkText.Font = new Font(AkrobatSemiBold, 10f * _dpiDefaultScale / CreateGraphics().DpiX, FontStyle.Bold);
            settingsMainSrvText.Font = new Font(AkrobatRegular, 10.5f * _dpiDefaultScale / CreateGraphics().DpiX, FontStyle.Regular);
            settingsMainCDNText.Font = new Font(AkrobatRegular, 10.5f * _dpiDefaultScale / CreateGraphics().DpiX, FontStyle.Regular);
            settingsBkupSrvText.Font = new Font(AkrobatRegular, 10.5f * _dpiDefaultScale / CreateGraphics().DpiX, FontStyle.Regular);
            settingsBkupCDNText.Font = new Font(AkrobatRegular, 10.5f * _dpiDefaultScale / CreateGraphics().DpiX, FontStyle.Regular);
            settingsLauncherVersion.Font = new Font(AkrobatSemiBold, 10f * _dpiDefaultScale / CreateGraphics().DpiX, FontStyle.Bold);
            settingsSave.Font = new Font(AkrobatSemiBold, 10f * _dpiDefaultScale / CreateGraphics().DpiX, FontStyle.Bold);
            settingsCancel.Font = new Font(AkrobatSemiBold, 10f * _dpiDefaultScale / CreateGraphics().DpiX, FontStyle.Bold);
        /* Log In Panel */
            currentWindowInfo.Font = new Font(AkrobatSemiBold, 11f * _dpiDefaultScale / CreateGraphics().DpiX, FontStyle.Bold);
            email.Font = new Font(AkrobatRegular, 10f * _dpiDefaultScale / CreateGraphics().DpiX, FontStyle.Regular);
            password.Font = new Font(AkrobatRegular, 10f * _dpiDefaultScale / CreateGraphics().DpiX, FontStyle.Regular);
            rememberMe.Font = new Font(AkrobatSemiBold, 10f * _dpiDefaultScale / CreateGraphics().DpiX, FontStyle.Bold);
            forgotPassword.Font = new Font(AkrobatSemiBold, 9f * _dpiDefaultScale / CreateGraphics().DpiX, FontStyle.Bold);
            loginButton.Font = new Font(AkrobatSemiBold, 10f * _dpiDefaultScale / CreateGraphics().DpiX, FontStyle.Bold);
            registerText.Font = new Font(AkrobatSemiBold, 10f * _dpiDefaultScale / CreateGraphics().DpiX, FontStyle.Bold);
            ServerPingStatusText.Font = new Font(AkrobatSemiBold, 11f * _dpiDefaultScale / CreateGraphics().DpiX, FontStyle.Bold);
            logoutButton.Font = new Font(AkrobatSemiBold, 10f * _dpiDefaultScale / CreateGraphics().DpiX, FontStyle.Bold);
            playButton.Font = new Font(AkrobatSemiBold, 15f * _dpiDefaultScale / CreateGraphics().DpiX, FontStyle.Bold);
            playProgressText.Font = new Font(AkrobatSemiBold, 10f * _dpiDefaultScale / CreateGraphics().DpiX, FontStyle.Bold);
            playProgressTextTimer.Font = new Font(AkrobatSemiBold, 10f * _dpiDefaultScale / CreateGraphics().DpiX, FontStyle.Bold);
        /* Registering Panel */
            registerEmail.Font = new Font(AkrobatRegular, 10f * _dpiDefaultScale / CreateGraphics().DpiX, FontStyle.Regular);
            registerPassword.Font = new Font(AkrobatRegular, 10f * _dpiDefaultScale / CreateGraphics().DpiX, FontStyle.Regular);
            registerConfirmPassword.Font = new Font(AkrobatRegular, 10f * _dpiDefaultScale / CreateGraphics().DpiX, FontStyle.Regular);
            registerTicket.Font = new Font(AkrobatRegular, 10f * _dpiDefaultScale / CreateGraphics().DpiX, FontStyle.Regular);
            registerAgree.Font = new Font(AkrobatSemiBold, 9f * _dpiDefaultScale / CreateGraphics().DpiX, FontStyle.Bold);
            registerButton.Font = new Font(AkrobatSemiBold, 10f * _dpiDefaultScale / CreateGraphics().DpiX, FontStyle.Bold);
            registerCancel.Font = new Font(AkrobatSemiBold, 10f * _dpiDefaultScale / CreateGraphics().DpiX, FontStyle.Bold);
        }

        private void RegisterText_LinkClicked(object sender, EventArgs e)
        {
            registerButton.Image = Properties.Resources.greenbutton_click;
            if (_allowRegistration) {
                if(!string.IsNullOrEmpty(json.WebSignupUrl)) {
                    Process.Start(json.WebSignupUrl);
                    MessageBox.Show(null, "A browser window has been opened to complete registration on " + json.ServerName, "GameLauncher", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if(_realServername == "WorldUnited Official" || _realServername == "WorldUnited OFFICIAL") {
                    Process.Start("https://signup.worldunited.gg/?discordid=" + Self.discordid);
                    MessageBox.Show(null, "A browser window has been opened to complete registration on WorldUnited OFFICIAL", "GameLauncher", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                currentWindowInfo.Text = "REGISTER ON \n" + _realServername.ToUpper();
                LoginFormElements(false);
                RegisterFormElements(true);
            } else {
                MessageBox.Show(null, "Server seems to be offline.", "GameLauncher", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ForgotPassword_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            if (!string.IsNullOrEmpty(json.WebRecoveryUrl)) {
                Process.Start(json.WebRecoveryUrl);
                MessageBox.Show(null, "A browser window has been opened to complete password recovery on " + json.ServerName, "GameLauncher", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string send = Prompt.ShowDialog("Please specify your email address.", "GameLauncher");

            if(send != String.Empty) {
                String responseString;
                try { 
                    Uri resetPasswordUrl = new Uri(_serverInfo.IpAddress + "/RecoveryPassword/forgotPassword");

                    var request = (HttpWebRequest)System.Net.WebRequest.Create(resetPasswordUrl);
                    var postData = "email="+send;
                    var data = Encoding.ASCII.GetBytes(postData);
                    request.Method = "POST";
                    request.ContentType = "application/x-www-form-urlencoded";
                    request.ContentLength = data.Length;

                    using (var stream = request.GetRequestStream()) {
                        stream.Write(data, 0, data.Length);
                    }

                    var response = (HttpWebResponse)request.GetResponse();
                    responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                } catch {
                    responseString = "Failed to send email!";
                }

                MessageBox.Show(null, responseString, "GameLauncher", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        /* Main Screen Elements */

        /* Social Panel | Ping or Offline | */
        private void DisableSocialPanelandClearIt()
        {
            //Hides Social Panel
            ServerInfoPanel.Visible = false;
            //Home
            HomePageIcon.BackgroundImage = Properties.Resources.social_home_page_disabled;
            HomePageLink.Enabled = false;
            _serverWebsiteLink = null;
            //Discord
            DiscordIcon.BackgroundImage = Properties.Resources.social_discord_disabled;
            DiscordInviteLink.Enabled = false;
            _serverDiscordLink = null;
            //Facebook
            FacebookIcon.BackgroundImage = Properties.Resources.social_facebook_disabled;
            FacebookGroupLink.Enabled = false;
            _serverFacebookLink = null;
            //Twitter
            TwitterIcon.BackgroundImage = Properties.Resources.social_twitter_disabled;
            TwitterAccountLink.Enabled = false;
            _serverTwitterLink = null;
            //Scenery
            SceneryGroupText.Text = "Expecting something?";
            //Restart Timer
            ServerShutDown.Text = "But it's me, Game Launcher!";
        }

        /*  After Successful Login, Hide Login Forms */
        private void LoggedInFormElements(bool hideElements)
        {
            if (hideElements)
            {
                DateTime currentTime = DateTime.Now;

                if (currentTime.Hour < 12)
                {
                    _loginWelcomeTime = "Good Morning";
                }
                else if (currentTime.Hour <= 16)
                {
                    _loginWelcomeTime = "Good Afternoon";
                }
                else if (currentTime.Hour <= 20)
                {
                    _loginWelcomeTime = "Good Evening";
                }
                else
                {
                    _loginWelcomeTime = "Good Night";
                }
                currentWindowInfo.Text = string.Format(_loginWelcomeTime + "\n{0}", email.Text).ToUpper();
            }

            ServerPingStatusText.Visible = hideElements;
            ShowPlayPanel.Visible = hideElements;
            extractingProgress.Visible = hideElements;
            playProgressText.Visible = hideElements;
            playProgressTextTimer.Visible = hideElements;
            playButton.Visible = hideElements;
            settingsButton.Visible = hideElements;
            verticalBanner.Visible = hideElements;
            ServerStatusText.Visible = hideElements;
            ServerStatusIcon.Visible = hideElements;
            ServerStatusDesc.Visible = hideElements;
            launcherIconStatus.Visible = hideElements;
            launcherStatusDesc.Visible = hideElements;
            launcherStatusText.Visible = hideElements;
            //allowedCountriesLabel.Visible = hideElements;
            APIStatusText.Visible = hideElements;
            APIStatusDesc.Visible = hideElements;
            APIStatusIcon.Visible = hideElements;
        }

        private void LoginFormElements(bool hideElements = false)
        {
            if (hideElements)
            {
                currentWindowInfo.Text = "Enter Your Account Information to Log In".ToUpper();
            }

            rememberMe.Visible = hideElements;
            loginButton.Visible = hideElements;
            ServerStatusText.Visible = hideElements;
            ServerStatusIcon.Visible = hideElements;
            ServerStatusDesc.Visible = hideElements;
            launcherIconStatus.Visible = hideElements;
            launcherStatusDesc.Visible = hideElements;
            launcherStatusText.Visible = hideElements;

            APIStatusText.Visible = hideElements;
            APIStatusDesc.Visible = hideElements;
            APIStatusIcon.Visible = hideElements;

            registerText.Visible = hideElements;
            serverPick.Visible = hideElements;
            email.Visible = hideElements;
            password.Visible = hideElements;
            forgotPassword.Visible = hideElements;
            settingsButton.Visible = hideElements;
            verticalBanner.Visible = hideElements;
            playProgressText.Visible = hideElements;
            playProgressTextTimer.Visible = hideElements;
            playProgress.Visible = hideElements;
            extractingProgress.Visible = hideElements;
            addServer.Visible = hideElements;
            addServer.Enabled = true;
            //allowedCountriesLabel.Visible = hideElements;
            serverPick.Enabled = true;

            //Input Strokes
            MainEmailBorder.Visible = hideElements;
            MainEmailBorder.Image = Properties.Resources.email_text_border;
            MainPasswordBorder.Visible = hideElements;
            MainPasswordBorder.Image = Properties.Resources.password_text_border;
        }

        private void RegisterFormElements(bool hideElements = true) {

            RegisterPanel.Visible = hideElements;
            registerTicket.Visible = (_ticketRequired) ? hideElements : false;

            verticalBanner.Visible = hideElements;
            extractingProgress.Visible = hideElements;
            playProgress.Visible = hideElements;
            playProgressText.Visible = hideElements;

            ServerStatusText.Visible = hideElements;
            ServerStatusIcon.Visible = hideElements;
            ServerStatusDesc.Visible = hideElements;
            launcherIconStatus.Visible = hideElements;
            launcherStatusDesc.Visible = hideElements;
            launcherStatusText.Visible = hideElements;

            APIStatusText.Visible = hideElements;
            APIStatusDesc.Visible = hideElements;
            APIStatusIcon.Visible = hideElements;

            addServer.Visible = hideElements;
            addServer.Enabled = false;
            serverPick.Visible = hideElements;
            serverPick.Enabled = false;

            // Reset fields
            registerEmail.Text = "";
            registerPassword.Text = "";
            registerConfirmPassword.Text = "";
            registerAgree.Checked = false;

            //Input Strokes
            RegisterEmailBorder.Visible = hideElements;
            RegisterPasswordBorder.Visible = hideElements;
            RegisterPasswordValidateBorder.Visible = hideElements;
            RegisterTicketBorder.Visible = (_ticketRequired) ? hideElements : false;
        }

        private void LogoutButton_Click(object sender, EventArgs e) {
            if(_disableLogout == true) {
                return;
            }
            BackgroundImage = Properties.Resources.mainbackground;
            _loggedIn = false;
            LoggedInFormElements(false);
            LoginFormElements(true);

            _userId = String.Empty;
            _loginToken = String.Empty;
        }

        private void Greenbutton_hover_MouseEnter(object sender, EventArgs e)
        {
            settingsSave.Image = Properties.Resources.greenbutton_hover;
            registerText.Image = Properties.Resources.greenbutton_hover;
            registerButton.Image = Properties.Resources.greenbutton_hover;
        }

        private void Greenbutton_MouseLeave(object sender, EventArgs e)
        {
            settingsSave.Image = Properties.Resources.greenbutton;
            registerText.Image = Properties.Resources.greenbutton;
            registerButton.Image = Properties.Resources.greenbutton;
        }

        private void Greenbutton_hover_MouseUp(object sender, EventArgs e)
        {
            settingsSave.Image = Properties.Resources.greenbutton_hover;
            registerText.Image = Properties.Resources.greenbutton_hover;
            registerButton.Image = Properties.Resources.greenbutton_hover;
        }

        private void Greenbutton_click_MouseDown(object sender, EventArgs e)
        {
            settingsSave.Image = Properties.Resources.greenbutton_click;
            registerText.Image = Properties.Resources.greenbutton_click;
            registerButton.Image = Properties.Resources.greenbutton_click;
        }

        private void RegisterCancel_Click(object sender, EventArgs e)
        {
            BackgroundImage = Properties.Resources.mainbackground;
            currentWindowInfo.Text = "Enter your account information to Log In:".ToUpper();
            RegisterFormElements(false);
            LoginFormElements(true);
            ResetRegisterErrorColors();
        }

        private void ResetRegisterErrorColors()
        {
            registerAgree.ForeColor = Color.White;
            //Reset Input Stroke Images
            RegisterEmailBorder.Image = Properties.Resources.email_text_border;
            RegisterPasswordBorder.Image = Properties.Resources.password_text_border;
            RegisterPasswordValidateBorder.Image = Properties.Resources.password_text_border;
            RegisterTicketBorder.Image = Properties.Resources.ticket_text_border;
        }

        private void RegisterAgree_CheckedChanged(object sender, EventArgs e)
        {
            registerAgree.ForeColor = Color.White;
        }

        private void RegisterEmail_TextChanged(object sender, EventArgs e)
        {
            RegisterEmailBorder.Image = Properties.Resources.email_text_border;
        }

        private void RegisterTicket_TextChanged(object sender, EventArgs e)
        {
            RegisterTicketBorder.Image = Properties.Resources.ticket_text_border;
        }

        private void RegisterConfirmPassword_TextChanged(object sender, EventArgs e)
        {
            RegisterPasswordValidateBorder.Image = Properties.Resources.password_text_border;
        }

        private void RegisterPassword_TextChanged(object sender, EventArgs e)
        {
            RegisterPasswordBorder.Image = Properties.Resources.password_text_border;
        }

        private void Email_TextChanged(object sender, EventArgs e)
        {
            MainEmailBorder.Image = Properties.Resources.email_text_border;
        }

        private void Password_TextChanged(object sender, EventArgs e)
        {
            MainEmailBorder.Image = Properties.Resources.email_text_border;
            MainPasswordBorder.Image = Properties.Resources.password_text_border;
        }

        private void Graybutton_click_MouseDown(object sender, EventArgs e)
        {
            settingsCancel.Image = Properties.Resources.graybutton_click;
            logoutButton.Image = Properties.Resources.graybutton_click;
            registerCancel.Image = Properties.Resources.graybutton_click;
        }

        private void Graybutton_hover_MouseEnter(object sender, EventArgs e)
        {
            settingsCancel.Image = Properties.Resources.graybutton_hover;
            logoutButton.Image = Properties.Resources.graybutton_hover;
            registerCancel.Image = Properties.Resources.graybutton_hover;
        }

        private void Graybutton_MouseLeave(object sender, EventArgs e)
        {
            settingsCancel.Image = Properties.Resources.graybutton;
            logoutButton.Image = Properties.Resources.graybutton;
            registerCancel.Image = Properties.Resources.graybutton;
        }

        private void Graybutton_hover_MouseUp(object sender, EventArgs e)
        {
            settingsCancel.Image = Properties.Resources.graybutton_hover;
            logoutButton.Image = Properties.Resources.graybutton_hover;
            registerCancel.Image = Properties.Resources.graybutton_hover;
        }

        public void DrawErrorAroundTextBox(TextBox x)
        {
            x.BorderStyle = BorderStyle.FixedSingle;
            var p = new Pen(Color.Red);
            var g = CreateGraphics();
            var variance = 1;
            g.DrawRectangle(p, new Rectangle(x.Location.X - variance, x.Location.Y - variance, x.Width + variance, x.Height + variance));
        }

        private void RegisterButton_Click(object sender, EventArgs e) {
            Refresh();

            List<string> registerErrors = new List<string>(); 

            if (string.IsNullOrEmpty(registerEmail.Text)) {
                registerErrors.Add("Please enter your e-mail.");
                RegisterEmailBorder.Image = Properties.Resources.email_error_text_border;

            } else if (Self.ValidateEmail(registerEmail.Text) == false) {
                registerErrors.Add("Please enter a valid e-mail address.");
                RegisterEmailBorder.Image = Properties.Resources.email_error_text_border;
            }

            if (string.IsNullOrEmpty(registerTicket.Text) && _ticketRequired) {
                registerErrors.Add("Please enter your ticket.");
                RegisterTicketBorder.Image = Properties.Resources.ticket_error_text_border;
            }

            if (string.IsNullOrEmpty(registerPassword.Text)) {
                registerErrors.Add("Please enter your password.");
                RegisterPasswordBorder.Image = Properties.Resources.password_error_text_border;
            }

            if (string.IsNullOrEmpty(registerConfirmPassword.Text)) {
                registerErrors.Add("Please confirm your password.");
                RegisterPasswordValidateBorder.Image = Properties.Resources.password_error_text_border;
            }

            if (registerConfirmPassword.Text != registerPassword.Text) {
                registerErrors.Add("Passwords don't match.");
                RegisterPasswordBorder.Visible = true;
                RegisterPasswordValidateBorder.Image = Properties.Resources.password_error_text_border;
            }

            if (!registerAgree.Checked) {
                registerErrors.Add("You have not agreed to the Terms of Service.");
                registerAgree.ForeColor = Color.FromArgb(254, 0, 0);
            }

            if (registerErrors.Count == 0) {
                bool allowReg = false;

                try {
                    WebClient breachCheck = new WebClient();
                    String checkPassword = SHA.HashPassword(registerPassword.Text.ToString()).ToUpper();

                    var regex = new Regex(@"([0-9A-Z]{5})([0-9A-Z]{35})").Split(checkPassword);

                    String range = regex[1];
                    String verify = regex[2];
                    String serverReply = breachCheck.DownloadString("https://api.pwnedpasswords.com/range/"+range);

                    string[] hashes = serverReply.Split('\n');
                    foreach (string hash in hashes) {
                        var splitChecks = hash.Split(':');
                        if(splitChecks[0] == verify) {
                            var passwordCheckReply = MessageBox.Show(null, "Password used for registration has been breached " + Convert.ToInt32(splitChecks[1])+ " times, you should consider using different one.\r\nAlternatively you can use unsafe password anyway. Use it?", "GameLauncher", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                            if(passwordCheckReply == DialogResult.Yes) {
                                allowReg = true;
                            } else {
                                allowReg = false;
                            }
                        } else {
                            allowReg = true;
                        }
                    }
                } catch {
                    allowReg = true;
                }

                if(allowReg == true) {
                    Tokens.Clear();

                    String username = registerEmail.Text.ToString();
                    String realpass;
                    String token = (_ticketRequired) ? registerTicket.Text : null;

                    Tokens.IPAddress = _serverInfo.IpAddress;
                    Tokens.ServerName = _serverInfo.Name;

                    if (_modernAuthSupport == false) {
                        realpass = SHA.HashPassword(registerPassword.Text.ToString()).ToLower();
                        ClassicAuth.Register(username, realpass, token);
                    } else {
                        realpass = registerPassword.Text.ToString();
                        ModernAuth.Register(username, realpass, token);
                    }

                    if (!String.IsNullOrEmpty(Tokens.Success)) {
                        _loggedIn = true;
                        _userId = Tokens.UserId;
                        _loginToken = Tokens.LoginToken;
                        _serverIp = Tokens.IPAddress;

                        MessageBox.Show(null, Tokens.Success, "GameLauncher", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        ResetRegisterErrorColors();

                        BackgroundImage = Properties.Resources.mainbackground;

                        RegisterFormElements(false);
                        LoginFormElements(true);

                        _loggedIn = true;
                    } else {
                        MessageBox.Show(null, Tokens.Error, "GameLauncher", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                } else {
                    var message = "There were some errors while registering, please fix them:\n\n";

                    foreach (var error in registerErrors) {
                        message += "• " + error + "\n";
                    }

                    MessageBox.Show(null, message, "GameLauncher", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /*
         * SETTINGS PAGE LAYOUT
         */

        private void SettingsButton_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                WindowState = FormWindowState.Normal;
            }

            BackgroundImage = Properties.Resources.secondarybackground;
            SettingsFormElements(true);
            RegisterFormElements(false);
            LoggedInFormElements(false);
            LoginFormElements(false);
            IsCDNDownGame();
            PingAPIStatus();
            //Hide Social Panel
            ServerInfoPanel.Visible = false;

            if (File.Exists(_settingFile.Read("InstallationDirectory") + "/NFSWO_COMMUNICATION_LOG.txt"))
            {
                SettingsClearCommunicationLogButton.Enabled = true;
            }

            var crashLogFilesDirectory = new DirectoryInfo(_settingFile.Read("InstallationDirectory"));

            foreach (var file in crashLogFilesDirectory.EnumerateFiles("SBRCrashDump_CL0*.dmp"))
            {
                SettingsClearCrashLogsButton.Enabled = true;
            }

        }
        private void CDN_Offline_Switch()
        {
            if (WindowState == FormWindowState.Minimized)
            {
                WindowState = FormWindowState.Normal;
            }

            BackgroundImage = Properties.Resources.secondarybackground;
            SettingsFormElements(true);
            RegisterFormElements(false);
            LoggedInFormElements(false);
            LoginFormElements(false);
            IsCDNDownGame();
            PingAPIStatus();
        }

        private void SettingsButton_MouseEnter(object sender, EventArgs e) {

        }

        private void SettingsButton_MouseLeave(object sender, EventArgs e) {
            settingsButton.BackgroundImage = Properties.Resources.settingsbtn;
        }

        private void Logo_MouseLeave(object sender, EventArgs e)
        {
            logo.Image = Properties.Resources.logo;
        }

        private void Logo_MouseEnter(object sender, EventArgs e)
        {
            logo.Image = Properties.Resources.logo_hover;
        }

        private void SettingsCancel_Click(object sender, EventArgs e)
        {
            SettingsFormElements(false);
            LoggedInFormElements(false);
            LoginFormElements(true);
            BackgroundImage = Properties.Resources.mainbackground;
            //Show Social Panel
            ServerInfoPanel.Visible = true;
        }

        public void ClearColoredPingStatus()
        {
            //Reset Connection Status Labels - DavidCarbon
            settingsMainSrvText.Text = "Main Server List API: PINGING";
            settingsMainSrvText.ForeColor = Color.FromArgb(66, 179, 189);
            settingsMainCDNText.Text = "Main CDN List API: PINGING";
            settingsMainCDNText.ForeColor = Color.FromArgb(66, 179, 189);
            settingsBkupSrvText.Text = "Backup Server List API: PINGING";
            settingsBkupSrvText.ForeColor = Color.FromArgb(66, 179, 189);
            settingsBkupCDNText.Text = "Backup CDN List API: PINGING";
            settingsBkupCDNText.ForeColor = Color.FromArgb(66, 179, 189);
        }

        private void SettingsSave_Click(object sender, EventArgs e) {

            //TODO null check
            _settingFile.Write("Language", settingsLanguage.SelectedValue.ToString());

            var userSettingsXml = new XmlDocument();

            try { 
                if (File.Exists(_userSettings)) {
                    try  {
                        userSettingsXml.Load(_userSettings);
                        var language = userSettingsXml.SelectSingleNode("Settings/UI/Language");
                        language.InnerText = settingsLanguage.SelectedValue.ToString();
                    } catch {
                        File.Delete(_userSettings);

                        var setting = userSettingsXml.AppendChild(userSettingsXml.CreateElement("Settings"));
                        var ui = setting.AppendChild(userSettingsXml.CreateElement("UI"));

                        var persistentValue = setting.AppendChild(userSettingsXml.CreateElement("PersistentValue"));
                        var chat = persistentValue.AppendChild(userSettingsXml.CreateElement("Chat"));
                        chat.InnerXml = "<DefaultChatGroup Type=\"string\">" + settingsLanguage.SelectedValue + "</DefaultChatGroup>";
                        ui.InnerXml = "<Language Type=\"string\">" + settingsLanguage.SelectedValue + "</Language>";

                        var directoryInfo = Directory.CreateDirectory(Path.GetDirectoryName(_userSettings));
                    }
                } else {
                    try {
                        var setting = userSettingsXml.AppendChild(userSettingsXml.CreateElement("Settings"));
                        var ui = setting.AppendChild(userSettingsXml.CreateElement("UI"));

                        var persistentValue = setting.AppendChild(userSettingsXml.CreateElement("PersistentValue"));
                        var chat = persistentValue.AppendChild(userSettingsXml.CreateElement("Chat"));
                        chat.InnerXml = "<DefaultChatGroup Type=\"string\">" + settingsLanguage.SelectedValue + "</DefaultChatGroup>";
                        ui.InnerXml = "<Language Type=\"string\">" + settingsLanguage.SelectedValue + "</Language>";

                        var directoryInfo = Directory.CreateDirectory(Path.GetDirectoryName(_userSettings));
                    } catch (Exception ex) {
                        MessageBox.Show(null, "There was an error saving your settings to actual file. Restoring default.\n" + ex.Message, "GameLauncher", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        File.Delete(_userSettings);
                    }
                }
            } catch(Exception ex) {
                MessageBox.Show(null, "There was an error saving your settings to actual file. Restoring default.\n" + ex.Message, "GameLauncher", MessageBoxButtons.OK, MessageBoxIcon.Error);
                File.Delete(_userSettings);
            }

            userSettingsXml.Save(_userSettings);

            if (WindowsProductVersion.GetWindowsNumber() >= 10.0 && (_settingFile.Read("InstallationDirectory") != _newGameFilesPath))
            {
                WindowsDefenderGameFilesDirctoryChange();
            }
            else if (_settingFile.Read("InstallationDirectory") != _newGameFilesPath)
            {
                _settingFile.Write("InstallationDirectory", _newGameFilesPath);
                _restartRequired = true;
                //Clean Mods Files from New Dirctory (If it has .links in directory)
                var linksPath = Path.Combine(_settingFile.Read("InstallationDirectory"), ".links");
                if (File.Exists(linksPath))
                {
                    Log.Debug("CLEANLINKS: Cleaning Up Mod Files {Settings}");
                    CleanLinks(linksPath);
                }
            }

            if (_settingFile.Read("CDN") != ((CDNObject)settingsCDNPick.SelectedItem).Url)
            {
                settingsCDNCurrentText.Text = "CHANGED CDN";
                settingsCDNCurrent.Text = ((CDNObject)settingsCDNPick.SelectedItem).Url;
                _settingFile.Write("CDN", ((CDNObject)settingsCDNPick.SelectedItem).Url);
                _restartRequired = true;
            }

            String disableProxy = (settingsProxyCheckbox.Checked == true) ? "1" : "0";
            if (_settingFile.Read("DisableProxy") != disableProxy) {
                _settingFile.Write("DisableProxy", (settingsProxyCheckbox.Checked == true) ? "1" : "0");
                _restartRequired = true;
            }

            String disableRPC = (settingsDiscordRPCCheckbox.Checked == true) ? "1" : "0";
            if (_settingFile.Read("DisableRPC") != disableRPC)
            {
                _settingFile.Write("DisableRPC", (settingsDiscordRPCCheckbox.Checked == true) ? "1" : "0");
                _restartRequired = true;
            }


            if (_restartRequired) {
                MessageBox.Show(null, "In order to see settings changes, you need to restart launcher manually.", "GameLauncher", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            //Actually lets check those 2 files
            if(File.Exists(_settingFile.Read("InstallationDirectory") + "/profwords") && File.Exists(_settingFile.Read("InstallationDirectory") + "/profwords_dis")) {
                File.Delete(_settingFile.Read("InstallationDirectory") + "/profwords_dis");
            }

            //Delete/Enable profwords filter here
            if (settingsWordFilterCheck.Checked) {
                if (File.Exists(_settingFile.Read("InstallationDirectory") + "/profwords")) File.Move(_settingFile.Read("InstallationDirectory") + "/profwords", _settingFile.Read("InstallationDirectory") + "/profwords_dis");
            } else {
                if (File.Exists(_settingFile.Read("InstallationDirectory") + "/profwords_dis")) File.Move(_settingFile.Read("InstallationDirectory") + "/profwords_dis", _settingFile.Read("InstallationDirectory") + "/profwords");
            }

            SettingsFormElements(false);
            LoggedInFormElements(false);
            LoginFormElements(true);
            BackgroundImage = Properties.Resources.mainbackground;
            //Show Social Panel
            ServerInfoPanel.Visible = true;
        }

        //Changing GameFiles Location from Settings - DavidCarbon & Zacam
        private void SettingsGameFiles_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.OpenFileDialog changeGameFilesPath = new System.Windows.Forms.OpenFileDialog();
            changeGameFilesPath.InitialDirectory = "C:\\";
            changeGameFilesPath.ValidateNames = false;
            changeGameFilesPath.CheckFileExists = false;
            changeGameFilesPath.CheckPathExists = true;
            changeGameFilesPath.Title = "Select the location to Find or Download nfsw.exe";
            changeGameFilesPath.FileName = "Select Game Files Folder";
            if (changeGameFilesPath.ShowDialog() == DialogResult.OK)
            {
                _newGameFilesPath = Path.GetDirectoryName(changeGameFilesPath.FileName);
                settingsGameFilesCurrentText.Text = "NEW DIRECTORY";
                settingsGameFilesCurrent.Text = _newGameFilesPath;
            }
        }

        private void SettingsLauncherPathCurrent_Click(object sender, EventArgs e) {
            Process.Start(_newLauncherPath);
        }

        private void SettingsGameFilesCurrent_Click(object sender, EventArgs e) {
            Process.Start(_newGameFilesPath);
        }

        private void SettingsCDNCurrent_LinkClicked(object sender, EventArgs e)
        {
            Process.Start(_settingFile.Read("CDN"));
        }

        private void SettingsFormElements(bool hideElements = true) {
            if (hideElements) {
                currentWindowInfo.Text = "";
            }

            SettingsPanel.Visible = hideElements;
        }

        private void StartGame(string userId, string loginToken) {

            if(UriScheme.ServerIP != String.Empty) {
                _serverIp = UriScheme.ServerIP;
            }

            if(_realServername == "Freeroam Sparkserver") {
                //Force proxy enabled.
                Log.Info("LAUNCHER: Forcing Proxified connection for FRSS");
                _disableProxy = false;
            }

            _nfswstarted = new Thread(() => {
                if(_disableProxy == true) {
                    if (_disableDiscordRPC == true)
                    {
                        discordRpcClient.Dispose();
                        discordRpcClient = null;
                    }

                    Uri convert = new Uri(_serverIp);

                    if(convert.Scheme == "http") {
                        Match match = Regex.Match(convert.Host, @"\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}");
                        if (!match.Success) {
                            _serverIp = _serverIp.Replace(convert.Host, Self.HostName2IP(convert.Host));
                        }
                    }

                    LaunchGame(userId, loginToken, _serverIp, this);
                } else {
                    if (_disableDiscordRPC == true)
                    {
                        discordRpcClient.Dispose();
                        discordRpcClient = null;
                    }
                    LaunchGame(userId, loginToken, "http://127.0.0.1:" + Self.ProxyPort + "/nfsw/Engine.svc", this);
                }
            }) { IsBackground = true };

            _nfswstarted.Start();

            if (_disableDiscordRPC == false)
            {
                _presenceImageKey = _serverInfo.DiscordPresenceKey;
                _presence.State = _realServername;
                _presence.Details = "In-Game";
                _presence.Assets = new Assets
                {
                    LargeImageText = "Need for Speed: World",
                    LargeImageKey = "nfsw",
                    SmallImageText = _realServername,
                    SmallImageKey = _presenceImageKey
                };

                if(discordRpcClient != null) discordRpcClient.SetPresence(_presence);
            }

        }

        //DavidCarbon
        private async void PingAPIStatus ()
        {
            ClearColoredPingStatus();
            Log.Debug("SETTINGS PINGING API: Checking APIs");
            await Task.Delay(500);
            HttpWebRequest requestMainServerList = (HttpWebRequest)HttpWebRequest.Create(Self.mainserver + "/serverlist.json");
            requestMainServerList.AllowAutoRedirect = false; // Find out if this site is up and don't follow a redirector
            requestMainServerList.Method = "HEAD";
            requestMainServerList.UserAgent = "GameLauncher (+https://github.com/SoapBoxRaceWorld/GameLauncher_NFSW)";
            try
            {
                HttpWebResponse mainServerListResponse = (HttpWebResponse)requestMainServerList.GetResponse();
                mainServerListResponse.Close();
                settingsMainSrvText.Text = "Main Server List API: ONLINE";
                settingsMainSrvText.ForeColor = Color.FromArgb(159, 193, 32);
                Log.Debug("SETTINGS PINGING API: Main Server List Online");
                //Do something with response.Headers to find out information about the request
            }
            catch (WebException)
            {
                settingsMainSrvText.Text = "Main Server List API: ERROR";
                settingsMainSrvText.ForeColor = Color.FromArgb(254, 0, 0);
                Log.Debug("SETTINGS PINGING API: Main Server List Failed to Connect");
                //Set flag if there was a timeout or some other issues
            }

            await Task.Delay(1000);
            HttpWebRequest requestBkupServerList = (HttpWebRequest)HttpWebRequest.Create(Self.staticapiserver + "/serverlist.json");
            requestBkupServerList.AllowAutoRedirect = false;
            requestBkupServerList.Method = "HEAD";
            requestBkupServerList.UserAgent = "GameLauncher (+https://github.com/SoapBoxRaceWorld/GameLauncher_NFSW)";
            try
            {
                HttpWebResponse bkupServerListResponse = (HttpWebResponse)requestBkupServerList.GetResponse();
                bkupServerListResponse.Close();
                settingsBkupSrvText.Text = "Backup Server List API: ONLINE";
                settingsBkupSrvText.ForeColor = Color.FromArgb(159, 193, 32);
                Log.Debug("SETTINGS PINGING API: Backup Server List Online");
            }
            catch (WebException)
            {
                settingsBkupSrvText.Text = "Backup Server List API: ERROR";
                settingsBkupSrvText.ForeColor = Color.FromArgb(254, 0, 0);
                Log.Debug("SETTINGS PINGING API: Backup Server List failed to Connect");
            }

            await Task.Delay(1500);
            HttpWebRequest requestMainCDNList = (HttpWebRequest)HttpWebRequest.Create(Self.mainserver + "/cdn_list.json");
            requestMainCDNList.AllowAutoRedirect = false;
            requestMainCDNList.Method = "HEAD";
            requestMainCDNList.UserAgent = "GameLauncher (+https://github.com/SoapBoxRaceWorld/GameLauncher_NFSW)";
            try
            {
                HttpWebResponse mainCDNListResponse = (HttpWebResponse)requestMainCDNList.GetResponse();
                mainCDNListResponse.Close();
                settingsMainCDNText.Text = "Main CDN List API: ONLINE";
                settingsMainCDNText.ForeColor = Color.FromArgb(159, 193, 32);
                Log.Debug("SETTINGS PINGING API: Main CDN List Online");
            }
            catch (WebException)
            {
                settingsMainCDNText.Text = "Main CDN List API: ERROR";
                settingsMainCDNText.ForeColor = Color.FromArgb(254, 0, 0);
                Log.Debug("SETTINGS PINGING API: Main CDN List failed to Connect");
            }

            await Task.Delay(2000);
            HttpWebRequest requestBkupCDNList = (HttpWebRequest)HttpWebRequest.Create(Self.staticapiserver + "/cdn_list.json");
            requestBkupCDNList.AllowAutoRedirect = false;
            requestBkupCDNList.Method = "HEAD";
            requestBkupCDNList.UserAgent = "GameLauncher (+https://github.com/SoapBoxRaceWorld/GameLauncher_NFSW)";
            try
            {
                HttpWebResponse bkupCDNListResponse = (HttpWebResponse)requestBkupCDNList.GetResponse();
                bkupCDNListResponse.Close();
                settingsBkupCDNText.Text = "Backup CDN List API: ONLINE";
                settingsBkupCDNText.ForeColor = Color.FromArgb(159, 193, 32);
                Log.Debug("SETTINGS PINGING API: Backup CDN List Online");
            }
            catch (WebException)
            {
                settingsBkupCDNText.Text = "Backup CDN List API: ERROR";
                settingsBkupCDNText.ForeColor = Color.FromArgb(254, 0, 0);
                Log.Debug("SETTINGS PINGING API: Backup CDN List failed to Connect");
            }

        }

        //Check Serverlist API Status Upon Main Screen load - DavidCarbon
        private async void PingServerListAPIStatus()
        {
            Log.Debug("PINGING API: Checking API Status");
            HttpWebRequest requestMainServerListAPI = (HttpWebRequest)HttpWebRequest.Create(Self.mainserver + "/serverlist.json");
            requestMainServerListAPI.AllowAutoRedirect = false;
            requestMainServerListAPI.Method = "HEAD";
            requestMainServerListAPI.UserAgent = "GameLauncher (+https://github.com/SoapBoxRaceWorld/GameLauncher_NFSW)";
            try
            {
                HttpWebResponse mainServerListResponseAPI = (HttpWebResponse)requestMainServerListAPI.GetResponse();
                mainServerListResponseAPI.Close();
                APIStatusText.Text = "Main API - Online";
                APIStatusText.ForeColor = Color.FromArgb(159, 193, 32);
                APIStatusDesc.Text = "Connected to Main API";
                APIStatusIcon.Image = Properties.Resources.api_success;
                Log.Debug("PINGING API: Main Server has responded. Its Online!");
            }
            catch (WebException)
            {
                APIStatusText.Text = "Main API - Offline";
                APIStatusText.ForeColor = Color.FromArgb(254, 0, 0);
                APIStatusDesc.Text = "Checking to Backup API";
                APIStatusIcon.Image = Properties.Resources.api_error;
                Log.Debug("PINGING API: Main Server has responded. Its Offline! Checking Backup...");

                await Task.Delay(1500);
                APIStatusText.Text = "Backup API - Pinging";
                APIStatusText.ForeColor = Color.FromArgb(66, 179, 189);
                APIStatusIcon.Image = Properties.Resources.api_checking;
                
                await Task.Delay(1500);
                try
                {
                    //Check Using Backup API
                    HttpWebRequest requestBkupServerListAPI = (HttpWebRequest)HttpWebRequest.Create(Self.staticapiserver + "/serverlist.json");
                    requestBkupServerListAPI.AllowAutoRedirect = false;
                    requestBkupServerListAPI.Method = "HEAD";
                    requestBkupServerListAPI.UserAgent = "GameLauncher (+https://github.com/SoapBoxRaceWorld/GameLauncher_NFSW)";
                    try
                    {
                        HttpWebResponse bkupServerListResponseAPI = (HttpWebResponse)requestBkupServerListAPI.GetResponse();
                        bkupServerListResponseAPI.Close();
                        APIStatusText.Text = "Backup API - Online";
                        APIStatusText.ForeColor = Color.FromArgb(159, 193, 32);
                        APIStatusDesc.Text = "Connected to Backup API";
                        APIStatusIcon.Image = Properties.Resources.api_success;
                        Log.Debug("PINGING API: Backup Server has responded. Its Online!");
                    }
                    catch (WebException)
                    {
                        APIStatusText.Text = "Connection API - Error";
                        APIStatusText.ForeColor = Color.FromArgb(254, 0, 0);
                        APIStatusDesc.Text = "Failed to Connect to APIs";
                        APIStatusIcon.Image = Properties.Resources.api_error;
                        Log.Debug("PINGING API: Failed to Connect to APIs! Quick Hide and Bunker Down! (Ask for help)");
                    }
                }
                catch { }
            }
        }

        //CDN Display Playing Game! - DavidCarbon
        private async void IsCDNDownGame()
        {
            settingsCDNCurrent.LinkColor = Color.FromArgb(66, 179, 189);
            Log.Debug("SETTINGS PINGING CDN: Checking Current CDN from Settings.ini");
            await Task.Delay(500);
            HttpWebRequest pingCurrentCDN = (HttpWebRequest)HttpWebRequest.Create(_settingFile.Read("CDN"));
            pingCurrentCDN.AllowAutoRedirect = false;
            pingCurrentCDN.Method = "HEAD";
            pingCurrentCDN.UserAgent = "GameLauncher (+https://github.com/SoapBoxRaceWorld/GameLauncher_NFSW)";
            try
            {
                HttpWebResponse cdnResponse = (HttpWebResponse)pingCurrentCDN.GetResponse();
                cdnResponse.Close();
                settingsCDNCurrent.LinkColor = Color.FromArgb(159, 193, 32);
                Log.Debug("SETTINGS PINGING CDN: " + _settingFile.Read("CDN") + " Is Online!");
            }
            catch (WebException)
            {
                settingsCDNCurrent.LinkColor = Color.FromArgb(254, 0, 0);
                Log.Debug("SETTINGS PINGING CDN: " + _settingFile.Read("CDN") + " Is Offline!");
            }

        }

        private async void IsChangedCDNDown()
        {
            settingsCDNText.Text = "CDN: PINGING";
            settingsCDNText.ForeColor = Color.FromArgb(66, 179, 189);
            Log.Debug("SETTINGS PINGING CHANGED CDN: Checking Changed CDN from Drop Down List");
            await Task.Delay(500);
            HttpWebRequest pingCurrentCDN = (HttpWebRequest)HttpWebRequest.Create(((CDNObject)settingsCDNPick.SelectedItem).Url);
            pingCurrentCDN.AllowAutoRedirect = false;
            pingCurrentCDN.Method = "HEAD";
            pingCurrentCDN.UserAgent = "GameLauncher (+https://github.com/SoapBoxRaceWorld/GameLauncher_NFSW)";
            try
            {
                HttpWebResponse cdnResponse = (HttpWebResponse)pingCurrentCDN.GetResponse();
                cdnResponse.Close();
                settingsCDNText.Text = "CDN: ONLINE";
                settingsCDNText.ForeColor = Color.FromArgb(159, 193, 32);
                Log.Debug("SETTINGS PINGING CHANGED CDN: " + ((CDNObject)settingsCDNPick.SelectedItem).Url + " Is Online!");
            }
            catch (WebException)
            {
                settingsCDNText.Text = "CDN: OFFLINE";
                settingsCDNText.ForeColor = Color.FromArgb(254, 0, 0);
                Log.Debug("SETTINGS PINGING CHANGED CDN: " + ((CDNObject)settingsCDNPick.SelectedItem).Url + " Is Offline!");
            }

        }

        private void LaunchGame(string userId, string loginToken, string serverIp, Form x) {
            var oldfilename = _settingFile.Read("InstallationDirectory") + "/nfsw.exe";

            var args = _serverInfo.Id.ToUpper() + " " + serverIp + " " + loginToken + " " + userId;
            var psi = new ProcessStartInfo();

            if(DetectLinux.LinuxDetected()) { 
                psi.UseShellExecute = false;
            }

            psi.WorkingDirectory = _settingFile.Read("InstallationDirectory");
            psi.FileName = oldfilename;
            psi.Arguments = args;

            var nfswProcess = Process.Start(psi);
            nfswProcess.PriorityClass = ProcessPriorityClass.AboveNormal;

            var processorAffinity = 0;
            for (var i = 0; i < Math.Min(Math.Max(1, Environment.ProcessorCount), 8); i++)
            {
                processorAffinity |= 1 << i;
            }

            nfswProcess.ProcessorAffinity = (IntPtr)processorAffinity;

            AntiCheat.process_id = nfswProcess.Id;

            //TIMER HERE
            int secondsToShutDown = (json.SecondsToShutDown != 0) ? json.SecondsToShutDown : 2*60*60;
                System.Timers.Timer shutdowntimer = new System.Timers.Timer();
                shutdowntimer.Elapsed += (x2, y2) => {
                    if(secondsToShutDown == 300) {
                        Notification.Visible = true;
                        Notification.BalloonTipIcon = ToolTipIcon.Info;
                        Notification.BalloonTipTitle = "SpeedBug Fix - " + _realServername;
                        Notification.BalloonTipText = "Game is going to shut down in 5 minutes. Please restart it manually before the launcher does it.";
                        Notification.ShowBalloonTip(5000);
                        Notification.Dispose();
                    }

                    Process[] allOfThem = Process.GetProcessesByName("nfsw");

                    if (secondsToShutDown <= 0) {
                        if (Self.CanDisableGame == true) {
                            foreach (var oneProcess in allOfThem) {
                                _gameKilledBySpeedBugCheck = true;
                                Process.GetProcessById(oneProcess.Id).Kill();
                            }
                        } else {
                            secondsToShutDown = 0;
                        }
                    }

                    //change title

                    foreach (var oneProcess in allOfThem) {
                        //if (oneProcess.ProcessName == "nfsw") {
                            long p = oneProcess.MainWindowHandle.ToInt64();
                            TimeSpan t = TimeSpan.FromSeconds(secondsToShutDown);
                            string secondsToShutDownNamed = string.Format("{0:D2}:{1:D2}:{2:D2}", t.Hours, t.Minutes, t.Seconds);

                            if (secondsToShutDown == 0) {
                                secondsToShutDownNamed = "Waiting for event to finish.";
                            }

                            User32.SetWindowText((IntPtr)p, "NEED FOR SPEED™ WORLD | Launcher Build: " + ProductVersion + " | Server: " + _realServername + " | Force Restart In: " + secondsToShutDownNamed);
                        //}
                    }

                    --secondsToShutDown;
                };

                shutdowntimer.Interval = 1000;
                shutdowntimer.Enabled = true;
            

                if (nfswProcess != null) {
                    nfswProcess.EnableRaisingEvents = true;
                    _nfswPid = nfswProcess.Id;

                    nfswProcess.Exited += (sender2, e2) => {
                        _nfswPid = 0;
                        var exitCode = nfswProcess.ExitCode;

                        if (_gameKilledBySpeedBugCheck == true) exitCode = 2137;

                        if (exitCode == 0) {
                            Closebtn_Click(null, null);
                        } else {
                            x.BeginInvoke(new Action(() => {
                                x.WindowState = FormWindowState.Normal;
                                x.Opacity = 1;
                                x.ShowInTaskbar = true;

                                String errorMsg = "Game Crash with exitcode: " + exitCode.ToString() + " (0x" + exitCode.ToString("X") + ")";
                                if (exitCode == -1073741819)    errorMsg = "Game Crash: Access Violation (0x" + exitCode.ToString("X") + ")";
                                if (exitCode == -1073740940)    errorMsg = "Game Crash: Heap Corruption (0x" + exitCode.ToString("X") + ")";
                                if (exitCode == -1073740791)    errorMsg = "Game Crash: Stack buffer overflow (0x" + exitCode.ToString("X") + ")";
                                if (exitCode == -805306369)     errorMsg = "Game Crash: Application Hang (0x" + exitCode.ToString("X") + ")";
                                if (exitCode == -1073741515)    errorMsg = "Game Crash: Missing dependency files (0x" + exitCode.ToString("X") + ")";
                                if (exitCode == -1073740972)    errorMsg = "Game Crash: Debugger crash (0x" + exitCode.ToString("X") + ")";
                                if (exitCode == -1073741676)    errorMsg = "Game Crash: Division by Zero (0x" + exitCode.ToString("X") + ")";

                                if (exitCode == 1)              errorMsg = "The process nfsw.exe was killed via Task Manager";
                                if (exitCode == 2137)           errorMsg = "Launcher killed your game to prevent SpeedBugging.";

                                if (exitCode == -3)             errorMsg = "The Server was unable to resolve the request";
                                if (exitCode == -4)             errorMsg = "Another instance is already executed";
                                if (exitCode == -5)             errorMsg = "DirectX Device was not found. Please install GPU Drivers before playing";
                                if (exitCode == -6)             errorMsg = "Server was unable to resolve your request";

                                //ModLoader
                                if (exitCode == 2)              errorMsg = "ModNet: Game was launched with invalid command line parameters.";
                                if (exitCode == 3)              errorMsg = "ModNet: .links file should not exist upon startup!";
                                if (exitCode == 4)              errorMsg = "ModNet: An Unhandled Error Appeared";

                                playProgressText.Text = errorMsg.ToUpper();
                                playProgress.Value = 100;
                                playProgress.ForeColor = Color.Red;

                                if (_nfswPid != 0) {
                                    try {
                                        Process.GetProcessById(_nfswPid).Kill();
                                    } catch { /* ignored */ }
                                }

                                _nfswstarted.Abort();

                                DialogResult restartApp = MessageBox.Show(null, errorMsg + "\nWould you like to restart the launcher?", "GameLauncher", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                                if(restartApp == DialogResult.Yes) {
                                    Properties.Settings.Default.IsRestarting = true;
                                    Properties.Settings.Default.Save();
                                    Application.Restart();
                                    Application.ExitThread();
                                }
                                this.Closebtn_Click(null, null);
                            }));
                        }
                    };
                }
            //}
        }

        public void DownloadModNetFilesRightNow(string path)
        {
            while (isDownloadingModNetFiles == false)
            {
                CurrentModFileCount++;
                var url = modFilesDownloadUrls.Dequeue();
                string FileName = url.ToString().Substring(url.ToString().LastIndexOf("/") + 1, (url.ToString().Length - url.ToString().LastIndexOf("/") - 1));

                ModNetFileNameInUse = FileName;

                WebClient client2 = new WebClient();

                client2.DownloadProgressChanged += new DownloadProgressChangedEventHandler(Client_DownloadProgressChanged_RELOADED);
                client2.DownloadFileCompleted += (test, stuff) => {
                    Log.Debug("LAUNCHER: Downloaded: " + FileName);
                    isDownloadingModNetFiles = false;
                    if (modFilesDownloadUrls.Any() == false)
                    {
                        LaunchGame();
                    }
                    else
                    {
                        //Redownload other file
                        DownloadModNetFilesRightNow(path);
                    }
                };
                client2.DownloadFileAsync(url, path + "/" + FileName);
                isDownloadingModNetFiles = true;
            }
        }

        private void PlayButton_Click(object sender, EventArgs e) {
            if(UriScheme.ForceGame != true) { 
                if (_loggedIn == false) {
                    if(_useSavedPassword == false) return;
                    LoginButton_Click(sender, e);
                }

                if (_playenabled == false) {
                    return;
                }
            } else {
                //set background black
                imageServerName.Text = "WebLogin";
                verticalBanner.Image = null;

                _userId = UriScheme.UserID;
                _loginToken = UriScheme.LoginToken;
                _serverIp = UriScheme.ServerIP;
            }

            _disableLogout = true;

            DisablePlayButton();

            if (!DetectLinux.LinuxDetected())
            {
                var installDir = _settingFile.Read("InstallationDirectory");
                DriveInfo driveInfo = new DriveInfo(installDir);

                if (!string.Equals(driveInfo.DriveFormat, "NTFS", StringComparison.InvariantCultureIgnoreCase))
                {
                    MessageBox.Show(
                        $"Playing the game on a non-NTFS-formatted drive is not supported.\nDrive '{driveInfo.Name}' is formatted with: {driveInfo.DriveFormat}", 
                        "Compatibility",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }
            }

            ModManager.ResetModDat(_settingFile.Read("InstallationDirectory"));

            if (Directory.Exists(_settingFile.Read("InstallationDirectory") + "/modules")) Directory.Delete(_settingFile.Read("InstallationDirectory") + "/modules", true);
            if (!Directory.Exists(_settingFile.Read("InstallationDirectory") + "/scripts")) Directory.CreateDirectory(_settingFile.Read("InstallationDirectory") + "/scripts");
            String[] ModNetReloadedFiles = new string[]
            {
                "dinput8.dll",
                "global.ini",
                "7z.dll", 
                "fmt.dll", 
                "libcurl.dll", 
                "zlib1.dll", 
                "ModLoader.asi"
            };

            playButton.BackgroundImage = Properties.Resources.playbutton;

            Log.Debug("LAUNCHER: Installing ModNet");
            playProgressText.Text = ("Detecting ModNetSupport for " + _realServernameBanner).ToUpper();
            String jsonModNet = ModNetReloaded.ModNetSupported(_serverIp);

            if (jsonModNet != String.Empty) {
                playProgressText.Text = "ModNetReloaded support detected, setting up...".ToUpper();

                try {
                    string[] newFiles = ModNetReloadedFiles.ToArray();
                    WebClient newModNetFilesDownload = new WebClient();
                    foreach (string file in newFiles) {
                        playProgressText.Text = ("Fetching ModNetReloaded Files: " + file).ToUpper();
                        Application.DoEvents();
                        newModNetFilesDownload.DownloadFile(Self.modnetserver + "/modules-v2/" + file, _settingFile.Read("InstallationDirectory") + "/" + file);
                    }

                    //get files now
                    MainJson json2 = JsonConvert.DeserializeObject<MainJson>(jsonModNet);

                    //metonator was here!
                    try
                    {
                        CarsList.remoteCarsList = new WebClient().DownloadString(json2.BasePath + "/cars.json");
                    }
                    catch { }
                    if (CarsList.remoteCarsList != String.Empty) { Log.Debug("DISCORD: Found RemoteRPC List for cars.json"); }
                    if (CarsList.remoteCarsList == String.Empty) { Log.Debug("DISCORD: RemoteRPC List for cars.json does not exist"); }

                    try
                    {
                        EventsList.remoteEventsList = new WebClient().DownloadString(json2.BasePath + "/events.json");
                    }
                     catch { }
                    if (EventsList.remoteEventsList != String.Empty) { Log.Debug("DISCORD: Found RemoteRPC List for events.json"); }
                    if (EventsList.remoteEventsList == String.Empty) { Log.Debug("DISCORD: RemoteRPC List for events.json does not exist"); }

                    //get new index
                    Uri newIndexFile = new Uri(json2.BasePath + "/index.json");
                    Log.Debug("CORE: Loading Server Mods List");
                    String jsonindex = new WebClient().DownloadString(newIndexFile);

                    IndexJson json3 = JsonConvert.DeserializeObject<IndexJson>(jsonindex);

                    int CountFilesTotal = 0;
                    CountFilesTotal = json3.Entries.Count;

                    String path = Path.Combine(_settingFile.Read("InstallationDirectory"), "MODS", MDFive.HashPassword(json2.ServerID).ToLower());
                    if(!Directory.Exists(path)) Directory.CreateDirectory(path);

                    foreach (IndexJsonEntry modfile in json3.Entries) {
                        if (SHA.HashFile(path + "/" + modfile.Name).ToLower() != modfile.Checksum) {
                            modFilesDownloadUrls.Enqueue(new Uri(json2.BasePath + "/" + modfile.Name));
                            TotalModFileCount++;
                        }
                    }

                    if (modFilesDownloadUrls.Count != 0) {
                        this.DownloadModNetFilesRightNow(path);
                    } else {
                        LaunchGame();
                    }

                    foreach (var file in Directory.GetFiles(path)) {
                        var name = Path.GetFileName(file);

                        if (json3.Entries.All(en => en.Name != name)) {
                            Log.Debug("LAUNCHER: removing package: " + file);
                            try { 
                                File.Delete(file);
                            } catch(Exception ex) {
                                Log.Error($"Failed to remove {file}: {ex.Message}");
                            }
                        }
                    }
                } catch(Exception ex) {
                    Log.Debug("LAUNCHER " + ex.Message);
                    MessageBox.Show(null, $"There was an error downloading ModNet Files:\n{ex.Message}", "GameLauncher", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            } else {
                //Rofl
                LaunchGame();
            }        
        }

        private static readonly object LinkCleanerLock = new object();

        private void CleanLinks(string linksPath)
        {
            lock (LinkCleanerLock)
            {
                if (File.Exists(linksPath))
                {
                    Log.Debug("CLEANLINKS: Found Server Mod Files to remove {Process}");
                    string dir = _settingFile.Read("InstallationDirectory");
                    foreach (var readLine in File.ReadLines(linksPath))
                    {
                        var parts = readLine.Split(new[] { '\t' }, StringSplitOptions.RemoveEmptyEntries);

                        if (parts.Length != 2)
                        {
                            continue;
                        }

                        string loc = parts[0];
                        int type = int.Parse(parts[1]);
                        string realLoc = Path.Combine(dir, loc);
                        if (type == 0)
                        {
                            if (!File.Exists(realLoc))
                            {
                                throw new Exception(".links file includes nonexistent file: " + realLoc);
                            }

                            string origPath = realLoc + ".orig";

                            if (!File.Exists(origPath)) {
                                File.Delete(realLoc);
                                continue;
                            }

                            File.Delete(realLoc);
                            File.Move(origPath, realLoc);
                        }
                        else
                        {
                            if (!Directory.Exists(realLoc))
                            {
                                throw new Exception(".links file includes nonexistent directory: " + realLoc);
                            }
                            Directory.Delete(realLoc, true);
                        }
                    }

                    File.Delete(linksPath);
                }
            }
        }

        void Client_DownloadProgressChanged_RELOADED(object sender, DownloadProgressChangedEventArgs e) {
            this.BeginInvoke((MethodInvoker)delegate {
                double bytesIn = double.Parse(e.BytesReceived.ToString());
                double totalBytes = double.Parse(e.TotalBytesToReceive.ToString());
                double percentage = bytesIn / totalBytes * 100;
                playProgressText.Text = ("["+CurrentModFileCount+" / "+TotalModFileCount+"] Downloading " + ModNetFileNameInUse + ": " + FormatFileSize(e.BytesReceived) + " of " + FormatFileSize(e.TotalBytesToReceive)).ToUpper();

                extractingProgress.Value = Convert.ToInt32(Decimal.Divide(e.BytesReceived, e.TotalBytesToReceive) * 100);
                extractingProgress.Width = Convert.ToInt32(Decimal.Divide(e.BytesReceived, e.TotalBytesToReceive) * 519);
            });
        }

        //Launch game
        public void LaunchGame() {
            if (_serverInfo.DiscordAppId != null) {
                discordRpcClient.Dispose();
                discordRpcClient = null;
                discordRpcClient = new DiscordRpcClient(_serverInfo.DiscordAppId);
                discordRpcClient.Initialize();
            }

            if ((_disableDiscordRPC == false) && ((ServerInfo)serverPick.SelectedItem).Category == "DEV") {
                discordRpcClient.Dispose();
                discordRpcClient = null;
            }

            try {
                if (
                    SHA.HashFile(_settingFile.Read("InstallationDirectory") + "/nfsw.exe") == "7C0D6EE08EB1EDA67D5E5087DDA3762182CDE4AC" ||
                    SHA.HashFile(_settingFile.Read("InstallationDirectory") + "/nfsw.exe") == "DB9287FB7B0CDA237A5C3885DD47A9FFDAEE1C19" ||
                    SHA.HashFile(_settingFile.Read("InstallationDirectory") + "/nfsw.exe") == "E69890D31919DE1649D319956560269DB88B8F22"
                ) {
                    ServerProxy.Instance.SetServerUrl(_serverIp);
                    ServerProxy.Instance.SetServerName(_realServername);

                    AntiCheat.user_id = _userId;
                    AntiCheat.serverip = new Uri(_serverIp).Host;

                    StartGame(_userId, _loginToken);

                    if (_builtinserver) {
                        playProgressText.Text = "Soapbox server launched. Waiting for queries.".ToUpper();
                    } else {
                        var secondsToCloseLauncher = 10;

                        extractingProgress.Value = 100;
                        extractingProgress.Width = 519;

                        while (secondsToCloseLauncher > 0) {
                            playProgressText.Text = string.Format("Loading game. Launcher will minimize in {0} seconds.", secondsToCloseLauncher).ToUpper(); //"LOADING GAME. LAUNCHER WILL MINIMIZE ITSELF IN " + secondsToCloseLauncher + " SECONDS";
                            Delay.WaitSeconds(1);
                            secondsToCloseLauncher--;
                        }

                        playProgressText.Text = "";

                        WindowState = FormWindowState.Minimized;
                        ShowInTaskbar = false;

                        ContextMenu = new ContextMenu();
                        ContextMenu.MenuItems.Add(new MenuItem("Donate", (b, n) => { Process.Start("https://paypal.me/metonator95"); }));
                        ContextMenu.MenuItems.Add("-");
                        ContextMenu.MenuItems.Add(new MenuItem("Close Launcher", (sender2, e2) =>
                        {
                            MessageBox.Show(null, "Please close the game before closing launcher.", "Please close the game before closing launcher.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }));

                        Update();
                        Refresh();

                        Notification.ContextMenu = ContextMenu;
                    }
                } else {
                    MessageBox.Show(null, "Your NFSW.exe is modified. Please re-download the game.", "GameLauncher", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            } catch (Exception ex) {
                MessageBox.Show(null, ex.Message, "GameLauncher", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PlayButton_MouseUp(object sender, EventArgs e)
        {
            if (_playenabled == false)
            {
                return;
            }

            playButton.BackgroundImage = Properties.Resources.playbutton_hover;
        }

        private void PlayButton_MouseDown(object sender, EventArgs e)
        {
            if (_playenabled == false)
            {
                return;
            }

            playButton.BackgroundImage = Properties.Resources.playbutton_click;
        }

        private void PlayButton_MouseEnter(object sender, EventArgs e)
        {
            if (_playenabled == false)
            {
                return;
            }

            playButton.BackgroundImage = Properties.Resources.playbutton_hover;
        }

        private void PlayButton_MouseLeave(object sender, EventArgs e)
        {
            if (_playenabled == false)
            {
                return;
            }

            playButton.BackgroundImage = Properties.Resources.playbutton;
        }

        private void LaunchNfsw()
        {
            playButton.BackgroundImage = Properties.Resources.playbutton;
            playButton.ForeColor = Color.Gray;

            playProgressText.Text = "Checking up all files".ToUpper();
            playProgress.Width = 0;
            extractingProgress.Width = 0;

            string speechFile;

            try
            {
                speechFile = string.IsNullOrEmpty(_settingFile.Read("Language")) ? "en" : _settingFile.Read("Language").ToLower();
            }
            catch (Exception)
            {
                speechFile = "en";
            }

            if (!File.Exists(_settingFile.Read("InstallationDirectory") + "/Sound/Speech/copspeechhdr_" + speechFile + ".big")) {
                playProgressText.Text = "Loading list of files to download...".ToUpper();

                DriveInfo[] allDrives = DriveInfo.GetDrives();
                foreach (DriveInfo d in allDrives) {
                    if (d.Name == Path.GetPathRoot(_settingFile.Read("InstallationDirectory"))) {
                        if (d.TotalFreeSpace <= 10000000000)  {

                            extractingProgress.Value = 100;
                            extractingProgress.Width = 519;
                            extractingProgress.Image = Properties.Resources.progress_warning;
                            extractingProgress.ProgressColor = Color.Orange;

                            playProgressText.Text = "Please make sure you have at least 10GB free space on hard drive.".ToUpper();

                            TaskbarProgress.SetState(Handle, TaskbarProgress.TaskbarStates.Paused);
                            TaskbarProgress.SetValue(Handle, 100, 100);
                        }
                        else {
                            DownloadCoreFiles();
                        }
                    }
                }
            } else {
                OnDownloadFinished();
			}
		}

        public void RemoveTracksHighFiles()
        {
            if (File.Exists(_settingFile.Read("InstallationDirectory") + "/TracksHigh/STREAML5RA_98.BUN"))
            {
                Directory.Delete(_settingFile.Read("InstallationDirectory") + "/TracksHigh", true);
            }
        }

        public void DownloadCoreFiles()
        {
            playProgressText.Text = "Checking Core Files...".ToUpper();
            playProgress.Width = 0;
            extractingProgress.Width = 0;

            TaskbarProgress.SetState(Handle, TaskbarProgress.TaskbarStates.Indeterminate);

            //Guess who is Back - DavidCarbon
            if (File.Exists(filename_pack))
            {
                playProgressTextTimer.Visible = true;
                playProgressText.Text = "Local GameFiles sbrwpack Found In Launcher Folder".ToUpper();
                playProgressTextTimer.Text = "Loading".ToUpper() ;

                //GameFiles.sbrwpack
                LocalGameFiles();
            }
            else if (!File.Exists(_settingFile.Read("InstallationDirectory") + "/nfsw.exe"))
            {
                _downloadStartTime = DateTime.Now;
                _downloader.StartDownload(_NFSW_Installation_Source, "", _settingFile.Read("InstallationDirectory"), false, false, 1130632198);
            }
            else
            {
                DownloadTracksFiles();
            }
        }

        public void DownloadTracksFiles()
        {
            playProgressText.Text = "Checking Tracks Files...".ToUpper();
            playProgress.Width = 0;
            extractingProgress.Width = 0;

            TaskbarProgress.SetState(Handle, TaskbarProgress.TaskbarStates.Indeterminate);

            if (!File.Exists(_settingFile.Read("InstallationDirectory") + "/Tracks/STREAML5RA_98.BUN"))
            {
                _downloadStartTime = DateTime.Now;
                _downloader.StartDownload(_NFSW_Installation_Source, "Tracks", _settingFile.Read("InstallationDirectory"), false, false, 615494528);
            }
            else
            {
                DownloadSpeechFiles();
            }
        }

        public void DownloadSpeechFiles()
        {
            playProgressText.Text = "Looking for correct Speech Files...".ToUpper();
            playProgress.Width = 0;
            extractingProgress.Width = 0;

            TaskbarProgress.SetState(Handle, TaskbarProgress.TaskbarStates.Indeterminate);

            string speechFile;
            ulong speechSize;

            try
            {
                if (string.IsNullOrEmpty(_settingFile.Read("Language")))
                {
                    speechFile = "en";
                    speechSize = 141805935;
                    _langInfo = "ENGLISH";
                }
                else
                {
                    WebClient wc = new WebClient();
                    var response = wc.DownloadString(_NFSW_Installation_Source + "/" + _settingFile.Read("Language").ToLower() + "/index.xml");

                    response = response.Substring(3, response.Length - 3);

                    var speechFileXml = new XmlDocument();
                    speechFileXml.LoadXml(response);
                    var speechSizeNode = speechFileXml.SelectSingleNode("index/header/compressed");

                    speechFile = _settingFile.Read("Language").ToLower();
                    speechSize = Convert.ToUInt64(speechSizeNode.InnerText);
                    _langInfo = settingsLanguage.GetItemText(settingsLanguage.SelectedItem).ToUpper();
                }
            }
            catch (Exception)
            {
                speechFile = "en";
                speechSize = 141805935;
                _langInfo = "ENGLISH";
            }

            playProgressText.Text = string.Format("Checking for {0} Speech Files.", _langInfo).ToUpper();

            if (!File.Exists(_settingFile.Read("InstallationDirectory") + "\\Sound\\Speech\\copspeechsth_" + speechFile + ".big"))
            {
                _downloadStartTime = DateTime.Now;
                _downloader.StartDownload(_NFSW_Installation_Source, speechFile, _settingFile.Read("InstallationDirectory"), false, false, speechSize);
            }
            else
            {
                OnDownloadFinished();
            }
        }

        //Check Local GameFiles Hash
        private async void LocalGameFiles()
        {
            await Task.Delay(5000);
            if (SHA.HashFile("GameFiles.sbrwpack") == "B42E00939DC656C14BF5A05644080AD015522C8C")
            {
                TaskbarProgress.SetValue(Handle, 100, 100);
                playProgress.Value = 100;
                playProgress.Width = 519;

                GoForUnpack(filename_pack);
            }
        }

        //That's right the Protype Extractor from 2.1.5.x, now back from the dead - DavidCarbon
        public void GoForUnpack(string filename_pack)
        {
            //Thread.Sleep(1);

            Thread unpacker = new Thread(() => {
                this.BeginInvoke((MethodInvoker)delegate {
                    using (ZipArchive archive = ZipFile.OpenRead(filename_pack))
                    {
                        int numFiles = archive.Entries.Count;
                        int current = 1;

                        foreach (ZipArchiveEntry entry in archive.Entries)
                        {
                            string fullName = entry.FullName;

                            extractingProgress.Value = (int)((long)100 * current / numFiles);
                            extractingProgress.Width = (int)((long)519 * current / numFiles);

                            TaskbarProgress.SetValue(Handle, (int)(100 * current / numFiles), 100);

                            if (!File.Exists(Path.Combine(_settingFile.Read("InstallationDirectory"), fullName.Replace(".sbrw", String.Empty))))
                            {
                                playProgressText.Text = ("Unpacking " + fullName.Replace(".sbrw", String.Empty)).ToUpper();
                                playProgressTextTimer.Text = "[" + current + " / " + archive.Entries.Count + "]";


                                if (fullName.Substring(fullName.Length - 1) == "/")
                                {
                                    //Is a directory, create it!
                                    string folderName = fullName.Remove(fullName.Length - 1);
                                    if (Directory.Exists(Path.Combine(_settingFile.Read("InstallationDirectory"), folderName)))
                                    {
                                        Directory.Delete(Path.Combine(_settingFile.Read("InstallationDirectory"), folderName), true);
                                    }

                                    Directory.CreateDirectory(Path.Combine(_settingFile.Read("InstallationDirectory"), folderName));
                                }
                                else
                                {
                                    String oldFileName = fullName.Replace(".sbrw", String.Empty);
                                    String[] split = oldFileName.Split('/');

                                    String newFileName = String.Empty;

                                    if (split.Length >= 2)
                                    {
                                        newFileName = Path.Combine(split[split.Length - 2], split[split.Length - 1]);
                                    }
                                    else
                                    {
                                        newFileName = split.Last();
                                    }

                                    String KEY = Regex.Replace(SHA.HashPassword(newFileName), "[^0-9.]", "").Substring(0, 8);
                                    String IV = Regex.Replace(MDFive.HashPassword(newFileName), "[^0-9.]", "").Substring(0, 8);

                                    entry.ExtractToFile(getTempNa, true);

                                    DESCryptoServiceProvider dESCryptoServiceProvider = new DESCryptoServiceProvider()
                                    {
                                        Key = Encoding.ASCII.GetBytes(KEY),
                                        IV = Encoding.ASCII.GetBytes(IV)
                                    };

                                    FileStream fileStream = new FileStream(Path.Combine(_settingFile.Read("InstallationDirectory"), oldFileName), FileMode.Create);
                                    CryptoStream cryptoStream = new CryptoStream(fileStream, dESCryptoServiceProvider.CreateDecryptor(), CryptoStreamMode.Write);
                                    BinaryWriter binaryFile = new BinaryWriter(cryptoStream);

                                    using (BinaryReader reader = new BinaryReader(File.Open(getTempNa, FileMode.Open)))
                                    {
                                        long numBytes = new FileInfo(getTempNa).Length;
                                        binaryFile.Write(reader.ReadBytes((int)numBytes));
                                        binaryFile.Close();
                                    }
                                }
                            }
                            else
                            {
                                playProgressText.Text = ("Skipping " + fullName).ToUpper();
                            }

                            _presence.State = "Unpacking game: " + (100 * current / numFiles) + "%";
                            if(discordRpcClient != null) discordRpcClient.SetPresence(_presence);

                            Application.DoEvents();

                            if (numFiles == current)
                            {
                                playProgressTextTimer.Visible = false;
                                playProgressTextTimer.Text = "";

                                _isDownloading = false;
                                OnDownloadFinished();

                                Notification.Visible = true;
                                Notification.BalloonTipIcon = ToolTipIcon.Info;
                                Notification.BalloonTipTitle = "GameLauncherReborn";
                                Notification.BalloonTipText = "Your game is now ready to launch!";
                                Notification.ShowBalloonTip(5000);
                                Notification.Dispose();
                            }

                            current++;
                        }
                    }
                });
            });

            unpacker.Start();
        }

        private string FormatFileSize(long byteCount, bool si = true) {
            int unit = si ? 1000 : 1024;
            if (byteCount < unit) return byteCount + " B";
            int exp = (int)(Math.Log(byteCount) / Math.Log(unit));
            String pre = (si ? "kMGTPE" : "KMGTPE")[exp - 1] + (si ? "" : "i");
            return String.Format("{0}{1}B", Convert.ToDecimal(byteCount / Math.Pow(unit, exp)).ToString("0.##"), pre);
        }

        private string EstimateFinishTime(long current, long total) {
            try { 
                var num = current / (double)total;
                if (num < 0.00185484899838312) {
                    return "Calculating";
                }

                var now = DateTime.Now - _downloadStartTime;
                var timeSpan = TimeSpan.FromTicks((long)(now.Ticks / num)) - now;

                int rHours = Convert.ToInt32(timeSpan.Hours.ToString()) + 1;
                int rMinutes = Convert.ToInt32(timeSpan.Minutes.ToString()) + 1;
                int rSeconds = Convert.ToInt32(timeSpan.Seconds.ToString()) + 1;

                if (rHours > 1) return rHours.ToString() + " hours remaining";
                if (rMinutes > 1) return rMinutes.ToString() + " minutes remaining";
                if (rSeconds > 1) return rSeconds.ToString() + " seconds remaining";

                return "Just now";
            } catch {
                return "N/A";
            }
        }

        private void OnDownloadProgress(long downloadLength, long downloadCurrent, long compressedLength, string filename, int skiptime = 0)
        {
            if (downloadCurrent < compressedLength) {
                playProgressText.Text = String.Format("Downloading — {0} of {1} ({3}%) — {2}", FormatFileSize(downloadCurrent), FormatFileSize(compressedLength), EstimateFinishTime(downloadCurrent, compressedLength), (int)(100 * downloadCurrent / compressedLength)).ToUpper();
            }

            try {
                playProgress.Value = (int)(100 * downloadCurrent / compressedLength);
                playProgress.Width = (int)(519 * downloadCurrent / compressedLength);

                TaskbarProgress.SetValue(Handle, (int)(100 * downloadCurrent / compressedLength), 100);
            } catch {
                TaskbarProgress.SetValue(Handle, 0, 100);
                playProgress.Value = 0;
                playProgress.Width = 0;
            }

            TaskbarProgress.SetState(Handle, TaskbarProgress.TaskbarStates.Normal);
        }

        private void OnDownloadFinished() {
            try {
                File.WriteAllBytes(_settingFile.Read("InstallationDirectory") + "/GFX/BootFlow.gfx", ExtractResource.AsByte("GameLauncher.SoapBoxModules.BootFlow.gfx"));
            } catch {
                // ignored
            }

            playProgressText.Text = "Ready!".ToUpper();
            _presence.State = "Ready!";
            if(discordRpcClient != null) discordRpcClient.SetPresence(_presence);

            EnablePlayButton();

            extractingProgress.Width = 519;

            TaskbarProgress.SetValue(Handle, 100, 100);
            TaskbarProgress.SetState(Handle, TaskbarProgress.TaskbarStates.Normal);

        }

        private void EnablePlayButton() {
            _isDownloading = false;
            _playenabled = true;

            extractingProgress.Value = 100;
            extractingProgress.Width = 519;

            playButton.BackgroundImage = Properties.Resources.playbutton;
            playButton.ForeColor = Color.White;
        }

        private void DisablePlayButton() {
            _isDownloading = false;
            _playenabled = false;

            ShowPlayPanel.Visible = false;

            extractingProgress.Value = 100;
            extractingProgress.Width = 519;

            playButton.BackgroundImage = Properties.Resources.graybutton;
            playButton.ForeColor = Color.White;
        }

        private void OnDownloadFailed(Exception ex)
        {
            string failureMessage;
            MessageBox.Show(null, "Failed to download gamefiles. \n\nCDN might be offline. \n\nPlease select a different CDN on Next Screen", "GameLauncher - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            //CDN Went Offline Screen switch - DavidCarbon
            CDN_Offline_Switch();

            try {
                failureMessage = ex.Message;
            } catch {
                failureMessage = "Download failed.";
            }

            extractingProgress.Value = 100;
            extractingProgress.Width = 519;
            extractingProgress.Image = Properties.Resources.progress_error;
            extractingProgress.ProgressColor = Color.FromArgb(254,0,0);

            playProgressText.Text = failureMessage.ToUpper();

            TaskbarProgress.SetValue(Handle, 100, 100);
            TaskbarProgress.SetState(Handle, TaskbarProgress.TaskbarStates.Error);
        }

		private void OnShowExtract(string filename, long currentCount, long allFilesCount) {
            if(playProgress.Value == 100)
                playProgressText.Text = String.Format("Extracting — {0} of {1} ({3}%) — {2}", FormatFileSize(currentCount), FormatFileSize(allFilesCount), EstimateFinishTime(currentCount, allFilesCount), (int)(100 * currentCount / allFilesCount)).ToUpper();
            
            extractingProgress.Value = (int)(100 * currentCount / allFilesCount);
            extractingProgress.Width = (int)(519 * currentCount / allFilesCount);
        }

        private void OnShowMessage(string message, string header)
        {
            MessageBox.Show(message, header);
        }

        public void ServerStatusBar(Pen color, Point startPoint, Point endPoint, int Thickness = 2) {
            Graphics _formGraphics = CreateGraphics();
            
            for (int x = 0; x <= Thickness; x++) {
                _formGraphics.DrawLine(color, new Point(startPoint.X, startPoint.Y-x), new Point(endPoint.X, endPoint.Y-x));
            }

            _formGraphics.Dispose();
        }

        //VerifyHash
        private void VFilesButton_Click(object sender, EventArgs e)
        {
            //In Development (Zacam got this)
        }

        private void SelectServerBtn_Click(object sender, EventArgs e) {
            new SelectServer().ShowDialog();

            if(ServerName != null) {
                this.SelectServerBtn.Text = "[...] " + ServerName.Name;

                var index = finalItems.FindIndex(i => string.Equals(i.IpAddress, ServerName.IpAddress));
                serverPick.SelectedIndex = index;
            }
        }

        private void SettingsClearCrashLogsButton_Click(object sender, EventArgs e)
        {
            var crashLogFilesDirectory = new DirectoryInfo(_settingFile.Read("InstallationDirectory"));

            foreach (var file in crashLogFilesDirectory.EnumerateFiles("SBRCrashDump_CL0*.dmp"))
            {
                file.Delete();
            }

            foreach (var file in crashLogFilesDirectory.EnumerateFiles("SBRCrashDump_CL0*.txt"))
            {
                file.Delete();
            }

            foreach (var file in crashLogFilesDirectory.EnumerateFiles("NFSCrashDump_CL0*.dmp"))
            {
                file.Delete();
            }

            MessageBox.Show(null, "Deleted Crash Logs", "GameLauncher", MessageBoxButtons.OK, MessageBoxIcon.Information);
            SettingsClearCrashLogsButton.Enabled = false;
        }

        private void SettingsClearCommunicationLogButton_Click(object sender, EventArgs e)
        {
            File.Delete(_settingFile.Read("InstallationDirectory") + "/NFSWO_COMMUNICATION_LOG.txt");
            MessageBox.Show(null, "Deleted NFSWO Communication Log", "GameLauncher", MessageBoxButtons.OK, MessageBoxIcon.Information);
            SettingsClearCommunicationLogButton.Enabled = false;
        }

        private void SettingsCDNPick_SelectedIndexChanged(object sender, EventArgs e)
        {
            IsChangedCDNDown();
        }

        private void PatchNotes_Click(object sender, EventArgs e)
        {
            new About().Show();
        }

        private void DiscordInviteLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_serverDiscordLink != null)
                Process.Start(_serverDiscordLink);
        }

        private void HomePageLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_serverWebsiteLink != null)
                Process.Start(_serverWebsiteLink);
        }

        private void FacebookGroupLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_serverFacebookLink != null)
                Process.Start(_serverFacebookLink);
        }

        private void TwitterAccountLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_serverTwitterLink != null)
                Process.Start(_serverTwitterLink);
        }
        private void WindowsDefenderFirstRun()
        {
            // Create Windows Defender Exclusion
            try
            {
                Log.Debug("WINDOWS DEFENDER: Excluding Core Folders");
                //Add Exclusion to Windows Defender
                using (PowerShell ps = PowerShell.Create())
                {
                    ps.AddScript($"Add-MpPreference -ExclusionPath \"{AppDomain.CurrentDomain.BaseDirectory}\"");
                    ps.AddScript($"Add-MpPreference -ExclusionPath \"{_settingFile.Read("InstallationDirectory")}\"");
                    var result = ps.Invoke();
                }
                _settingFile.Write("WindowsDefender", "Excluded");
            }
            catch
            {
                Log.Debug("WINDOWS DEFENDER: Failed to Exclude Folders");
                _settingFile.Write("WindowsDefender", "Not Excluded");
            }
        }

        private void WindowsDefenderGameFilesDirctoryChange()
        {
            using (PowerShell ps = PowerShell.Create())
            {
                Log.Debug("WINDOWS DEFENDER: Removing OLD Game Files Directory: " + _settingFile.Read("InstallationDirectory"));
                ps.AddScript($"Remove-MpPreference -ExclusionPath \"{_settingFile.Read("InstallationDirectory")}\"");
                Log.Debug("WINDOWS DEFENDER: Excluding NEW Game Files Directory: " + _newGameFilesPath);
                ps.AddScript($"Add-MpPreference -ExclusionPath \"{_newGameFilesPath}\"");
                var result = ps.Invoke();
            }
            _settingFile.Write("InstallationDirectory", _newGameFilesPath);
            _restartRequired = true;
            //Clean Mods Files from New Dirctory (If it has .links in directory)
            var linksPath = Path.Combine(_settingFile.Read("InstallationDirectory"), ".links");
            if (File.Exists(linksPath))
            {
                Log.Debug("CLEANLINKS: Cleaning Up Mod Files {Settings}");
                CleanLinks(linksPath);
            }
        }

    }
    /* Moved 7 Unused Code to Gist */
    /* https://gist.githubusercontent.com/DavidCarbon/97494268b0175a81a5f89a5e5aebce38/raw/00de505302fbf9f8cfea9b163a707d9f8f122552/MainScreen.cs */
}
