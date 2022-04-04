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

namespace MayNazMuth {
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();

            EventHandlers();
        }

        public void EventHandlers() {
            BookFlightButton.Click += ToBookFlightWindow;
            AddAirportButton.Click += AddAirportWindow;
        }

        public void ToBookFlightWindow(object sender, EventArgs args) {
            BookFlightWindow ToBookFlight = new BookFlightWindow();
            this.Visibility = Visibility.Hidden;
            ToBookFlight.Show();
        }

        public void AddAirportWindow(object sender, EventArgs args)
        {
            AirportWindow Airport = new AirportWindow();
            this.Visibility = Visibility.Hidden;
            Airport.Show();
        }
    }
}
