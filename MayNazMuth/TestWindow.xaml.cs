using MayNazMuth.Entities;
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
    /// <summary>
    /// Interaction logic for TestWindow.xaml
    /// </summary>
    public partial class TestWindow : Window {
        public TestWindow() {
            InitializeComponent();
        }

        public TestWindow(AddPassengerToBookingWindow a) {
            InitializeComponent();

            /*foreach (Passenger p in a.getAddedPassengers()) {
                Console.WriteLine(p.FullName);
            }*/
        }
    }
}
