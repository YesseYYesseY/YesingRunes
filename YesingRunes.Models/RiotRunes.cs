using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YesingRunes.Models
{
	public struct RiotRune
	{
		public int id { get; set; }
		public string key { get; set; }
		public string icon { get; set; }
		public string name { get; set; }
		public string shortDesc { get; set; }
		public string longDesc { get; set; }
	}

	public struct RiotRunePathSlots
	{
		public List<RiotRune> runes { get; set; }
	}

	public struct RiotRunePath
	{
		public int id { get; set; }
		public string key { get; set; }
		public string icon { get; set; }
		public List<RiotRunePathSlots> slots { get; set; }
	}
}
