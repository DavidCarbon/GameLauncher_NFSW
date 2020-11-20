using GameLauncher.App.Classes.Logger;
using GameLauncher.HashPassword;
using GameLauncherReborn;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace GameLauncher.App.Classes
{
    public class CDNListUpdater
    {
        private static List<CDNInfo> finalCDNItems = new List<CDNInfo>();

        public static void UpdateCDNList()
        {
            List<CDNInfo> cdnInfos = new List<CDNInfo>();

            foreach (var cdnListURL in Self.cdnlisturl) {
                try {
                    Log.Debug("Loading cdnlist from: " + cdnListURL);
                    var wc = new WebClient();
                    var response = wc.DownloadString(cdnListURL);
                    Log.Debug("Loaded cdnlist from: " + cdnListURL);

                    try {
                        cdnInfos.AddRange(JsonConvert.DeserializeObject<List<CDNInfo>>(response));
                    } catch (Exception error) {
                        Log.Error("Error occurred while deserializing cdn list from [" + cdnListURL + "]: " + error.Message);
                    }
                } catch (Exception error) {
                    Log.Error("Error occurred while loading cdn list from [" + cdnListURL + "]: " + error.Message);
                }
            }
        }

        public static List<CDNInfo> GetCDNList()
        {
            List<CDNInfo> newFinalCDNItems = new List<CDNInfo>();

            foreach (CDNInfo xCDN in finalCDNItems)
            {
                if (newFinalCDNItems.FindIndex(i => string.Equals(i.Name, xCDN.Name)) == -1)
                {
                    newFinalCDNItems.Add(xCDN);
                }
            }
            return newFinalCDNItems;
        }
    }
}