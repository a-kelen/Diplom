using Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Interfaces
{
    public interface iJWTGenerator
    {
       public string CreateToken(User user);
       public string GenerateRefreshToken();
       public JwtSecurityToken GetPrincipalFromExpiredToken(string token);
    }
}
