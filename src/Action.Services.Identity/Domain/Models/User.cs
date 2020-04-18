using System;
using Action.Common.Exceptions;
using Action.Services.Identity.Domain.Services;

namespace Action.Services.Identity.Domain.Models
{
    public class User
    {
        public Guid Id {get; protected set;}
        public string Email { get ; protected set; }
        public string Password { get ; protected set; }
        public string Name { get ; protected set; }
        public string Salt { get; protected set; }
        public DateTime CreatedAt { get; protected set; } 

        protected User()
        {
            
        }
        public User(string email, string name)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ActioException("bad_email","The email is incorrect");
            }
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ActioException("bad_name","The name is incorrect");
            }
            Id = Guid.NewGuid();
            Email = email.ToLowerInvariant();
            Name = name;
            CreatedAt = DateTime.Now;
        }


        public void SetPassword(string password, IEncrypter encrypter)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ActioException("BAD_PASSWORD","The password can´t be empty");
            }
            
            Salt = encrypter.GetSalt();
            Password = encrypter.GetHash(password, Salt);
        }

        public bool ValidatePassword(string password, IEncrypter encrypter)
            =>  Password.Equals(encrypter.GetHash(password, Salt));
        
    }
}