using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Festive_Phonebook.Models.DTO
{
    public class PhoneBookEntryDTO
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public int Kind { get; set; }
        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }

        public PhoneBookEntryDTO()
        {

        }

        public PhoneBookEntryDTO(PhonebookEntry data)
        {
            Id = data.Id.ToString();
            UserId = data.User.Id.ToString();
            Kind = (int)data.Kind;
            PhoneNumber = data.PhoneNumber;
            FirstName = data.FirstName;
            Surname = data.Surname;
        }
    }
}
