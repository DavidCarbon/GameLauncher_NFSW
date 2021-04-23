﻿using GameLauncher.App.Classes.Logger;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using GameLauncher.App.Classes.LauncherCore.Global;
using System.Globalization;
using GameLauncher.App.Classes.Hash;
using GameLauncher.App.Classes.LauncherCore.Lists.JSON;
using System.Windows.Forms;
using GameLauncher.App.Classes.SystemPlatform.Windows;

namespace GameLauncher.App.Classes.LauncherCore.Lists
{
    public class ServerListUpdater
    {
        public static List<ServerList> NoCategoryList = new List<ServerList>();

        public static List<ServerList> CleanList = new List<ServerList>();

        public static void GetList()
        {
            List<ServerList> serverInfos = new List<ServerList>();

            foreach (var serverListURL in URLs.ServerList)
            {
                try
                {
                    Log.UrlCall("LIST CORE: Loading Server List from: " + serverListURL);
                    FunctionStatus.TLS();
                    WebClient Client = new WebClient();
                    Client.Headers.Add("user-agent", "GameLauncher " + Application.ProductVersion + " (+https://github.com/SoapBoxRaceWorld/GameLauncher_NFSW)");
                    var response = Client.DownloadString(serverListURL);
                    Log.UrlCall("LIST CORE: Loaded Server List from: " + serverListURL);

                    try
                    {
                        serverInfos.AddRange(
                            JsonConvert.DeserializeObject<List<ServerList>>(response));
                        InformationCache.ServerListStatus = "Loaded";
                        break;
                    }
                    catch (Exception error)
                    {
                        Log.Error("LIST CORE: Error occurred while deserializing Server List from [" + serverListURL + "]: " + error.Message);
                        InformationCache.ServerListStatus = "Error";
                    }
                }
                catch (Exception error)
                {
                    Log.Error("LIST CORE: Error occurred while loading Server List from [" + serverListURL + "]: " + error.Message);
                    InformationCache.ServerListStatus = "Error";
                }
            }

            if (File.Exists("servers.json"))
            {
                var fileItems = JsonConvert.DeserializeObject<List<ServerList>>(File.ReadAllText("servers.json")) ?? new List<ServerList>();

                if (fileItems.Count > 0)
                {
                    fileItems.Select(si =>
                    {
                        si.DistributionUrl = "";
                        si.DiscordPresenceKey = "";
                        si.Id = SHA.HashPassword($"{si.Name}:{si.Id}:{si.IpAddress}");
                        si.IsSpecial = false;
                        si.Category = "CUSTOM";

                        return si;
                    }).ToList().ForEach(si => serverInfos.Add(si));
                }
            }

            if (File.Exists("libOfflineServer.dll"))
            {
                serverInfos.Add(new ServerList
                {
                    Name = "Offline Built-In Server",
                    Category = "OFFLINE",
                    DiscordPresenceKey = "",
                    IsSpecial = false,
                    DistributionUrl = "",
                    IpAddress = "http://localhost:4416/sbrw/Engine.svc",
                    Id = "OFFLINE"
                });
            }

            if (Debugger.IsAttached)
            {
                serverInfos.Add(new ServerList
                {
                    Name = "Local Debug Server",
                    Category = "DEBUG",
                    DiscordPresenceKey = "",
                    IsSpecial = false,
                    DistributionUrl = "",
                    IpAddress = "http://localhost:8680",
                    Id = "DEV"
                });
            }

            /* Create Final Server List without Categories */
            foreach (ServerList NoCatList in serverInfos)
            {
                if (NoCategoryList.FindIndex(i => string.Equals(i.Name, NoCatList.Name)) == -1)
                {
                    NoCategoryList.Add(NoCatList);
                }
            }

            /* Create Rough Draft Server List with Categories */
            List<ServerList> RawList = new List<ServerList>();

            foreach (var serverItemGroup in serverInfos.GroupBy(s => s.Category))
            {
                if (RawList.FindIndex(i => string.Equals(i.Name, $"<GROUP>{serverItemGroup.Key} Servers")) == -1)
                {
                    RawList.Add(new ServerList
                    {
                        Id = $"__category-{serverItemGroup.Key}__",
                        Name = $"<GROUP>{serverItemGroup.Key} Servers",
                        IsSpecial = true
                    });
                }
                RawList.AddRange(serverItemGroup.ToList());
            }

            /* Create Final Server List with Categories */
            foreach (ServerList CList in RawList)
            {
                if (CleanList.FindIndex(i => string.Equals(i.Name, CList.Name)) == -1)
                {
                    CleanList.Add(CList);
                }
            }

            /* (Start Process) Check Up to Date Certificate Status */
            CertificateStore.Latest();
        }

        /* Converts 2 Letter Country Code and Returns Full Country Name (In English) */
        public static string CountryName(string twoLetterCountryCode)
        {
            CultureInfo[] cultures = CultureInfo.GetCultures(CultureTypes.SpecificCultures);

            foreach (CultureInfo culture in cultures)
            {
                RegionInfo region = new RegionInfo(culture.LCID);
                if (region.TwoLetterISORegionName.ToUpper() == twoLetterCountryCode.ToUpper())
                {
                    return region.EnglishName;
                }
            }

            return "Unknown";
        }
    }
}