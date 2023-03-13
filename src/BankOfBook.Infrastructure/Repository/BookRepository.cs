using BankOfBook.Domain.Entities;
using BankOfBook.Domain.Interfaces;
using BankOfBook.Domain.Page;
using Raven.Client.Documents;
using Raven.Client.Documents.Session;

namespace BankOfBook.Infrastructure.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly IDocumentStore _documentStore;

        public BookRepository(IDocumentStore documentStore)
        {
            _documentStore = documentStore;
        }
        public async Task<(List<Book>, int totalResults)> GetAsync(Pagination pagination)
        {
            using var session = _documentStore.OpenAsyncSession();

            using (session.Advanced.DocumentStore.AggressivelyCacheFor(TimeSpan.FromHours(1)))
            {
                return (await session.Query<Book>()
                    .Statistics(out QueryStatistics stats)
                    .Skip(pagination.PageNumber * pagination.PageSize)
                    .Take(pagination.PageSize)
                    .OrderBy(q => q.Id)
                    .ToListAsync(), stats.TotalResults);
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
                entity.Audit = new Audit
                {
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    Revision = 1,
                    UserName = "QuandoTiverAuth",
                    KeyAutorization = Guid.NewGuid()
                };

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

            if(entity != null)
            {
                entity.Audit.Revision++;
                entity.Audit.UpdatedAt = DateTime.Now;

                await session.StoreAsync(entity);
                session.Advanced.Revisions.ForceRevisionCreationFor(entity, Raven.Client.Documents.Session.ForceRevisionStrategy.Before);
                await session.SaveChangesAsync();
            }
        }
    }
}
