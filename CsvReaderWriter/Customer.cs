using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace CsvReaderWriter
{
    class Customer
    {

        [Index(0)]
        public string FirstName { get; set; }
        [Index(1)]
        public string LastName { get; set; }
        [Index(2)]
        public int StreetNumber { get; set; }
        [Index(3)]
        public string Street { get; set; }
        [Index(4)]
        public string City { get; set; }
        [Index(5)]
        public string Province { get; set; }
        [Index(6)]
        public string PostalCode { get; set; }
        [Index(7)]
        public string Country { get; set; }
        [Index(8)]
        public string PhoneNumber { get; set; }
        [Index(9)]
        public string EmailAddress { get; set; }
    }
}

