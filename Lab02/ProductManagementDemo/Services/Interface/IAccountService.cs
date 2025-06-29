using BusinessObjects;

namespace Services.Interface
{
    public interface IAccountService
    {
        AccountMember GetAccountById(string accountID);
    }
}
