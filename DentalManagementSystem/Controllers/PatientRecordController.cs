using DentalManagementSystem.DAL;
using Microsoft.AspNetCore.Mvc;

namespace DentalManagementSystem.Controllers
{
    public class PatientRecordController : AuthController
    {
        SystemLogDBContext Log = new SystemLogDBContext();
        PatientRecordDBContext DB = new PatientRecordDBContext();
        public IActionResult Index()
        {
            return View();
        }
    }
}
