using Festive_Phonebook_App.Models;
using Flurl;
using Flurl.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Festive_Phonebook_App.Services
{
    public class PhonebookServiceImpl : IPhonebookService
    {
        private string BaseURL { get; set; } = "https://festivephonebook20191212022330.azurewebsites.net/api/";

        public async Task<bool> CreateEntry(string token, PhoneBookEntry entry)
        {
            var result = await BaseURL
                    .AppendPathSegment("phonebook/create")
                    .WithOAuthBearerToken(token)
                    .PostJsonAsync(entry);

            return result.IsSuccessStatusCode;
        }

        public async Task<IEnumerable<PhoneBookEntry>> GetEntries(string token)
        {
            var result = await BaseURL
                .AppendPathSegment("phonebook/all")
                .WithOAuthBearerToken(token)
                .GetJsonAsync<IEnumerable<PhoneBookEntry>>();

            return result;
        }

        public async Task<string> LoginUser(string email, string password)
        {
            try
            {
                var token = await BaseURL
                    .AppendPathSegment("user/login")
                    .PostJsonAsync(new LoginModel() { UserName = email, Password = password })
                    .ReceiveJson<LoginResponseModel>();

                return token.Token;

            } catch (Exception)
            {
                throw;
            }
        }

        //private string BaseURL { get; set; } = "https://10.8.0.138:8899/api/";

        public async Task<bool> UserExists(string email)
        {

            try
            {
                var result = await BaseURL
                    .AppendPathSegment("user/exists")
                    .PostJsonAsync(new LoginModel() { UserName = email });

                return result.IsSuccessStatusCode;
            } catch (Exception ex)
            {
                return false;
            }
        }
    }
}
