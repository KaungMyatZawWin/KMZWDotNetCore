using KMZWDotNetCore.KpayDomain.Features.Tranlog;

namespace KMZWDotNetCore.KpayApi.EndPoint.Transfer
{
    public static class TransactionEndPoint
    {
        public static void MapTransactionEndPoint(this IEndpointRouteBuilder app)
        {
            app.MapGet("/transaction-history", () =>
            {
                TransferServices transferServices = new TransferServices();
                var model = transferServices.GetAllTransactionHistory();

                if (model is null)
                {
                    return Results.BadRequest("There is no transaction!!");
                }

                return Results.Ok(model);
            })
            .WithName("GetTransactionHistory")
            .WithOpenApi();
        }
    }
}
