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
    /// Interaction logic for AirportFlightReportWindow.xaml
    /// </summary>
    public partial class AirportFlightReportWindow : Window
    {
        public AirportFlightReportWindow()
        {
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
            using (var ctx = new CustomDbContext())
            {
                //airportsList = ctx.Airports.ToList<Airport>();
                //AirportFlightGrid.ItemsSource = airportsList;
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
