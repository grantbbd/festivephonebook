using Festive_Phonebook_App.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Festive_Phonebook_App.Services
{
    public interface IPhonebookService
    {
        Task<bool> UserExists(string email);
        Task<string> LoginUser(string email, string password);
        Task<IEnumerable<PhoneBookEntry>> GetEntries(string token);
        Task<bool> CreateEntry(string token, PhoneBookEntry entry);
        Task<bool> UpdateEntry(string token, PhoneBookEntry entry);
        Task<bool> DeleteEntry(string token, string id);
    }
}
