using DentalManagementSystem.DAL;
using DentalManagementSystem.Models;
using DentalManagementSystem.Utils;
using Microsoft.AspNetCore.Mvc;

namespace DentalManagementSystem.Controllers
{
    public class PatientsRecordController : AuthController
    {
        SystemLogDBContext Log = new SystemLogDBContext();
        PatientRecordDBContext DB = new PatientRecordDBContext();
        PatientRecordServiceMapDBContext SDB = new PatientRecordServiceMapDBContext();    
        ServiceDBContext ServiceDB = new ServiceDBContext();

        


        public IActionResult Details(long id)
        {
            if (!isAuth("/PatientsRecord/Details", out User user))
            {
                return NotFound();
            }
            ViewData["FullName"] = user.FullName;
            ViewData["Role"] = RoleHelper.GetRoleNameById(user.RoleId);
            ViewData["Email"] = user.Email;
            var s= SDB.ListAll(id);
            
            var patientRecord = DB.Get(id);
            var list = SDB.ListAll(id);
            List<Service> tablecheck = new List<Service>();
            foreach(var item in list)
            {
                tablecheck.Add(ServiceDB.Get(item.ServiceId));
            }
            ViewData["Allservice"] = ServiceDB.ListAll();
            ViewData["listService"] = tablecheck;
            return View(patientRecord);
        }


        [HttpPost]
        public IActionResult Edit(long id, [Bind("Id,Reason,Diagnostic,Causal,Date,TreatmentName, PatientId,MarrowRecord,Debit,Note,TreatmentId,UserId,Prescription")] PatientRecord patientRecord, int PatientId, string PatientName, string PatientPhone, string PatientEmail)
        {
            if (!isAuth("/PatientsRecord/Edit", out User user))
            {
                return NotFound();
            }
            patientRecord.PatientId = PatientId;
            ViewData["FullName"] = user.FullName;
            ViewData["Role"] = RoleHelper.GetRoleNameById(user.RoleId);
            ViewData["Email"] = user.Email;
            Log.Add(new SystemLog { CreatedDate = DateTime.Now, OwnerId = user.Id, Content = "người dùng đã thay đổi thông tin bệnh án số" + id +" của bệnh nhân có mã số " + PatientId});
            patientRecord.Trim();
            DB.Update(patientRecord);
            TempData["editsuccess"] = "edit thành công";
            return RedirectToAction("Details", new { id = patientRecord.Id });
        }

    }
}
