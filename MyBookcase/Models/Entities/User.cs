﻿using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MyBookcase.Models.Entities
{
    public class User
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int typeUser { get; set; }
        public DateTime dataRegister { get; set; }

    }
}
