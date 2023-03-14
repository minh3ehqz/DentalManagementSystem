using DentalManagementSystem.DAL;
using DentalManagementSystem.Models;
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Reason,Diagnostic,Causal,Date,TreatmentName,MarrowRecord,Debit,Note,TreatmentId,UserId,PatientId,Prescription")] PatientRecord patientRecord)
        {
            if (isAuth("/PatientsRecord/Create", out User user))
            {
                TempData["addsuccess"] = "thêm mới thành công";
                //patient.Trim();
                DB.Add(patientRecord);
                Log.Add(new SystemLog
                {
                    CreatedDate = DateTime.Now,
                    OwnerId = user.Id,
                    Content = "người dùng đã thêm mới bệnh nhân " +
                    patientRecord.Patient.Name + " có sô điện thoại là " + patientRecord.Patient.Phone + " và email là " + patientRecord.Patient.Email
                });
                return RedirectToAction(nameof(Index));
            }
            else return NotFound();

        }
    }
}
