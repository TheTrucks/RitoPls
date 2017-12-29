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
    public partial class MainWindow : Window
    {
        Page HomePage;
        Page OptPage;
        public event EventHandler ChangedOpts;
        public event EventHandler UnloadHomepage;
        public MainWindow()
        {
            InitializeComponent();
            // dirty af
            TitleBorder.MouseLeftButtonDown += (object o, MouseButtonEventArgs m) => 
                { Task.Run(() => this.Dispatcher.Invoke(Dragging)); };
            HomePage = new Home(this);
            Pager.Navigate(HomePage);
        }

        public void ShowOptions()
        {
            UnloadHomepage(this, new EventArgs());
            OptPage = new Options(this);
            Pager.Navigate(OptPage);
        }

        public void OptsSaved()
        {
            OptPage = null;
            Pager.Navigate(HomePage);
            ChangedOpts(this, new EventArgs());
        }

        private void Dragging()
        {
            this.DragMove();
        }
    }
}