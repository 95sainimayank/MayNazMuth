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
        List<Passenger> addedPassengers = new List<Passenger>();

        public AddPassengerToBookingWindow() {
            InitializeComponent();

            InitializeDataGrid();

            txtAddedPassengers.IsEnabled = false;

            btnSeeAll.Click += SeeAll;
            btnSearch.Click += SearchPassenger;
            btnAdd.Click += AddPassenger;
            btnPassengerToGrid.Click += AddPassengerToGrid;
        }

        public void InitializeDataGrid() {
            using (var db = new CustomDbContext()) {
                PassengersDataGrid.ItemsSource = db.Passengers.ToList();
                PassengersDataGrid.SelectionMode = (DataGridSelectionMode)SelectionMode.Single;

                List<Booking> bookings = db.Bookings.ToList();
                int bookingId = bookings[bookings.Count - 1].BookingId;

                lblBookingId.Content = bookingId;

                db.SaveChanges();
            }
        }

        public void SeeAll(object sender, EventArgs args) {
            InitializeDataGrid();

            txtEmail.Text = "";
            txtFullName.Text = "";
            txtPassport.Text = "";
            txtPhone.Text = "";
        }

        public void SearchPassenger(object sender, EventArgs args) {
            string FullName = txtFullName.Text.Trim();
            string Email = txtEmail.Text.Trim();
            string PhoneNumber = txtPhone.Text.Trim();
            string PassportNumber = txtPassport.Text.Trim();

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

                        var passengers = from passenger in AllPassengers
                                         where passenger.FullName.Contains(FullName) ||
                                            passenger.Email.Contains(Email) ||
                                            passenger.PhoneNo.Contains(PhoneNumber) ||
                                            passenger.PassportNo.Contains(PassportNumber)
                                         select passenger;
                        Console.WriteLine(passengers.Count());
                        PassengersDataGrid.ItemsSource = passengers.ToList();
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

        public bool isValid(string fullName, string email, string phone, string passport) {
            if (fullName.Equals("") && email.Equals("")
                && phone.Equals("") && passport.Equals("")) {
                return false;
            }//!phone.All(char.IsDigit)

            return true;
        }

        public bool isPhoneValid(string phone) {
            if (!phone.All(char.IsDigit)) {
                return false;
            }

            return true;
        }

        public void AddPassenger(object sender, EventArgs args) {
            if (isPassengerSelected()) {
                if (isAlreadyAdded()) {
                    MessageBox.Show("Passenger already added!!");
                }
                else {
                    addedPassengers.Add((Passenger)PassengersDataGrid.SelectedValue);
                    MessageBox.Show("Passenger Added!");

                    String addedPassengerString = "Number of passengers added to bookings : " + addedPassengers.Count + "\n";

                    foreach(Passenger p in addedPassengers) {
                        addedPassengerString += p.ToString();
                    }

                    txtAddedPassengers.Text = addedPassengerString;
                }
                
            }
            else {
                MessageBox.Show("Please select at least one passenger!");
            }
        }

        public bool isPassengerSelected() {
            if (PassengersDataGrid.SelectedIndex == -1) {
                return false;
            }

            return true;
        }

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
    }
}
