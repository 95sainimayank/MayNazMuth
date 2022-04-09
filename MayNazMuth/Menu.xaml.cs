﻿using System;
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
            CloseAllWindows();
            ToBookFlight.Show();

        }
        
        public void OpenAddAirportWindow(object sender, EventArgs args)
        {            
           
            AirportWindow Airport = new AirportWindow();
            CloseAllWindows();
            Airport.Show();
            
            
        }
        public void OpenAddPassengerWindow(object sender, EventArgs args)
        {
           
            AddPassengerWindow Passenger = new AddPassengerWindow();
            CloseAllWindows();
            Passenger.Show();
        }
        public void OpenUpdatePassengerWindow(object sender, EventArgs args)
        {

            UpdatePassengerWindow updatePassenger = new UpdatePassengerWindow();
            CloseAllWindows();
            updatePassenger.Show();
        }
        
        public void OpenFlightDetailWindow(object sender, EventArgs args)
        {
            FlightDetailWindow flightDetail = new FlightDetailWindow();
            CloseAllWindows();
            flightDetail.Show();
        }
        public void OpenPassengerReportWindow(object sender, EventArgs args)
        {
            PassengerReportWindow PassengerReport = new PassengerReportWindow();
            CloseAllWindows();
            PassengerReport.Show();
        }
        public void OpenPaymentReportWindow(object sender, EventArgs args)
        {
            PaymentReportWindow PaymentReport = new PaymentReportWindow();
            CloseAllWindows();
            PaymentReport.Show();
        }
        public void OpenPaymentWindow(object sender, EventArgs args)
        {
            PaymentWindow Payment = new PaymentWindow();
            CloseAllWindows();
            Payment.Show();
        }
        

        public void CloseAllWindows()
        {
            foreach (Window window in Application.Current.Windows)
            {
                window.Hide();
            }
        }
    }
}
