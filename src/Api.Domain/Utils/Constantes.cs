using System;

namespace Api.Domain.Utils
{
    public class Constantes
    {
        public class Papeis
        {
            private Papeis() { }

            public const string Administrator = "Administrator";
            public const string Client = "Client";
            public const string Manager = "Manager";
            public const string Employee = "Employee";
            public const string AdministratorOrClient = Administrator + "," + Client;
        }
    }
}