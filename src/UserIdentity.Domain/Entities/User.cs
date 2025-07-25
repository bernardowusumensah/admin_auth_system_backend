using System.ComponentModel.DataAnnotations.Schema;
using UserIdentity.Domain.ValueObjects;

namespace UserIdentity.Domain.Entities
{
    public class User
    {
        [Column("id")]
        public Guid Id { get; set; }
        [Column("first_name")]
        public string FirstName { get; set; } 
        [Column("last_name")]
        public string LastName { get; set; }

        public List<UserRole> UserRoles { get; set; } = new();
    }
}
