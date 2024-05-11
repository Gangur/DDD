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
                @$"([{nameof(Product.Id)}], 
                        [{nameof(Product.Name)}], 
                        [{nameof(Product.Brand)}], 
                        [{nameof(Product.PictureName)}], 
                        [{nameof(Product.Price)}_{nameof(Product.Price.Currency)}],
                        [{nameof(Product.Price)}_{nameof(Product.Price.Amount)}],  
                        [{nameof(Sku)}], [{nameof(Product.Category)}]) VALUES" +
                            "(NEWID(), 'Phone 1', 'Samsung', 'phone.png', 'EUR', 320000, '4225-7276-32342', 1)," +
                            "(NEWID(), 'Phone 2', 'Xiaomi', 'phone.png', 'USD', 100000, '4225-7276-32342', 1)," +
                            "(NEWID(), 'Phone 3', 'Apple', 'phone.png', 'USD', 200000, '4225-7276-32342', 1)," +
                            "(NEWID(), 'Book 1', 'Mystery','book.png', 'EUR', 1050, '5131-2252-36336', 3)," +
                            "(NEWID(), 'Book 2', 'Umbrella','book.png', 'EUR', 1200, '5131-2252-36336', 3)," +
                            "(NEWID(), 'Book 3', 'Mystery','book.png', 'USD', 1000, '5131-2252-36336', 3)," +
                            "(NEWID(), 'Tablet 1', 'Xiaomi','tablet.png', 'EUR', 40000, '1431-7622-38653', 2)," +
                            "(NEWID(), 'Tablet 2', 'Samsung','tablet.png', 'EUR', 73000, '1431-7622-38653', 2)," +
                            "(NEWID(), 'Tablet 3', 'Apple','tablet.png', 'EUR', 112000, '1431-7622-38653', 2);",
            customers = 
                $"INSERT INTO [{nameof(Customer)}] " +
                $"([{nameof(Customer.Id)}], [{nameof(Customer.Email)}], [{nameof(Customer.Name)}])" +
                "VALUES (NEWID(), 'RobertSmith@gamil.com', 'Robert Smith')," +
                       "(NEWID(), 'MaryWilliams@hotmail.com', 'Mary Williams')," +
                       "(NEWID(), 'JohnBrown@hotmail.com', 'John Brown');",
            orders = 
                $"INSERT INTO [{nameof(Order)}] " +
                $"([{nameof(Order.Id)}]," +
                $"[{nameof(Order.CustomerId)}], " +
                $"[{nameof(Order.Paid)}]) " +
                $"SELECT [Id] = NEWID(), " +
                $"[{nameof(Order.CustomerId)}] = c.Id, " +
                $"[{nameof(Order.Paid)}] = GETDATE()" +
                $"FROM [{nameof(Product)}] p " +
                    $"CROSS JOIN [{nameof(Customer)}] c;",
            lineItems = 
                $"INSERT INTO [{nameof(LineItem)}] " +
                $"([{nameof(LineItem.Id)}]," +
                $"[{nameof(LineItem.OrderId)}]," +
                $"[{nameof(LineItem.ProductId)}]," +
                $"[{nameof(LineItem.Price)}_{nameof(LineItem.Price.Currency)}]," +
                $"[{nameof(LineItem.Price)}_{nameof(LineItem.Price.Amount)}]," +
                $"[{nameof(LineItem.Quantity)}])" +
                $"SELECT  [Id] = NEWID(), " +
                $"[{nameof(LineItem.OrderId)}] = o.Id, " +
                $"[{nameof(LineItem.ProductId)}] = p.Id, " +
                $"[{nameof(LineItem.Price)}_{nameof(LineItem.Price.Currency)}] = p.Price_Currency, " +
                $"[{nameof(LineItem.Price)}_{nameof(LineItem.Price.Amount)}] = p.Price_Amount, " +
                $"[{nameof(LineItem.Quantity)}] = 1" +
                $"FROM [{nameof(Order)}] o " +
                    $"CROSS JOIN [{nameof(Product)}] p;";

            dbContext.Database.ExecuteSqlRaw(products);
            dbContext.Database.ExecuteSqlRaw(customers);
            dbContext.Database.ExecuteSqlRaw(orders);
            dbContext.Database.ExecuteSqlRaw(lineItems);
        }
    }
}
