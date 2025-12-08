using MindLog.Application.Common.Models;
using MindLog.Domain.Identity;
using System.Security.Claims;

namespace MindLog.Application.Common.Abstractions.Services;

public interface ITokenService
{
    TokenPair GenerateTokenPair(ApplicationUser user, IEnumerable<string> roles, IEnumerable<Claim>? additionalClaims = null);
    string ComputeHash(string value);
}
