using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LagetonixLibraryAssessment.Data.Enums
{
   public enum ResponseCode
    {
        [Description("Success")]
        Ok = 00,
        [Description("Validation Error")]
        ValidationError = 01,
        [Description("Not Found")]
        NotFound = 02,
        [Description("Bad Request")]
        ProcessingError = 03,
        [Description("Unauthorized Access")]
        AuthorizationError = 04,
        [Description("Insufficient Fund")]
        InsufficientFund = 05,
        [Description("Duplicate Error")]
        DuplicateError = 06,
        [Description("Pending")]
        Pending = 07,
        [Description("Exception Occurred")]
        Exception = 08,
        [Description("Internal Server Error")]
        InternalServer = 09,
        [Description("OTP Validation")]
        OtpValidation = 10,
        [Description("Invalid Request")]
        INVALID_REQUEST = 11,
        [Description("Already Exist")]
        Already_Exist = 11
    }
}
