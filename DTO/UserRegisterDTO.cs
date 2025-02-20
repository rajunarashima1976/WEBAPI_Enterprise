namespace Enterprise_Expense_Tracker.DTO
{
    public class UserRegisterDTO
    {
        public string UserName { get; set; }
        public string Password{ get; set; }
        public string Role { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
    }
}
