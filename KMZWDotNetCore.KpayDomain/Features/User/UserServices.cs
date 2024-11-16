using KMZWDotNetCore.KpayDatabase.Models;
using KMZWDotNetCore.KpayDomain.Validations.User;
using Microsoft.EntityFrameworkCore;

namespace KMZWDotNetCore.KpayDomain.Features.User
{
    public class UserServices
    {
        private readonly AppDbContext _db;
        private readonly UserValidation _userValidation;

        public UserServices()
        {
            _db = new AppDbContext();
            _userValidation = new UserValidation();
        }

        public decimal GetBalance(string mobileNo)
        {
            var model = _db.TblUsers.AsNoTracking().FirstOrDefault(x => x.MobileNo == mobileNo);

            return model.Balance;
        }

        public TblUser GetUserInfo(string mobileNo)

        {
            var model = _db.TblUsers.AsNoTracking().FirstOrDefault(x => x.MobileNo == mobileNo);

            return model;
        }

        public ValidationResultModel Deposit(string mobileNo, string amount)
        {

            var result = _userValidation.DepositValidation(mobileNo, amount);

            if (result.IsSuccess is false)
            {
                return result;
            }

            result.User.Balance += Int32.Parse(amount);

            _db.TblUsers.Add(result.User);
            _db.Entry(result.User).State = EntityState.Modified;
            _db.SaveChanges();

            return result;

        }

        public ValidationResultModel Withdraw(string mobileNo, string amount)
        {
            var result = _userValidation.WithdrawValidation(mobileNo, amount);

            if (result.IsSuccess is false)
            {
                return result;
            }

            result.User.Balance -= Int32.Parse(amount);

            _db.TblUsers.Add(result.User);
            _db.Entry(result.User).State = EntityState.Modified;
            _db.SaveChanges();

            return result;
        }


    }
}
