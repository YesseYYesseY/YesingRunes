using System.Text.Json;

const string DateFormat = "ddd, dd MMM yyyy HH:mm:ss 'GMT'";

Directory.CreateDirectory("./Data/Images/Champs/");

using (var client = new HttpClient())
{
    if(!client.DownloadFile("https://ddragon.leagueoflegends.com/api/versions.json", "./Data/versions.json")) throw new FileNotFoundException("./Data/versions.json does not exist!");

    var versions = JsonSerializer.Deserialize<string[]>(File.ReadAllText("./Data/versions.json"));
    
    string version = "12.23.1"; // default
    if(versions is not null &&
       versions.Length > 0)
    {
        version = versions[0];
    }

    File.Create("./Data/version.json").Close();
    File.WriteAllText("./Data/version.json", version);

    if (!client.DownloadFile($"http://ddragon.leagueoflegends.com/cdn/{version}/data/en_US/champion.json", "./Data/champion.json")) throw new FileNotFoundException("./Data/champion.json does not exist!");

    var championjson = JsonSerializer.Deserialize<OuterChampion>(File.ReadAllText("./Data/champion.json"));
    Dictionary<string, string> downloads = new Dictionary<string, string>();
    foreach (var champ in championjson.data)
    {
        downloads.Add($"http://ddragon.leagueoflegends.com/cdn/12.23.1/img/champion/{champ.Value.image.full}", $"./Data/Images/Champs/{champ.Value.image.full}");
        //client.DownloadFile($"http://ddragon.leagueoflegends.com/cdn/12.23.1/img/champion/{champ.Value.image.full}", $"./Data/Images/Champs/{champ.Value.image.full}");
    }
    client.DownloadFiles(downloads);
}


static string GetCurrentTime()
{
    return DateTime.Now.ToUniversalTime().ToString(DateFormat);
}

public static class Extensions
{
    static string DateFormat = "ddd, dd MMM yyyy HH:mm:ss 'GMT'";
    public static HttpResponseMessage? Get(this HttpClient client, string uri)
    {
        return client.Send(new HttpRequestMessage(HttpMethod.Get, uri));
    }


    /*
     * true = File exists
     * false = File does not exist
     */
    public static bool DownloadFile(this HttpClient client, string uri, string outPath)
    {
        var req = new HttpRequestMessage(HttpMethod.Get, uri);
        if(File.Exists(outPath))
        {
            req.Headers.Add("If-Modified-Since", File.GetLastWriteTimeUtc(outPath).ToString(DateFormat));
        }
        var res = client.Send(req);

        if (res.StatusCode == System.Net.HttpStatusCode.NotModified)
        {
            Console.WriteLine($"Latest version of {Path.GetFileName(outPath)} is already downloaded!");
            return true;
        }

        if (res.Content is not null)
        {
            using (var stream = File.Create(outPath))
            {
                Console.WriteLine($"Saving {Path.GetFileName(outPath)} in {outPath}");
                res.Content.ReadAsStream().CopyTo(stream);
                return true;
            }
        }
        return false;
    }

    public static void DownloadFiles(this HttpClient client, Dictionary<string, string> uris)
    {
        List<Task> tasks = new List<Task>();
        foreach (var tempuri in uris)
        {
            var uri = tempuri.Key;
            var outPath = tempuri.Value;

            var req = new HttpRequestMessage(HttpMethod.Get, uri);
            if (File.Exists(outPath))
            {
                req.Headers.Add("If-Modified-Since", File.GetLastWriteTimeUtc(outPath).ToString(DateFormat));
            }
            var task = client.SendAsync(req);
            task.ContinueWith(t =>
            {
                var res = t.Result;
                if (res.StatusCode == System.Net.HttpStatusCode.NotModified)
                {
                    Console.WriteLine($"Latest version of {Path.GetFileName(outPath)} is already downloaded!");
                    return;
                }

                if (res.Content is not null)
                {
                    using (var stream = File.Create(outPath))
                    {
                        Console.WriteLine($"Saving {Path.GetFileName(outPath)} in {outPath}");
                        res.Content.ReadAsStream().CopyTo(stream);
                        return;
                    }
                }
            });
            tasks.Add(task);
        }
        foreach (var task in tasks)
        {
            task.Wait();
        }
    }
}

struct ChampionStats
{
    public float hp{ get; set; }
    public float hpperlevel{ get; set; }
    public float mp{ get; set; }
    public float mpperlevel{ get; set; }
    public float movespeed{ get; set; }
    public float armor{ get; set; }
    public float armorperlevel{ get; set; }
    public float spellblock{ get; set; }
    public float spellblockperlevel{ get; set; }
    public float attackrange{ get; set; }
    public float hpregen{ get; set; }
    public float hpregenperlevel{ get; set; }
    public float mpregen{ get; set; }
    public float mpregenperlevel{ get; set; }
    public float crit{ get; set; }
    public float critperlevel{ get; set; }
    public float attackdamage{ get; set; }
    public float attackdamageperlevel{ get; set; }
    public float attackspeedperlevel{ get; set; }
    public float attackspeed{ get; set; }
}

struct ChampionImage
{
    public string full{ get; set; }
    public string sprite{ get; set; }
    public string group{ get; set; }
    public int x{ get; set; }
    public int y{ get; set; }
    public int w{ get; set; }
    public int h{ get; set; }
}

struct ChampionInfo
{
    public int attack{ get; set; }
    public int defense{ get; set; }
    public int magic{ get; set; }
    public int difficulty{ get; set; }
}

struct InnerChampion
{
    public string version{ get; set; }
    public string id{ get; set; }
    public string key{ get; set; }
    public string name{ get; set; }
    public string title{ get; set; }
    public string blurb{ get; set; }
    public ChampionInfo info{ get; set; }
    public ChampionImage image{ get; set; }
    public List<string> tags{ get; set; }
    public string partype{ get; set; }
    public ChampionStats stats{ get; set; }
}

struct OuterChampion
{
    public string type { get; set; }
    public string format { get; set; }
    public string version { get; set; }
    public Dictionary<string, InnerChampion> data { get; set; }
}