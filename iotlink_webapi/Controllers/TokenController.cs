using iotlink_webapi.DataModels;
using iotlink_webapi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace iotlink_webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : Controller
    {
        private readonly AccountService _accountServices;
        private readonly IConfiguration _config;

        public TokenController(IConfiguration config, AccountService accountServices)
        {
            this._config = config;
            this._accountServices = accountServices;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Account account)
        {
            if (account != null)
            {
                var accountIn = await _accountServices.Get(account.Username, account.Password);

                if (accountIn != null)
                {
                    var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, _config["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("Id", accountIn.Id.ToString()),
                        new Claim("Username", accountIn.Username)
                    };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));

                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var token = new JwtSecurityToken(_config["Jwt:Issuer"], _config["Jwt:Audience"], 
                        claims, 
                        expires: DateTime.UtcNow.AddDays(1), 
                        signingCredentials: signIn);

                    return Ok(new JwtSecurityTokenHandler().WriteToken(token));

                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest();
            }
        }

    }
}
