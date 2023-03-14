using AutoMapper;
using BankOfBook.Api.v1.Model.Page;
using BankOfBook.Domain.Extensions;

namespace BankOfBook.Api.Mappers;

public class PaginationMapperProfile : Profile
{
    public PaginationMapperProfile()
    {
        CreateMap<PaginationRequestModel, Pagination>(MemberList.Destination)
            .ConstructUsing(src => new Pagination());
    }
}
