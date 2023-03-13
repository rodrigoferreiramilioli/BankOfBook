using Raven.Client.Documents;
using Raven.Client.Documents.Operations;
using Raven.Client.Exceptions;
using Raven.Client.Exceptions.Database;
using Raven.Client.ServerWide;
using Raven.Client.ServerWide.Operations;

namespace BankOfBook.Infrastructure.Config;
internal static partial class Config
{
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
            catch (ConcurrencyException) { }
        }
    }
}
