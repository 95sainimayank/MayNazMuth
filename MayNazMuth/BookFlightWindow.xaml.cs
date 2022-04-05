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

namespace MayNazMuth {
    /// <summary>
    /// Interaction logic for BookFlightWindow.xaml
    /// </summary>
    public partial class BookFlightWindow : Window {

        List<Flight> allFlightList = new List<Flight>();

        //Utility Classes
        FileService fs = new FileService();
        FlightParser fp = new FlightParser();

        public BookFlightWindow() {
            InitializeComponent();
            string fileContents = FileService.readFile(@"..\..\Data\FlightDetails.csv");
            allFlightList = FlightParser.parseFlightFile(fileContents);

            //call the function to initialize the datagrid
            initializeDataGrid();
            populateDataGrid();
        }

        private void initializeDataGrid()
        {
            flightDataGrid.IsReadOnly = true;
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
            AirlineColumn.Binding = new Binding("Airline");

            DataGridTextColumn SourceAirportColumn = new DataGridTextColumn();
            SourceAirportColumn.Header = "Departure Airport";
            SourceAirportColumn.Binding = new Binding("SourceAirport");

            DataGridTextColumn DestinationAirportColumn = new DataGridTextColumn();
            DestinationAirportColumn.Header = "Destination Airport";
            DestinationAirportColumn.Binding = new Binding("DestinationAirport");

            flightDataGrid.Columns.Add(FlightNumberColumn);
            flightDataGrid.Columns.Add(DepartureTimeColumn);
            flightDataGrid.Columns.Add(ArrivalTimeColumn);
            flightDataGrid.Columns.Add(AirlineColumn);
            flightDataGrid.Columns.Add(SourceAirportColumn);
            flightDataGrid.Columns.Add(DestinationAirportColumn);


        }

        private void populateDataGrid()
        {
            foreach (Flight f in allFlightList)
            {
                flightDataGrid.Items.Add(f);
            }
        }

        private void txtFlightDetails_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
