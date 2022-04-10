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
        List<Payment> PaymentList = new List<Payment>();
        decimal totalAmount;
        public PaymentReportWindow()
        {
            InitializeComponent();            

            //turn the event handlers off
            ToggleEventHandlers(false);

            //Initialize the data grid
            SetupGrid();

            //populate the grid
            Updategrid();

            //Turn event handlers on
            ToggleEventHandlers(true);
        }

        private void Updategrid()
        {
            TotalSaleValueLabel.Content = "";
            transactionCountValueLabel.Content = "";
            //backToAirportButon.Content = ApId.ToString();
            using (var ctx = new CustomDbContext())
            {
                PaymentList = ctx.Payments.ToList<Payment>();

                //var FoundFlights = FlightList.Where(x => x.SourceAirportId == ApId || x.DestinationAirportId == ApId);               

                paymentsDataGrid.Items.Clear();
                foreach (Payment p in PaymentList)
                {
                    paymentsDataGrid.Items.Add(p);
                    totalAmount += Convert.ToDecimal(p.TotalPrice);
                }
                TotalSaleValueLabel.Content = totalAmount;
                transactionCountValueLabel.Content = PaymentList.Count();
            }
        }

        private void SetupGrid()
        {
            paymentsDataGrid.SelectionMode = DataGridSelectionMode.Single;
            //Make it read only
            paymentsDataGrid.IsReadOnly = true;

            DataGridTextColumn PaymentDate = new DataGridTextColumn();
            PaymentDate.Header = "Payment Date/time";
            PaymentDate.Binding = new Binding("PaymentDatetime");

            DataGridTextColumn PaymentStatus = new DataGridTextColumn();
            PaymentStatus.Header = "Status";
            PaymentStatus.Binding = new Binding("PaymentStatus");

            DataGridTextColumn PaymentCardNo = new DataGridTextColumn();
            PaymentCardNo.Header = "Card Number";
            PaymentCardNo.Binding = new Binding("CardNumber");

            DataGridTextColumn PaymentTotalPrice = new DataGridTextColumn();
            PaymentTotalPrice.Header = "Total ($)";
            PaymentTotalPrice.Binding = new Binding("TotalPrice");


            paymentsDataGrid.Columns.Add(PaymentDate);
            paymentsDataGrid.Columns.Add(PaymentStatus);
            paymentsDataGrid.Columns.Add(PaymentCardNo);
            paymentsDataGrid.Columns.Add(PaymentTotalPrice);         
        }

        private void ToggleEventHandlers(bool toggle)
        {
            if (toggle)
            {
                //turn on               
                FilterButton.Click += searchDate;
                resetButton.Click += resetfilter;

            }
            else
            {
                //Turn off               

                FilterButton.Click -= searchDate;
                resetButton.Click -= resetfilter;
            }
        }

        private void resetfilter(object sender, RoutedEventArgs e)
        {
            fromDatePicker.SelectedDate = DateTime.Now;
            toDatePicker.SelectedDate = DateTime.Now;
            Updategrid();
        }

        private void searchDate(object sender, RoutedEventArgs e)
        {
            TotalSaleValueLabel.Content = "";
            transactionCountValueLabel.Content = "";

            var startFrom = fromDatePicker.SelectedDate;
            var endTo = toDatePicker.SelectedDate;

            if (startFrom <= endTo)
            {               
                using (var ctx = new CustomDbContext())
                {
                    PaymentList = ctx.Payments.ToList<Payment>();
                    var filteredList = PaymentList.Where(x =>  x.PaymentDatetime >= startFrom && x.PaymentDatetime <= endTo);
                    paymentsDataGrid.Items.Clear();
                    totalAmount = 0;
                    foreach (Payment p in filteredList)
                    {
                        paymentsDataGrid.Items.Add(p);
                        totalAmount += Convert.ToDecimal(p.TotalPrice);
                    }

                    TotalSaleValueLabel.Content = totalAmount;
                    transactionCountValueLabel.Content = filteredList.Count();

                }
                
            }
            else
            {
                MessageBox.Show("Start Date can not be later than End Date");
            }
        }
    }
}
