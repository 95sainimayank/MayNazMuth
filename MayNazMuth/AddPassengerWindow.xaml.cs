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
    public partial class AddPassengerWindow : Window {

        /*List<Booking> bookingLsit = new List<Booking>();
        int bookingId;
*/
        public AddPassengerWindow() {

            //Initializing the components and the data grid
            InitializeComponent();
            InitializeDataGrid();

            //adding events to the button clicks
            btnAddPassenger.Click += AddPassenger;
            btnDeletePassenger.Click += DeletePassenger;
          
        }

        //Closes all open windows
        public void CloseAllWindows()
        {
            foreach (Window window in Application.Current.Windows)
            {
                window.Hide();
            }
        }
        
        //Initialzing the datagrid with columsn
        public void InitializeDataGrid() {
            using(var db = new CustomDbContext()) {
                //Adding columns to the datagrid
                DataGridTextColumn passengerName = new DataGridTextColumn {
                    Header = "Passenger Name",
                    Binding = new Binding("FullName")
                };

                DataGridTextColumn passengerPassport = new DataGridTextColumn {
                    Header = "Passport Number",
                    Binding = new Binding("PassportNo")
                };

                DataGridTextColumn passengerEmail = new DataGridTextColumn {
                    Header = "Email",
                    Binding = new Binding("Email")
                };

                DataGridTextColumn passengerPhone = new DataGridTextColumn {
                    Header = "Phone Number",
                    Binding = new Binding("PhoneNo")
                };

                DataGridTextColumn dateofBirth = new DataGridTextColumn {
                    Header = "Date of Birth",
                    Binding = new Binding("DateOfBirth")
                };

                DataGridTextColumn gender = new DataGridTextColumn {
                    Header = "Gender",
                    Binding = new Binding("Gender")
                };

                //Adding columns to the grid
                AllPassengersDataGrid.Columns.Add(passengerName);
                AllPassengersDataGrid.Columns.Add(passengerEmail);
                AllPassengersDataGrid.Columns.Add(passengerPassport);
                AllPassengersDataGrid.Columns.Add(dateofBirth);
                AllPassengersDataGrid.Columns.Add(passengerPhone);
                AllPassengersDataGrid.Columns.Add(gender);

                //clearing existing values of data grid
                AllPassengersDataGrid.Items.Clear();

                //addig values to the datagrid
                foreach (Passenger p in db.Passengers.ToList()) {
                    AllPassengersDataGrid.Items.Add(p);
                }

                db.SaveChanges();
            }
        }

        //Event to handle addition of passengers to db
        public void AddPassenger(object sender, EventArgs args) {
            string FullName = txtFullName.Text;
            string Email = txtEmail.Text;
            string PhoneNumber = txtPhoneNumber.Text;
            string DateOfBirth = dateOfBirth.Text;
            string Gender = gender.Text;
            string PassportNumber = txtPassportNumber.Text;

            //checking if input is valid
            if(isValid(FullName, Email, PhoneNumber, DateOfBirth, Gender, PassportNumber)) {
                using(var db = new CustomDbContext()) {
                    Passenger newPassenger = new Passenger(
                                            FullName, 
                                            PassportNumber,
                                            Email,
                                            PhoneNumber,
                                            Convert.ToDateTime(DateOfBirth),
                                            Gender);

                    db.Passengers.Add(newPassenger);

                    db.SaveChanges();
                    InitializeDataGrid();

                    //clearing all the fields after addition of user has been done
                    txtFullName.Clear();
                    txtEmail.Clear();
                    txtPassportNumber.Clear();
                    txtPhoneNumber.Clear();
                    dateOfBirth.SelectedDate = null;
                    gender.SelectedIndex = 0;

                }
            }
            else {
                MessageBox.Show("One or two fields values are not correct!");
            }

        }

        //Method to check validation of inputs
        public bool isValid(string fullName, string email, string phone, string dob, string gender, string passport) {

            if (fullName.Equals("")) {
                return false;
            }
            else if (email.Equals("")) {
                return false;
            }
            else if (!phone.All(char.IsDigit) || phone.Equals("")) {
                return false;
            }
            else if (dob.Equals("")) {
                return false;
            }
            else if (passport.Equals("")) {
                return false;
            }
            else if (gender.Equals("")) {
                return false;
            }

            return true;
        }

        //Deletion of passenger from the list
        public void DeletePassenger(object sender, EventArgs args) {
            using (var db = new CustomDbContext()) {
                var selectedPassengers = AllPassengersDataGrid.SelectedItems;

                //checks if the passenger is selected
                if(selectedPassengers.Count == 0) {
                    MessageBox.Show("No Passenger selected!!");
                }
                else {//removing passengers
                    foreach (Passenger p in selectedPassengers) {
                        db.Passengers.Remove(p);
                    }
                    MessageBox.Show("Passenger(s) deleted successfully");
                }

                db.SaveChanges();
                InitializeDataGrid();
            }
        }

    }

    
}
