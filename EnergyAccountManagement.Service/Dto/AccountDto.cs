using EnergyAccountManagement.Service.Validators;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace EnergyAccountManagement.Service.Dto
{
    public class AccountDto : IValidatableObject
    {
        [Required]
        public int AccountId { get; set; }

        [Required, MaxLength(50)]
        public string FirstName { get; set; }

        [Required, MaxLength(50)]
        public string LastName { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validator = new AccountValidator();

            var result = validator.Validate(this);

            return result.Errors
                .Select(
                    error => new ValidationResult(error.ErrorMessage, new[] { error.PropertyName })
                );
        }
    }
}
