using Enterprise_Expense_Tracker.DTO;
using Enterprise_Expense_Tracker.Models;

namespace Enterprise_Expense_Tracker.ServiceRepository
{
    public interface IUserRepository
    {
        User Authenticate(UserLoginDTO model);
        void Register(UserRegisterDTO model);
        User GetById(int id);
        public int getUserIdbyname(string userName);
        bool UserExists(string username, string? email);
        void Update_User(int id, UpdateUserDTO model);
    }
}
