using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CheqStore.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PathPhoto { get; set; }
        public int? RolID { get; set; } //Tipo de rol, será relacionada en el futuro.
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }

    }
}