﻿using GameLauncher.App.Classes.Logger;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using GameLauncher.App.Classes.LauncherCore.Lists.JSON;

namespace GameLauncher.App.Classes.LauncherCore.Lists
{
    public class LanguageListUpdater
    {
        public static List<LangObject> NoCategoryList = new List<LangObject>();

        public static List<LangObject> CleanList = new List<LangObject>();

        public static void GetList()
        {
            List<LangObject> langInfos = new List<LangObject>();

            String json_language = String.Empty;

            json_language += "[";
            json_language += "    { \"category\": \"Official\", \"name\": \"English\",             \"xml_value\": \"EN\", \"ini_value\": \"EN\"},";
            json_language += "    { \"category\": \"Official\", \"name\": \"Deutsch\",             \"xml_value\": \"DE\", \"ini_value\": \"DE\"},";
            json_language += "    { \"category\": \"Official\", \"name\": \"Español\",             \"xml_value\": \"ES\", \"ini_value\": \"ES\"},";
            json_language += "    { \"category\": \"Official\", \"name\": \"Français\",            \"xml_value\": \"FR\", \"ini_value\": \"FR\"},";
            json_language += "    { \"category\": \"Official\", \"name\": \"Polski\",              \"xml_value\": \"PL\", \"ini_value\": \"PL\"},";
            json_language += "    { \"category\": \"Official\", \"name\": \"Русский\",             \"xml_value\": \"RU\", \"ini_value\": \"RU\"},";
            json_language += "    { \"category\": \"Official\", \"name\": \"Português (Brasil)\",  \"xml_value\": \"PT\", \"ini_value\": \"PT\"},";
            json_language += "    { \"category\": \"Official\", \"name\": \"繁體中文\",             \"xml_value\": \"TC\", \"ini_value\": \"TC\"},";
            json_language += "    { \"category\": \"Official\", \"name\": \"简体中文\",             \"xml_value\": \"SC\", \"ini_value\": \"SC\"},";
            json_language += "    { \"category\": \"Official\", \"name\": \"ภาษาไทย\",              \"xml_value\": \"TH\", \"ini_value\": \"TH\"},";
            json_language += "    { \"category\": \"Official\", \"name\": \"Türkçe\",               \"xml_value\": \"TR\", \"ini_value\": \"TR\"},";
            json_language += "    { \"category\": \"Custom\",   \"name\": \"Italiano\",             \"xml_value\": \"EN\", \"ini_value\": \"IT\"}";
            json_language += "]";


            try
            {
                langInfos.AddRange(JsonConvert.DeserializeObject<List<LangObject>>(json_language));
            }
            catch (Exception error)
            {
                Log.Error("LIST CORE: Error occurred while deserializing LANG List: " + error.Message);
            }

            foreach (LangObject NoCatList in langInfos)
            {
                if (NoCategoryList.FindIndex(i => string.Equals(i.Name, NoCatList.Name)) == -1)
                {
                    NoCategoryList.Add(NoCatList);
                }
            }

            List<LangObject> RawList = new List<LangObject>();

            foreach (var langItemGroup in langInfos.GroupBy(s => s.Category))
            {
                if (RawList.FindIndex(i => string.Equals(i.Name, $"<GROUP>{langItemGroup.Key} Mirrors")) == -1)
                {
                    RawList.Add(new LangObject
                    {
                        Name = $"<GROUP>{langItemGroup.Key} Languages",
                        IsSpecial = true
                    });
                }
                RawList.AddRange(langItemGroup.ToList());
            }

            foreach (LangObject CList in RawList)
            {
                if (CleanList.FindIndex(i => string.Equals(i.Name, CList.Name)) == -1)
                {
                    CleanList.Add(CList);
                }
            }

            /* (Start Process) Check ServerList Status */
            ServerListUpdater.GetList();
        }
    }
}