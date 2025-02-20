using AutoMapper;
using Enterprise_Expense_Tracker.ModelContext;
using Enterprise_Expense_Tracker.Models;
using System.Security.Claims;

namespace Enterprise_Expense_Tracker.ServiceRepository
{
    public class Manager_Expenses : IManager_Expenses
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _http;
        public Manager_Expenses(AppDbContext dbcontext, IMapper mapper, IConfiguration configuration, IHttpContextAccessor http)
        {
            _context = dbcontext;
            _mapper = mapper;
            _http = http;
        }
        public List<Expense> GetAllExpensebyfilter(string manangerName, int userId, string status)
        {
            int managerId = _context.Users.FirstOrDefault(e => e.UserName == manangerName).Id;
            if (userId != -1 && status != "status")
            {
                return _context.Expenses.Where(e => e.UserId == userId && e.Status == status && e.ManagerId == managerId).ToList();
            }
            else if (status != "status")
            {
                return _context.Expenses.Where(e => e.Status == status && e.ManagerId == managerId).ToList();
            }
            else if (userId != -1)
            {
                return _context.Expenses.Where(e => e.UserId == userId && e.ManagerId == managerId).ToList();
            }
            return _context.Expenses.Where(e => e.ManagerId == managerId).ToList();

        }
        public List<Expense> ExpenseApprovalProcess(int ExpenseId, string status)
        {
            var expense = _context.Expenses.Where(e => e.Id == ExpenseId && e.Status==status);
            if (expense == null) throw new Exception();
            return expense.ToList();
            //expense.Status = status;
            //expense.ApprovalDate = DateTime.Now;
            //_context.SaveChanges();
        }

        public string GetCurrentUser()
        {
            return _http.HttpContext.User.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
            //string n_user2 = n_username;
            //return n_user2;
        }
        public dynamic GetAllExpenses() //get all expenses of mapped employees
        {
            var username = GetCurrentUser();
            dynamic user = (from u in _context.Users
                            join ur in _context.Expenses on u.Id equals ur.ManagerId
                            where u.UserName == username && u.Role==UserRoles.manager
                            select new { ur.Amount, ur.Description, ur.Status, ur.SubmissionDate, ur.ApprovalDate, ur.UserId, ur.ManagerId });
            //return _dbcontext.Expenses;
            return user;
        }

        public void Update_Employee_Expense_Status(int expenseid, string _status)
        {
            var result = _context.Expenses.Where(x => x.Id == expenseid && x.Status == _status).ToList();

            result.ForEach(e => e.Status = "Approved");
            result.ForEach(e => e.ApprovalDate = DateTime.Now);

            //result.ForEach(e => e.Status = "Rejected");
            //result.ForEach(e => e.ApprovalDate = DateTime.Now);

            _context.SaveChanges();

        }


    }
}
