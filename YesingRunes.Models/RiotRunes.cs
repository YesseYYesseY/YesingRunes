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

	public struct RiotRunePage
	{
		public int[] autoModifiedSelections { get; set; }
		public bool current { get; set; }
		public int id { get; set; }
		public bool isActive { get; set; }
		public bool isDeletable { get; set; }
		public bool isEditable { get; set; }
		public bool isValid { get; set; }
		public long lastModified { get; set; }
		public string name { get; set; }
		public int order { get; set; }
		public int primaryStyleId { get; set; }
		public int[] selectedPerkIds { get; set; }
		public int subStyleId { get; set; }

		public YesingRunePage Yesified => new YesingRunePage()
		{
			Name= name,
			CurrentRunes = selectedPerkIds.ToList(),
			PrimaryRunePath = primaryStyleId,
			SecondaryRunePath = subStyleId,
			PageID = id
		};
    }
}
