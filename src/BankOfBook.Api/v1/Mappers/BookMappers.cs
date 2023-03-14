using AutoMapper;
using BankOfBook.Api.v1.Model;
using BankOfBook.Domain.Entities;

namespace BankOfBook.Api.Mappers;

public static class BookMappers
{
    static BookMappers()
    {
        Mapper = new MapperConfiguration(cfg => cfg.AddProfile<BookMapperProfile>()).CreateMapper();
    }

    internal static IMapper Mapper { get; }

    public static BookGetResponseModel? ToResponseModel(this Book entity)
    {
        return entity == null ? null : Mapper.Map<BookGetResponseModel>(entity);
    }
    public static List<BookGetResponseModel>? ToResponseModel(this List<Book> entity)
    {
        return entity == null ? null : Mapper.Map<List<BookGetResponseModel>>(entity);
    }
    public static Book? ToModel(this BookCreateResquestModel entity)
    {
        return entity == null ? null : Mapper.Map<Book>(entity);
    }
}
