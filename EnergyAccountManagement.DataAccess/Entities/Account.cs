using System.ComponentModel.DataAnnotations;

namespace EnergyAccountManagement.DataAccess.Entities
{
    public class Account
    {
        public int Id { get; set; }

        [Required]
        public int AccountId { get; set; }

        [Required, MaxLength(50)]
        public string FirstName { get; set; }

        [Required, MaxLength(50)]
        public string LastName { get; set; }
    }
}
