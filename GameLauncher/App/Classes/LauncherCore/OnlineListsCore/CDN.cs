﻿using Newtonsoft.Json;
using System;

namespace GameLauncher.App.Classes
{
    class CDN {
        public static string CDNUrl = String.Empty;
        public static string TrackHigh = String.Empty;
    }

    public class CDNObject {
        public string Name { get; set; }
        public string Url { get; set; }
    }

    public class CDNInfo
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }
}
