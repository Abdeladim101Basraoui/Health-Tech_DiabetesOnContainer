namespace DiabetesOnContainer.Services.DocService;

public interface IRefreshToken
{

    string GetRole();

    DateTime GetExpires();

    string GetToken();
}
