using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;
using Microsoft.SqlServer.Server;

namespace GigHub.ViewModels
{
    public class FutureDate : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime _DateTime;
            var isValed = DateTime.TryParseExact(value?.ToString(), "dd MMM yyyy",
                CultureInfo.CurrentCulture, DateTimeStyles.None,
                 out _DateTime);
            return (isValed && _DateTime > DateTime.Now);

        }
    }
}