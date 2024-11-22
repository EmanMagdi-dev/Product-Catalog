using System.ComponentModel.DataAnnotations;


namespace Product_Catalog.DTOs
{

    public class UserVM
    {

        public required string UserName { get; set; }
        [DataType(DataType.Password)]
        public required string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare("Password")]
        public required string confirmPassword { get; set; }
        [DataType(DataType.EmailAddress)]
        public required string Email { get; set; }
    }
}


