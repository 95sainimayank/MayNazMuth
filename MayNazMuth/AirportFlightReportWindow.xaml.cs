using MayNazMuth.Utilities;
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
using System.Windows.Shapes;
using MayNazMuth.Entities;

namespace MayNazMuth
{
    /// <summary>
    /// Interaction logic for AirportFlightReportWindow.xaml
    /// </summary>
    public partial class AirportFlightReportWindow : Window
    {

        List<Flight> FlightList = new List<Flight>();
        List<Airport> AirportList = new List<Airport>();
        int ApId;
        int selectedFlightItem;
        string ApName;

        public AirportFlightReportWindow(string airportId)
        {
            //incoming AirportId of selected row from Airport window
            ApId = Convert.ToInt32(airportId);

            InitializeComponent();

            //turn the event handlers off
            ToggleEventHandlers(false);

            ShowAirportName();

            //Initialize the data grid
            SetupGrid();

            //populate the grid
            Updategrid();

            //Turn event handlers on
            ToggleEventHandlers(true);
        }

        private void ShowAirportName()
        {
            using (var ctx = new CustomDbContext())
            {
                AirportList = ctx.Airports.ToList<Airport>();                
                AFReportWindow .Title = "Airpot Name: " + AirportList.Where(x => x.AirportId == ApId).First().AirportName;
            }
        }

        private void ToggleEventHandlers(bool toggle)
        {
            if (toggle)
            {
                //turn on                               
                backToAirportButon.Click += backToAirportList;
                filterComboBox.SelectionChanged += filterFlights;
                FilterButton.Click += searchDate;
            }
            else
            {
                //Turn off               
                backToAirportButon.Click -= backToAirportList;
                filterComboBox.SelectionChanged -= filterFlights;
                FilterButton.Click -= searchDate;
            }
        }

        //this function calls by filter button clicking
        private void searchDate(object s, EventArgs e)
        {
            var startFrom = fromDatePicker.SelectedDate;
            var endTo = toDatePicker.SelectedDate;

            if(startFrom <= endTo)
            {
                //check combobox and do different filter
                switch (selectedFlightItem)
                {
                    case 0:
                        AllFlightsFromToDate(startFrom, endTo);
                        break;
                    case 1:
                        ArrivalFlightsFromToDate(startFrom, endTo);
                        break;
                    case 2:
                        DepartureFlightsFromTo(startFrom, endTo);
                        break;
                }
            }
            else
            {
                MessageBox.Show("Start Date can not be later than End Date");
            }
        }


        //filter departual flights in selected period
        private void DepartureFlightsFromTo(DateTime? startFrom, DateTime? endTo)
        {
            Console.WriteLine("Depart"+ startFrom + endTo);
            using (var ctx = new CustomDbContext())
            {
                FlightList = ctx.Flights.ToList<Flight>();
                var DepartingList = FlightList.Where(x => x.SourceAirportId == ApId && x.DepartureTime >= startFrom && x.ArrivalTime <= endTo);
               
                AirportFlightGrid.Items.Clear();
                foreach (Flight f in DepartingList)
                {
                    AirportFlightGrid.Items.Add(f);
                }
            }
        }

        //filter arrival flights in selected period
        private void ArrivalFlightsFromToDate(DateTime? startFrom, DateTime? endTo)
        {
            Console.WriteLine("Arrving"+ startFrom+ endTo);
            using (var ctx = new CustomDbContext())
            {
                FlightList = ctx.Flights.ToList<Flight>();
                var ArrivalList = FlightList.Where(x => x.DestinationAirportId == ApId && x.DepartureTime >= startFrom && x.ArrivalTime <= endTo);
                
                AirportFlightGrid.Items.Clear();
                foreach (Flight f in ArrivalList)
                {
                    AirportFlightGrid.Items.Add(f);
                }
            }
        }

        //filter all flights in selected period
        private void AllFlightsFromToDate(DateTime? startFrom, DateTime? endTo)
        {
            Console.WriteLine("all"+ startFrom+ endTo);
            using (var ctx = new CustomDbContext())
            {
                FlightList = ctx.Flights.ToList<Flight>();
                var AllList = FlightList.Where(x => (x.SourceAirportId == ApId || x.DestinationAirportId == ApId) && x.DepartureTime >= startFrom && x.ArrivalTime <= endTo);               
                AirportFlightGrid.Items.Clear();
                foreach (Flight f in AllList)
                {
                    AirportFlightGrid.Items.Add(f);
                }
            }
        }

       //This function calls by select an option of combobox
        private void filterFlights(object s, EventArgs e)
        {
            selectedFlightItem =  filterComboBox.SelectedIndex;
            switch (selectedFlightItem)
            {
                case 0: showAll();
                    break;
                case 1: showArrivals();
                    break;
                case 2: showDepartures();
                    break;
            }
        }

