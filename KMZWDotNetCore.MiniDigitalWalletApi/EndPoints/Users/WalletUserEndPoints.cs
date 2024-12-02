using KMZWDotNetCore.MiniDigitalWalletDatabase.Models;
using KMZWDotNetCore.MiniDigitalWalletDomain.Features;
using KMZWDotNetCore.MiniDigitalWalletDomain.Model;

namespace KMZWDotNetCore.MiniDigitalWalletApi.EndPoints.Users
{
    public static class WalletUserEndPoints
    {
        public static void UserEndPoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("/users", async (string mobileNo, int amount) =>
            {
                WalletUserService walletUserService = new WalletUserService();
                var model = await walletUserService.DepositAsync(mobileNo, amount);
                if (model.IsValidationError)
                {
                    return Results.BadRequest(model.Message);
                }

                if (model.IsSystemError)
                {
                    return Results.BadRequest(model.Message);
                }

                return Results.Ok(model);
            });
        }
    }
}
