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

            //setting selection of the datagrid data to single item at a time
            AllPassengerDataGrid.SelectionMode = (DataGridSelectionMode)SelectionMode.Single;

            //attchcing events to datagrid and buttons
            AllPassengerDataGrid.SelectionChanged += PopulateForm;
            btnUpdatePassenger.Click += UpdatePassenger;

            comboGender.SelectedIndex = 0;

            InitializeDataGrid();
        }

        //Initializes and populates the datagrid
        public void InitializeDataGrid() {
            using (var db = new CustomDbContext()) {
                //Setting columns for the datagrid
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

                //Adding datagrid columns
                AllPassengerDataGrid.Columns.Add(passengerName);
                AllPassengerDataGrid.Columns.Add(passengerEmail);
                AllPassengerDataGrid.Columns.Add(passengerPassport);
                AllPassengerDataGrid.Columns.Add(dateofBirth);
                AllPassengerDataGrid.Columns.Add(passengerPhone);
                AllPassengerDataGrid.Columns.Add(gender);

                AllPassengerDataGrid.Items.Clear();

                foreach (Passenger p in db.Passengers.ToList()) {
                    AllPassengerDataGrid.Items.Add(p);
                }

                //disabling the textboxes and other input fields
                txtFullName.IsEnabled = false;
                txtEmail.IsEnabled = false;
                txtPassport.IsEnabled = false;
                txtPhone.IsEnabled = false;
                txtDate.IsEnabled = false;
                comboGender.IsEnabled = false;

                db.SaveChanges();
            }
        }

        //populates the form based on the passenger selected
        public void PopulateForm(object sender, EventArgs args) {
                Passenger passenger = (Passenger)AllPassengerDataGrid.SelectedItem;

            //enabling fields to enable editing
            txtFullName.IsEnabled = true;
            txtEmail.IsEnabled = true;
            txtPassport.IsEnabled = true;
            txtPhone.IsEnabled = true;
            txtDate.IsEnabled = true;
            comboGender.IsEnabled = true;

            //setting content of fields based on passenger selected
            lblPassengerId.Content = passenger.PassengerId;
            txtFullName.Text = passenger.FullName;
            txtEmail.Text = passenger.Email;
            txtPassport.Text = passenger.PassportNo;
            txtPhone.Text = passenger.PhoneNo;
            txtDate.SelectedDate = passenger.DateOfBirth;

            //setting gender based on the input selected
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

        //EVent to update passenger
        public void UpdatePassenger(object sender, EventArgs args) {
            //Getting values from the form
            string name = txtFullName.Text;
            string email = txtEmail.Text;
            string phone = txtPhone.Text;
            string dob = txtDate.SelectedDate.ToString();
            string gender = ((ComboBoxItem)comboGender.SelectedItem).Content.ToString();
            string passport = txtPassport.Text;

            //Checking if the inputs are valid
            if (isValid(name, email, phone, dob, gender, passport)){
                using (var db = new CustomDbContext()) {
                    //creating passenger object based on input values
                    Passenger updatedPassenger = new Passenger(
                                        txtFullName.Text.Trim(),
                                        txtPassport.Text.Trim(),
                                        txtEmail.Text.Trim(),
                                        txtPhone.Text.Trim(),
                                        Convert.ToDateTime(txtDate.SelectedDate),
                                        ((ComboBoxItem)comboGender.SelectedItem).Content.ToString());

                    updatedPassenger.PassengerId = Convert.ToInt32(lblPassengerId.Content);
                    
                    db.Update(updatedPassenger);
                    db.SaveChanges();

                    //Disabling the event to initialze the datagrid
                    AllPassengerDataGrid.SelectionChanged -= PopulateForm;
                    InitializeDataGrid();
                    //enabling the event again
                    AllPassengerDataGrid.SelectionChanged += PopulateForm;

                    MessageBox.Show("Passenger details updated.");

                    //refreshing the form values after data added to db
                    lblPassengerId.Content = "";
                    txtFullName.Clear();
                    txtEmail.Clear();
                    txtPassport.Clear();
                    txtPhone.Clear();
                    txtDate.SelectedDate = null;
                    comboGender.SelectedIndex = 0;
                }
            }
            else {
                MessageBox.Show("Either user not selected OR One or more field values are not correct!");
            }
        }

        //Method to check if input valid
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
