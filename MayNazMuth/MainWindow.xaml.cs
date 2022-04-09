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

        //Utility Classes
        FileService fs = new FileService();
        FlightParser fp = new FlightParser();

        public MainWindow() {
            InitializeComponent();

            string fileContents = FileService.readFile(@"..\..\Data\FlightDetails.csv");
            allFlightList = FlightParser.parseFlightFile(fileContents);

            //call the function to initialize the datagrid
            toggleEventHandlers(false);
            initializeDataGrid();
            populateDataGrid();
            toggleEventHandlers(true);


        }

        private void toggleEventHandlers(bool toggle)
        {
            if (toggle)
            {
                btnBackup.Click += addFlightData;
                txtSourceAirport.TextChanged += searchFlights;
                flightDataGrid.SelectionChanged += displaySelectedFlightInfo;
                btnAddPassenger.Click += addBooking;



            }
            else
            {
                btnBackup.Click -= addFlightData;
                txtSourceAirport.TextChanged -= searchFlights;
                flightDataGrid.SelectionChanged -= displaySelectedFlightInfo;
                btnAddPassenger.Click -= addBooking;


            }
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

            DataGridTextColumn AirlineIDColumn = new DataGridTextColumn();
            AirlineIDColumn.Header = "Airline ID";
            AirlineIDColumn.Binding = new Binding("AirlineId");

            DataGridTextColumn AirlineColumn = new DataGridTextColumn();
            AirlineColumn.Header = "Airline";
            AirlineColumn.Binding = new Binding("AirlineName");

            DataGridTextColumn SourceAirportIDColumn = new DataGridTextColumn();
            SourceAirportIDColumn.Header = "Departure Airport ID";
            SourceAirportIDColumn.Binding = new Binding("SourceAirportId");

            DataGridTextColumn SourceAirportColumn = new DataGridTextColumn();
            SourceAirportColumn.Header = "Departure Airport";
            SourceAirportColumn.Binding = new Binding("SourceAirportName");

            DataGridTextColumn DestinationAirportIDColumn = new DataGridTextColumn();
            DestinationAirportIDColumn.Header = "Destination Airport ID";
            DestinationAirportIDColumn.Binding = new Binding("DestinationAirportId");

            DataGridTextColumn DestinationAirportColumn = new DataGridTextColumn();
            DestinationAirportColumn.Header = "Destination Airport";
            DestinationAirportColumn.Binding = new Binding("DestinationAirportName");

            flightDataGrid.Columns.Add(FlightNumberColumn);
            flightDataGrid.Columns.Add(DepartureTimeColumn);
            flightDataGrid.Columns.Add(ArrivalTimeColumn);
            flightDataGrid.Columns.Add(AirlineIDColumn);
            flightDataGrid.Columns.Add(AirlineColumn);
            flightDataGrid.Columns.Add(SourceAirportIDColumn);
            flightDataGrid.Columns.Add(SourceAirportColumn);
            flightDataGrid.Columns.Add(DestinationAirportIDColumn);
            flightDataGrid.Columns.Add(DestinationAirportColumn);
            

        }

        private void populateDataGrid()
        {
            foreach (Flight f in allFlightList)
            {
                flightDataGrid.Items.Add(f);
            }
        }

        private void SetupFlightGrid()
        {
            //Turn off multiselect
            flightDataGrid.SelectionMode = DataGridSelectionMode.Single;
            //Make it read only
            flightDataGrid.IsReadOnly = true;
        }

        private void addFlightData(Object s, EventArgs e)
        {
            //Flight newFlight = new Flight();

            foreach(Flight f in allFlightList)
            {
             
                string flightNumber = f.FlightNo;
                DateTime departureTime = f.DepartureTime;
                DateTime arrivalTime = f.ArrivalTime;
                int? airlineid = f.AirlineId;
                string airlineName = f.AirlineName;
                int? sourceAirportid = f.SourceAirportId;
                int? destinationAirportid = f.DestinationAirportId;
                string sourceAirport = f.SourceAirportName;
                



                string destinationAirport = f.DestinationAirportName;
                using (var ctx = new CustomDbContext())
                {
                    ctx.Flights.Add(f);
                    ctx.SaveChanges();
                }

                //using (var ctx = new CustomDbContext())
                //{
                //    Flight newf = ctx.Flights.Where();
                //var query = db.Flights
                //              .Select(m => new
                //              {
                //                  flightNo = m.FlightNo,

                //              });


                //var flightNumbers = from x in query
                //                   select x;

                //foreach(var fn in flightNumbers)
                //{
                //    if (!flightNumber.Equals(fn))
                //    {
                //        db.Flights.Add(f);

                //    }
                //    else
                //    {
                //        continue;
                //    }

                // }
                //  db.SaveChanges();


            }

            lblFlightData.Content = "Flight Details are backed up.";

        }

        

    

        public void populatefileredDataGrid()
        {
            flightDataGrid.Items.Clear();
            foreach(Flight flight in filteredFlightList)
            {
                flightDataGrid.Items.Add(flight);
            }
        }

        private void searchFlights(object sender, EventArgs args)
        {
            string searchSourceAirport = txtSourceAirport.Text.Trim();
            string searchDestinationAirport = txtDestinationAirport.Text.Trim();

            if(!searchSourceAirport.Equals(""))
            {
                if (!(searchDestinationAirport.Equals("")))
                {
                    var searchResult = from s in allFlightList
                                       where s.SourceAirportName.Contains(searchSourceAirport) || s.DestinationAirportName.Contains(searchDestinationAirport)
                                       select s;

                    filteredFlightList = searchResult.ToList();
                }
            }
            else
            {
                filteredFlightList = allFlightList;
            }

            populatefileredDataGrid();
        }

        private void displaySelectedFlightInfo(object sender, EventArgs args)
        {
            //clear out the text box from last time.
            txtFlightDetails.Text = "";

            //Grab the selected flight
            Flight selectedFlight = (Flight)flightDataGrid.SelectedItem;

            //Populate the textbox
            txtFlightDetails.Text += " Flight Number : " + selectedFlight.FlightNo;
            txtFlightDetails.Text += "\n From : " + selectedFlight.SourceAirportName;
            txtFlightDetails.Text += "\n To : " + selectedFlight.DestinationAirportName;
            txtFlightDetails.Text += "\n Departure Date/Time : " + selectedFlight.DepartureTime;
            txtFlightDetails.Text += "\n Arrival Date/Time : " + selectedFlight.ArrivalTime;



        }

        private void addBooking(object sender, EventArgs arg)
        {
            

            if(!(flightDataGrid.SelectedItems.Count==1))
            {
                MessageBox.Show("Please select a flight!");
            }
            else
            {
                Booking newBooking = new Booking();
                //Grab the selected flight
                Flight selectedFlight = (Flight)flightDataGrid.SelectedItem;
                string flightNo = selectedFlight.FlightNo;
                DateTime bookingDateTime = DateTime.Now;
                string bookingStatus = "In Progress";

                using (var ctx = new CustomDbContext())
                {
                    Flight fl = ctx.Flights.Where(x => x.FlightNo == flightNo).First();
                    int flightId = fl.FlightId;

                    newBooking.BookingDatetime = bookingDateTime;
                    newBooking.BookingStatus = bookingStatus;
                    newBooking.FlightId = flightId;

                    ctx.Bookings.Add(newBooking);
                    ctx.SaveChanges();

                }

                AddPassengerWindow Passeger = new AddPassengerWindow();
                CloseAllWindows();
                Passeger.Show();
            }
            
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
