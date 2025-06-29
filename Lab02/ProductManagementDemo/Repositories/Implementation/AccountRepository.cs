using BusinessObjects;
using DataAccessLayer;
using Repositories.Interface;

namespace Repositories.Implementation
{
    public class AccountRepository : IAccountRepository
    {
        public AccountMember GetAccountById(string accountID)
            => AccountDAO.GetAccountById(accountID);
    }
}
