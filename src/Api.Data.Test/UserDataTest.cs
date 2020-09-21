using System;
using System.Linq;
using System.Threading.Tasks;
using Api.Data.Context;
using Api.Data.Repository;
using Api.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using Api.Domain.Utils;
using Xunit;

namespace Api.Data.Test
{
    public class UserDataTest : BaseTest, IClassFixture<DataBaseTest>
    {
        private readonly ServiceProvider _serviceProvider;

        public UserDataTest(DataBaseTest dataBaseTest)
        {
            _serviceProvider = dataBaseTest.ServiceProvider;
        }

        [Fact(DisplayName = "DeveCadastrarUmUsuario")]
        [Trait("Cadastros", "User")]
        public async Task DeveCadastrarUmUsuario()
        {
            using (var context = _serviceProvider.GetService<MyContext>())
            {
                UserRepository _repositorio = new UserRepository(context);
                UserEntity _entity = new UserEntity
                {
                    Id = Guid.NewGuid(),
                    Name = "Novo",
                    Email = "novo@novo.com",
                    Role = Constantes.Papeis.Administrator,
                    CreateAt = DateTime.Now,
                    UpdateAt = DateTime.Now
                };

                var registroCriado = await _repositorio.InsertAsync(_entity);

                Assert.NotNull(registroCriado);
                Assert.Equal("Novo", registroCriado.Name);
            }
        }

        [Fact(DisplayName = "DeveCriarEditarRemoverUmUsuario")]
        [Trait("Cadastros", "User")]
        public async Task DeveCriarEditarRemoverUmUsuario()
        {
            using (var context = _serviceProvider.GetService<MyContext>())
            {
                UserRepository _repositorio = new UserRepository(context);
                UserEntity _entity = new UserEntity
                {
                    Id = Guid.NewGuid(),
                    Name = "Novo",
                    Email = "novo@novo.com",
                    Role = Constantes.Papeis.Administrator,
                    CreateAt = DateTime.Now,
                    UpdateAt = DateTime.Now
                };

                var registroCriado = await _repositorio.InsertAsync(_entity);
                
                Assert.NotNull(registroCriado);
                Assert.Equal("Novo", registroCriado.Name);
                var idRegistroCriado = registroCriado.Id;

                string nomeAlterado = "Name Alterado";
                registroCriado.Name = nomeAlterado;

                var registroAlterado = await _repositorio.UpdateAsync(registroCriado);

                Assert.NotNull(registroAlterado);
                Assert.Equal(nomeAlterado, registroAlterado.Name);

                var resultado = await _repositorio.DeleteAsync(idRegistroCriado);

                Assert.Equal(true, resultado);

                var registroExcluido = await _repositorio.SelectAsync(idRegistroCriado);
                Assert.Null(registroExcluido);
            }
        }
    }
}