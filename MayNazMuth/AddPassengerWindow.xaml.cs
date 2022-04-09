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
        List<Booking> bookingLsit = new List<Booking>();
        int bookingId;
        public AddPassengerWindow() {
            InitializeComponent();

            InitializeDataGrid();

            btnAddPassenger.Click += AddPassenger;
            btnDeletePassenger.Click += DeletePassenger;
            btnOpenPayment.Click  += btnOpenPaymentWindow;
        }

        private void btnOpenPaymentWindow(object sender, RoutedEventArgs e)
        {
            using (var ctx = new CustomDbContext())
            {                 
                bookingLsit = ctx.Bookings.ToList<Booking>();
                bookingId= bookingLsit[bookingLsit.Count() - 1].BookingId;
            }
            PaymentWindow Payment = new PaymentWindow(bookingId);
            CloseAllWindows();
            Payment.Show();
        }
        public void CloseAllWindows()
        {
            foreach (Window window in Application.Current.Windows)
            {
                window.Hide();
            }
        }
        public void InitializeDataGrid() {
            using(var db = new CustomDbContext()) {
                AllPassengersDataGrid.ItemsSource =  db.Passengers.ToList();

                db.SaveChanges();

            }
        }

        public void AddPassenger(object sender, EventArgs args) {
            string FullName = txtFullName.Text;
            string Email = txtFullName.Text;
            string PhoneNumber = txtPhoneNumber.Text;
            string DateOfBirth = dateOfBirth.Text;
            string Gender = gender.Text;
            string PassportNumber = txtPassportNumber.Text;

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
            else if (!phone.All(char.IsDigit)) {
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

        public void DeletePassenger(object sender, EventArgs args) {
            using (var db = new CustomDbContext()) {
                var selectedPassengers = AllPassengersDataGrid.SelectedItems;

                foreach(Passenger p in selectedPassengers) {
                    db.Passengers.Remove(p);
                }

                db.SaveChanges();
                InitializeDataGrid();
            }
        }

    }

    
}
