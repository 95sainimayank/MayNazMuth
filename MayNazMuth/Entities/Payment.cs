using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MayNazMuth.Entities {
    class Payment {
        public int PaymentId { get; set; }
        public DateTime PaymentDatetime { get; set; }        
        public string PaymentMethod { get; set; }
        public string PaymentStatus { get; set; }
        public string CardHolderName { get; set; }
        public string CardNumber { get; set; }
        public float TotalPrice { get; set; }


        //navigational properties
        public int BookingId { get; set; }
        public Booking Booking { get; set; }

        public Payment() {

        }


        public Payment(DateTime nDt, string nMethod, string nStatus, string nHolderName, string nCardNo, float nPrice) {
            PaymentDatetime = nDt;          
            PaymentMethod = nMethod;
            PaymentStatus = nStatus;
            CardHolderName = nHolderName;
            CardNumber = nCardNo;
            TotalPrice = nPrice;
        }
    }
}
