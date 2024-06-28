using kt5.Models;
using kt5.Repositories;

namespace kt5.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Product> Products { get; }
        IRepository<Category> Categories { get; }
        Task<int> CompleteAsync();
    }
}
