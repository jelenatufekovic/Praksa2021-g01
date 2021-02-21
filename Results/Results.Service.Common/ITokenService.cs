using Results.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Results.Service.Common
{
    public interface ITokenService
    {
        Task<string> GenerateTokenAsync(IUser user, bool rememberMe);
        Task<ClaimsPrincipal> GetPrincipalAsync(string token);
    }
}
