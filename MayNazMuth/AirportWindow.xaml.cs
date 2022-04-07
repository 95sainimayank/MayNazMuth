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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MayNazMuth
{
    /// <summary>
    /// Interaction logic for AirportWindow.xaml
    /// </summary>
    public partial class AirportWindow : Window
    {

        List<Airport> airportsList = new List<Airport>();
        public AirportWindow()
        {
            InitializeComponent();

            //turn the event handlers off
            ToggleEventHandlers(false);

            //Edit and Delete Buttons are disbale unless one row selected
            SetupButton();

            //Initialize the data grid
            SetupGrid();

            //populate the grid
            Updategrid();

            //Turn event handlers on
            ToggleEventHandlers(true);
        }
   
        private void SetupButton()
        {
            EditBtn.IsEnabled = false;
            DeleteBtn.IsEnabled = false;
            AirportIdTextBox.IsEnabled = false;
            AirportFlightReportButton.IsEnabled = false;
        }

        private void Updategrid()
        {
            using (var ctx = new CustomDbContext())
            {
                airportsList = ctx.Airports.ToList<Airport>();
                AirportDataGrid.ItemsSource = airportsList;
            }
        }

        private void SetupGrid()
        {
            
            AirportDataGrid.SelectionMode = DataGridSelectionMode.Single;
            //Make it read only
            AirportDataGrid.IsReadOnly = true;
        }

        private void ToggleEventHandlers(bool toggle)
        {
            if (toggle)
            {
                //turn on               
                AirportDataGrid.SelectionChanged += updateAirportInfo;
                EditBtn.Click += EditButtonClick;
                DeleteBtn.Click += DeleteButtonClick;
                AirportFlightReportButton.Click += OpenAirportFlightWindow;
            }
            else
            {
                //Turn off               
                AirportDataGrid.SelectionChanged -= updateAirportInfo;
                EditBtn.Click -= EditButtonClick;
                DeleteBtn.Click -= DeleteButtonClick;
                AirportFlightReportButton.Click -= OpenAirportFlightWindow;
            }
        }

        private void OpenAirportFlightWindow(object s, EventArgs e)
        {

            AirportFlightReportWindow AirportFlightReport = new AirportFlightReportWindow(AirportIdTextBox.Text);
            CloseAllWindows();
            AirportFlightReport.Show();
        }
        public void CloseAllWindows()
        {
            foreach (Window window in Application.Current.Windows)
            {
                window.Hide();
            }
        }
        private void DeleteButtonClick(object s, EventArgs e)
        {
            using (var ctx = new CustomDbContext())
            {
                //Pull the airport 
                Airport AP = ctx.Airports.Where(x => x.AirportId == Convert.ToInt32(AirportIdTextBox.Text)).First();

                ProgressBarHandler();

                //Update the object
                ctx.Airports.Remove(AP);
                //Save my changes
                ctx.SaveChanges();
                //update the data grid
                Updategrid();

                resetForm();

            }
        }

        private void EditButtonClick(object s, EventArgs e)
        {
            using (var ctx = new CustomDbContext())
            {
                //Pull the airport for the id we have
                Airport AP = ctx.Airports.Where(x => x.AirportId == Convert.ToInt32(AirportIdTextBox.Text)).First();

                //Reconstruct the object based on the form data
                AP.AirportName = AirportNameTextBox.Text;
                AP.AirportAddress = AirportAddressTextBox.Text;
                AP.AirportCity = CityTextBox.Text;
                AP.AirportCountry = CountryTextBox.Text;
                AP.AirportAbbreviation = AbbvTextBox.Text;
                AP.AirportPhoneno = PhoneTextBox.Text;
                AP.AirportEmail = EmailTextBox.Text;
                AP.AirportWebsite = WebsiteTextBox.Text;


                //Update the object
                ctx.Airports.Update(AP);
                //Save my changes
                ctx.SaveChanges();
                //update the data grid
                Updategrid();

            }
        }

        private void updateAirportInfo(object s, EventArgs e)
        {
            if (AirportDataGrid.SelectedItem != null)
            {

                //Grab the selected Airport
                Airport selectedItem = (Airport)AirportDataGrid.SelectedItem;

                //Populate the form elements
                AirportIdTextBox.Text = selectedItem.AirportId.ToString();
                AirportNameTextBox.Text = selectedItem.AirportName.ToString();
                AirportAddressTextBox.Text = selectedItem.AirportAddress.ToString();
                CityTextBox.Text = selectedItem.AirportCity.ToString();
                CountryTextBox.Text = selectedItem.AirportCountry.ToString();
                AbbvTextBox.Text = selectedItem.AirportAbbreviation.ToString();
                PhoneTextBox.Text = selectedItem.AirportPhoneno.ToString();
                EmailTextBox.Text = selectedItem.AirportEmail.ToString();
                WebsiteTextBox.Text = selectedItem.AirportWebsite.ToString();


                //Enable the edit button becuase the user selected a airport
                EditBtn.IsEnabled = true;
                DeleteBtn.IsEnabled = true;
                AirportFlightReportButton.IsEnabled = true;
                DoneSlider.IsEnabled = false;                
            }
            else
            {
                EditBtn.IsEnabled = false;
                DeleteBtn.IsEnabled = false;
                DoneSlider.IsEnabled = true;              
            }
        }

        private void AddEventHandler(object s, EventArgs e)
        {
            Airport newAirport = new Airport();
            newAirport.AirportName = AirportNameTextBox.Text;
            newAirport.AirportAddress = AirportAddressTextBox.Text;
            newAirport.AirportCity = CityTextBox.Text;
            newAirport.AirportCountry = CountryTextBox.Text;
            newAirport.AirportAbbreviation = AbbvTextBox.Text;
            newAirport.AirportPhoneno = PhoneTextBox.Text;
            newAirport.AirportEmail = EmailTextBox.Text;
            newAirport.AirportWebsite = WebsiteTextBox.Text;
                       

            using (var ctx = new CustomDbContext())
            {
                
                if (!newAirport.AirportName.Equals("") && !newAirport.AirportAddress.Equals("") && !newAirport.AirportCity.Equals("") && !newAirport.AirportCountry.Equals("")
                    && !newAirport.AirportAbbreviation.Equals("") && !newAirport.AirportPhoneno.Equals("") && !newAirport.AirportEmail.Equals("") && !newAirport.AirportWebsite.Equals(""))
                {
                    ProgressBarHandler();
                    ctx.Airports.Add(newAirport);
                    ctx.SaveChanges();
                    Updategrid();
                }
                else
                {
                    MessageBox.Show("Please fill all fields.");
                }
               resetForm();
            }

                    
        }

        private void ProgressBarHandler()
        {                      
            MyProgressbar.IsIndeterminate = false;
            MyProgressbar.Orientation = Orientation.Horizontal;            
            Duration duration = new Duration(TimeSpan.FromSeconds(1));
            DoubleAnimation doubleanimation = new DoubleAnimation(200.0, duration);
            MyProgressbar.BeginAnimation(ProgressBar.ValueProperty, doubleanimation);            
        }

        private void resetForm()
        {
            AirportNameTextBox.Text="";
            AirportAddressTextBox.Text = "";
            CityTextBox.Text = "";
            CountryTextBox.Text = "";
            AbbvTextBox.Text = "";
            PhoneTextBox.Text = "";
            EmailTextBox.Text = "";
            WebsiteTextBox.Text = "";
            DoneSlider.Value = 0;
           

        }
       
    }
}

