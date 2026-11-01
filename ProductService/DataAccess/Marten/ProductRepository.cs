using Marten;
using Marten.Linq;
using ProductService.Domain;

namespace ProductService.DataAccess.Marten;

public sealed class ProductRepository : IProductRepository
{
    private readonly IDocumentSession session;

    public ProductRepository(IDocumentSession session)
    {
        this.session = session;
    }

    public async Task<Product> Add(Product product)
    {
        session.Store(product);
        await session.SaveChangesAsync();
        return product;
    }

    public async Task<List<Product>> FindAllActive() => (await session.Query<Product>()
        .Where(product => product.Status == ProductStatus.Active)
        .ToListAsync()).ToList();

    public async Task<Product> FindOne(string productCode) => (await session.Query<Product>()
        .FirstOrDefaultAsync(product => product.Code.ToLower() == productCode.ToLower()))!;

    public async Task<Product> FindById(Guid id) => (await session.LoadAsync<Product>(id))!;

    public async Task Save(Product product)
    {
        session.Store(product);
        await session.SaveChangesAsync();
    }
}
