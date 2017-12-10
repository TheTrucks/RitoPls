using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RitoPls.Response;

namespace RitoPls.OfflineData
{
    //temporary workaround until riot will change their api to contain runes reforged
    public static class OfflineData
    {
        private static readonly Dictionary<int, Rune> RuneNames = new Dictionary<int, Rune>()
        {
            { 8005, new Rune("Press the Attack", "") },
            { 8229, new Rune("Arcane Comet", "") },
            { 8439, new Rune("Aftershock", "") },
            { 8128, new Rune("Dark Harvest", "") },
            { 8437, new Rune("Grasp of the Undying", "") },
            { 8465, new Rune("Guardian", "") },
            { 8112, new Rune("Electrocute", "") },
            { 8124, new Rune("Predator", "") },
            { 8214, new Rune("Summon Aery", "") },
            { 8230, new Rune("Phase Rush", "") },
            { 8359, new Rune("Kleptomancy", "") },
            { 8351, new Rune("Glacial Augment", "") },
            { 8326, new Rune("Unsealed Spellbook", "") },
            { 8008, new Rune("Lethal Tempo", "") },
            { 8021, new Rune("Fleet Footwork", "") }
        };

        private struct Rune
        {
            public string name;
            public string description;
            public Rune(string Name, string Desc)
            {
                name = Name;
                description = Desc;
            }
        }
        public static Static.TempRune GetRune(long id)
        {
            Static.TempRune Tmp = new Static.TempRune();
            Tmp.id = (int)id;
            if (RuneNames.ContainsKey(Tmp.id))
            {
                Rune TmpRune = RuneNames[Tmp.id];
                Tmp.name = TmpRune.name;
                Tmp.description = TmpRune.description;
            }
            else
            {
                Tmp.name = Tmp.id + " not found";
                Tmp.description = "null desc";
            }
            return Tmp;
        }
    }
}
