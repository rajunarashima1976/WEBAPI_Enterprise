using Enterprise_Expense_Tracker.Models;

namespace Enterprise_Expense_Tracker.ServiceRepository
{
    public interface ITokenRepository
    {
        string CreateJWTToken(User user);
    }
}
