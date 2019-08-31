using System.ComponentModel.DataAnnotations;

namespace Meyer.Common.ModelValidation.UnitTests.Mocks
{
    public class NestedModel
    {
        [Phone]
        public string Phone { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string MyProperty { get; set; }

        public Model Model { get; set; }
    }
}