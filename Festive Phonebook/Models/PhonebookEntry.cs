using Festive_Phonebook.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Festive_Phonebook.Models
{
    public class PhonebookEntry
    {
        public Guid Id { get; set; }
        [Required]
        public ApplicationUser User { get; set; }
        [Required]
        public EntryKindEnum Kind { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string Surname { get; set; }
    }
}
