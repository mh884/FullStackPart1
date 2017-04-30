using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace GigHub.ViewModels
{
    public class FutureTime : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime _DateTime;
            var isValed = DateTime.TryParseExact(value?.ToString(), "HH:mm",
                CultureInfo.CurrentCulture, DateTimeStyles.None,
                out _DateTime);
            return (isValed);

        }
    }
}