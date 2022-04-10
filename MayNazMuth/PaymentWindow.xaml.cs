using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MayNazMuth.Entities;
using MayNazMuth.Utilities;

namespace MayNazMuth
{
    /// <summary>
    /// Interaction logic for PaymentReportWindow.xaml
    /// </summary>
    public partial class PaymentWindow : Window
    {
        float totalAmount =0;
        int bkId = 0;
        string bookingStatus;
        public PaymentWindow(int bookingId)
        {

            InitializeComponent();
            bkId = bookingId;

            
            using (var ctx = new CustomDbContext())
            {
                Booking AP = ctx.Bookings.Where(x => x.BookingId == bkId).First();
                bookingStatus = AP.BookingStatus;
            }
            bookingStatusLabel.Content = bookingStatus;
            BookingRefrenceNoValueLabel.Content = bkId;
            taxPriceValueLabel.Content = Convert.ToDecimal(ticketPriceValueLabel.Content) * Convert.ToDecimal(0.12);
            totalPriceValueLabel.Content = Convert.ToDecimal(ticketPriceValueLabel.Content) + Convert.ToDecimal(taxPriceValueLabel.Content);
            totalAmount = (float)(Convert.ToDecimal(ticketPriceValueLabel.Content) + Convert.ToDecimal(taxPriceValueLabel.Content));

            //turn the event handlers off
            ToggleEventHandlers(false);           

            //Turn event handlers on
            ToggleEventHandlers(true);
        }
        private void ToggleEventHandlers(bool toggle)
        {
            if (toggle)
            {
                //turn on               
                paymentButton.Click += addPaymentInfo;
               
            }
            else
            {
                //Turn off               
                paymentButton.Click -= addPaymentInfo;
                
            }
        }

        private void addPaymentInfo(object s, EventArgs e)
        {
            
            Payment newPayment = new Payment();
            newPayment.BookingId = bkId;
            DateTime thisDay = DateTime.Today;
            newPayment.PaymentDatetime = thisDay;     
            newPayment.PaymentStatus = "Completed";
            newPayment.PaymentMethod = "Online";
            newPayment.CardHolderName = CardHolderNameTextBox.Text;
            newPayment.CardNumber = CardNumberTextBox.Text;
            newPayment.TotalPrice = totalAmount;


            using (var ctx = new CustomDbContext())
            {
                 if (string.IsNullOrEmpty(newPayment.CardHolderName))
                {
                    MessageBox.Show("Card holder name is empty.");
                }
                              
                else if (!IsCreditNumberValid(newPayment.CardNumber))
                {
                    MessageBox.Show("Card Number is not valid.");
                }

                else if (ExpiryDateTextBox.Text.Equals(""))
                {
                    MessageBox.Show("Expity date is empty.");
                }
                else if (!IsCreditExpiryValid(ExpiryDateTextBox.Text))
                {
                    MessageBox.Show("Expity date is not valid.");
                }
                else if (!CVVTextBox.Text.All(char.IsDigit))
                {
                    MessageBox.Show("CVV must be numeric.");

                }
                
                else if (CVVTextBox.Text.Equals(""))
                {
                    MessageBox.Show("CVV is empty.");
                }
                else if (!IsCVvValid(CVVTextBox.Text))
                {
                    MessageBox.Show("CVV is not valid.");
                }
                else
                {
                    ctx.Payments.Add(newPayment);
                    ctx.SaveChanges();
                    Booking AP = ctx.Bookings.Where(x => x.BookingId == bkId).First();
                    AP.BookingStatus = "Completed";
                    //Update the object
                    ctx.Bookings.Update(AP);
                    //Save my changes
                    ctx.SaveChanges();
                    MessageBox.Show("Payment is done successfully !");
                    OpenMainWindow();
                }                
            }
        }


        private void OpenMainWindow()
        {

            MainWindow flights = new MainWindow();
            CloseAllWindows();
            flights.Show();
        }

        public void CloseAllWindows()
        {
            foreach (Window window in Application.Current.Windows)
            {
                window.Hide();
            }
        }


        //this function checks credit card format by using Regex
        public static bool IsCreditNumberValid(string cardNo)
        {
            var cardCheck = new Regex(@"([\-\s]?[0-9]{4}){3}$");

            //check card number is valid
            if (!cardCheck.IsMatch(cardNo))
                 return false;
            else return true;                       
        }

        //this function checks credit card CVV by using Regex
        public static bool IsCVvValid(string cvv)
        {            
            var cvvCheck = new Regex(@"^\d{3}$");

            //check cvv is valid as "999"
            if (!cvvCheck.IsMatch(cvv))
                return false;
            else return true;
        }

        //this function checks credit card Expiry by using Regex
        public static bool IsCreditExpiryValid(string expiryDate)
        {            
            var monthCheck = new Regex(@"^(0[1-9]|1[0-2])$");
            var yearCheck = new Regex(@"^[0-9]{2}$");

            //expiry date format MM/yy
            var dateParts = expiryDate.Split('/');
            if (!monthCheck.IsMatch(dateParts[0]) || !yearCheck.IsMatch(dateParts[1]))
                return false;
            else return true;

            //var year = int.Parse(dateParts[1]);
            //var month = int.Parse(dateParts[0]);
            //var lastDateOfExpiryMonth = DateTime.DaysInMonth(year, month); //get actual expiry date
            //var cardExpiry = new DateTime(year, month, lastDateOfExpiryMonth, 23, 59, 59);

            ////check expiry greater than today & within next 6 years 
            //return (cardExpiry > DateTime.Now && cardExpiry < DateTime.Now.AddYears(6));
        }
    }
}
