using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using RitoPls.Response;
using Newtonsoft.Json;
using System.Threading;

namespace RitoPls.Request
{
    //do not create multiple instances of this class, create one and make it accessible for all ur needs
    public sealed class Request
    {
        private static HttpClient RequestHandler = new HttpClient();
        private string AppKey;
        private string SrvEndpoint;
        private Action<string> OutInfo;
        Timer FrostControl;
        private bool IsFrozen = false;
        private DateTime UnfrozeIn;
        //static data can only be accessed 10 times per hour so caching at least last results will be nice
        private Dictionary<Type, object> Cache = new Dictionary<Type, object>();

        /// <summary>
        /// Class which helps you to take info from riot's api
        /// </summary>
        /// <param name="appKey">Riot Application Key (kinda sucks that u need to regenerate it each day)</param>
        /// <param name="CurrentServer">Server u're playing on (should be selected from Request.Server.ServerList)</param>
        /// <param name="Output">Ur void(string) which will process debug info (can be null)</param>
        public Request(string appKey, Server.ServerList CurrentServer, Action<string> Output)
        {
            SrvEndpoint = Server.GetServer(CurrentServer);
            AppKey = appKey;
            OutInfo = Output;
            RequestHandler.Timeout = TimeSpan.FromSeconds(5);
        }

        public CurrentGameInfo GetCurrentGameInfo(string SumID)
        {
            string Executable = "lol/spectator/v3/active-games/by-summoner/" + SumID;
            string Result = ExecuteRequest(Executable).Result;
            if (Result == null)
                return null;
            return JsonConvert.DeserializeObject<CurrentGameInfo>(Result);
        }

        public Static.ChampionDto GetChampById(string ID)
        {
            string Executable = "lol/static-data/v3/champions/" + ID;
            Static.ChampionDto Champ;
            if (Cache.ContainsKey(typeof(Static.ChampionDto)) &&
                ((Static.ChampionDto)(Cache[typeof(Static.ChampionDto)])).id.ToString() == ID)
                Champ = (Static.ChampionDto)(Cache[typeof(Static.ChampionDto)]);
            else
            {
                string Result = ExecuteRequest(Executable).Result;
                if (Result == null)
                    return null;
                Champ = JsonConvert.DeserializeObject<Static.ChampionDto>(Result);
                if (Cache.ContainsKey(typeof(Static.ChampionDto)))
                    Cache[typeof(Static.ChampionDto)] = Champ;
                else
                    Cache.Add(typeof(Static.ChampionDto), Champ);
            }
            return Champ;
        }

        public SummonerDto GetSummonerByName(string Name)
        {
            string Executable = "lol/summoner/v3/summoners/by-name/" + Name;
            SummonerDto Summoner;
            if (Cache.ContainsKey(typeof(SummonerDto)) &&
                ((SummonerDto)(Cache[typeof(SummonerDto)])).name == Name)
                Summoner = (SummonerDto)(Cache[typeof(SummonerDto)]);
            else
            {
                string Result = ExecuteRequest(Executable).Result;
                if (Result == null)
                    return null;
                Summoner = JsonConvert.DeserializeObject<SummonerDto>(Result);
                if (Cache.ContainsKey(typeof(SummonerDto)))
                    Cache[typeof(SummonerDto)] = Summoner;
                else
                    Cache.Add(typeof(SummonerDto), Summoner);
            }
            return Summoner;
        }

        private async Task<string> ExecuteRequest(string Req)
        {
            string Result;

            if (IsFrozen)
            {
                OutInfo(string.Format("Next request can be processed after {0} minutes", 
                    (UnfrozeIn - DateTime.Now).ToString(@"m\:ss")));
                return null;
            }

            HttpRequestMessage Msg = new HttpRequestMessage();
            Msg.RequestUri = new Uri(string.Format("https://{0}/{1}", SrvEndpoint, Req));
            Msg.Method = HttpMethod.Get;
            Msg.Headers.Add("X-Riot-Token", AppKey);

            HttpResponseMessage Response = await RequestHandler.SendAsync(Msg);
            if (Response.IsSuccessStatusCode)
            {
                Result = await Response.Content.ReadAsStringAsync();
            }
            else
            {
                Result = null;
                if (Response.Headers.RetryAfter != null)
                {
                    IsFrozen = true;
                    TimeSpan RetryTime = Response.Headers.RetryAfter.Delta ?? TimeSpan.FromSeconds(3500);
                    TimerCallback FrostCb = new TimerCallback(FrozeState);
                    UnfrozeIn = DateTime.Now.Add(RetryTime);
                    FrostControl = new Timer(FrostCb, false, (long)Math.Floor(RetryTime.TotalMilliseconds), -1);
                    OutInfo(string.Format("Retry after {0} minutes", RetryTime.ToString(@"m\:ss")));
                }
                else
                {
                    StringBuilder SB = new StringBuilder();
                    SB.AppendLine(string.Format("[Response Code] {0} ({1})", Response.StatusCode.ToString(), (int)Response.StatusCode));
                    foreach (var Header in Response.Headers)
                    {
                        SB.Append("[");
                        SB.Append(Header.Key);
                        SB.Append("]");
                        SB.Append(": ");
                        foreach (string Val in Header.Value)
                        {
                            SB.Append(Val);
                            SB.Append("; ");
                        }
                        SB.AppendLine();
                    }
                    OutInfo(SB.ToString());
                    SB.Clear();
                }
            }
            return Result;
        }

        private void FrozeState(object state)
        {
            IsFrozen = (bool)state;
            if (FrostControl != null)
                FrostControl.Dispose();
        }
    }

    public static class Server
    {
        private static readonly Dictionary<int, string> Servers = new Dictionary<int, string>()
        {
            {0, "br1.api.riotgames.com"},
            {1, "eun1.api.riotgames.com"},
            {2, "euw1.api.riotgames.com"},
            {3, "jp1.api.riotgames.com"},
            {4, "kr.api.riotgames.com"},
            {5, "la1.api.riotgames.com"},
            {6, "la2.api.riotgames.com"},
            {7, "na1.api.riotgames.com"},
            {8, "oc1.api.riotgames.com"},
            {9, "tr1.api.riotgames.com"},
            {10, "ru.api.riotgames.com"} //sucks massive balls since ru-riots spending a lot of riot's money on nothing
        };

        public enum ServerList
        {
            BR = 0,
            EUNE = 1,
            EUW = 2,
            JP = 3,
            KR = 4,
            LAN = 5,
            LAS = 6,
            NA = 7,
            OCE = 8,
            TR = 9,
            RU = 10
        }

        public static string GetServer(ServerList srv)
        {
            return Servers[(int)srv];
        }
    }
}
