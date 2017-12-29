using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using RitoPls.Request;

namespace StatusTrackerUI
{
    public partial class Home : Page
    {
        private MainWindow Master;
        private Request GetData;
        private System.Timers.Timer Tracker;
        private bool TrackerLastState = false;
        private string CurrentSumm;

        public Home(MainWindow MasterWindow)
        {
            InitializeComponent();
            Tracker.AutoReset = true;
            Tracker.Interval = 60000;
            Master = MasterWindow;
            Settings.MouseLeftButtonUp += (object o, MouseButtonEventArgs m) => { MasterWindow.ShowOptions(); };
            Master.ChangedOpts += (object o, EventArgs e) => { LoadOpts(); };
            Master.UnloadHomepage += (object o, EventArgs e) => { Tracker.Stop(); };
            Tracker.Elapsed += (object o, System.Timers.ElapsedEventArgs e) => { TrackLeague(); };
            LoadOpts();
        }

        private void LoadOpts()
        {
            string SrvName = Helper.SavedSrvName;
            Server.ServerList SrvEnum;
            Enum.TryParse(SrvName, out SrvEnum);
            if (SrvEnum.ToString() == "") SrvEnum = Server.ServerList.EUW;
            CurrentSumm = Helper.SavedSumName;
            Srv.Text = SrvEnum.ToString();
            Smr.Text = CurrentSumm;
            string DevKey = Helper.SavedDevKey;
            GetData = new Request(DevKey, SrvEnum, Write);
            Tracker.Start();
        }

        private void TrackLeague()
        {
            var Procs = System.Diagnostics.Process.GetProcesses();
            bool Found = false;
            foreach (var Process in Procs)
            {
                if (Process.ProcessName == "leagueprocessidk")
                {
                    Found = true;
                    break;
                }
            }

            if (Found)
            {
                TrackerLastState = true;
                var Summ = GetData.GetSummonerByName(CurrentSumm);
                if (Summ != null)
                {
                    var CurGame = GetData.GetCurrentGameInfo(Summ.id.ToString());
                    if (CurGame != null)
                    {
                        RitoPls.Response.CurrentGameParticipant Me = null;
                        foreach (var Part in CurGame.participants)
                        {
                            if (Part.summonerName == CurrentSumm)
                            {
                                Me = Part;
                                break;
                            }
                        }
                        var Champ = GetData.GetChampById(Me.championId.ToString());
                        var Rune = RitoPls.OfflineData.OfflineData.GetRune(Me.perks.perkIds[0]);
                        //send data to the discord
                    }
                    else
                        Write("Nor current game found");
                }
                else
                    Write("Unable to find summoner " + CurrentSumm);
            }
            else
            {
                if (TrackerLastState)
                { 
                    //make an empty request to clean discord status
                }
            }
        }

        private void Write(string input)
        {
            Logging.Text += input + "\r\n";
        }
    }
}