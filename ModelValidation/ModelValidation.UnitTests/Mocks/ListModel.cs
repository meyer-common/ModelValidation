using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Meyer.Common.ModelValidation.UnitTests.Mocks
{
    public class ListModel
    {
        [Phone]
        public string Phone { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string MyProperty { get; set; }

        public List<Model> Model { get; set; }
    }
}