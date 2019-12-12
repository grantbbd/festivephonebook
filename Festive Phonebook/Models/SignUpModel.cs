using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Festive_Phonebook.Models
{
    public class SignUpModel : LoginModel
    {
        public string FirstName { get; set; }
        public string Surname { get; set; }
    }
}
