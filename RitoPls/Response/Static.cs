using System;
using System.Collections.Generic;

namespace RitoPls.Response
{
    public class Static
    {
        public class ChampionDto
        {
            public string name { get; set; }
            public string title { get; set; }
            public int id { get; set; }
            public string key { get; set; }
            //TODO: expanded list of champ's data; isn't neccessary for my need thou
        }

        public class MasteryDto //doesn't rly works since runes reforged, will keep it until changes in riot api
        {
            public string name { get; set; }
            public int id { get; set; }
            public IList<string> description { get; set; }
        }

        public class TempRune //riot api have no new runes, so it's temporary workaround
        {
            public int id;
            public string name;
            public string description;
        }
    }
}
