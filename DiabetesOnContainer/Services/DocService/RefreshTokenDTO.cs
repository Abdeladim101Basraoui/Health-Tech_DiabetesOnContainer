using System.Security.Claims;

namespace DiabetesOnContainer.Services.DocService;

public class RefreshTokenDTO:IRefreshToken
{
    public string Role { get; set; }
    public string Token { get; set; }
    public DateTime Created { get; set; } = DateTime.Now;

    public DateTime Expires{ get; set; }
    public IHttpContextAccessor _HttpContext { get; }

    public RefreshTokenDTO(IHttpContextAccessor httpContext)
    {
        _HttpContext = httpContext;
    }

    public DateTime GetExpires( )
    {
        string result = string.Empty;
        if (_HttpContext.HttpContext is not null)
        {
            result = _HttpContext.HttpContext.User.FindFirstValue(ClaimTypes.Expired);
        }
        return Convert.ToDateTime(result);
    }

    public string GetRole()
    {
        var result = string.Empty;
        if (_HttpContext.HttpContext is not null)
        {
            result = _HttpContext.HttpContext.User.FindFirstValue(ClaimTypes.Role);
        }
        return result;
    }

    public string GetToken()
    {
        throw new NotImplementedException();
    }
}
