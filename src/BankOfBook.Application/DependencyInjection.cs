﻿using BankOfBook.Application.Services;
using BankOfBook.Domain.Interfaces;
using BankOfBook.Infrastructure.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace BankOfBook.Application
{
    public static class DependencyInjectionExtension
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services)
        {
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IBookService, BookService>();
            return services;
        }
    }
}
