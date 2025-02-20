using AutoMapper;
using Enterprise_Expense_Tracker.DTO;
using Enterprise_Expense_Tracker.ModelContext;
using Enterprise_Expense_Tracker.Models;
using static System.Net.WebRequestMethods;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace Enterprise_Expense_Tracker.ServiceRepository
{
    public class Employee_Expenses:IEmployee_Expenses
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _http;
        public Employee_Expenses(AppDbContext dbcontext, IMapper mapper, IConfiguration configuration, IHttpContextAccessor http)
        {
            _context = dbcontext;
            _mapper = mapper;
            _configuration = configuration;
            _http = http;
        }
        public void CreateExpenses(int UserId, ExpenseCreateDTO model)
        {

            var expense = _mapper.Map<Expense>(model);
            expense.UserId = UserId;
            expense.SubmissionDate = DateTime.Now;
            _context.Expenses.Add(expense);
            _context.SaveChanges();

        }


        //Below code is working
        /*public List<Expense> GetAllExpense(string username)
        {
            //dynamic user = _context.Users.Where(e => e.UserName == username).ToList();
            //return _context.Expenses.Where(e => e.UserId == user.Id).ToList();
            //return user;
            return _context.Expenses.Include(e => e.User_UserID).ToList();

        }*/
        public string GetCurrentUser() //user whose expenses we want
        {
            return _http.HttpContext.User.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
            //string n_user2 = n_username;
            //return n_user2;
        }
        public dynamic GetAllExpenses()
        {
            var username = GetCurrentUser();
            dynamic user = (from u in _context.Users
                            join ur in _context.Expenses on u.Id equals ur.UserId
                            where u.UserName == username && u.Role==UserRoles.employee
                            select new { ur.Id,ur.Amount,ur.Type, ur.Description, ur.Status, ur.SubmissionDate, ur.ApprovalDate, ur.UserId, ur.ManagerId });
            //return _dbcontext.Expenses;
            return user;
        }

        public Expense GetExpenseById(int id)
        {
            return _context.Expenses.Find(id);
        }
        public void DeleteExpenses(int id)
        {
            var expense = GetExpenseById(id);
            _context.Expenses.Remove(expense);
            _context.SaveChanges();
        }
        public void ModifyExpenses(int id, ExpenseUpdateDTO model)
        {
            var expense = GetExpenseById(id);
            expense.SubmissionDate = DateTime.Now;
            var newmodel = _mapper.Map(model, expense);
            _context.Expenses.Update(newmodel);
            _context.SaveChanges();
        }
        public dynamic GetExpensebyUserIdandStatus(int userid, string status)
        {
            //var user= _context.Expenses.Where(e => e.UserId == userid && e.Status == status).ToList();
            //return user;

            //var username = GetCurrentUser();
            var user = (from u in _context.Expenses
                        join ur in _context.Users on u.UserId equals ur.Id
                        where u.UserId == userid && u.Status == status
                        select new { u.Amount,u.Type, u.Description, u.Status, u.SubmissionDate, u.ApprovalDate, u.UserId, u.ManagerId });
            return user;
        }

        public dynamic SearchByExpenseIdandType(int expid, string exptype)
        {
            var user = (from u in _context.Expenses
                        //join ur in _context.Users on u.UserId equals ur.Id
                        where u.Id == expid && u.Type == exptype
                        select new { u.Amount, u.Type, u.Description, u.Status, u.SubmissionDate, u.ApprovalDate, u.UserId, u.ManagerId });
            return user;
        }
    }
}
   