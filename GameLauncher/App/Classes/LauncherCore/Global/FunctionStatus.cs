﻿using GameLauncher.App.Classes.LauncherCore.APICheckers;
using GameLauncher.App.Classes.LauncherCore.FileReadWrite;
using GameLauncher.App.Classes.LauncherCore.Lists;
using GameLauncher.App.Classes.LauncherCore.Lists.JSON;
using GameLauncher.App.Classes.LauncherCore.RPC;
using GameLauncher.App.Classes.Logger;
using GameLauncher.App.Classes.SystemPlatform.Linux;
using GameLauncher.App.Classes.SystemPlatform.Windows;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;

namespace GameLauncher.App.Classes.LauncherCore.Global
{
    /* This is Used to Cache Responses From the Launcher */
    class InformationCache
    {
        /* Parent Screen Cords */
        public static Point ParentScreenLocation;

        /* ServerList Load Checks */
        public static string ServerListStatus = "Unknown";

        /* CDNList Load Checks */
        public static string CDNListStatus = "Unknown";

        /* System Language */
        public static string CurrentLanguage = "EN";

        /* Sets Game Launchers User Agent (If Required) */
        public static string UserAgent;

        /* System OS Name */
        public static string OSName;

        /* Selected Server Auth Support */
        public static bool ModernAuthSupport = false;

        /* Selected Server Category */
        public static string SelectedServerCategory;

        /* Selected Server List Key Information */
        public static ServerList SelectedServerData;

        /* Selected Server JSON (GetServerInformation) */
        public static GetServerInformation SelectedServerJSON = new GetServerInformation();

        /* Holds a collection of Server Status of Servers */
        public static Dictionary<string, int> ServerStatusBook = new Dictionary<string, int>();

        /* Selected Server Force Restart Timer */
        public static int RestartTimer;
    }

    /* This is Used to call Certain Functions (Such as Completion Status or Function Callbacks) */
    class FunctionStatus
    {
        /* Updater.cs Sets Conditional on If Launcher had Finished Loading (It Self) */
        public static bool LoadingComplete = false;

        /* Disables/Enables Proxy (Global) */
        public static bool DisableProxy = false;

        /* Allows Registration Button to be Enabled/Disabled */
        public static bool AllowRegistration;

        /* Verify Hash Status */
        public static bool IsVerifyHashDisabled = false;

        /* Visual API Status */
        public static bool IsVisualAPIsChecked = false;

        /* Sets Conditional to If its Possible to Close Game */
        public static Boolean CanCloseGame = true;

        /* If In-Game OverLays was Used */
        public static bool ExternalToolsWasUsed = false;

        /* Sets Conditional if Game was Closed (By Timer) */
        public static bool GameKilledBySpeedBugCheck = false;

        /* Detect and Set System Language */
        public static CultureInfo Lang = CultureInfo.CurrentUICulture;

        /* Checks if we have Write Permissions */
        public static bool HasWriteAccessToFolder(string path)
        {
            try
            {
                File.Create(path + "temp.txt").Close();
                File.Delete(path + "temp.txt");
            }
            catch
            {
                return false;
            }

            return true;
        }

        /* Used to Center WinForms Forms (Parent Screen) */
        public static void CenterScreen(Form form)
        {
            form.StartPosition = FormStartPosition.Manual;
            form.Top = (Screen.PrimaryScreen.Bounds.Height - form.Height) / 2;
            form.Left = (Screen.PrimaryScreen.Bounds.Width - form.Width) / 2;
        }

        public static void CenterParent(Form form)
        {
            form.StartPosition = FormStartPosition.Manual;
            form.Location = InformationCache.ParentScreenLocation;
        }

        /* Check if Folder Location is Acceptable and Returns a Value
        /* Let's actually make it cleaner and nicer - MeTonaTOR */
        public static FolderType CheckFolder(string FolderName)
        {
            if (FolderName.Contains("C:\\Users") && FolderName.Contains("Temp")) return FolderType.IsTempFolder;
            if (FolderName.Contains("C:\\Users")) return FolderType.IsUsersFolders;
            if (FolderName.Contains("C:\\Program Files")) return FolderType.IsProgramFilesFolder;
            if (FolderName.Contains("C:\\Windows")) return FolderType.IsWindowsFolder;
            if (FolderName.Length == 3) return FolderType.IsRootFolder;
            if (FolderName + "\\" == AppDomain.CurrentDomain.BaseDirectory) return FolderType.IsSameAsLauncherFolder;

            return FolderType.Unknown;
        }

