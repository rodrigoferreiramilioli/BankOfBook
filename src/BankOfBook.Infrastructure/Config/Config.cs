using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Raven.Client.Documents;
using Raven.Client.Documents.Operations;
using Raven.Client.Exceptions;
using Raven.Client.Exceptions.Database;
using Raven.Client.ServerWide;
using Raven.Client.ServerWide.Operations;

namespace BankOfBook.Infrastructure.Config;
internal static partial class Config
{
    public static void RavenDB(
        IServiceCollection services,
        string connection,
        string database
    )
    {
        var documentStore = CreateDocument(connection, database);
        DatabaseExists(documentStore.Value);
        services.TryAddSingleton(_ => documentStore.Value);
    }
    private static Lazy<IDocumentStore> CreateDocument(string connection, string database) =>
    new(() =>
    {
        var connectionStore = new DocumentStore
        {
            Urls = new[] { connection },
            Database = database
        };
        _ = connectionStore.Initialize();
        return connectionStore;
    });
    private static void DatabaseExists(IDocumentStore store)
    {
        try
        {
            _ = store.Maintenance.ForDatabase(store.Database).Send(new GetStatisticsOperation());
        }
        catch (DatabaseDoesNotExistException)
        {
            try
            {
                _ = store.Maintenance.Server.Send(new CreateDatabaseOperation(new DatabaseRecord(store.Database)));
            }
            catch (ConcurrencyException msg) {
                Console.WriteLine(msg);
            }
        }
    }
}
