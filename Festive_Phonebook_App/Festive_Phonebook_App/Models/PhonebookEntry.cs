using System;
using System.Collections.Generic;
using System.Text;

namespace Festive_Phonebook_App.Models
{
    public class PhoneBookEntry
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public int Kind { get; set; }
        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public bool IsNice { get { return (Kind == 1); }  }
        public bool IsNaughty { get { return (Kind == 2); } }
    }

}
