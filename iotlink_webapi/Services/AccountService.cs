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

        public async Task<Account> Get(string id) =>
            await _accounts.Find(account => account.Id == id).FirstOrDefaultAsync();

        public async Task<Account> Get(string username, string password) =>
            await _accounts.Find<Account>(account => account.Username == username && account.Password == password)
                                        .FirstOrDefaultAsync();
        public async Task<Account> Create(Account account)
        {
            await _accounts.InsertOneAsync(account);
            return account;
        }
        public async Task Update(string id, Account account)
        {
            await _accounts.ReplaceOneAsync(accountIn => accountIn.Id == id, account);
        }
        public async Task Remove(Account accountIn)
        {
            await _accounts.DeleteOneAsync(account => account.Id == accountIn.Id);
        }
    }
}
