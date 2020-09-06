criando a solução do projeto
dentro do diretorio \src
dotnet new sln --name Api

criando um projeto
dentro do diretorio \src
"no-https" usamos devido ao desenvolvimento, depois ativamos o https
dotnet new webapi -n application -o Api.Application --no-https

adicionando o projeto na solução
dentro do diretorio \src
dotnet sln add Api.Application

intalando Entity Framework Core
dentro da pasta \src\Api.Data
dotnet tool install --global dotnet-ef

adicionando referencia do projeto Api.Domain no projeto Api.Data
dentro da pasta \src\
dotnet add .\Api.Data reference .\Api.Domain

criando uma migração
dentro da pasta \src\Api.Data
dotnet ef migrations add UserMigration

aplicando migrações
dentro da pasta \src\Api.Data
dotnet ef database update

comandos para restaurar e construir o projeto
dotnet restore
dotnet build