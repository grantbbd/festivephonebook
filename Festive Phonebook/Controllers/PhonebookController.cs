using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Festive_Phonebook.Models;
using Festive_Phonebook.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Festive_Phonebook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PhonebookController : ControllerBase
    {
        private UserManager<ApplicationUser> _userManager;
        private ApplicationDBContext _context;

        public PhonebookController(UserManager<ApplicationUser> userManager, ApplicationDBContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        [HttpPost]
        [Route("update")]
        public async Task<ActionResult<Object>> Update(PhoneBookEntryDTO data)
        {
            var user = GetCurrentUser();
            if (user != null)
            {
                Guid.TryParse(data.Id, out Guid entryId);
                PhonebookEntry entry = _context.PhonebookEntries.FirstOrDefault(x => x.User.Id == user.Id && x.Id == entryId);
                if (entry != null)
                {
                    entry.FirstName = data.FirstName;
                    entry.Surname = data.Surname;
                    entry.PhoneNumber = data.PhoneNumber;
                    entry.Kind =  (Enums.EntryKindEnum)data.Kind;

                    _ = await _context.SaveChangesAsync();

                    return Ok();
                }
            }

            return NotFound();
        }

        [HttpPost]
        [Route("create")]
        public async Task<ActionResult<Object>> Create(PhoneBookEntryDTO data)
        {
            var user = GetCurrentUser();
            if (user != null)
            {
                PhonebookEntry entry = new PhonebookEntry()
                {
                    Id = Guid.NewGuid(),
                    FirstName = data.FirstName,
                    Surname = data.Surname,
                    PhoneNumber = data.PhoneNumber,
                    Kind = (Enums.EntryKindEnum)data.Kind,
                    User = user
                };

                try
                {
                    _context.PhonebookEntries.Add(entry);
                    _ = await _context.SaveChangesAsync();
                    return Ok();
                } catch (Exception ex)
                {
                    throw ex;
                }
            }

            return BadRequest();
        }

        [HttpGet]
        [Route("all")]
        public ActionResult<List<PhoneBookEntryDTO>> GetAll()
        {
            var user = GetCurrentUser();
            if (user != null)
            {
                var entries = _context.PhonebookEntries
                    .Where(x => x.User.Id == user.Id)
                    .OrderBy(x => x.Surname).ThenBy(x => x.FirstName)
                    .ToList();
                var result = new List<PhoneBookEntryDTO>();
                entries.ForEach(x => result.Add(new PhoneBookEntryDTO(x)));
                return result;
            }

            return NotFound();
        }

        [HttpGet]
        [Route("get")]
        public ActionResult<PhoneBookEntryDTO> Get(string id)
        {
            var user = GetCurrentUser();
            if (user != null)
            {
                Guid.TryParse(id, out Guid entryId);
                PhonebookEntry entry = _context.PhonebookEntries.FirstOrDefault(x => x.User.Id == user.Id && x.Id == entryId);
                if (entry != null)
                {
                    return new PhoneBookEntryDTO(entry);
                }
            }
            return NotFound();
        }

        [HttpGet]
        [Route("delete")]
        public async Task<ActionResult> Delete(string id)
        {
            var user = GetCurrentUser();
            if (user != null)
            {
                Guid.TryParse(id, out Guid entryId);
                PhonebookEntry entry = _context.PhonebookEntries.FirstOrDefault(x => x.User.Id == user.Id && x.Id == entryId);
                if (entry != null)
                {
                    _context.Remove(entry);
                    _ = await _context.SaveChangesAsync();
                    return Ok();
                }
            }

            return NotFound();
        }

        private ApplicationUser GetCurrentUser()
        {
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            return _userManager.FindByIdAsync(userId).Result;
        }

    }
}