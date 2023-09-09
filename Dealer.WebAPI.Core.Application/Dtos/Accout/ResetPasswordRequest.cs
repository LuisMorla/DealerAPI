namespace Dealer.WebAPI.Core.Application.Dtos.Accout {
    public class ResetPasswordRequest {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
