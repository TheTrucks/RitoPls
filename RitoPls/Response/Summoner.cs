using System;
using System.Collections.Generic;

namespace RitoPls.Response
{
    public class SummonerDto
    {
        public int profileIconId { get; set; }
        public string name { get; set; }
        public long summonerLevel { get; set; }
        public long revisionDate { get; set; }
        public long id { get; set; }
        public long accountId { get; set; }
    }
}
