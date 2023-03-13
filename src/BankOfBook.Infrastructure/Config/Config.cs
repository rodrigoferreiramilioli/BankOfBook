using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace BankOfBook.Infrastructure.Config;
internal static partial class Config
{
    public static void RavenDB(
        IServiceCollection services,
        string connectionString,
        string database
    )
    {
        var lazyDocumentStore = CreateDocument(connectionString, database);
        DatabaseExists(lazyDocumentStore.Value);
        CreateIndexes(lazyDocumentStore.Value);
        services.TryAddSingleton(_ => lazyDocumentStore.Value);
    }
}
