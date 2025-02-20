using Enterprise_Expense_Tracker.Models;
using Enterprise_Expense_Tracker.DTO;

namespace Enterprise_Expense_Tracker.ServiceRepository
{
    public interface IEmployee_Expenses
    {
        void CreateExpenses(int UserId, ExpenseCreateDTO model);
        void ModifyExpenses(int id, ExpenseUpdateDTO model);
        dynamic GetAllExpenses();
        string GetCurrentUser();
        void DeleteExpenses(int id);
        Expense GetExpenseById(int id);
        dynamic GetExpensebyUserIdandStatus(int userid, string status);
        dynamic SearchByExpenseIdandType(int expid, string exptype);
    }
}
