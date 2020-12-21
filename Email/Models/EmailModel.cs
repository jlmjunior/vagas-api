using System;
using System.Collections.Generic;
using System.Text;

namespace Email.Models
{
    public class EmailModel
    {
        public string AddresseeName { get; set; }
        public string AddresseeEmail { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
    }
}
