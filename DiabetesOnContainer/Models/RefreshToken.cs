using System;
using System.Collections.Generic;

namespace DiabetesOnContainer.Models
{
    public partial class RefreshToken
    {
        public Guid Id { get; set; }
        public string Role { get; set; } = null!;
        public string Token { get; set; } = null!;
        public DateTime Created { get; set; }
        public DateTime Expires { get; set; }
    }
}
