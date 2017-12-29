using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatusTrackerUI
{
    public static class Helper
    {
        public static string SavedSrvName
        {
            get
            {
                string Result = null;
                if (Properties.Settings.Default.Server != null)
                    Result = Properties.Settings.Default.Server;
                return Result;
            }
            set
            {
                Properties.Settings.Default.Server = value.ToString();
            }
        }

        public static string SavedSumName
        {
            get
            {
                string Result = null;
                if (Properties.Settings.Default.SummonerName != null)
                    Result = Properties.Settings.Default.SummonerName;
                return Result;
            }
            set
            {
                Properties.Settings.Default.SummonerName = value;
            }
        }

        public static string SavedDevKey
        {
            get
            {
                string Result = null;
                if (Properties.Settings.Default.DevKey != null)
                    Result = Properties.Settings.Default.DevKey;
                return Result;
            }
            set
            {
                Properties.Settings.Default.DevKey = value;
            }
        }
    }
}