        //This function calls when Departure Flights selected in combobox
        private void showDepartures()
        {
            using (var ctx = new CustomDbContext())
            {
                FlightList = ctx.Flights.ToList<Flight>();               
                var DepartingFlights = FlightList.Where(x => x.SourceAirportId == ApId);
                AirportFlightGrid.Items.Clear();
                foreach (Flight f in DepartingFlights)
                {
                    AirportFlightGrid.Items.Add(f);
                }
            }
        }

        //This function calls when Arrival Flights selected in combobox
        private void showArrivals()
        {
            using (var ctx = new CustomDbContext())
            {
                FlightList = ctx.Flights.ToList<Flight>();               
                var ArrivingFlights = FlightList.Where(x => x.DestinationAirportId == ApId);                
                AirportFlightGrid.Items.Clear();
                foreach (Flight f in ArrivingFlights)
                {
                    AirportFlightGrid.Items.Add(f);
                }
            }
        }

        //This Function calls when All Flight selected in filter combobox
        private void showAll()
        {
            using (var ctx = new CustomDbContext())
            {
                FlightList = ctx.Flights.ToList<Flight>();
                var FoundFlights = FlightList.Where(x => x.SourceAirportId == ApId || x.DestinationAirportId == ApId);
                AirportFlightGrid.Items.Clear();
                foreach (Flight f in FoundFlights)
                {
                    AirportFlightGrid.Items.Add(f);
                }
            }
        }

        //This function calls by clickng on back button
        private void backToAirportList(object s, EventArgs e)
        {
            AirportWindow Airport = new AirportWindow();
            CloseAllWindows();
            Airport.Show();
        }

        //This function close all open window
        public void CloseAllWindows()
        {
            foreach (Window window in Application.Current.Windows)
            {
                window.Hide();
            }
        }

        //Update Grid
        private void Updategrid()
        {
            //backToAirportButon.Content = ApId.ToString();
            using (var ctx = new CustomDbContext())
            {
                FlightList = ctx.Flights.ToList<Flight>();

                var FoundFlights = FlightList.Where(x => x.SourceAirportId == ApId || x.DestinationAirportId == ApId);
                var ArrivingFlights = FlightList.Where(x => x.DestinationAirportId == ApId);
                var DepartingFlights = FlightList.Where(x => x.SourceAirportId == ApId);

                AirportFlightGrid.Items.Clear();
                foreach (Flight f in FoundFlights)
                {
                    AirportFlightGrid.Items.Add(f);
                }
                TotalArrivingValueLable.Content = ArrivingFlights.Count();
                TotalDepartingValueLable.Content = DepartingFlights.Count();
            }
        }

        //Setup Grid
        private void SetupGrid()
        {
            AirportFlightGrid.SelectionMode = DataGridSelectionMode.Single;
            //Make it read only
            AirportFlightGrid.IsReadOnly = true;

            DataGridTextColumn FlightNumberColumn = new DataGridTextColumn();
            FlightNumberColumn.Header = "Flight Number";
            FlightNumberColumn.Binding = new Binding("FlightNo");

            DataGridTextColumn AirlineNameColumn = new DataGridTextColumn();
            AirlineNameColumn.Header = "Airline Name";
            AirlineNameColumn.Binding = new Binding("AirlineName");

            DataGridTextColumn DepartureTimeColumn = new DataGridTextColumn();
            DepartureTimeColumn.Header = "Departure Date/Time";
            DepartureTimeColumn.Binding = new Binding("DepartureTime");

            DataGridTextColumn ArrivalTimeColumn = new DataGridTextColumn();
            ArrivalTimeColumn.Header = "Arrival Date/Time";
            ArrivalTimeColumn.Binding = new Binding("ArrivalTime");

            DataGridTextColumn SourceAirportColumn = new DataGridTextColumn();
            SourceAirportColumn.Header = "Source Airport";
            SourceAirportColumn.Binding = new Binding("SourceAirportName");

            DataGridTextColumn DestinationAirportColumn = new DataGridTextColumn();
            DestinationAirportColumn.Header = "Destination Airport";
            DestinationAirportColumn.Binding = new Binding("DestinationAirportName");

            AirportFlightGrid.Columns.Add(FlightNumberColumn);
            AirportFlightGrid.Columns.Add(AirlineNameColumn);
            AirportFlightGrid.Columns.Add(DepartureTimeColumn);
            AirportFlightGrid.Columns.Add(ArrivalTimeColumn);
            AirportFlightGrid.Columns.Add(SourceAirportColumn);
            AirportFlightGrid.Columns.Add(DestinationAirportColumn);

        }
    }
}
