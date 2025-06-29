using BusinessObjects;

namespace DataAccessLayer
{
    public class AccountDAO
    {
        public static AccountMember GetAccountById(string accountID)
        {
            using var db = new MyStoreContext();
            return db.AccountMember.FirstOrDefault(c => c.MemberID.Equals(accountID));
        }
    }
}
