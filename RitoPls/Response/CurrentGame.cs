using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace RitoPls.Response
{
    [JsonObject(MemberSerialization.OptIn)]
    public class CurrentGameInfo
    {
        [JsonProperty]
        public long gameId { get; set; }
        [JsonProperty]
        public long gameStartTime { get; set; }
        [JsonProperty]
        public string platformId { get; set; }
        [JsonProperty]
        public string gameMode { get; set; }
        [JsonProperty]
        public long mapId { get; set; }
        [JsonProperty]
        public string gameType { get; set; }
        [JsonProperty]
        public IList<BannedChampions> bannedChampions { get; set; }
        [JsonProperty]
        public Observer observers { get; set; }
        [JsonProperty]
        public IList<CurrentGameParticipant> participants { get; set; }
        [JsonProperty]
        public long gameLength { get; set; }
        [JsonProperty]
        public long gameQueueConfigId { get; set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    public class BannedChampions
    {
        [JsonProperty]
        public int pickTurn { get; set; }
        [JsonProperty]
        public long championId { get; set; }
        [JsonProperty]
        public long teamId { get; set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    public class Observer
    {
        [JsonProperty]
        public string encryptionKey { get; set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    public class CurrentGameParticipant
    {
        [JsonProperty]
        public long profileIconId { get; set; }
        [JsonProperty]
        public long championId { get; set; }
        [JsonProperty]
        public string summonerName { get; set; }
        [JsonProperty]
        public IList<GameCustomizationObject> gameCustomizationObjects { get; set; }
        [JsonProperty]
        public bool bot { get; set; }
        [JsonProperty]
        public Perks perks { get; set; }
        [JsonProperty]
        public long spell2Id { get; set; }
        [JsonProperty]
        public long teamId { get; set; }
        [JsonProperty]
        public long spell1Id { get; set; }
        [JsonProperty]
        public long summonerId { get; set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    public class GameCustomizationObject
    {
        [JsonProperty]
        public string category { get; set; }
        [JsonProperty]
        public string content { get; set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    public class Perks
    {
        [JsonProperty]
        public long perkStyle { get; set; }
        [JsonProperty]
        public IList<long> perkIds { get; set; }
        [JsonProperty]
        public long perkSubStyle { get; set; }
    }
}

