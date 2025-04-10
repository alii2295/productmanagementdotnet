﻿using gestionproduit.Models;
using Microsoft.EntityFrameworkCore;

namespace gestionproduit.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
    }
}
