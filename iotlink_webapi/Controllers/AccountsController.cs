using iotlink_webapi.DataModels;
using iotlink_webapi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace iotlink_webapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : Controller
    {
        private readonly AccountService _accountServices;

        public AccountsController(AccountService accountServices)
        {
            this._accountServices = accountServices;
        }
        // GET: api/Accounts
        [HttpGet]
        public async Task<IActionResult> GetAccounts()
        {
            var accounts = await _accountServices.Get();
            return Ok(accounts);
        }
        // GET: api/Accounts/id
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Account>> GetAccount([FromRoute] string id)
        {
            var place = await _accountServices.Get(id);

            if (place == null)
                return NotFound();
            return place;
        }
        // POST: api/Accounts
        [HttpPost]
        public async Task<IActionResult> Create(Account account)
        {
            await _accountServices.Create(account);
            return CreatedAtAction(nameof(GetAccounts), new { name = account.Username }, account);
        }
        // PUT: api/Accounts/id
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] string id, Account account)
        {
            var accountIn = await _accountServices.Get(id);

            if (accountIn == null)
            {
                return NotFound();
            }

            await _accountServices.Update(id, account);
            return Content("Success");
        }
        // DELETE api/Accounts/id
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            var place = await _accountServices.Get(id);

            if (place == null) { return NotFound(); }

            await _accountServices.Remove(place);

            return Content("Success");
        }

    }
}
