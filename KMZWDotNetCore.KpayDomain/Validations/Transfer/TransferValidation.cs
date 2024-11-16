using KMZWDotNetCore.KpayDatabase.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMZWDotNetCore.KpayDomain.Validations.Transfer
{
    public class TransferValidation
    {
        private readonly AppDbContext _db;
        private readonly ValidationResultModel _resultModel;

        public TransferValidation()
        {
            _db = new AppDbContext();
            _resultModel = new ValidationResultModel();
        }

        public ValidationResultModel TranValidation(string fromMobileNo, string toMobileNo, string amount, string pinCode)
        {
            var model1 = _db.TblUsers.AsNoTracking().FirstOrDefault(x => x.MobileNo == fromMobileNo);
            if (model1 is null)
            {
                _resultModel.IsSuccess = false;
                _resultModel.Message = "From account doesn't exist!!";
                _resultModel.FromUser = null;
                _resultModel.ToUser = null;
                return _resultModel;
            }

            var model2 = _db.TblUsers.AsNoTracking().FirstOrDefault(x => x.MobileNo == toMobileNo);
            if (model2 is null)
            {
                _resultModel.IsSuccess = false;
                _resultModel.Message = "To account doesn't exist!!";
                _resultModel.FromUser = null;
                _resultModel.ToUser = null;
                return _resultModel;
            }

            if (toMobileNo == fromMobileNo)
            {
                _resultModel.IsSuccess = false;
                _resultModel.Message = "You can't transfer your own account!!";
                _resultModel.FromUser = null;
                _resultModel.ToUser = null;
                return _resultModel;
            }

            if (model1.Balance < Int32.Parse(amount))
            {
                _resultModel.IsSuccess = false;
                _resultModel.Message = "Insufficient balance!!";
                _resultModel.FromUser = null;
                _resultModel.ToUser = null;
                return _resultModel;
            }

            if (model1.PinCode != pinCode)
            {
                _resultModel.IsSuccess = false;
                _resultModel.Message = "Incorrect Passcode!!";
                _resultModel.FromUser = null;
                _resultModel.ToUser = null;
                return _resultModel;
            }

            _resultModel.IsSuccess = true;
            _resultModel.Message = "Success";
            _resultModel.FromUser = model1;
            _resultModel.ToUser = model2;
            return _resultModel;

        }
    }


    public class ValidationResultModel
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }

        public TblUser FromUser { get; set; }

        public TblUser ToUser { get; set; }
    }
}
