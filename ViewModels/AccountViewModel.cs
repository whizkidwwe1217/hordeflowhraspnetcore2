namespace HordeFlow.HR.ViewModels
{
    public class AccountViewModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Email { get; set; }
        public string RecoveryEmail { get; set; }
        public string MobileNo { get; set; }
        public bool? Active { get; set; }
        public bool? IsSystemAdministrator { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public bool LockoutEnabled { get; set; }
    }
}