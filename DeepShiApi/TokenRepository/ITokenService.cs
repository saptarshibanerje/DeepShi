using DeepShiEntityModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DeepShiApi.TokenRepository
{
    public interface ITokenService
    {
        string BuildToken(string key, string issuer, ApplicationUser applicationUser, List<Claim> claims);
        bool IsTokenValid(string key, string issuer, string token);
    }
}
