using BankOfBook.Domain.Entities;
using BankOfBook.Domain.Enums;
using BankOfBook.Domain.Extensions;
using BankOfBook.Domain.Interfaces;

namespace BankOfBook.Application.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }
        public async Task CreateAsync(Book book)
        {
            CanValidatorEntity(book);
            book.StatusLocalization = Status.Available;
            await _bookRepository.CreateAsync(book);
            
        }
        public void CanValidatorEntity(Book book)
        {
            var errors = new List<Error>();
            if (book.PriceSale <= 0)
            {
                errors.Add(new Error("409", "Preço de venda zerado"));
            }
            if (book.PriceRent <= 0)
            {
                errors.Add(new Error("408", "Preço de aluguel zerado"));
            }
            if (errors.Count > 0)
            {
                throw new BusinessException("BusinessException", errors);
            }
        }
    }
}
