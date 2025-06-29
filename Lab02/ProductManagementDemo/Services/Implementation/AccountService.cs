using BusinessObjects;
using Repositories.Implementation;
using Repositories.Interface;
using Services.Interface;

namespace Services.Implementation
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository iAccountRepository;
        public AccountService()
        {
            iAccountRepository = new AccountRepository();
        }
        public AccountMember GetAccountById(string accountID)
        {
            return iAccountRepository.GetAccountById(accountID);
        }
    }
}
