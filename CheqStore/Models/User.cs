using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CheqStore.Models
{
    public enum Rol
    {
        Admin,Cliente
    }

    public class User
    {
        [Key]
        public int UserID { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreatedAt { get; set; }

        public Rol Rol { get; set; } = Rol.Cliente;
       
        public ICollection<Order> Orders { get; set; }

    }
}