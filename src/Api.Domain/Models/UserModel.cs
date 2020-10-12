using System;
using Api.Domain.Utils;

namespace Api.Domain.Models
{
    public class UserModel : BaseModel
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string _email;
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        private string _role;
        public string Role
        {
            get { return _role == null ? Constantes.Papeis.Client : _role; }
            set
            {
                _role = value == null ? Constantes.Papeis.Client : value;
            }
        }
    }
}