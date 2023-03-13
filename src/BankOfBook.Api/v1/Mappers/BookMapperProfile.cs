using AutoMapper;
using BankOfBook.Api.v1.Model;
using BankOfBook.Domain.Entities;

namespace ABCBrasil.VCF.Tofi.Purchase.UseCases.Mapping;

public class BookMapperProfile : Profile
{
    public BookMapperProfile()
    {
        CreateMap<Book, BookGetResponseModel>(MemberList.Destination)
            .ConstructUsing(src => new BookGetResponseModel());
    }
}
