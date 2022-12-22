using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
