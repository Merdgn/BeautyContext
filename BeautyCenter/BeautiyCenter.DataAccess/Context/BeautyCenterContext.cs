using BeautiyCenter.Entity.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace BeautiyCenter.DataAccess.Context
{
    public class BeautyCenterContext : DbContext
    {
        public DbSet<Salon> Salons { get; set; } // Salonlar tablosu
        public DbSet<Employee> Employees { get; set; } // Çalışanlar tablosu
        public DbSet<Appointment> Appointments { get; set; } // Randevular tablosu
        public DbSet<Service> Services { get; set; } // Hizmetler tablosu
        public DbSet<Category> Categories { get; set; } // Kategoriler tablosu
        public DbSet<About> Abouts { get; set; } // Hakkımızda tablosu

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // PostgreSQL bağlantı dizesi
            optionsBuilder.UseNpgsql("Host=localhost;Database=BeautyCenterDb;Username=postgres;Password=1100");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Salon ve Employee ilişkisi
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Salon)
                .WithMany()
                .HasForeignKey(e => e.SalonId);

            // Employee ve Appointment ilişkisi
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Employee)
                .WithMany()
                .HasForeignKey(a => a.EmployeeId);

            // Category ve Service ilişkisi
            modelBuilder.Entity<Service>()
                .HasOne(s => s.Category)
                .WithMany(c => c.Services)
                .HasForeignKey(s => s.CategoryId);
        }
    }
}
