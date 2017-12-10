using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RitoPls.Request;
using RitoPls.Response;

namespace testingGrounds
{
    class Program
    {
        public static Request GetData;
        static void Main(string[] args)
        {
            GetData = new Request("RGAPI-744103c8-0ebf-4f45-9eaa-89328e6ccc1d", Server.ServerList.EUW, Out);
            Cmd();
        }

        static void Cmd()
        {
            string cmd = Console.ReadLine();

            if (cmd.StartsWith("getCurGame"))
            {
                string[] pieces = cmd.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                if (pieces.Length == 2)
                {
                    string ID = pieces[1];
                    SummonerDto Chuvak = GetData.GetSummonerByName(ID);
                    if (Chuvak == null)
                    {
                        Out("Cant find that dude", ConsoleColor.DarkYellow);
                    }
                    else
                    {
                        CurrentGameInfo Data = GetData.GetCurrentGameInfo(Chuvak.id.ToString());
                        if (Data == null)
                            Out("Cant find game", ConsoleColor.DarkYellow);
                        else
                        {
                            Out(Data.gameMode + " " + Data.gameType, ConsoleColor.DarkCyan);
                            foreach (CurrentGameParticipant Chelik in Data.participants)
                            {
                                if (Chelik.summonerId == Chuvak.id)
                                {
                                    try
                                    {
                                        Static.ChampionDto Champ = GetData.GetChampById(Chelik.championId.ToString());
                                        Static.TempRune Rune = RitoPls.OfflineData.OfflineData.GetRune(Chelik.perks.perkIds[0]);
                                        Out(Champ.name + " with " + Rune.name, ConsoleColor.DarkGreen);
                                    }
                                    catch (Exception e)
                                    {
                                        Out(@"\\\\\ Exception \\\\\", ConsoleColor.DarkGray);
                                        Out(e.ToString());
                                        Out(@"/////////////////////", ConsoleColor.DarkGray);
                                        break;
                                    }
                                    break;
                                }
                            }
                        } 
                    }
                }
                else
                    Ebanuty();
            }
            else
                Ebanuty();

            Cmd();
        }

        static void Out(string Text)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(Text);
            Console.ForegroundColor = ConsoleColor.Gray;
        }
        static void Out(string Text, ConsoleColor Color)
        {
            Console.ForegroundColor = Color;
            Console.WriteLine(Text);
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        static void Ebanuty()
        {
            char[] Text = "ti che, ebanuty?".ToCharArray();
            ConsoleColor[] Colors = ((ConsoleColor[])ConsoleColor.GetValues(typeof(ConsoleColor)))
                .Where(x => x.ToString("G") != "Black" && !x.ToString("G").Contains("Dark")).ToArray<ConsoleColor>();
            Random Rand = new Random((int)DateTime.UtcNow.Ticks);
            foreach (char C in Text)
            {
                Console.ForegroundColor = Colors[Rand.Next(0, Colors.Length - 1)];
                Console.Write(C);
            }
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}