        /* Converts Host Name to a IP (ex. http://localhost -> 192.168.1.69 */
        public static string HostName2IP(string hostname)
        {
            IPHostEntry iphost = Dns.GetHostEntry(hostname);
            IPAddress[] addresses = iphost.AddressList;
            return addresses[0].ToString();
        }

        /* Check System Language and Return Current Lang for Speech Files */
        public static string SpeechFiles()
        {
            string CurrentLang = Lang.ThreeLetterISOLanguageName;

            if (CurrentLang == "eng") return "en";
            else if (CurrentLang == "ger" || CurrentLang == "deu") return "de";
            else if (CurrentLang == "rus") return "ru";
            else if (CurrentLang == "spa") return "es";
            else return "en";
        }

        public static int SpeechFilesSize()
        {
            string CurrentLang = Lang.ThreeLetterISOLanguageName;

            if (CurrentLang == "eng") return 141805935;
            else if (CurrentLang == "ger" || CurrentLang == "deu") return 105948386;
            else if (CurrentLang == "rus") return 121367723;
            else if (CurrentLang == "spa") return 101540466;
            else return 141805935;
        }

        public static void TLS()
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol |= SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            ServicePointManager.ServerCertificateValidationCallback = (Object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) =>
            {
                bool isOk = true;
                if (sslPolicyErrors != SslPolicyErrors.None)
                {
                    for (int i = 0; i < chain.ChainStatus.Length; i++)
                    {
                        if (chain.ChainStatus[i].Status == X509ChainStatusFlags.RevocationStatusUnknown)
                        {
                            continue;
                        }
                        chain.ChainPolicy.RevocationFlag = X509RevocationFlag.EntireChain;
                        chain.ChainPolicy.RevocationMode = X509RevocationMode.Online;
                        chain.ChainPolicy.UrlRetrievalTimeout = new TimeSpan(0, 1, 0);
                        chain.ChainPolicy.VerificationFlags = X509VerificationFlags.AllFlags;
                        bool chainIsValid = chain.Build((X509Certificate2)certificate);
                        if (!chainIsValid)
                        {
                            isOk = false;
                            break;
                        }
                    }
                }
                return isOk;
            };
        }

        public static void ExtractToDirectory(string sourceZipFilePath, string destinationDirectoryName, bool overwrite)
        {
            try
            {
                if (!Directory.Exists(destinationDirectoryName))
                {
                    Directory.CreateDirectory(destinationDirectoryName);
                }

                using (var archive = ZipFile.Open(sourceZipFilePath, ZipArchiveMode.Read))
                {
                    if (!overwrite)
                    {
                        archive.ExtractToDirectory(destinationDirectoryName);
                        return;
                    }

                    DirectoryInfo di = Directory.CreateDirectory(destinationDirectoryName);
                    string destinationDirectoryFullPath = di.FullName;

                    foreach (ZipArchiveEntry file in archive.Entries)
                    {
                        string completeFileName = Path.GetFullPath(Path.Combine(destinationDirectoryFullPath, file.FullName));

                        if (!completeFileName.StartsWith(destinationDirectoryFullPath, StringComparison.OrdinalIgnoreCase))
                        {
                            throw new IOException("Trying to extract file outside of destination directory. See this link for more info: https://snyk.io/research/zip-slip-vulnerability");
                        }

                        if (file.Name == "")
                        {
                            Directory.CreateDirectory(Path.GetDirectoryName(completeFileName));
                            continue;
                        }

                        Log.Debug("MODNET ZIP: Extracted File " + completeFileName);
                        file.ExtractToFile(completeFileName, true);
                    }
                }
            }
            catch (Exception error)
            {
                Log.Error("MODNET ZIP: " + error.Message);
            }
            finally
            {
                try
                {
                    File.Delete(sourceZipFilePath);
                }
                catch (Exception error)
                {
                    Log.Error("MODNET ZIP: Unable to Delete ZIP File - " + error);
                }
            }
        }

