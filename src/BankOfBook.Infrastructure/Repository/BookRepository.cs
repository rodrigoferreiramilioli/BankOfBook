using BankOfBook.Domain.Entities;
using Raven.Client.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankOfBook.Infrastructure.Repository
{
    public class BookRepository
    {
        private readonly IDocumentStore _documentStore;

        public BookRepository(IDocumentStore documentStore)
        {
            _documentStore = documentStore;
        }
        public async Task<List<Book>> GetAsync()
        {
            using var session = _documentStore.OpenAsyncSession();

            using (session.Advanced.DocumentStore.AggressivelyCacheFor(TimeSpan.FromHours(1)))
            {
                return await session.Query<Book>().OrderBy(q => q.Id).ToListAsync();
            }
        }
        public async Task<Book> GetByIdAsync(Guid id)
        {
            using var session = _documentStore.OpenAsyncSession();

            using (session.Advanced.DocumentStore.AggressivelyCacheFor(TimeSpan.FromHours(1)))
            {
                return await session.Query<Book>().FirstOrDefaultAsync(q => q.Id == id);
            }
        }
        public async Task CreateAsync(Book entity)
        {
            using var session = _documentStore.OpenAsyncSession();

            var book = await session.Query<Book>().FirstOrDefaultAsync(q => q.Id == entity.Id);

            if (book == null)
            {
                await session.StoreAsync(entity);
                session.Advanced.Revisions.ForceRevisionCreationFor(entity, Raven.Client.Documents.Session.ForceRevisionStrategy.Before);
                await session.SaveChangesAsync();
            }
        }
        public void Delete(Guid id)
        {
            using var session = _documentStore.OpenAsyncSession();

            session.Delete(id);
        }
        public async Task PutAsync(Book entity)
        {
            using var session = _documentStore.OpenAsyncSession();

            var book = await session.Query<Book>().FirstOrDefaultAsync(q => q.Id == entity.Id);

            if(book != null)
            {
                await session.StoreAsync(entity);
                session.Advanced.Revisions.ForceRevisionCreationFor(entity, Raven.Client.Documents.Session.ForceRevisionStrategy.Before);
                await session.SaveChangesAsync();
            }
        }
    }
}
