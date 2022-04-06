using iotlink_webapi.DataModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace iotlink_webapi.Services
{
    public interface IAccountService
    {
        public Task<List<Account>> Get();
        public Task<Account> Get(string id);
        public Task<Account> Create(Account account);
        public Task Update(string id, Account account);
        public Task Remove(Account accountIn);
    }
}
