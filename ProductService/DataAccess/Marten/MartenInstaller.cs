using Marten;
using Marten.Services;
using Newtonsoft.Json;
using ProductService.Domain;

namespace ProductService.DataAccess.Marten;

public static class MartenInstaller
{
    public static IServiceCollection AddMartenConfiguration(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("PgConnection");
        if (string.IsNullOrWhiteSpace(connectionString))
            throw new InvalidOperationException("ConnectionStrings:PgConnection is required for ProductService.");

        var serializer = new JsonNetSerializer();
        serializer.Configure(settings =>
        {
            settings.ContractResolver = new ProtectedSettersContractResolver();
            settings.PreserveReferencesHandling = PreserveReferencesHandling.Objects;
        });

        var store = DocumentStore.For(options =>
        {
            options.Connection(connectionString);
            options.DatabaseSchemaName = "product_service";
            options.Serializer(serializer);
            options.Schema.For<Product>().Duplicate(product => product.Code);
        });

        services.AddSingleton<IDocumentStore>(store);
        services.AddScoped(sp => sp.GetRequiredService<IDocumentStore>().LightweightSession());
        services.AddScoped<IProductRepository, ProductRepository>();
        return services;
    }
}
