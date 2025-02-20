using AutoMapper;
using Enterprise_Expense_Tracker.ModelContext;
using Enterprise_Expense_Tracker.Models;
using Enterprise_Expense_Tracker.ServiceRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Enterprise_Expense_Tracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = UserRoles.manager)]
    public class Manager_Employee_ExpenseController : ControllerBase
    {
        private readonly AppDbContext _context;
        private IManager_Expenses _mgr;
        //private readonly ILogger<Expense> _logger;
        private IMapper _mapper;
        private IConfiguration _config;
        private IUserRepository _user;
        public Manager_Employee_ExpenseController(AppDbContext context, IManager_Expenses mgr, IMapper mapper, IConfiguration configuration, IUserRepository user)
        {
            _context = context;
            _mgr = mgr;
            _mapper = mapper;
            _user = user;
            _config = configuration;
            _user = user;
        }
        //[HttpGet("GetAllExpensebyfilter")]
        //[HttpGet("GetAllExpensebyfilter/{userId}")]
        [HttpGet("GetAllExpensebyfilter/{userId}/{status}")]
        public IActionResult GetAllExpensebyfilter(int userId = -1, string status = "status")
        {
            var userName = User.Identity.Name;

            var result = _mgr.GetAllExpensebyfilter(userName, userId, status);
            return Ok(result);
        }
        [HttpGet("ExpenseApprovalProcess/{ExpenseId:int}/{status}")]
        public IActionResult ExpenseApprovalProcess(int ExpenseId, string status)
        {
            var result=_mgr.ExpenseApprovalProcess(ExpenseId, status);
            return Ok(result);
        }
        [HttpGet("getallexpense")]
        public IActionResult GetAllExpense()
        {
            var Username = User.Identity.Name;
            //var result = _emp.GetAllExpense(Username);
            var result = _mgr.GetAllExpenses();
            return Ok(result);
        }
        [HttpPut("New_Udpate_Employee_Expenses/{expenseid}/{_status}")]
        public IActionResult New_Udpate_Employee_Expenses(int expenseid, string _status)
        {
            _mgr.Update_Employee_Expense_Status(expenseid, _status);
            return Ok(new { message = "Employee Expenses status is updated successfully" });
        }


    }
}
