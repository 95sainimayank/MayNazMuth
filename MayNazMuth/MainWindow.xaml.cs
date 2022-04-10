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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MayNazMuth {
    public partial class MainWindow : Window {

        List<Flight> allFlightList = new List<Flight>();
        List<Flight> filteredFlightList = new List<Flight>();

        List<Airport> allAirportList = new List<Airport>();

        //Utility Classes
        FileService fs = new FileService();
        FlightParser fp = new FlightParser();
        AirportParser airps = new AirportParser();

        public MainWindow() {
            InitializeComponent();

            string fileContents = FileService.readFile(@"..\..\Data\FlightDetails.csv");
            allFlightList = FlightParser.parseFlightFile(fileContents);

            string airportFileContents = FileService.readFile(@"..\..\Data\initialAirports.csv");
            allAirportList = AirportParser.parseAirportFile(airportFileContents);
            //add initial airports to database
            AddinitialAirportsToDB();

            //call the function to initialize the datagrid
            toggleEventHandlers(false);
            addFlightData();
            initializeDataGrid();
            populateDataGrid();
            toggleEventHandlers(true);
        }

        private void AddinitialAirportsToDB()
        {

            List<int> AirportIdInDB = new List<int>();
            List<Airport> airportList = new List<Airport>();

            //Add all flight numbers in the DB to flightNoInDB list
            using (var ctx = new CustomDbContext())
            {
                airportList = ctx.Airports.ToList<Airport>();
                var airporIds = airportList.Select(x => x.AirportId);

                foreach (int pId in airporIds)
                {
                    AirportIdInDB.Add(pId);
                }
            }
            foreach (Airport ap in allAirportList)
            {
                //check if allAirportList contains the aiport id that are already in the DB
                //Add records only if the relevent airport id is not in the DB
                if (AirportIdInDB.Contains(ap.AirportId))
                {
                    continue;
                }
                else
                {
                    int id = ap.AirportId;
                    string name = ap.AirportName;                    
                    string address = ap.AirportAddress;
                    string city = ap.AirportCity;
                    string country = ap.AirportCountry;
                    string abbreviation = ap.AirportAbbreviation;
                    string phoneno = ap.AirportPhoneno;
                    string email = ap.AirportEmail;
                    string website = ap.AirportWebsite;

                    using (var ctx = new CustomDbContext())
                    {

                        ctx.Airports.Add(ap);
                        ctx.SaveChanges();
                    }                    
                }
            }
        }

        private void toggleEventHandlers(bool toggle) {
            if (toggle) {
                //btnBackUp.Click += addFlightData;
                btnAddPassenger.Click += addBooking;
                flightDataGrid.SelectionChanged += displaySelectedFlightInfo;
                btnSearch.Click += searchFlightsAllFilters;
                btnSearch.Click += searchFlightsDepartureAndArrival;
                btnClear.Click += clearFilters;

            }
            else {
                //btnBackUp.Click -= addFlightData;
                btnAddPassenger.Click -= addBooking;
                flightDataGrid.SelectionChanged -= displaySelectedFlightInfo;
                btnSearch.Click -= searchFlightsAllFilters;
                btnSearch.Click -= searchFlightsDepartureAndArrival;
                btnClear.Click -= clearFilters;


            }
        }

        private void initializeDataGrid() {
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
            AirlineColumn.Binding = new Binding("AirlineName");


            DataGridTextColumn SourceAirportColumn = new DataGridTextColumn();
            SourceAirportColumn.Header = "Departure Airport";
            SourceAirportColumn.Binding = new Binding("SourceAirportName");

            DataGridTextColumn DestinationAirportColumn = new DataGridTextColumn();
            DestinationAirportColumn.Header = "Destination Airport";
            DestinationAirportColumn.Binding = new Binding("DestinationAirportName");

            DataGridTextColumn PriceColumn = new DataGridTextColumn();
            PriceColumn.Header = "Price";
            PriceColumn.Binding = new Binding("Price");
            PriceColumn.Binding.StringFormat = "C";

            flightDataGrid.Columns.Add(FlightNumberColumn);
            flightDataGrid.Columns.Add(DepartureTimeColumn);
            flightDataGrid.Columns.Add(ArrivalTimeColumn);
            flightDataGrid.Columns.Add(AirlineColumn);
            flightDataGrid.Columns.Add(SourceAirportColumn);
            flightDataGrid.Columns.Add(DestinationAirportColumn);
            flightDataGrid.Columns.Add(PriceColumn);


        }

        private void populateDataGrid() {
            foreach (Flight f in allFlightList) {
                flightDataGrid.Items.Add(f);
            }

        }

        private void SetupFlightGrid() {
            //Turn off multiselect
            flightDataGrid.SelectionMode = DataGridSelectionMode.Single;
            //Make it read only
            flightDataGrid.IsReadOnly = true;
            departureDatePicker.SelectedDate = DateTime.Now;
        }

        private void addFlightData() {

            List<String> flightNoInDB = new List<string>();
            List<Flight> flightList = new List<Flight>();

            //Add all flight numbers in the DB to flightNoInDB list
            using (var ctx = new CustomDbContext()) {
                flightList = ctx.Flights.ToList<Flight>();
                var flightNumbers = flightList.Select(x => x.FlightNo);

                foreach (String fno in flightNumbers) {
                    flightNoInDB.Add(fno);
                }
            }


            //allFlightList.Count();
            foreach (Flight f in allFlightList) {
                //check if allFlightList contains the flight numbers that are already in the DB
                //Add records only if the relevent flight number is not in the DB
                if (flightNoInDB.Contains(f.FlightNo)) {
                    continue;
                }
                else {
                    string flightNumber = f.FlightNo;
                    DateTime departureTime = f.DepartureTime;
                    DateTime arrivalTime = f.ArrivalTime;
                    int? airlineid = f.AirlineId;
                    string airlineName = f.AirlineName;
                    int? sourceAirportid = f.SourceAirportId;
                    int? destinationAirportid = f.DestinationAirportId;
                    string sourceAirport = f.SourceAirportName;
                    string destinationAirport = f.DestinationAirportName;

                    using (var ctx = new CustomDbContext()) {

                        ctx.Flights.Add(f);
                        ctx.SaveChanges();
                    }
                    //lblFlightData.Content = "Flight Details are backed up.";
                }
            }

        }


        //Populate the flight data grid
        public void populatefileredDataGrid() {
            flightDataGrid.Items.Clear();
            foreach (Flight flight in filteredFlightList) {
                flightDataGrid.Items.Add(flight);
            }
        }

        //filter the flights when all 3 filters are given 
        private void searchFlightsAllFilters(object sender, EventArgs args) {
            string searchSourceAirport = txtSourceAirport.Text.Trim();
            string searchDestinationAirport = txtDestinationAirport.Text.Trim();
            var searchDepartureDate = departureDatePicker.SelectedDate;


            var searchResult = from s in allFlightList
                               where s.SourceAirportName.Contains(searchSourceAirport) &&
                               s.DestinationAirportName.Contains(searchDestinationAirport) &&
                               s.DepartureTime == searchDepartureDate
                               select s;

            filteredFlightList = searchResult.ToList();
            populatefileredDataGrid();

        }

        //filter the flights when only departure and arrival airport is given
        private void searchFlightsDepartureAndArrival(object sender, EventArgs args) {
            string searchSourceAirport = txtSourceAirport.Text.Trim();
            string searchDestinationAirport = txtDestinationAirport.Text.Trim();
            var searchDepartureDate = departureDatePicker.SelectedDate;

            
            if (searchSourceAirport.Equals("") || searchDestinationAirport.Equals(""))
            {
                MessageBox.Show("Please enter your departure and arrival aiports");
                populateDataGrid();
            }
            else {
                var searchResult = from s in allFlightList
                                   where s.SourceAirportName.Contains(searchSourceAirport) &&
                                   s.DestinationAirportName.Contains(searchDestinationAirport)
                                   select s;


                filteredFlightList = searchResult.ToList();
                populatefileredDataGrid();
            }

        }

        //Clear all the filters
        private void clearFilters(object sender, EventArgs args) {
            txtSourceAirport.Text = "";
            txtDestinationAirport.Text = "";
            departureDatePicker.SelectedDate = DateTime.Now;
            populateDataGrid();
        }

        private void displaySelectedFlightInfo(object sender, EventArgs args) {
            //clear out the text box from last time.
            txtFlightDetails.Text = "";

            //Grab the selected flight
            Flight selectedFlight  = (Flight)flightDataGrid.SelectedItem; ;

            if (flightDataGrid.SelectedItems.Count == 1)
            {

                TimeSpan numberOfHours = selectedFlight.ArrivalTime - selectedFlight.DepartureTime;



                //Populate the textbox
                txtFlightDetails.Text += " Flight Number : " + selectedFlight.FlightNo;
                txtFlightDetails.Text += "\n From : " + selectedFlight.SourceAirportName;
                txtFlightDetails.Text += "\n To : " + selectedFlight.DestinationAirportName;
                txtFlightDetails.Text += "\n Departure Date/Time : " + selectedFlight.DepartureTime;
                txtFlightDetails.Text += "\n Arrival Date/Time : " + selectedFlight.ArrivalTime;
                txtFlightDetails.Text += "\n Price Per Person : $" + selectedFlight.Price;
                txtFlightDetails.Text += "\n Number of hours : " + numberOfHours;

            }

        }

        private void addBooking(object sender, EventArgs arg) {
            if (!(flightDataGrid.SelectedItems.Count == 1)) {
                MessageBox.Show("Please select a flight!");
            }
            else {
                Booking newBooking = new Booking();

                //Grab the selected
                Flight selectedFlight = (Flight)flightDataGrid.SelectedItem;
                string flightNo = selectedFlight.FlightNo;
                DateTime bookingDateTime = DateTime.Now;
                string bookingStatus = "In Progress";

                using (var ctx = new CustomDbContext()) {
                    Flight fl = ctx.Flights.Where(x => x.FlightNo == flightNo).First();
                    int flightId = fl.FlightId;

                    newBooking.BookingDatetime = bookingDateTime;
                    newBooking.BookingStatus = bookingStatus;
                    newBooking.FlightId = flightId;

                    //AddPassengerToBookingWindow Passeger = new AddPassengerToBookingWindow();
                    //Passeger.lblFlightPrice.Content = "$" + selectedFlight.Price;

                    ctx.Bookings.Add(newBooking);

                    ctx.SaveChanges();

                    CloseAllWindows();

                    AddPassengerToBookingWindow Passeger = new AddPassengerToBookingWindow();
                    Passeger.lblFlightPrice.Content = "$" + selectedFlight.Price;
                    Passeger.Show();
                }

                //Open Passenger window when add passenger is clicked
            }

        }

        //Close all open windows
        public void CloseAllWindows() {
            foreach (Window window in Application.Current.Windows) {
                window.Hide();
            }
        }



    }
}
