using Dealer.WebAPI.Core.Application.Dtos.Base;

namespace Dealer.WebAPI.Core.Application.Dtos.Accout {
    public class AuthenticationResponse : BaseEntity {
        public string Id { get; set; }
        public string UserName { get; set; }
        public List<string> Roles { get; set; }
        public string Email { get; set; }
        public bool IsVerified { get; set; }

    }
}
