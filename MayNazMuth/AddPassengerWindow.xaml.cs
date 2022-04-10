﻿using MayNazMuth.Entities;
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
                //AllPassengersDataGrid.ItemsSource =  db.Passengers.ToList();

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

                AllPassengersDataGrid.Columns.Add(passengerName);
                AllPassengersDataGrid.Columns.Add(passengerEmail);
                AllPassengersDataGrid.Columns.Add(passengerPassport);
                AllPassengersDataGrid.Columns.Add(dateofBirth);
                AllPassengersDataGrid.Columns.Add(passengerPhone);
                AllPassengersDataGrid.Columns.Add(gender);

                AllPassengersDataGrid.Items.Clear();

                foreach (Passenger p in db.Passengers.ToList()) {
                    AllPassengersDataGrid.Items.Add(p);
                }

                db.SaveChanges();
            }
        }

        public void AddPassenger(object sender, EventArgs args) {
            string FullName = txtFullName.Text;
            string Email = txtEmail.Text;
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

        public void DeletePassenger(object sender, EventArgs args) {
            using (var db = new CustomDbContext()) {
                var selectedPassengers = AllPassengersDataGrid.SelectedItems;

                if(selectedPassengers.Count == 0) {
                    MessageBox.Show("No Passenger selected!!");
                }
                else {
                    foreach (Passenger p in selectedPassengers) {
                        db.Passengers.Remove(p);
                        MessageBox.Show("Passenger deleted successfully");
                    }
                }

                db.SaveChanges();
                InitializeDataGrid();
            }
        }

              

    }

    
}
