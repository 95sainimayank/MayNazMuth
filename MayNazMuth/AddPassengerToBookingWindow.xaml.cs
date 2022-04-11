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
    public partial class AddPassengerToBookingWindow : Window {
        //List to hold passengers added to booking
        List<Passenger> addedPassengers = new List<Passenger>();
        //price from selecdt flight window
        float passPrice;
        public AddPassengerToBookingWindow() {
            InitializeComponent();

            InitializeDataGrid();

            //disabling textbox to make uneditable
            txtAddedPassengers.IsEnabled = false;

            //Attaching events
            btnSeeAll.Click += SeeAll;
            btnSearch.Click += SearchPassenger;
            btnAdd.Click += AddPassenger;
            btnPassengerToGrid.Click += AddPassengerToGrid;
            btnToPayment.Click += GoToPayment;
        }

        //Intialization of data grid
        public void InitializeDataGrid() {
            int bookingId;
            using (var db = new CustomDbContext()) {

                //creating columns for the datagrid
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

                //adding columns to the datagrid
                PassengersDataGrid.Columns.Add(passengerName);
                PassengersDataGrid.Columns.Add(passengerEmail);
                PassengersDataGrid.Columns.Add(passengerPassport);
                PassengersDataGrid.Columns.Add(dateofBirth);
                PassengersDataGrid.Columns.Add(passengerPhone);
                PassengersDataGrid.Columns.Add(gender);

                //clearing the data grid of any pre-existing values
                PassengersDataGrid.Items.Clear();

                //adding values to the datagrid
                foreach(Passenger p in db.Passengers.ToList()) {
                    PassengersDataGrid.Items.Add(p);
                }

                //selection mode set to one 
                PassengersDataGrid.SelectionMode = (DataGridSelectionMode)SelectionMode.Single;

                List<Booking> bookings = db.Bookings.ToList();

                //setting booking number on the window label
                if(bookings.Count > 0)
                {
                     bookingId = bookings[bookings.Count - 1].BookingId;
                }
                else
                {
                    bookingId = 0;
                }
                

                lblBookingId.Content = bookingId;

                db.SaveChanges();
            }
        }

        //event to see all passengers in the list
        public void SeeAll(object sender, EventArgs args) {
            InitializeDataGrid();

            txtEmail.Text = "";
            txtFullName.Text = "";
            txtPassport.Text = "";
            txtPhone.Text = "";
        }

        //event to handle passenger search 
        public void SearchPassenger(object sender, EventArgs args) {
            string FullName = txtFullName.Text.Trim();
            string Email = txtEmail.Text.Trim();
            string PhoneNumber = txtPhone.Text.Trim();
            string PassportNumber = txtPassport.Text.Trim();

            //checking if valid values are entered 
            if (isValid(FullName, Email, PhoneNumber, PassportNumber)) {
                if (isPhoneValid(PhoneNumber)) {
                    using (var db = new CustomDbContext()) {

                        if (FullName.Length == 0) {
                            FullName = "---";
                        }
                        if (Email.Length == 0) {
                            Email = "---";
                        }
                        if (PhoneNumber.Length == 0) {
                            PhoneNumber = "---";
                        }
                        if (PassportNumber.Length == 0) {
                            PassportNumber = "---";
                        }

                        List<Passenger> AllPassengers = db.Passengers.ToList();

                        //getting passengers based on the filtered values
                        var passengers = from passenger in AllPassengers
                                         where passenger.FullName.Contains(FullName) ||
                                            passenger.Email.Contains(Email) ||
                                            passenger.PhoneNo.Contains(PhoneNumber) ||
                                            passenger.PassportNo.Contains(PassportNumber)
                                         select passenger;
                        Console.WriteLine(passengers.Count());

                        //intializing columns for the datagrid
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

                        //adding columns for the grid
                        PassengersDataGrid.Columns.Add(passengerName);
                        PassengersDataGrid.Columns.Add(passengerEmail);
                        PassengersDataGrid.Columns.Add(passengerPassport);
                        PassengersDataGrid.Columns.Add(dateofBirth);
                        PassengersDataGrid.Columns.Add(passengerPhone);
                        PassengersDataGrid.Columns.Add(gender);

                        PassengersDataGrid.Items.Clear();

                        foreach (Passenger p in passengers.ToList()) {
                            PassengersDataGrid.Items.Add(p);
                        }
                    }
                }
                else {
                    MessageBox.Show("Phone Number should contain numbers only!");
                }
            }
            else {
                MessageBox.Show("Please enter atleast one value!");
            }

        }

        //Method to check if entered values of filter are correct
        public bool isValid(string fullName, string email, string phone, string passport) {
            if (fullName.Equals("") && email.Equals("")
                && phone.Equals("") && passport.Equals("")) {
                return false;
            }//!phone.All(char.IsDigit)

            return true;
        }

        //checking if phone number is valid
        public bool isPhoneValid(string phone) {
            if (!phone.All(char.IsDigit)) {
                return false;
            }

            return true;
        }

        //event to handle addition of passenger to the booking
        //data is saved in temporary list
        public void AddPassenger(object sender, EventArgs args) {
            if (isPassengerSelected()) {
                if (isAlreadyAdded()) {
                    MessageBox.Show("Passenger already added!!");
                }
                else {
                    //Adding passagers to temporary list of passangers
                    addedPassengers.Add((Passenger)PassengersDataGrid.SelectedValue);
                    MessageBox.Show("Passenger Added!");

                    String addedPassengerString = "Number of passengers added to bookings : " + addedPassengers.Count + "\n";

                    foreach(Passenger p in addedPassengers) {
                        addedPassengerString += p.ToString();
                    }

                    txtAddedPassengers.Text = addedPassengerString;

                    //setting values for the total prices
                    lblTotalPrice.Content = "$ " + (Convert.ToDecimal(lblFlightPrice.Content.ToString().Substring(1)) *
                                                    addedPassengers.Count);
                    passPrice = (float)(Convert.ToDecimal(lblFlightPrice.Content.ToString().Substring(1)) * addedPassengers.Count);
                }
                
            }
            else {
                MessageBox.Show("Please select at least one passenger!");
            }
        }

        //Checking if passenger is selected for addition
        public bool isPassengerSelected() {
            if (PassengersDataGrid.SelectedIndex == -1) {
                return false;
            }

            return true;
        }

        //Checking if passenger already exists
        public bool isAlreadyAdded() {
            Passenger passenger = (Passenger)PassengersDataGrid.SelectedValue;

            var sameIdUsers = from alreadyAdded in addedPassengers
            where alreadyAdded.PassengerId == passenger.PassengerId
            select alreadyAdded;

            if(sameIdUsers.ToList().Count() > 0) {
                return true;
            }

            return false;
        }

        //Adding passengers to list if not present
        public void AddPassengerToGrid(object sender, EventArgs args) {
            string FullName = txtName.Text.Trim();
            string Email = txtEmailId.Text.Trim();
            string PhoneNumber = txtPhoneNumber.Text.Trim();
            string DateOfBirth = txtDateOfBirth.Text.Trim();
            string Gender = comboGender.Text.Trim();
            string PassportNumber = txtPassportNumber.Text.Trim();

            if (isValid(FullName, Email, PhoneNumber, DateOfBirth, Gender, PassportNumber)) {
                using (var db = new CustomDbContext()) {
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

                    txtName.Text = "";
                    txtEmailId.Text = "";
                    txtPhoneNumber.Text = "";
                    txtPassportNumber.Text = "";
                    txtDateOfBirth.SelectedDate = null;
                    comboGender.SelectedIndex = -1;
                }
            }
            else {
                MessageBox.Show("One or two fields values are not correct!");
            }
        }

        //checking if values entered to add passengers is valid or not
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

        //Event to go to next payment page and linking passenger data to booking
        public void GoToPayment(object sender, EventArgs args) {
            if (addedPassengers.Count() == 0) {
                MessageBox.Show("Please add atleast one passenger to the booking!");
            }
            //if passengers are added to the temporary list
            else {
                
                int bookingId = Convert.ToInt32(lblBookingId.Content);

                //Adding passengers to booking
                using (var db = new CustomDbContext()) {
                    foreach (Passenger p in addedPassengers) {
                        BookingPassenger bp = new BookingPassenger();
                        bp.BookingId = bookingId;
                        bp.PassengerId = p.PassengerId;
                        var book = from booking in db.Bookings
                                   where booking.BookingId == bookingId
                                   select booking;
                        /*bp.Booking = book.ToList().First();
                        bp.Passenger = p;
*/
                        db.BookingPassengers.Add(bp);
                    }

                    db.SaveChanges();
                }
                CloseAllWindows();
                PaymentWindow paymentWindow = new PaymentWindow(bookingId, passPrice);
                //paymentWindow.ticketPriceValueLabel.Content = lblTotalPrice;
                //this.Hide();
                paymentWindow.Show();

                
            }
        }

        //To close all open windows
        public void CloseAllWindows() {
            foreach (Window window in Application.Current.Windows) {
                window.Hide();
            }
        }
    }
}
