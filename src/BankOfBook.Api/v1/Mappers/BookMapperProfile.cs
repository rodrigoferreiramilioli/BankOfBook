using AutoMapper;
using BankOfBook.Api.v1.Model;
using BankOfBook.Domain.Entities;

namespace BankOfBook.Api.Mappers;

public class BookMapperProfile : Profile
{
    public BookMapperProfile()
    {
        CreateMap<Book, BookGetResponseModel>(MemberList.Destination)
            .ConstructUsing(src => new BookGetResponseModel());

        CreateMap<BookCreateResquestModel, Book>(MemberList.Destination)
            .ConstructUsing(src => new Book());
    }
}
