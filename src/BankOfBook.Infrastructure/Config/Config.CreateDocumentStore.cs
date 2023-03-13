using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Raven.Client.Documents;
using Raven.Client.Json.Serialization.NewtonsoftJson;

namespace BankOfBook.Infrastructure.Config;
internal static partial class Config
{
    private static Lazy<IDocumentStore> CreateDocument(string connectionString, string database)
        => new(() =>
        {
            var store = new DocumentStore
            {
                Urls = new[] { connectionString },
                Database = database,
                Conventions =
                {
                    Serialization = new NewtonsoftJsonSerializationConventions
                    {
                        CustomizeJsonDeserializer = serializer => {
                            serializer.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                            serializer.NullValueHandling = NullValueHandling.Ignore;
                            serializer.MissingMemberHandling = MissingMemberHandling.Ignore;
                            serializer.TypeNameHandling = TypeNameHandling.Auto;
                            serializer.Converters.Add(new StringEnumConverter());
                        }
                    }
                }
            };
            _ = store.Initialize();
            return store;
        });
}
