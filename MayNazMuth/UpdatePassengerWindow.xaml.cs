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
    public partial class UpdatePassengerWindow : Window {
        public UpdatePassengerWindow() {
            InitializeComponent();

            AllPassengerDataGrid.SelectionMode = (DataGridSelectionMode)SelectionMode.Single;
            AllPassengerDataGrid.SelectionChanged += PopulateForm;
            btnUpdatePassenger.Click += UpdatePassenger;

            InitializeDataGrid();
        }

        public void InitializeDataGrid() {
            using (var db = new CustomDbContext()) {
                AllPassengerDataGrid.ItemsSource = db.Passengers.ToList();

                db.SaveChanges();
            }
        }

        public void PopulateForm(object sender, EventArgs args) {
                Passenger passenger = (Passenger)AllPassengerDataGrid.SelectedItem;

            lblPassengerId.Content = passenger.PassengerId;
            txtFullName.Text = passenger.FullName;
            txtEmail.Text = passenger.Email;
            txtPassport.Text = passenger.PassportNo;
            txtPhone.Text = passenger.PhoneNo;
            txtDate.SelectedDate = passenger.DateOfBirth;

            switch (passenger.Gender) {
                case "Male":
                    comboGender.SelectedIndex = 0;
                    break;
                case "Female":
                    comboGender.SelectedIndex = 1;
                    break;
                case "Other":
                    comboGender.SelectedIndex = 2;
                    break;
                default:
                    comboGender.SelectedIndex = 0;
                    break;
            }

        }

        public void UpdatePassenger(object sender, EventArgs args) {
            Console.WriteLine();

            if(isValid(txtFullName.Text.Trim(), txtEmail.Text.Trim(), txtPhone.Text.Trim(), txtDate.SelectedDate.ToString().Trim(), comboGender.SelectedItem.ToString().Trim(), txtPassport.Text.Trim())){
                using (var db = new CustomDbContext()) {
                    Passenger updatedPassenger = new Passenger(
                                        txtFullName.Text.Trim(),
                                        txtPassport.Text.Trim(),
                                        txtEmail.Text.Trim(),
                                        txtPhone.Text.Trim(),
                                        Convert.ToDateTime(txtDate.SelectedDate),
                                        ((ComboBoxItem)comboGender.SelectedItem).Content.ToString());

                    updatedPassenger.PassengerId = Convert.ToInt32(lblPassengerId.Content);
                    //updatedPassenger.BookingPassengers = 
                    db.Update(updatedPassenger);
                    db.SaveChanges();

                    AllPassengerDataGrid.SelectionChanged -= PopulateForm;
                    InitializeDataGrid();
                    AllPassengerDataGrid.SelectionChanged += PopulateForm;
                }
            }
            else {
                MessageBox.Show("One or two fields values are not correct!");
            }
        }

        public bool isValid(string fullName, string email, string phone, string dob, string gender, string passport) {

            if (fullName.Trim().Equals("")) {
                return false;
            }
            else if (email.Trim().Equals("")) {
                return false;
            }
            else if (!phone.Trim().All(char.IsDigit) || phone.Trim().Equals("")) {
                return false;
            }
            else if (dob.Trim().Equals("")) {
                return false;
            }
            else if (passport.Trim().Equals("")) {
                return false;
            }
            else if (gender.Trim().Equals("")) {
                return false;
            }

            return true;
        }




    }


}
