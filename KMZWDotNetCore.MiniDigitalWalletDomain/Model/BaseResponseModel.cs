﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMZWDotNetCore.MiniDigitalWalletDomain.Model
{
    public class BaseResponseModel
    {
        public string RespCode { get; set; }
        public string RespDesp { get; set; }

        public EnumRespType RespType { get; set; }

        public bool IsSuccess { get; set; }

        public bool IsError { get { return !IsSuccess; } }

        public static BaseResponseModel Success(string respCode, string respDesc)
        {
            return new BaseResponseModel()
            {
                IsSuccess = true,
                RespCode = respCode,
                RespDesp = respDesc,
                RespType = EnumRespType.Success

            };
        }

        public static BaseResponseModel ValidationError(string respCode, string respDesc)
        {
            return new BaseResponseModel()
            {
                IsSuccess = false,
                RespCode = respCode,
                RespDesp = respDesc,
                RespType = EnumRespType.ValidationError
            };
        }

        public static BaseResponseModel SystemError(string respCode, string respDesc)
        {
            return new BaseResponseModel()
            {
                IsSuccess = false,
                RespCode = respCode,
                RespDesp = respDesc,
                RespType = EnumRespType.SystemError
            };
        }
    }

    public enum EnumRespType
    {
        None,
        ValidationError,
        SystemError,
        Success

    }
}
