using AutoMapper;
using FilmsAPI.GeneralDto.Auth;
using Microsoft.AspNetCore.Identity;
using NetTopologySuite.Geometries;

namespace FilmsAPI.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles(GeometryFactory geometryFactory)
        {
            CreateMap<IdentityUser, AuthDto>();
        }
    }
}