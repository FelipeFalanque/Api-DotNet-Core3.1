using System;
using Api.Domain.Entities;
using Api.Domain.Utils;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Seeds
{
    public static class UserSeeds
    {
        public static void Users(ModelBuilder modelBuilder)
        {
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
