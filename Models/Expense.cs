using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Enterprise_Expense_Tracker.Models
{
    public class Expense
    {
        public int Id { get; set; }

        //[ForeignKey("User")]
        [ForeignKey("User_UserID")] //,Column(Order = 0)]
        public int UserId { get; set; }
        //public User? User1 { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "Amount Cannot be String")]
        public decimal Amount { get; set; }
        [DataType(DataType.Text, ErrorMessage = "Only Text allowed")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Type Cannot be Empty")]
        public string? Type { get; set; }
        [DataType(DataType.Text, ErrorMessage = "Only Text allowed")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Description Cannot be Empty")]
        public string? Description { get; set; }

        public string? Status { get; set; } = "pending";

        [DataType(DataType.DateTime, ErrorMessage = "Invalid DateTime Format")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Type SubmissionDate")]
        public DateTime SubmissionDate { get; set; }
        public DateTime? ApprovalDate { get; set; }

        [ForeignKey("User_ManagerID")]//, Column(Order = 1)]
        public int? ManagerId { get; set; }
        



        public virtual User User_UserID { get; set; }
        public virtual User User_ManagerID { get; set; }
    }
}
