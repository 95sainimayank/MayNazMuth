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

namespace MayNazMuth
{
    /// <summary>
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class Menu : UserControl
    {
        public Menu()
        {
            InitializeComponent();
        }
        public void ToBookFlightWindow(object sender, EventArgs args)
        {
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
        public void OpenAddAirportWindow(object sender, EventArgs args)
        {
            AirportWindow Airport = new AirportWindow();
            this.Visibility = Visibility.Hidden;
            Airport.Show();
        }
        public void OpenAddPassengerWindow(object sender, EventArgs args)
        {
            AddPassengerWindow Passenger = new AddPassengerWindow();
            this.Visibility = Visibility.Hidden;
            Passenger.Show();
        }
        public void OpenFlightDetailWindow(object sender, EventArgs args)
        {
            FlightDetailWindow flightDetail = new FlightDetailWindow();
            this.Visibility = Visibility.Hidden;
            flightDetail.Show();
        }
        public void OpenPassengerReportWindow(object sender, EventArgs args)
        {
            PassengerReportWindow PassengerReport = new PassengerReportWindow();
            this.Visibility = Visibility.Hidden;
            PassengerReport.Show();
        }
        public void OpenPaymentReportWindow(object sender, EventArgs args)
        {
            PaymentReportWindow PaymentReport = new PaymentReportWindow();
            this.Visibility = Visibility.Hidden;
            PaymentReport.Show();
        }      
    }
}
