﻿using CustomerService.Entities;
using Microsoft.EntityFrameworkCore;

namespace CustomerService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options ) : base(options)
        {
        }

        public DbSet<Discount> Discounts { get; set; }
    }
}
