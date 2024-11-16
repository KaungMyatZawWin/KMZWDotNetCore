using KMZWDotNetCore.KpayDatabase.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMZWDotNetCore.KpayDomain.Validations.User
{
    public class UserValidation
    {
        private readonly AppDbContext _appDbContext;

        public UserValidation()
        {
            _appDbContext = new AppDbContext();
        }

        public ValidationResultModel DepositValidation(string mobileNo , string amount)
        {
            ValidationResultModel validationResult = new ValidationResultModel();

            var model = _appDbContext.TblUsers.AsNoTracking().FirstOrDefault(x => x.MobileNo == mobileNo);

            if (model is null)
            {
                validationResult.IsSuccess = false;
                validationResult.Message = "User doesn't exist!!";
                validationResult.User = null;
                return validationResult;
            }

            if(Int32.Parse(amount) <= 0 )
            {
                validationResult.IsSuccess = false;
                validationResult.Message = "Amount must be greater than zero!!";
                validationResult.User = null;
                return validationResult;
            }

            validationResult.IsSuccess = true;
            validationResult.Message = "Success";
            validationResult.User = model;
            return validationResult;

        }

        public ValidationResultModel WithdrawValidation(string mobileNo , string amount)
        {
            ValidationResultModel validationResult = new ValidationResultModel();

            var model = _appDbContext.TblUsers.AsNoTracking().FirstOrDefault(x => x.MobileNo == mobileNo);

            if (model is null)
            {
                validationResult.IsSuccess = false;
                validationResult.Message = "User doesn't exist!!";
                validationResult.User = null;
                return validationResult;
            }

            if(model.Balance < Int32.Parse(amount))
            {

                validationResult.IsSuccess = false;
                validationResult.Message = "Insufficient balance!!";
                validationResult.User = model;
                return validationResult;
            }

            if(model.Balance == 10000)
            {
                validationResult.IsSuccess = false;
                validationResult.Message = "Deposit balance can't be withdrawn!!";
                validationResult.User = model;
                return validationResult;
            }

            validationResult.IsSuccess = true;
            validationResult.Message = "Success";
            validationResult.User = model;
            return validationResult;

        }
    }


    public class ValidationResultModel
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }

        public TblUser User { get; set; }
    }
}
