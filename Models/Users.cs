﻿using System.ComponentModel.DataAnnotations;

namespace proeduedge.Models
{
    public class Users
    {
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        public string AvatarUrl { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Role { get; set; }
    }
}
