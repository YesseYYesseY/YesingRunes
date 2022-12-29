namespace YesingRunes.Models
{
    public struct YesingRunePage
    {
        public string Name { get; set; } = "";
        public List<int> CurrentRunes { get; set; } = new List<int>();
        public int PrimaryRunePath { get; set; }
        public int SecondaryRunePath { get; set; }
        public long PageID { get; set; }

        public YesingRunePage()
        {

        }

        public YesingRunePage(long customID)
        {
            PageID = customID;
        }

        public string ToJson()
        {
            string ret = $$"""{"current":true,"name":"{{Name}}","primaryStyleId":{{PrimaryRunePath}},"selectedPerkIds":[""";

            foreach (var rune in CurrentRunes)
            {
                ret += $"{rune},";
            }
            ret = ret.Remove(ret.Length-1);
            ret += $$"""],"subStyleId":{{SecondaryRunePath}}}""";

            return ret;
        }
    }
}
// {"name":"Shyvana","primaryStyleId":8100,"subStyleId":8200,"selectedPerkIds":[8128,8143,8138,8105,8210,8236,5005,5008,5002],"current":true}