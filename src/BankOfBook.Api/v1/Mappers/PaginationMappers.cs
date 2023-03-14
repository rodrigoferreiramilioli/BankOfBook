using AutoMapper;
using BankOfBook.Api.v1.Model.Page;
using BankOfBook.Domain.Extensions;

namespace BankOfBook.Api.Mappers;

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
