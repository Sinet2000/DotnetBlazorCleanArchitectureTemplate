using AutoMapper;
using FPAAgentura.Application.Requests.Identity;
using FPAAgentura.Application.Responses.Identity;

namespace Client.Infrastructure.Mappings
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<PermissionResponse, PermissionRequest>().ReverseMap();
            CreateMap<RoleClaimResponse, RoleClaimRequest>().ReverseMap();
        }
    }
}