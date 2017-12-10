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
    public partial class Options : Page
    {
        private MainWindow Master;
        public Options(MainWindow MasterWin)
        {
            InitializeComponent();
            Master = MasterWin;
        }

        private void SaveSettings(string Server, string SumName)
        {
            Properties.Settings.Default.SummonerName = SumName;
            Properties.Settings.Default.Server = Server;
        }

        private Server.ServerList SrvName()
        {
            Server.ServerList Tmp = Server.ServerList.EUW;
            string Result = null;
            if (Properties.Settings.Default.Server != null)
                Result = Properties.Settings.Default.Server;
            if (Result != null)
                Enum.TryParse(Result, out Tmp);
            return Tmp;
        }

        private string SumName()
        {
            string Result = null;
            if (Properties.Settings.Default.SummonerName != null)
                Result = Properties.Settings.Default.SummonerName;
            return Result;
        }
    }
}
