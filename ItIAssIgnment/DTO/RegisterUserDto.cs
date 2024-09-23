﻿using System.ComponentModel.DataAnnotations;

namespace ItIAssIgnment.DTO
{
    public class RegisterUserDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [Compare("Password")]   
        public string ConfirmPassword { get; set; }
    }
}
