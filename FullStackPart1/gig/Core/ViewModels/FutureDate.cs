using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace GigHub.Core.ViewModels
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