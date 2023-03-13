using Raven.Client.Documents;
using Raven.Client.Documents.Indexes;
using System.Reflection;

namespace BankOfBook.Infrastructure.Config;
internal static partial class Config
{
    private static void CreateIndexes(IDocumentStore store)
    {
        var assemblies = Assembly.GetEntryAssembly()!
               .GetReferencedAssemblies()
               .Where(_ => (_.Name?.StartsWith("BankOfBook") ?? false) && !(_.Name?.StartsWith("BankOfBook.Api") ?? false))
               .Select(_ => Assembly.Load(_))
               .ToArray();
        foreach (var assembly in assemblies)
            IndexCreation.CreateIndexes(assembly, store);
    }
}
