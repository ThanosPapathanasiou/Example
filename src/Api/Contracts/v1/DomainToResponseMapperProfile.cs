using AutoMapper;
using Example.Api.Contracts.v1.Responses;
using Example.Api.Domain;

namespace Example.Api.Contracts.v1
{
    public class DomainToResponseMapperProfile : Profile
    {
        public DomainToResponseMapperProfile()
        {
            CreateMap<Post, PostResponse>();
        }
    }
}
