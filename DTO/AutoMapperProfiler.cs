using AutoMapper;
using Enterprise_Expense_Tracker.Models;

namespace Enterprise_Expense_Tracker.DTO
{
    public class AutoMapperProfiler : Profile
    {
        public AutoMapperProfiler()
        {
            CreateMap<ExpenseCreateDTO, Expense>().ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId)).ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<Expense, ExpenseCreateDTO>().ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId));

            CreateMap<ExpenseUpdateDTO, Expense>().ForMember(dest => dest.UserId, opt => opt.Ignore()).ForMember(dest => dest.Id, opt => opt.Ignore()).ForMember(dest => dest.ManagerId, opt => opt.Ignore());
            //CreateMap<Expense, ExpenseUpdateDTO>().ForMember(src => src.Id, opt => opt.MapFrom(src => src.UserId));
            CreateMap<UserLoginDTO, User>().ForMember(dest => dest.Id, opt => opt.Ignore()).ForMember(dest => dest.Role, opt => opt.Ignore()).ForMember(dest => dest.FullName, opt => opt.Ignore()).ForMember(dest => dest.Email, opt => opt.Ignore());
            CreateMap<UserRegisterDTO, User>().ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName)).ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<User, UserRegisterDTO>().ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName));
            CreateMap<AuthenticateResponse, User>().ForMember(dest => dest.Id, opt => opt.Ignore()).ForMember(dest => dest.Password, opt => opt.Ignore()).ForMember(dest => dest.Role, opt => opt.Ignore()).ForMember(dest => dest.FullName, opt => opt.Ignore()).ForMember(dest => dest.Email, opt => opt.Ignore());
            CreateMap<User, AuthenticateResponse>().ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.UserName));
            CreateMap<UpdateUserDTO, User>().ForMember(dest => dest.Id, opt => opt.Ignore()).ForMember(dest => dest.Role, opt => opt.Ignore());
            CreateMap<User, UpdateUserDTO>().ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName));
            CreateMap<Expense_Data_All_Store, Expense>().ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<Expense, Expense_Data_All_Store>().ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId));
        }
    }
}
