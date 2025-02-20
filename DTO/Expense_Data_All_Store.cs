namespace Enterprise_Expense_Tracker.DTO
{
    public class Expense_Data_All_Store
    {
        public int UserId { get; set; }
        public decimal Amount { get; set; }
        public string? Type { get; set; }
        public string? Description { get; set; }
        public string? Status { get; set; }
        public DateTime SubmissionDate { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public int? ManagerId { get; set; }
    }
}
