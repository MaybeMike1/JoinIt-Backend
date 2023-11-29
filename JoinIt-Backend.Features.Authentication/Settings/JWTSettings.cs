using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoinIt_Backend.Features.Authentication.Settings
{
    public class JWTSettings
    {
        public const string JWTSettingsSection = "Features:Authentication:JWT";
        public string Secret { get; set; } = string.Empty;

    }
}
