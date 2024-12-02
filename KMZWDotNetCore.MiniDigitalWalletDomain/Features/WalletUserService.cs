using KMZWDotNetCore.MiniDigitalWalletDatabase.Models;
using KMZWDotNetCore.MiniDigitalWalletDomain.Model;
using Microsoft.EntityFrameworkCore;

namespace KMZWDotNetCore.MiniDigitalWalletDomain.Features
{
    public class WalletUserService
    {
        private readonly AppDbContext _appDbContext;

        public WalletUserService()
        {
            _appDbContext = new AppDbContext();
        }

        public async Task<Result<WalletUserResponseModel>> DepositAsync(string mobileNo, int amount)
        {
            Result<WalletUserResponseModel> model = new Result<WalletUserResponseModel>();

            var result = await _appDbContext.TblUsers.AsNoTracking().FirstOrDefaultAsync(x => x.MobileNo == mobileNo);

            if (result is null)
            {
                model = Result<WalletUserResponseModel>.ValidationError("User doesn't exist!!");
                goto Results;
            }

            if (amount < 10000)
            {
                model = Result<WalletUserResponseModel>.ValidationError($"Deposit amount must not lessthan 10k.");
                goto Results;
            }

            result.Balance += amount;
            _appDbContext.Entry(result).State = EntityState.Modified;
            int isSuccess = _appDbContext.SaveChanges();

            if (isSuccess != 1)
            {
                model = Result<WalletUserResponseModel>.SystemError("System error occur!!");
            }

            WalletUserResponseModel walletUser = new WalletUserResponseModel()
            {
                WalletUser = result,
            };

            model = Result<WalletUserResponseModel>.Success(walletUser, "Success");

        Results:
            return model;
        }
    }
}
