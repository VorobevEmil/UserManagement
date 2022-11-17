using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Shared.Models.Account
{
    public class ApiClaim
    {
        public ApiClaim(string type, string value)
        {
            Type = type;
            Value = value;
        }

        public string Type { get; set; } = default!;
        public string Value { get; set; } = default!;
    }
}