        public void ExtractZipFileToDirectory(string sourceZipFilePath, string destinationDirectoryName, bool overwrite)
        {
            using (var archive = ZipFile.Open(sourceZipFilePath, ZipArchiveMode.Read))
            {
                if (!overwrite)
                {
                    archive.ExtractToDirectory(destinationDirectoryName);
                    return;
                }

                DirectoryInfo di = Directory.CreateDirectory(destinationDirectoryName);
                string destinationDirectoryFullPath = di.FullName;

                foreach (ZipArchiveEntry file in archive.Entries)
                {
                    string completeFileName = Path.GetFullPath(Path.Combine(destinationDirectoryFullPath, file.FullName));

                    if (!completeFileName.StartsWith(destinationDirectoryFullPath, StringComparison.OrdinalIgnoreCase))
                    {
                        throw new IOException("Trying to extract file outside of destination directory. See this link for more info: https://snyk.io/research/zip-slip-vulnerability");
                    }

                    if (file.Name == "")
                    {// Assuming Empty for Directory
                        Directory.CreateDirectory(Path.GetDirectoryName(completeFileName));
                        continue;
                    }
                    file.ExtractToFile(completeFileName, true);
                }
            }
        }

        public static void FirstTimeRun()
        {
            Log.Core("LAUNCHER: Checking InstallationDirectory: " + FileSettingsSave.GameInstallation);
            if (string.IsNullOrEmpty(FileSettingsSave.GameInstallation))
            {
                Log.Core("LAUNCHER: First run!");

                try
                {
                    Form welcome = new WelcomeScreen();
                    DialogResult welcomereply = welcome.ShowDialog();

                    if (welcomereply != DialogResult.OK)
                    {
                        Process.GetProcessById(Process.GetCurrentProcess().Id).Kill();
                    }
                    else
                    {
                        FileSettingsSave.CDN = SelectedCDN.CDNUrl;
                        FileSettingsSave.SaveSettings();
                    }
                }
                catch
                {
                    Log.Warning("LAUNCHER: CDN Source URL was Empty! Setting a Null Safe URL 'http://localhost'");
                    FileSettingsSave.CDN = "http://localhost";
                    Log.Core("LAUNCHER: Installation Directory was Empty! Creating and Setting Directory at " + AppDomain.CurrentDomain.BaseDirectory + "\\Game Files");
                    FileSettingsSave.GameInstallation = AppDomain.CurrentDomain.BaseDirectory + "\\Game Files";
                    FileSettingsSave.SaveSettings();
                }

                var fbd = new CommonOpenFileDialog
                {
                    EnsurePathExists = true,
                    EnsureFileExists = false,
                    AllowNonFileSystemItems = false,
                    Title = "Select the location to Find or Download NFS:W",
                    IsFolderPicker = true
                };

                if (fbd.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    if (!HasWriteAccessToFolder(fbd.FileName))
                    {
                        Log.Error("LAUNCHER: Not enough permissions. Exiting.");
                        MessageBox.Show(null, "You don't have enough permission to select this path as installation folder. Please select another directory.", "GameLauncher", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Application.Exit();
                    }

                    if (fbd.FileName.Length == 3)
                    {
                        Log.Warning("LAUNCHER: Installing NFSW in root of the harddisk is not allowed.");
                        MessageBox.Show(null, string.Format("Installing NFSW in root of the harddisk is not allowed. " +
                            "Instead, we will install it on {0}.", AppDomain.CurrentDomain.BaseDirectory + "\\Game Files"), "GameLauncher", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        FileSettingsSave.GameInstallation = AppDomain.CurrentDomain.BaseDirectory + "\\Game Files";
                        FileSettingsSave.SaveSettings();
                    }
                    else if (fbd.FileName == AppDomain.CurrentDomain.BaseDirectory)
                    {
                        Directory.CreateDirectory("Game Files");
                        Log.Warning("LAUNCHER: Installing NFSW in same directory where the launcher resides is disadvised.");
                        MessageBox.Show(null, string.Format("Installing NFSW in same directory where the launcher resides is disadvised. " +
                            "Instead, we will install it on {0}.", AppDomain.CurrentDomain.BaseDirectory + "\\Game Files"), "GameLauncher", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        FileSettingsSave.GameInstallation = AppDomain.CurrentDomain.BaseDirectory + "\\Game Files";
                        FileSettingsSave.SaveSettings();
                    }
                    else
                    {
                        Log.Core("LAUNCHER: Directory Set: " + fbd.FileName);
                        FileSettingsSave.GameInstallation = fbd.FileName;
                        FileSettingsSave.SaveSettings();
                    }
                }
                else
                {
                    Log.Core("LAUNCHER: Exiting");
                    Application.Exit();
                }
                fbd.Dispose();
            }

            if (!DetectLinux.LinuxDetected())
            {
                switch (CheckFolder(FileSettingsSave.GameInstallation))
                {
                    case FolderType.IsSameAsLauncherFolder:
                        Directory.CreateDirectory("Game Files");
                        Log.Error("LAUNCHER: Installing NFSW in same location where the GameLauncher resides is NOT allowed.");
                        MessageBox.Show(null, string.Format("Installing NFSW in same location where the GameLauncher resides is NOT allowed.\n" +
                            "Instead, we will install it at {0}.", AppDomain.CurrentDomain.BaseDirectory + "Game Files"), "GameLauncher", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        FileSettingsSave.GameInstallation = AppDomain.CurrentDomain.BaseDirectory + "\\Game Files";
                        break;

                    case FolderType.IsTempFolder:
                    case FolderType.IsUsersFolders:
                    case FolderType.IsProgramFilesFolder:
                    case FolderType.IsWindowsFolder:
                    case FolderType.IsRootFolder:
                        String constructMsg = String.Empty;
                        Directory.CreateDirectory("Game Files");
                        constructMsg += "Using this location for Game Files is not allowed.\nThe following list are NOT allowed:\n\n";
                        constructMsg += "• X:\\nfsw.exe (Root of Drive, such as C:\\ or D:\\, must be in a folder)\n";
                        constructMsg += "• C:\\Program Files\n";
                        constructMsg += "• C:\\Program Files (x86)\n";
                        constructMsg += "• C:\\Users (Includes 'Desktop', 'Documents', 'Downloads')\n";
                        constructMsg += "• C:\\Windows\n\n";
                        constructMsg += "Instead, we will install the NFSW Game at " + AppDomain.CurrentDomain.BaseDirectory + "\\Game Files\n";

                        MessageBox.Show(null, constructMsg, "GameLauncher", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Log.Error("LAUNCHER: Installing NFSW in a Restricted Location is not allowed.");
                        FileSettingsSave.GameInstallation = AppDomain.CurrentDomain.BaseDirectory + "\\Game Files";
                        break;
                }
                FileSettingsSave.SaveSettings();

                /* Windows Defender (Windows 10) Not Included in This Release Build */
            }

            /* Check If Launcher Failed to Connect to any APIs */
            if (VisualsAPIChecker.WOPLAPI == false)
            {
                DialogResult restartAppNoApis = MessageBox.Show(null, "There's no internet connection, Launcher might crash \n \nClick Yes to Close Launcher \nor \nClick No Continue", "GameLauncher has Stopped, Failed To Connect To API", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (restartAppNoApis == DialogResult.No)
                {
                    MessageBox.Show("Good Luck... \n No Really \n ...Good Luck", "GameLauncher Will Continue, When It Failed To Connect To API");
                    Log.Warning("PRE-CHECK: User has Bypassed 'No Internet Connection' Check and Will Continue");
                }

                if (restartAppNoApis == DialogResult.Yes)
                {
                    Application.Exit();
                }
            }

            DiscordLauncherPresense.Start("Start Up", "540651192179752970");

            Log.Visuals("CORE: Starting MainScreen");
            Application.Run(new MainScreen());
        }

        /* Moved "runAsAdmin" Code to Gist */
        /* https://gist.githubusercontent.com/DavidCarbon/97494268b0175a81a5f89a5e5aebce38/raw/eec2f9f80aa4b350ab98d32383e1ee1f2e1c26fd/Self.cs */
    }
}