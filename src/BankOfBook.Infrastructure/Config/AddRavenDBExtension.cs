using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace BankOfBook.Infrastructure.Config;
public static class AddRavenDBExtension
{
    public static IHostBuilder AddRavenDB(this IHostBuilder builder, string connection, string database)
     => builder.ConfigureServices((context, services)
        => Config.RavenDB(
                services,
                connection,
                database
            ));
}
