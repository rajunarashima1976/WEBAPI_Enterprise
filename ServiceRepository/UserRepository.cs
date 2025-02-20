using AutoMapper;
using Enterprise_Expense_Tracker.DTO;
using Enterprise_Expense_Tracker.ModelContext;
using Enterprise_Expense_Tracker.Models;
using Microsoft.CodeAnalysis.Scripting;
using Enterprise_Expense_Tracker.Helper;

namespace Enterprise_Expense_Tracker.ServiceRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        //private IJwtUtils _jwtUtils;
        private readonly IConfiguration _configuration;
        private readonly ILogger<UserRepository> _logger;   
        public UserRepository(AppDbContext context, IMapper mapper, IConfiguration configuration, 
            ILogger<UserRepository> logger)
        {
            _context = context;
            _mapper = mapper;
            //_jwtUtils = jwtUtils;
            _configuration = configuration;
            _logger = logger;
        }

        public User Authenticate(UserLoginDTO model)
        {
            var user= _context.Users.FirstOrDefault(e => e.UserName == model.UserName && e.Password == model.Password);

            if (user==null)
                throw new Exception("Invalid Username/Password!!!");
            return user;
            //new change 

        }
        public void Register(UserRegisterDTO model)
        {
            // validate
           
            if (_context.Users.Any(x => x.UserName == model.UserName))
                
                throw new Exception("Username is already taken");
            if (_context.Users.Any(x => x.Email == model.Email))
                
                throw new Exception("Email id is already taken");
            // map model to new user object
            var user = _mapper.Map<User>(model);
            
            _context.Users.Add(user);
            _context.SaveChanges();
           
        }

        public void Update_User(int id, UpdateUserDTO model)
        {
            var user = getUser(id);

            // validate
            if (model.UserName != user.UserName && _context.Users.Any(x => x.UserName == model.UserName))
                throw new AppException("Username is already taken");

            if (model.Email != user.Email && _context.Users.Any(x => x.Email == model.Email))
                throw new AppException("Email is already taken");


            // copy model to user and save
            _mapper.Map(model, user);
            _context.Users.Update(user);
            _context.SaveChanges();
        }


        public User GetById(int id)
        {
            return getUser(id);
        }



        private User getUser(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null) throw new KeyNotFoundException("User not found");
            return user;
        }

        public int getUserIdbyname(string userName)
        {
            return _context.Users.FirstOrDefault(e => e.UserName == userName).Id;
        }
        public bool UserExists(string username, string? email)
        {
            return _context.Users.Any(x => x.UserName == username && x.Email == email);
        }
    }
}
