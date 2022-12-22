using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YesingRunes.Models
{
    public struct ChampionStats
    {
        public float hp { get; set; }
        public float hpperlevel { get; set; }
        public float mp { get; set; }
        public float mpperlevel { get; set; }
        public float movespeed { get; set; }
        public float armor { get; set; }
        public float armorperlevel { get; set; }
        public float spellblock { get; set; }
        public float spellblockperlevel { get; set; }
        public float attackrange { get; set; }
        public float hpregen { get; set; }
        public float hpregenperlevel { get; set; }
        public float mpregen { get; set; }
        public float mpregenperlevel { get; set; }
        public float crit { get; set; }
        public float critperlevel { get; set; }
        public float attackdamage { get; set; }
        public float attackdamageperlevel { get; set; }
        public float attackspeedperlevel { get; set; }
        public float attackspeed { get; set; }
    }

    public struct ChampionImage
    {
        public string full { get; set; }
        public string sprite { get; set; }
        public string group { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public int w { get; set; }
        public int h { get; set; }
    }

    public struct ChampionInfo
    {
        public int attack { get; set; }
        public int defense { get; set; }
        public int magic { get; set; }
        public int difficulty { get; set; }
    }

    public struct InnerChampion
    {
        public string version { get; set; }
        public string id { get; set; }
        public string key { get; set; }
        public string name { get; set; }
        public string title { get; set; }
        public string blurb { get; set; }
        public ChampionInfo info { get; set; }
        public ChampionImage image { get; set; }
        public List<string> tags { get; set; }
        public string partype { get; set; }
        public ChampionStats stats { get; set; }
    }

    public struct OuterChampion
    {
        public string type { get; set; }
        public string format { get; set; }
        public string version { get; set; }
        public Dictionary<string, InnerChampion> data { get; set; }
    }
}
