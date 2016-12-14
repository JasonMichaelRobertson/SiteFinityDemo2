using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SitefinityWebApp.Mvc.Models
{
    public class EmailModel
    {
        public EmailModel()
        { }

        public EmailModel(string to, string bcc, string from, string firstName, string lastName)
        {
            To = to;
            Bcc = bcc;
            From = from;
            FirstName = firstName;
            LastName = lastName;
        }

        public string To { get; set; }
        public string Bcc { get; set; }
        public string From { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string FullName()
        {
            return FirstName + " " + LastName;
        }

    }
}