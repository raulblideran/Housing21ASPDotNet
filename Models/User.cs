using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Housing21ASPdotNet.Models;

public partial class User
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = null!;

    [Display(Name = "Date of Birth")]
    [Required]
    [DataType(DataType.Date)]
    public DateTime Dob { get; set; }


    [Display(Name = "Phone Number")]
    [Required]
    [DataType(DataType.PhoneNumber)]
    [RegularExpression(@"^[+]\d{5,15}", ErrorMessage = "You must include a country code and only digits.")]
    public string Phonenumber { get; set; } = null!;

    [Required]
    [EmailAddress(ErrorMessage = "Invalid Email Address")]
    public string Email { get; set; } = null!;
}
