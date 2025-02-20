using AutoMapper;
using Enterprise_Expense_Tracker.DTO;
using Enterprise_Expense_Tracker.ModelContext;
using Enterprise_Expense_Tracker.Models;
using Enterprise_Expense_Tracker.ServiceRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Enterprise_Expense_Tracker.Helper;

namespace Enterprise_Expense_Tracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = UserRoles.employee)]
    public class EmployeeExpensesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private IEmployee_Expenses _emp;
        //private readonly ILogger<Expense> _logger;
        private IMapper _mapper;
        private IUserRepository _user;


        public EmployeeExpensesController(IEmployee_Expenses emp, IMapper mapper, AppDbContext context, IUserRepository user)
        {
            _emp = emp;
            _mapper = mapper;
            _context = context;
            _user = user;
        }
        [HttpGet("getallexpense")]
        public IActionResult GetAllExpense()
        {
            var Username = User.Identity.Name;
            dynamic user = (from u in _context.Users
                            join ur in _context.Expenses on u.Id equals ur.UserId
                            where u.UserName == Username && u.Role == UserRoles.employee
                            select new { ur.Id, ur.Amount, ur.Type, ur.Description, ur.Status, ur.SubmissionDate, ur.ApprovalDate, ur.UserId, ur.ManagerId });
            //return _dbcontext.Expenses;

            if (Username == null)
            {
                throw new AppException("UserName Can't be Null!");
            }
            //var result = _emp.GetAllExpense(Username);
            var result = _emp.GetAllExpenses();
            return Ok(result);
        }
        [HttpPost("CreateNewExpense")]
        public IActionResult CreateNewExpense(ExpenseCreateDTO model)
        {
            var username = User.Identity.Name;
            var userId = _user.getUserIdbyname(username);
            _emp.CreateExpenses(userId, model);
            return Ok(new { message = "employee expenses saved successfully..." });
        }
        [HttpPut("UpdateExpense")]
        public IActionResult UpdateExpense(int id, ExpenseUpdateDTO model)
        {
            _emp.ModifyExpenses(id, model);
            return Ok(new { message = "Expense updated successfully" });
        }
        [HttpPut("DeleteExpense")]
        public IActionResult DeleteExpense(int id)
        {
            _emp.DeleteExpenses(id);
            return Ok(new { message = "Expense deleted successfully..." });
        }

        [HttpGet("GetExpensebyUserIdandStatus/{userid}/{_status}")]
        public IActionResult GetExpensebyUserIdandStatus(int userid, string _status)
        {
            dynamic result = _emp.GetExpensebyUserIdandStatus(userid, _status);
            return Ok(result);

        }

        [HttpGet("SearchByExpenseIdandType/{expid}/{exptype}")]
        public IActionResult SearchByExpenseIdandType(int expid, string exptype)
        {
            dynamic result = _emp.SearchByExpenseIdandType(expid, exptype);
            return Ok(result);

        }

    }
}

