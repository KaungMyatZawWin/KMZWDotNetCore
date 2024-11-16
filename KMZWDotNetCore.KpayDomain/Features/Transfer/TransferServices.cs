using KMZWDotNetCore.KpayDatabase.Models;
using KMZWDotNetCore.KpayDomain.Validations.Transfer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMZWDotNetCore.KpayDomain.Features.Tranlog
{
    public class TransferServices
    {
        public readonly AppDbContext _db;
        public readonly TransferValidation _transferValidation;

        public TransferServices()
        {
            _db = new AppDbContext();
            _transferValidation = new TransferValidation();
        }

        public ValidationResultModel Transfer(string fromMobileNo, string toMobileNo, string amount, string pinCode , string note)
        {
            TblTranLog tblTranLog = new TblTranLog();

            var result = _transferValidation.TranValidation(fromMobileNo,toMobileNo,amount,pinCode);

            if (result.IsSuccess is false)
            {
                return result;
            }

            result.FromUser.Balance -= Int32.Parse(amount);
            _db.Entry(result.FromUser).State = EntityState.Modified;
            _db.SaveChanges();

            result.ToUser.Balance += Int32.Parse(amount);
            _db.Entry(result.ToUser).State = EntityState.Modified;
            _db.SaveChanges();

            tblTranLog.FromMobileNo = fromMobileNo;
            tblTranLog.ToMobileNo = toMobileNo;
            tblTranLog.Amount = amount;
            tblTranLog.Note = note;
            tblTranLog.Time = DateTime.Now.ToShortDateString();


            _db.TblTranLogs.Add(tblTranLog);
            _db.SaveChanges();

            //result.Message = model == 1 ? "Successfully transfer." : "Transaction Failed!!";

            return result;
        }
    }

}



