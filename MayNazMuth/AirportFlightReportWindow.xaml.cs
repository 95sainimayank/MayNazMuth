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
        int ApId;

        public AirportFlightReportWindow(string airportId)
        {
            //incoming AirportId of selected row from Airport window
            ApId = Convert.ToInt32(airportId);


            InitializeComponent();

            //turn the event handlers off
            ToggleEventHandlers(false);

            //Initialize the data grid
            SetupGrid();

            //populate the grid
            Updategrid();

            //Turn event handlers on
            ToggleEventHandlers(true);
        }

        private void ToggleEventHandlers(bool toggle)
        {
            if (toggle)
            {
                //turn on                               
                backToAirportButon.Click += backToAirportList;                                
            }
            else
            {
                //Turn off               
                backToAirportButon.Click -= backToAirportList;
            }
        }

        private void backToAirportList(object s, EventArgs e)
        {
            AirportWindow Airport = new AirportWindow();
            CloseAllWindows();
            Airport.Show();
        }
        public void CloseAllWindows()
        {
            foreach (Window window in Application.Current.Windows)
            {
                window.Hide();
            }
        }
        private void Updategrid()
        {
            //backToAirportButon.Content = ApId.ToString();
            using (var ctx = new CustomDbContext())
            {
                FlightList = ctx.Flights.ToList<Flight>();
                var FoundFlights = FlightList.Where(x => x.SourceAirportId == ApId || x.DestinationAirportId == ApId);
                foreach (var a in FoundFlights)
                {
                    Console.WriteLine(a);
                }
                 AirportFlightGrid.ItemsSource = FoundFlights;
            }
        }

        private void SetupGrid()
        {
            AirportFlightGrid.SelectionMode = DataGridSelectionMode.Single;
            //Make it read only
            AirportFlightGrid.IsReadOnly = true;
        }
    }
}
