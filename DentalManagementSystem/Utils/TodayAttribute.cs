using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
namespace DentalManagementSystem.Utils
{
    public class TodayAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime date = (DateTime)value;
            return date.Date == DateTime.Now.Date;
        }
    }
}
