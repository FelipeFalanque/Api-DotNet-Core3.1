using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Api.Data.Context
{
    public class ContextFactory : IDesignTimeDbContextFactory<MyContext>
    {
        public MyContext CreateDbContext(string[] args)
        {
            //Usado para Criar as Migrações
            //string dbConnection = Environment.GetEnvironmentVariable("DB_CONNECTION").ToLower();
            
            string dbConnection = "Server=localhost;Port=3306;DataBase=dbAPI;Uid=root;Pwd=root";
            var optionsBuilder = new DbContextOptionsBuilder<MyContext>();
            optionsBuilder.UseMySql(dbConnection);
            return new MyContext(optionsBuilder.Options);
        }
    }
}
