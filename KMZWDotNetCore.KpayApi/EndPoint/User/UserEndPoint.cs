using KMZWDotNetCore.KpayDatabase.Models;
using KMZWDotNetCore.KpayDomain.Features.Tranlog;
using KMZWDotNetCore.KpayDomain.Features.User;

namespace KMZWDotNetCore.KpayApi.EndPoint.User
{
    public static class UserEndPoint
    {
        public static void MapUserEndPoint(this IEndpointRouteBuilder app)
        {
            app.MapGet("/user/balance/{mobileNo}", (string mobileNo) =>
            {
                UserServices userService = new UserServices();
                decimal result = userService.GetBalance(mobileNo);

                return Results.Ok(result);
            })
            .WithName("GetUserBalance")
            .WithOpenApi();

            app.MapGet("/user/{mobileNo}", (string mobileNo) =>
            {
                UserServices userServices = new UserServices();
                var model = userServices.GetUserInfo(mobileNo);
                if (model is null)
                {
                    return Results.BadRequest("No user found!!");
                }

                return Results.Ok(model);
            })
            .WithName("GetUserInfo")
            .WithOpenApi();

            app.MapPut("/user/depoists/{mobileNo}", (string mobileNo, string amount) =>
            {
                UserServices userService = new UserServices();
                var model = userService.Deposit(mobileNo, amount);

                if (model.IsSuccess is false)
                {
                    return Results.BadRequest(model.Message);
                }

                return Results.Ok(model);
            })
            .WithName("BalanceDeposit")
            .WithOpenApi();

            app.MapPut("/user/withdraws/{mobileNo}", (string mobileNo, string amount) =>
            {
                UserServices userService = new UserServices();
                var model = userService.Withdraw(mobileNo, amount);

                if (model.IsSuccess is false)
                {
                    return Results.BadRequest(model.Message);
                }

                return Results.Ok(model);
            })
            .WithName("BalanceWithdraw")
            .WithOpenApi();

            app.MapPost("/transfer", (string fromMobileNo, string toMobileNo, string amount, string pinCode, string note) =>
            {
                TransferServices transferServices = new TransferServices();
                var model = transferServices.Transfer(fromMobileNo, toMobileNo, amount, pinCode, note);

                if (model.IsSuccess is false)
                {
                    return Results.BadRequest(model.Message);
                }

                return Results.Ok(model);
            })
            .WithName("Transfer")
            .WithOpenApi();
        }

    }
}
