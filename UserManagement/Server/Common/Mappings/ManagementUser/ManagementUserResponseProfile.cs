using AutoMapper;
using UserManagement.Server.Models.DbModels;
using UserManagement.Shared.Contracts.ManagementUser.Responses;

namespace UserManagement.Server.Common.Mappings.ManagementUser
{
    public class ManagementUserProfile : Profile
    {
        public ManagementUserProfile()
        {
            CreateMap<User, UserResponse>()
                .ForMember(user => user.Id, opt =>
                    opt.MapFrom(src => src.Id))
                .ForMember(user => user.Username, opt =>
                    opt.MapFrom(src => src.UserName))
                .ForMember(user => user.Email, opt =>
                    opt.MapFrom(src => src.Email))
                .ForMember(user => user.LastLoginDate, opt =>
                    opt.MapFrom(src => src.LastLoginDate))
                .ForMember(user => user.RegistrationDate, opt =>
                    opt.MapFrom(src => src.RegistrationDate))
                .ForMember(user => user.StatusBlock, opt =>
                    opt.MapFrom(src => src.IsBlocked ? "Заблокирован" : "Незаблокирован"));
        }
    }
}
