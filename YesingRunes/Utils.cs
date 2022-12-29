using PoniLCU;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Documents;
using YesingRunes.Models;

namespace YesingRunes
{
    public struct TranslationStruct
    {
        public string name { get; set; }
        public string description { get; set; }
    }
    public static class Utils
    {
        // Dictionary <
        //     int = PathID
        //     List<int> RuneIDS
        // >
        public static LeagueClient LCUClient;
        public static List<RiotRunePath> RunePaths = new List<RiotRunePath>();
        public static Dictionary<string, string> TranslateLang = new Dictionary<string, string>()
        {
            { "cs_CZ",   "Czech (Czech Republic)"  },
            { "el_GR",   "Greek (Greece)" },
            { "pl_PL",   "Polish (Poland)" },
            { "ro_RO",   "Romanian (Romania)" },
            { "hu_HU",   "Hungarian (Hungary)" },
            { "en_GB",   "English (United Kingdom)" },
            { "de_DE",   "German (Germany)" },
            { "es_ES",   "Spanish (Spain)" },
            { "it_IT",   "Italian (Italy)" },
            { "fr_FR",   "French (France)" },
            { "ja_JP",   "Japanese (Japan)" },
            { "ko_KR",   "Korean (Korea)" },
            { "es_MX",   "Spanish (Mexico)" },
            { "es_AR",   "Spanish (Argentina)" },
            { "pt_BR",   "Portuguese (Brazil)" },
            { "en_US",   "English (United States)" },
            { "en_AU",   "English (Australia)" },
            { "ru_RU",   "Russian (Russia)" },
            { "tr_TR",   "Turkish (Turkey)" },
            { "ms_MY",   "Malay (Malaysia)" },
            { "en_PH",   "English (Republic of the Philippines)" },
            { "en_SG",   "English (Singapore)" },
            { "th_TH",   "Thai (Thailand)" },
            { "vn_VN",   "Vietnamese (Viet Nam)" },
            { "id_ID",   "Indonesian (Indonesia)" },
            { "zh_MY",   "Chinese (Malaysia)" },
            { "zh_CN",   "Chinese (China)" },
            { "zh_TW",   "Chinese (Taiwan)" },
        };

        public static Dictionary<int, TranslationStruct> runeTranslation = new Dictionary<int, TranslationStruct>();

        public static List<YesingRunePage> runePages = new List<YesingRunePage>();

        public static string LocalAppdata => $"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}/YesingRunes";

        public static void Init()
        {
            LCUClient = new LeagueClient(LeagueClient.credentials.lockfile);

            var rnpt = JsonSerializer.Deserialize<List<RiotRunePath>>(File.ReadAllText("./Data/runes.json"));
            if (rnpt is not null)
            {
                RunePaths = rnpt;
            }

            var statmods = JsonSerializer.Deserialize<Dictionary<string, TranslationStruct>>(File.ReadAllText("./Data/statmods.json"));
            if (statmods is not null)
            {
                foreach (var stat in statmods)
                {
                    if(int.TryParse(stat.Key, out int val))
                    {
                        runeTranslation[val] = stat.Value;
                    }
                }
            }
        }

        public static void EquipPage(YesingRunePage page)
        {
            var res = LCUClient.Request(LeagueClient.requestMethod.POST, "/lol-perks/v1/pages", page.ToJson()).Result;
            if(res.StartsWith("{\"errorCode\":\""))
            {
                if(res.Contains("Max pages reached"))
                {
                    var current = LCUClient.Request(LeagueClient.requestMethod.GET, "/lol-perks/v1/currentpage").Result;
                    var currentjson = JsonSerializer.Deserialize<RiotRunePage>(current);

                    LCUClient.Request(LeagueClient.requestMethod.DELETE, $"/lol-perks/v1/pages/{currentjson.id}");
                    LCUClient.Request(LeagueClient.requestMethod.POST, "/lol-perks/v1/pages", page.ToJson());
                }
            }
        }

        public static void DownloadCurrentRunePages()
        {
            var res = LCUClient.Request(LeagueClient.requestMethod.GET, "/lol-perks/v1/pages").Result;
            var pages = JsonSerializer.Deserialize<List<RiotRunePage>>(res);
            if (pages is not null)
            {
                foreach (var page in pages)
                {
                    var yes = page.Yesified;

                    File.WriteAllText($"{LocalAppdata}/Pages/{yes.PageID}.json", JsonSerializer.Serialize(yes));
                }
            }
        }

        public static void ImportLocalRunePages()
        {
            foreach (var file in Directory.GetFiles($"{LocalAppdata}/Pages/"))
            {
                runePages.Add(JsonSerializer.Deserialize<YesingRunePage>(File.ReadAllText(file)));
            }
        }

        public static void SaveRunes()
        {
            foreach (var page in runePages)
            {
                File.WriteAllText($"{LocalAppdata}/Pages/{page.PageID}.json", JsonSerializer.Serialize(page));
            }
        }

        public static bool IsValidID(long id)
        {
            foreach (var page in runePages)
            {
                if (page.PageID == id) return false;
            }
            foreach (var file in Directory.GetFiles($"{LocalAppdata}/Pages/"))
            {
                if (Path.GetFileNameWithoutExtension(file) == id.ToString()) return false; 
            }
            return true;
        }

        public static Random rand = new Random(DateTime.Now.Millisecond);
        public static long GenerateID()
        {
            return rand.NextInt64();
        }

        public static long GenerateValidID()
        {
            long val = GenerateID();
            while (!IsValidID(val))
            {
                if (IsValidID(val)) return val;
                val = GenerateID();
            }
            return val;
        }
    }
}
