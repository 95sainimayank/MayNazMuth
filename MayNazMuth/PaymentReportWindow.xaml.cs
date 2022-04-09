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
using MayNazMuth.Entities;
using MayNazMuth.Utilities;

namespace MayNazMuth
{
    /// <summary>
    /// Interaction logic for PaymentReportWindow.xaml
    /// </summary>
    public partial class PaymentReportWindow : Window
    {
        public PaymentReportWindow()
        {
            InitializeComponent();

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
            newPayment.PaymentDatetime = new DateTime();
            newPayment.TotalPrice = 0;
            newPayment.PaymentMethod = "Online";
            newPayment.PaymentStatus = "Awaiting Payment";
            newPayment.CardHolderName = CardHolderNameTextBox.Text;
            newPayment.CardNumber = CardNumberTextBox.Text;
           


            using (var ctx = new CustomDbContext())
            {

                if (!newPayment.CardHolderName.Equals("") && !newPayment.CardNumber.Equals(""))
                {                   
                    ctx.Payments.Add(newPayment);
                    ctx.SaveChanges();                   
                }
                else
                {
                    MessageBox.Show("Please fill all fields.");
                }
              
            }
        }
    }
}
