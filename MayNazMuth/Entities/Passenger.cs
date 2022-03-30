using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MayNazMuth.Entities {
    class Passenger {
        public int PassengerId { get; set; }
        public string FullName { get; set; }
        public string PassportNo { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }

        //NavigationalProperties
        public List<BookingPassenger> BookingPassengers { get; set; }

        public Passenger() {
            BookingPassengers = new List<BookingPassenger>();
        }

        public Passenger(string nName, string nPassport, string nEmail, string nPhone, DateTime
            nDOB, string nGender) {
            FullName = nName;
            PassportNo = nPassport;
            Email = nEmail;
            PhoneNo = nPhone;
            DateOfBirth = nDOB;
            Gender = nGender;
        }
    }
}
