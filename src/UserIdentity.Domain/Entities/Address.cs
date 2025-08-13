using System.ComponentModel.DataAnnotations.Schema;

namespace UserIdentity.Domain.Entities
{
    public class Address
    {
        [Column("id")]
        public Guid Id { get; set; }
        
        [Column("user_id")]
        public Guid UserId { get; set; }
        
        [Column("street")]
        public string? Street { get; set; }
        
        [Column("city")]
        public string? City { get; set; }
        
        [Column("country")]
        public string? Country { get; set; }
        
        [Column("postal_code")]
        public string? PostalCode { get; set; }
        
        // Navigation property
        public User User { get; set; } = null!;
    }
}
