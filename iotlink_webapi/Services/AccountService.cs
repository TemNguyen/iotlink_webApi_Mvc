using iotlink_webapi.DataModels;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace iotlink_webapi.Services
{
    public class AccountService
    {
        private readonly IMongoCollection<Account> _accounts;

        public AccountService(IMongoDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _accounts = database.GetCollection<Account>("Account");

        }

        public async Task<List<Account>> Get() =>
            await _accounts.Find(account => true).ToListAsync();

        public async Task<Account> Get(string username) =>
            await _accounts.Find<Account>(account => account.Username == username).FirstOrDefaultAsync();

        public async Task<Account> Create(Account account)
        {
            await _accounts.InsertOneAsync(account);
            return account;
        }

        public async Task Update(string name, Account accountIn)
        {
            await _accounts.ReplaceOneAsync(account => account.Username == name, accountIn);
        }

        public async Task Remove(Account accountIn)
        {
            await _accounts.DeleteOneAsync(account => account.Username == accountIn.Username);
        }
    }
}
