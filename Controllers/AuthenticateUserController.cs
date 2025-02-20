using AutoMapper;
using Enterprise_Expense_Tracker.DTO;
using Enterprise_Expense_Tracker.ServiceRepository;
using Microsoft.AspNetCore.Mvc;

namespace Enterprise_Expense_Tracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AuthenticateUserController : ControllerBase
    {
        //private readonly AppDbContext _context;
        private IUserRepository _user;
        //private readonly ILogger<Expense> _logger;
        private IMapper _mapper;
        private IConfiguration _config;
        private ITokenRepository _tokenRepository;

        //private readonly AppSettings _appSettings;

        public AuthenticateUserController(IUserRepository user, IMapper mapper, IConfiguration configuration, ITokenRepository tokenRepository)
        {
            _user = user;
            _mapper = mapper;
            _config = configuration;
            _tokenRepository = tokenRepository;
        }

        [HttpPost("Login_User")]
        public IActionResult Login_User(UserLoginDTO model)
        {
            var user = _user.Authenticate(model);
            if (user == null) return Unauthorized();

            var response = _mapper.Map<AuthenticateResponse>(user);
            response.Token = _tokenRepository.CreateJWTToken(user);
            return Ok(response);
            //var token = _tokenRepository.CreateJWTToken(user);
            //return Ok(token);
        }

        [HttpPost("Register_User")]
        public IActionResult Register_User(UserRegisterDTO model)
        {
            _user.Register(model);
            return Ok(new { message = "Registration successful" });
        }
        [HttpPut("Update_User/{id}")]
        public IActionResult Update_User(int id, UpdateUserDTO model)
        {
            _user.Update_User(id, model);
            return Ok(new { message = "User updated successfully" });
        }
    }
}
