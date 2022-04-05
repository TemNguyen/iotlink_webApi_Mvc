using iotlink_webapi.DataModels;
using iotlink_webapi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace iotlink_webapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly AccountService _accountServices;

        public AccountController(AccountService accountServices)
        {
            this._accountServices = accountServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetAccount()
        {
            var accounts = await _accountServices.Get();
            return Ok(accounts);
        }
        [HttpGet]
        [Route("{username}")]
        public async Task<ActionResult<Account>> GetAccountByUsername([FromRoute] string username)
        {
            var place = await _accountServices.Get(username);

            if (place == null)
                return NotFound();
            return place;
        }

        [HttpPost]
        public async Task<IActionResult> Create(Account account)
        {
            await _accountServices.Create(account);
            return CreatedAtAction(nameof(GetAccount), new { name = account.Username }, account);

        }
    }
}
