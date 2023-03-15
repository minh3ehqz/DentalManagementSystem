using DentalManagementSystem.DAL;
using DentalManagementSystem.Models;
using DentalManagementSystem.Utils;
using Microsoft.AspNetCore.Mvc;

namespace DentalManagementSystem.Controllers
{
    public class PatientRecordController : AuthController
    {
        SystemLogDBContext Log = new SystemLogDBContext();
        PatientRecordDBContext DB = new PatientRecordDBContext();
        public IActionResult Delete(long[] selectedValues)
        {
            if (!isAuth("/PatientsRecord/Delete", out User user))
            {
                return NotFound();
            }
            ViewData["FullName"] = user.FullName;
            ViewData["Role"] = RoleHelper.GetRoleNameById(user.RoleId);
            ViewData["Email"] = user.Email;
            TempData["Delete messenger"] = "xóa thành công";
            foreach (long id in selectedValues)
            {
                Log.Add(new SystemLog { CreatedDate = DateTime.Now, OwnerId = user.Id, Content = "người dùng đã xóa bệnh nhân " + DB.Get(id).Patient.Name + " có sđt là " + DB.Get(id).Patient.Phone + " và email là " + DB.Get(id).Patient.Email + "" });
                DB.Delete(id);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Reason,Diagnostic,Causal,Date,TreatmentName,MarrowRecord,Debit,Note,TreatmentId,UserId,Prescription")] PatientRecord patientRecord, int PatientId, string PatientName, string PatientPhone, string PatientEmail)
        {
            if (isAuth("/PatientsRecord/Create", out User user))
            {
                TempData["addsuccess"] = "thêm mới thành công";
                //patient.Trim();
                patientRecord.Date= DateTime.Now;
                patientRecord.PatientId = PatientId;
                patientRecord.UserId= user.Id;
                DB.Add(patientRecord);
                Log.Add(new SystemLog
                {
                    CreatedDate = DateTime.Now,
                    OwnerId = user.Id,
                    Content = "người dùng đã thêm mới bệnh án " +
                    PatientName + " có sô điện thoại là " + PatientPhone + " và email là " + PatientEmail
                });
                return RedirectToAction(nameof(PatientsController.Details));
            }
            else return NotFound();

        }
        public IActionResult Index(long id)
        {
            if (!isAuth("/PatientsRecord/Index", out User user))
            {
                return NotFound();
            }
            ViewData["FullName"] = user.FullName;
            ViewData["Role"] = RoleHelper.GetRoleNameById(user.RoleId);
            ViewData["Email"] = user.Email;
            var patientRecord = DB.Get(id);
            return View(patientRecord);
        }
    }
}
