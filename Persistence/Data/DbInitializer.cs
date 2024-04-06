using Domain.Customers;
using Domain.Data;
using Domain.Orders;
using Domain.Products;

namespace Persistence.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext dbContext)
        {
            if (dbContext.GetQuery<Product>().Any())
                return;

            var products = new Product[]
            {
                Product.Create("phone", Money.Create(Money.USD, 1000), Sku.Create("4225-7276-32342")),
                Product.Create("book", Money.Create(Money.USD, 100),   Sku.Create("5131-2252-36336")),
                Product.Create("tablet", Money.Create(Money.EUR, 700), Sku.Create("1431-7622-38653"))
            };

            var customers = new Customer[]
            {
                Customer.Create("RobertSmith@gamil.com", "Robert Smith"),
                Customer.Create("MaryWilliams@hotmail.com", "Mary Williams"),
                Customer.Create("JohnBrown@hotmail.com", "John Brown")
            };

            for (int i = 0; i < customers.Length; i++)
                customers[i].ClearDomainEvents();

            for (int i = 0; i < products.Length; i++)
            {
                products[i].ClearDomainEvents();

                for (int j = 0; j < customers.Length; j++)
                {
                    var order = Order.Create(customers[j]);
                    order.ClearDomainEvents();
                    dbContext.Add(order);
                }
            }

            dbContext.AddRange(products);
            dbContext.AddRange(customers);

            dbContext.SaveChanges();
        }
    }
}
