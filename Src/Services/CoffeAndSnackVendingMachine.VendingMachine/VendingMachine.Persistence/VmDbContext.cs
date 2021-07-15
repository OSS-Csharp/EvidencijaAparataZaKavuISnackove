using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using VendingMachine.Domain.Entity;
using VendingMachine.Domain.Interfaces;

namespace VendingMachine.Persistence.Repository
{
    public class VmDbContext : DbContext
    {
        public VmDbContext(DbContextOptions<VmDbContext> options) : base(options) { }
        public DbSet<VendingMachineEntity> VendingMachineEntities { get; set; }
        public DbSet<ItemEntity> ItemEntities { get; set; }
        public DbSet<VendingMachineItemEntity> VendingMachineItemEntities { get; set; }
        public DbSet<JobMachineWorkerEntity> JobMachineWorkerEntities {get;set;}
        public DbSet<WorkerEntity> WorkerEntities {get;set;}

        protected override void OnConfiguring(DbContextOptionsBuilder options){
            options.UseSqlite(@"Data Source = ..\\VendingMachine.Persistence\\VmSqlite.db",assembly => assembly.MigrationsAssembly("VendingMachine.API"));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(VmDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
