using System.ComponentModel.DataAnnotations;

namespace Enterprise_Expense_Tracker.Models
{
    public class User
    {
        
        public int Id { get; set; }
        [DataType(DataType.Text, ErrorMessage = "Only Text allowed")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Type Your UserName")]
        public string? UserName { get; set; }
        [DataType(DataType.Password, ErrorMessage = "Invalid Password Format")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Type Your Password")]
        public string? Password { get; set; }
        [DataType(DataType.Text, ErrorMessage = "Only Text allowed")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Type Your Role")]
        public string? Role { get; set; }
        [DataType(DataType.Text, ErrorMessage = "Only Text allowed")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Type Your FullName")]
        public string? FullName { get; set; }
        [DataType(DataType.EmailAddress, ErrorMessage = "Invalid Email Format")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Type Your EmailAddress")]
        public string? Email { get; set; }
    }
}
