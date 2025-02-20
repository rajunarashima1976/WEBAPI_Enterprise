using Enterprise_Expense_Tracker.Models;

namespace Enterprise_Expense_Tracker.ServiceRepository
{
    public interface IManager_Expenses
    {
        public List<Expense> GetAllExpensebyfilter(string manangerName, int userId, string status);
        public List<Expense> ExpenseApprovalProcess(int id, string status);
        dynamic GetAllExpenses();//Get all mapped employees expenses
        string GetCurrentUser();
        void Update_Employee_Expense_Status(int expenseid, string status);
    }
}
