using BankOfBook.Domain.Entities;
using BankOfBook.Domain.Page;

namespace BankOfBook.Domain.Interfaces
{
    public interface IBookRepository
    {
        Task<(List<Book>, int totalResults)> GetAsync(Pagination pagination);
        Task<Book> GetByIdAsync(Guid id);
        Task CreateAsync(Book entity);
        void Delete(Guid id);
        Task PutAsync(Book entity);
    }
}
