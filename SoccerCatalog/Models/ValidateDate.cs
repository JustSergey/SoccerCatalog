using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SoccerCatalog.Models
{
    public class ValidateDate : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            long from = new DateTime(1900, 1, 1).Ticks;
            long to = DateTime.Now.Date.AddYears(-12).Ticks;
            if (value is DateTime date)
            {
                if (date.Ticks >= from && date.Ticks <= to)
                    return true;
            }
            return false;
        }
    }
}
