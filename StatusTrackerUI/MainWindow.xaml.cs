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

namespace StatusTrackerUI
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            // dirty af
            TitleBorder.MouseLeftButtonDown += (object o, MouseButtonEventArgs m) => 
                { Task.Run(() => this.Dispatcher.Invoke(Dragging)); };
            Home HomePage = new Home(this);
            Pager.Navigate(HomePage);
        }

        public void Test()
        {
            MessageBox.Show("");
        }

        private void Dragging()
        {
            this.DragMove();
        }
    }
}
