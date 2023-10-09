using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace CodeWithMosh.Data
{
    public class ApplicationUser : IdentityUser
    {
        
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
    }
}
