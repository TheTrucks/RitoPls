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
            Cancer.Click += (object o, RoutedEventArgs m) => { Master.OptsSaved(); };
            Oki.Click += (object o, RoutedEventArgs m) =>
                {
                    Save();
                    Master.OptsSaved();
                };
            Load();
        }

        public void Load()
        {
            SummName.Text = Helper.SavedSumName;
            foreach (ComboBoxItem item in ServerName.Items)
            {
                if (item.Content.ToString() == Helper.SavedSrvName)
                {
                    ServerName.SelectedItem = item;
                    break;
                }
            }
            DevKey.Text = Helper.SavedDevKey;
        }

        public void Save()
        {
            Helper.SavedSumName = SummName.Text;
            Helper.SavedSrvName = (ServerName.SelectedItem as ComboBoxItem).Content.ToString();
            Helper.SavedDevKey = DevKey.Text;
            Properties.Settings.Default.Save();
        }
    }
}