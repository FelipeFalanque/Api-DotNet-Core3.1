using System;
using Api.Data.Mapping;
using Api.Domain.Entities;
using Api.Domain.Utils;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Context
{
    public class MyContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }

        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {

        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserEntity>(new UserMap().Configure);
            modelBuilder.Entity<UserEntity>().HasData(
                new UserEntity
                {
                    Id = Guid.NewGuid(),
                    Name = "Administrador",
                    Email = "adm@adm.com",
                    Role = Constantes.Papeis.Administrator,
                    CreateAt = DateTime.Now,
                    UpdateAt = DateTime.Now
                },
                new UserEntity
                {
                    Id = Guid.NewGuid(),
                    Name = "Cliente",
                    Email = "user@example.com",
                    Role = Constantes.Papeis.Client,
                    CreateAt = DateTime.Now,
                    UpdateAt = DateTime.Now
                }
            );
        }

    }
}
