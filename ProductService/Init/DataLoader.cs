using Marten;
using ProductService.Domain;

namespace ProductService.Init;

public class DataLoader
{
    private readonly IDocumentStore documentStore;

    public DataLoader(IDocumentStore documentStore)
    {
        this.documentStore = documentStore;
    }

    public void Seed()
    {
        using var session = documentStore.LightweightSession();
        if (session.Query<Product>().Any()) return;

        session.Store(
            DemoProductFactory.Travel(),
            DemoProductFactory.House(),
            DemoProductFactory.Farm(),
            DemoProductFactory.Car());
        session.SaveChangesAsync().GetAwaiter().GetResult();
    }
}
