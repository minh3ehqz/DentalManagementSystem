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

        
        public IActionResult Details(long id)
        {
            if (!isAuth("/PatientsRecord/Details", out User user))
            {
                return NotFound();
            }
            ViewData["FullName"] = user.FullName;
            ViewData["Role"] = RoleHelper.GetRoleNameById(user.RoleId);
            ViewData["Email"] = user.Email;
            
            ViewData["listService"] = SDB.ListAll(id);
            var patientRecord = DB.Get(id);
            return View(patientRecord);
        }
    }
}
