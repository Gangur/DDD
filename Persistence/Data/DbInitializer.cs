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
                $"INSERT INTO [SalesDb].[dbo].[{nameof(Product)}] ([{nameof(Product.Id)}] ,[{nameof(Product.Name)}] ,[{nameof(Product.Price)}_{nameof(Product.Price.Currency)}] ,[{nameof(Product.Price)}_{nameof(Product.Price.Amount)}] ,[{nameof(Sku)}])" +
                "VALUES (NEWID(), 'phone', 'USD', 1000, '4225-7276-32342')," +
                       "(NEWID(), 'book', 'USD', 100, '5131-2252-36336')," +
                       "(NEWID(), 'tablet', 'EUR', 700, '1431-7622-38653');",
            customers = 
                $"INSERT INTO [SalesDb].[dbo].[{nameof(Customer)}] ([{nameof(Customer.Id)}], [{nameof(Customer.Email)}], [{nameof(Customer.Name)}])" +
                "VALUES (NEWID(), 'RobertSmith@gamil.com', 'Robert Smith')," +
                       "(NEWID(), 'MaryWilliams@hotmail.com', 'Mary Williams')," +
                       "(NEWID(), 'JohnBrown@hotmail.com', 'John Brown');",
            orders = 
                $"INSERT INTO [SalesDb].[dbo].[{nameof(Order)}] ([{nameof(Order.Id)}] ,[{nameof(Order.CustomerId)}]) " +
                "SELECT [Id] = NEWID(), [CustomerId] = c.ID " +
                $"FROM [SalesDb].[dbo].[{nameof(Product)}] p " +
                    $"CROSS JOIN [SalesDb].[dbo].[{nameof(Customer)}] c;",
            lineItems = 
                $"INSERT INTO [SalesDb].[dbo].[{nameof(LineItem)}] ([{nameof(LineItem.Id)}] ,[{nameof(OrderId)}] ,[{nameof(ProductId)}] ,,[{nameof(LineItem.Price)}_{nameof(LineItem.Price.Currency)}]) " +
                "SELECT  [Id] = NEWID(), [OrderId] = o.Id, [ProductId] = p.Id, [Price_Currency] = p.Price_Currency, [Price_Amount] = p.Price_Amount " +
                $"FROM [SalesDb].[dbo].[{nameof(Order)}] o " +
                    $"CROSS JOIN [SalesDb].[dbo].[{nameof(Product)}] p;";

            dbContext.Database.ExecuteSqlRaw(products);
            dbContext.Database.ExecuteSqlRaw(customers);
            dbContext.Database.ExecuteSqlRaw(orders);
            dbContext.Database.ExecuteSqlRaw(lineItems);

            dbContext.SaveChanges();
        }
    }
}
