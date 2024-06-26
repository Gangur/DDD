﻿using Application.Abstraction;
using Domain.Customers;
using Domain.Orders;
using Domain.OutboxMessage;
using Domain.Products;
using Domain.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Repositories;

namespace Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(
                (sp, options) =>
                {
                    options
                        .UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
                });

            services.AddScoped<IUnitOfWork>(x => x.GetRequiredService<ApplicationDbContext>());

            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IOutboxMessageRepository, OutboxMessageRepository>();

            return services;
        }
    }
}
