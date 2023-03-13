using AutoMapper;
using BankOfBook.Api.v1.Model.Page;
using BankOfBook.Domain.Page;

namespace ABCBrasil.VCF.Tofi.Purchase.UseCases.Mapping;

public static class PaginationMappers
{
    static PaginationMappers()
    {
        Mapper = new MapperConfiguration(cfg => cfg.AddProfile<PaginationMapperProfile>()).CreateMapper();
    }

    internal static IMapper Mapper { get; }

    public static Pagination? ToModel(this PaginationRequestModel entity)
    {
        return entity == null ? null : Mapper.Map<Pagination>(entity);
    }
}
