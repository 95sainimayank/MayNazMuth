using MayNazMuth.Entities;
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

namespace MayNazMuth
{
    /// <summary>
    /// Interaction logic for FlightReportWindow.xaml
    /// </summary>
    public partial class FlightReportWindow : Window
    {

        List<Flight> FlightList = new List<Flight>();
        public FlightReportWindow()
        {
            InitializeComponent();

            //turn the event handlers off
            ToggleEventHandlers(false);

            //Initialize the data grid
            initializeDataGrid();

            //populate the grid
            populateData();

            //Turn event handlers on
            ToggleEventHandlers(true);
        }

        private void ToggleEventHandlers(bool toggle)
        {
            if (toggle)
            {
                //turn on               
                FilterButton.Click += searchData;
                clearButton.Click += clearFilters;

            }
            else
            {
                //Turn off               

                FilterButton.Click -= searchData;
                clearButton.Click -= clearFilters;
            }
        }

        private void initializeDataGrid()
        {
            flightReportDataGrid.IsReadOnly = true;
            // flightDataGrid.ItemsSource = allFlightList;
            //Flight No, Depature, Date  Arrive,Date Airline, Source Airport,Destination Airport
            //string nFlightNo, DateTime nDepartureTime, DateTime nArrivalTime,Airline nAirline, Airport nSourceAirport, Airport nDestinationAirport

            DataGridTextColumn FlightNumberColumn = new DataGridTextColumn();
            FlightNumberColumn.Header = "Flight Number";
            FlightNumberColumn.Binding = new Binding("FlightNo");

            DataGridTextColumn DepartureTimeColumn = new DataGridTextColumn();
            DepartureTimeColumn.Header = "Departure Date/Time";
            DepartureTimeColumn.Binding = new Binding("DepartureTime");

            DataGridTextColumn ArrivalTimeColumn = new DataGridTextColumn();
            ArrivalTimeColumn.Header = "Arrival Date/Time";
            ArrivalTimeColumn.Binding = new Binding("ArrivalTime");

            DataGridTextColumn AirlineColumn = new DataGridTextColumn();
            AirlineColumn.Header = "Airline";
            AirlineColumn.Binding = new Binding("AirlineName");

            DataGridTextColumn SourceAirportColumn = new DataGridTextColumn();
            SourceAirportColumn.Header = "Departure Airport";
            SourceAirportColumn.Binding = new Binding("SourceAirportName");

            DataGridTextColumn DestinationAirportColumn = new DataGridTextColumn();
            DestinationAirportColumn.Header = "Destination Airport";
            DestinationAirportColumn.Binding = new Binding("DestinationAirportName");

            flightReportDataGrid.Columns.Add(FlightNumberColumn);
            flightReportDataGrid.Columns.Add(DepartureTimeColumn);
            flightReportDataGrid.Columns.Add(ArrivalTimeColumn);
            flightReportDataGrid.Columns.Add(AirlineColumn);
            flightReportDataGrid.Columns.Add(SourceAirportColumn);
            flightReportDataGrid.Columns.Add(DestinationAirportColumn);


        }

        private void populateData()
        {
            noOfFlightsLabel.Content = "";

            using (var ctx = new CustomDbContext())
            {
                FlightList = ctx.Flights.ToList<Flight>();

                //var FoundFlights = FlightList.Where(x => x.SourceAirportId == ApId || x.DestinationAirportId == ApId);               

                flightReportDataGrid.Items.Clear();
                foreach (Flight f in FlightList)
                {
                    flightReportDataGrid.Items.Add(f);

                }
                noOfFlightsLabel.Content = FlightList.Count.ToString();
            }

        }

        private void searchData(object sender, RoutedEventArgs e)
        {
            noOfFlightsLabel.Content = "";
            var startFrom = fromDatePicker.SelectedDate;
            var endTo = toDatePicker.SelectedDate;
            string airportName = fromAirportTextbox.Text;

            
            if (startFrom <= endTo)
            {
                using (var ctx = new CustomDbContext())
                {
                    
                    if(airportName.Equals(""))
                    {
                        FlightList = ctx.Flights.ToList<Flight>();
                        var filteredList = FlightList.Where(x => (x.DepartureTime >= startFrom && x.DepartureTime <= endTo));
                        flightReportDataGrid.Items.Clear();
                        foreach (Flight f in filteredList)
                        {
                            flightReportDataGrid.Items.Add(f);

                        }

                        noOfFlightsLabel.Content = filteredList.ToList().Count.ToString();
                    }
                    else
                    {
                        FlightList = ctx.Flights.ToList<Flight>();
                        var filteredList = FlightList.Where(x => (x.DepartureTime >= startFrom && x.DepartureTime <= endTo && x.SourceAirportName.Equals(airportName)));
                        flightReportDataGrid.Items.Clear();
                        foreach (Flight f in filteredList)
                        {
                            flightReportDataGrid.Items.Add(f);

                        }

                        noOfFlightsLabel.Content = filteredList.ToList().Count.ToString();
                    }
                    
                    

                }

            }
            else
            {
                MessageBox.Show("Start Date can not be later than End Date");
                
            }
        }

        private void clearFilters(object sender, EventArgs args)
        {

            fromDatePicker.SelectedDate = DateTime.Now;
            toDatePicker.SelectedDate = DateTime.Now;
            fromAirportTextbox.Text = "";
            populateData();
        }
    }
}
