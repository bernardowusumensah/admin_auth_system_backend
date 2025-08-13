using System.ComponentModel.DataAnnotations.Schema;
using UserIdentity.Domain.ValueObjects;

namespace UserIdentity.Domain.Entities
{
    public class User
    {
        [Column("id")]
        public Guid Id { get; set; }
        
        [Column("first_name")]
        public string? FirstName { get; set; }
        
        [Column("last_name")]
        public string? LastName { get; set; }
        
        [Column("date_of_birth")]
        public DateTime? DateOfBirth { get; set; }
        
        [Column("display_name")]
        public string? DisplayName { get; set; }
        
        [Column("gender")]
        public Gender? Gender { get; set; }
        
        [Column("avatar")]
        public string? Avatar { get; set; }
        
        // Navigation properties
        public List<UserRole> UserRoles { get; set; } = new();
        public Address? Address { get; set; }
    }
    
    public enum Gender
    {
        Male = 0,
        Female = 1,
    }
}
