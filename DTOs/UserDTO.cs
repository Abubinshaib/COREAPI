using System.ComponentModel.DataAnnotations;

namespace COREAPI.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Name must contain only alphabet letters.")]
        public string Name { get; set; }

        [Required]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be 10 digits.")]
        public string PhoneNumber { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"^(Male|Female)$", ErrorMessage = "Gender must be Male or Female.")]
        public string Gender { get; set; }
    }
}
