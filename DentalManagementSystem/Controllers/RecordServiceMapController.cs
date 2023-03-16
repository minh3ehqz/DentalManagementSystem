using DentalManagementSystem.DAL;
using DentalManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace DentalManagementSystem.Controllers
{
    public class RecordServiceMapController: Controller
    {
        PatientRecordServiceMapDBContext DB = new PatientRecordServiceMapDBContext();
        public IActionResult Delete(long id,long recordid)
        {
            DB.Delete(id,recordid);
            return Redirect("/PatientsRecord/Details/" + recordid);
        }

        public IActionResult Create([Bind("Id,PatientRecordId,ServiceId,Status")] PatientRecordServiceMap p )
        {
            DB.Add(p);
            return Redirect("/PatientsRecord/Details/" + p.PatientRecordId);
        }
    }
}
