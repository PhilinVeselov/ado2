using kt5.Models;

namespace kt5.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Categories.Any())
            {
                return;   // DB has been seeded
            }

            var categories = new Category[]
            {
                new Category{Name="Electronics"},
                new Category{Name="Groceries"}
            };

            foreach (Category c in categories)
            {
                context.Categories.Add(c);
            }

            context.SaveChanges();

            var products = new Product[]
            {
                new Product{Name="Laptop", Price=800, CategoryId=1},
                new Product{Name="Phone", Price=600, CategoryId=1},
                new Product{Name="Apple", Price=1, CategoryId=2}
            };

            foreach (Product p in products)
            {
                context.Products.Add(p);
            }

            context.SaveChanges();
        }
    }
}
