using DentalManagementSystem.DAL;
using DentalManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;

namespace DentalManagementSystem.Utils
{
    public class HomeHelper
    {
        public static int GetTodayRevenue()
        {
            DentalSystemDbContext DentalSystemDBContext = new DentalSystemDbContext();
            var Data = DentalSystemDBContext.PatientRecords
                .Include(x => x.PatientRecordServiceMaps).ThenInclude(x => x.Service)
                .Where(x => x.Date >= DateTime.Now.Date);
            return Data.SelectMany(x => x.PatientRecordServiceMaps).Select(x => x.Service).Sum(x => x.Price);
        }

        public static int GetPatientCame()
        {
            scheduleDBContext DB = new scheduleDBContext();
           return DB.ListAll().Where(x => x.Date >= DateTime.Now.Date && x.Date < DateTime.Now.Date.AddDays(1)).Count(x => x.Status == 1);
        }
    }
}
