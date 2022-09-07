using AutoMapper;
using PaperStop.Application.Requests.Identity;
using PaperStop.Application.Responses.Identity;

namespace PaperStop.Client.Infrastructure.Mappings;

public class RoleProfile : Profile
{
    public RoleProfile()
    {
        CreateMap<PermissionResponse, PermissionRequest>().ReverseMap();
        CreateMap<RoleClaimResponse, RoleClaimRequest>().ReverseMap();
    }
}