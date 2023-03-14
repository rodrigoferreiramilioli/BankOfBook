using BankOfBook.Domain.Entities;
using BankOfBook.Domain.Extensions;

namespace BankOfBook.Domain.Interfaces
{
    public interface IBookRepository
    {
        Task<(List<Book>, int totalResults)> GetAsync(Pagination pagination);
        Task<Book> GetByIdAsync(string id);
        Task CreateAsync(Book entity);
        void Delete(string id);
        Task PutAsync(Book entity);
    }
}
