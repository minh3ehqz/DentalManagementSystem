using DentalManagementSystem.DAL;
using Microsoft.AspNetCore.Mvc;

namespace DentalManagementSystem.Controllers
{
    public class HomeController1 : Controller
    {
        PatientRecordDBContext DB = new PatientRecordDBContext();
        public IActionResult Index()
        {
            var patientsRecordList = DB.ListAll();
            return View(patientsRecordList);
        }
    }
}
