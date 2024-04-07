using Domain.Customers;
using Domain.Data;
using Domain.LineItems;
using Domain.Orders;
using Domain.Products;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext dbContext)
        {
            const string products =
                $"INSERT INTO [{nameof(Product)}] " +
                $"([{nameof(Product.Id)}] ,[{nameof(Product.Name)}] ,[{nameof(Product.Price)}_{nameof(Product.Price.Currency)}] ,[{nameof(Product.Price)}_{nameof(Product.Price.Amount)}] ,[{nameof(Sku)}])" +
                "VALUES (NEWID(), 'phone', 'USD', 1000, '4225-7276-32342')," +
                       "(NEWID(), 'book', 'USD', 100, '5131-2252-36336')," +
                       "(NEWID(), 'tablet', 'EUR', 700, '1431-7622-38653');",
            customers = 
                $"INSERT INTO [{nameof(Customer)}] " +
                $"([{nameof(Customer.Id)}], [{nameof(Customer.Email)}], [{nameof(Customer.Name)}])" +
                "VALUES (NEWID(), 'RobertSmith@gamil.com', 'Robert Smith')," +
                       "(NEWID(), 'MaryWilliams@hotmail.com', 'Mary Williams')," +
                       "(NEWID(), 'JohnBrown@hotmail.com', 'John Brown');",
            orders = 
                $"INSERT INTO [{nameof(Order)}] " +
                $"([{nameof(Order.Id)}] ,[{nameof(Order.CustomerId)}]) " +
                $"SELECT [Id] = NEWID(), [{nameof(Order.CustomerId)}] = c.Id " +
                $"FROM [{nameof(Product)}] p " +
                    $"CROSS JOIN [{nameof(Customer)}] c;",
            lineItems = 
                $"INSERT INTO [{nameof(LineItem)}] " +
                $"([{nameof(LineItem.Id)}] ,[{nameof(LineItem.OrderId)}] ,[{nameof(LineItem.ProductId)}] ,[{nameof(LineItem.Price)}_{nameof(LineItem.Price.Currency)}], [{nameof(LineItem.Price)}_{nameof(LineItem.Price.Amount)}]) " +
                $"SELECT  [Id] = NEWID(), [{nameof(LineItem.OrderId)}] = o.Id, [{nameof(LineItem.ProductId)}] = p.Id, [{nameof(LineItem.Price)}_{nameof(LineItem.Price.Currency)}] = p.Price_Currency, [{nameof(LineItem.Price)}_{nameof(LineItem.Price.Amount)}] = p.Price_Amount " +
                $"FROM [{nameof(Order)}] o " +
                    $"CROSS JOIN [{nameof(Product)}] p;";

            dbContext.Database.ExecuteSqlRaw(products);
            dbContext.Database.ExecuteSqlRaw(customers);
            dbContext.Database.ExecuteSqlRaw(orders);
            dbContext.Database.ExecuteSqlRaw(lineItems);
        }
    }
}
