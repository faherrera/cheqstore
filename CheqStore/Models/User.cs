using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CheqStore.Models
{
    public enum Rol
    {
        Admin,Cliente,SuperAdmin
    }

    public class User
    {
        [Key]
        public int UserID { get; set; }
        public string Name { get; set; }

        [Required]
        [MaxLength(12)]
        [MinLength(3)]
        public string Username { get; set; }
        
        public string Email { get; set; }

        public string Password { get; set; }

        [Required]
        public Rol Rol { get; set; } = Rol.Cliente;

        public bool StatusLogic { get; set; } = true;

        public ICollection<Order> Orders { get; set; }

    }
}