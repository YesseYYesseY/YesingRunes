namespace YesingRunes.Models
{
    public struct YesingRunePage
    {
        public string Name { get; set; }
        public List<int> CurrentRunes { get; set; }
        public int PrimaryRunePath { get; set; }
        public int SecondaryRunePath { get; set; }
    }
}