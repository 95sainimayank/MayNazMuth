using MayNazMuth.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
                AirportDataGrid.Items.Clear();
                foreach (Airport AP in airportsList)
                {
                    AirportDataGrid.Items.Add(AP);
                }
                //AirportDataGrid.ItemsSource = airportsList;
            }
            totalAriportCountLable.Content = airportsList.Count();
        }

        private void SetupGrid()
        {
            
            AirportDataGrid.SelectionMode = DataGridSelectionMode.Single;
            //Make it read only
            AirportDataGrid.IsReadOnly = true;


            DataGridTextColumn AirportIdColumn = new DataGridTextColumn();
            AirportIdColumn.Header = "#";
            AirportIdColumn.Binding = new Binding("AirportId");

            DataGridTextColumn AirportNameColumn = new DataGridTextColumn();
            AirportNameColumn.Header = "Airport Name";
            AirportNameColumn.Binding = new Binding("AirportName");

            DataGridTextColumn AirportAddressColumn = new DataGridTextColumn();
            AirportAddressColumn.Header = "Address";
            AirportAddressColumn.Binding = new Binding("AirportAddress");

            DataGridTextColumn AirportCityColumn = new DataGridTextColumn();
            AirportCityColumn.Header = "City";
            AirportCityColumn.Binding = new Binding("AirportCity");

            DataGridTextColumn AirportCountryColumn = new DataGridTextColumn();
            AirportCountryColumn.Header = "Country";
            AirportCountryColumn.Binding = new Binding("AirportCountry");

            DataGridTextColumn AirportABBVColumn = new DataGridTextColumn();
            AirportABBVColumn.Header = "Abbreviation";
            AirportABBVColumn.Binding = new Binding("AirportAbbreviation");

            DataGridTextColumn AirportPhoneColumn = new DataGridTextColumn();
            AirportPhoneColumn.Header = "Phone";
            AirportPhoneColumn.Binding = new Binding("AirportPhoneno");

            DataGridTextColumn AirportWebsiteColumn = new DataGridTextColumn();
            AirportWebsiteColumn.Header = "Website";
            AirportWebsiteColumn.Binding = new Binding("AirportWebsite");

            DataGridTextColumn AirportEmailColumn = new DataGridTextColumn();
            AirportEmailColumn.Header = "Email";
            AirportEmailColumn.Binding = new Binding("AirportEmail");

            AirportDataGrid.Columns.Add(AirportIdColumn);
            AirportDataGrid.Columns.Add(AirportNameColumn);
            AirportDataGrid.Columns.Add(AirportAddressColumn);
            AirportDataGrid.Columns.Add(AirportCityColumn);
            AirportDataGrid.Columns.Add(AirportCountryColumn);
            AirportDataGrid.Columns.Add(AirportABBVColumn);
            AirportDataGrid.Columns.Add(AirportPhoneColumn);
            AirportDataGrid.Columns.Add(AirportWebsiteColumn);
            AirportDataGrid.Columns.Add(AirportEmailColumn);
            
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
                AirportFlightReportButton.IsEnabled = false;
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

                if (!AP.AirportName.Equals("") && !AP.AirportAddress.Equals("") && !AP.AirportCity.Equals("") && !AP.AirportCountry.Equals("")
                   && !AP.AirportAbbreviation.Equals("") && !AP.AirportPhoneno.Equals("") && !AP.AirportEmail.Equals("") && !AP.AirportWebsite.Equals(""))
                {
                    //Update the object
                    ctx.Airports.Update(AP);

                    ProgressBarHandler();

                    //Save my changes
                    ctx.SaveChanges();
                    //update the data grid
                    Updategrid();
                    AirportFlightReportButton.IsEnabled = false;

                }
                else
                {
                    MessageBox.Show("Please fill all fields.");
                }

               

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
            
            resetForm();
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
            //MyProgressbar.Visibility = Visibility.Hidden;

        }
       
    }
}

