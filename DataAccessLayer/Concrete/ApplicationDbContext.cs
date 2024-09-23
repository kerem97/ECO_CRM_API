﻿using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete
{
    public class ApplicationDbContext : DbContext
    {
        protected override void OnConfiguring
            (DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=HUAWEI\\SQLEXPRESS;database=EcoCRMDb;integrated security=true");
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerOperation> CustomerOperations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomerOperation>()
          .HasOne(co => co.User)
          .WithMany(u => u.CustomerOperations)
          .HasForeignKey(co => co.UserId)
          .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<CustomerOperation>()
                .HasOne(co => co.Customer)
                .WithMany(c => c.CustomerOperations)
                .HasForeignKey(co => co.CustomerId)
                .OnDelete(DeleteBehavior.NoAction); 


        }
    }
}