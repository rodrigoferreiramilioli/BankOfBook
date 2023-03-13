using AutoMapper;
using BankOfBook.Api.v1.Model.Page;
using BankOfBook.Domain.Page;

namespace ABCBrasil.VCF.Tofi.Purchase.UseCases.Mapping;

public class PaginationMapperProfile : Profile
{
    public PaginationMapperProfile()
    {
        CreateMap<PaginationRequestModel, Pagination>(MemberList.Destination)
            .ConstructUsing(src => new Pagination());
    }
}
