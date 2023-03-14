using DentalManagementSystem.DAL;
using DentalManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace DentalManagementSystem.Controllers
{
    public class TreatmentsController : AuthController
    {
        TreatmentsDBContext DB = new TreatmentsDBContext();
        SystemLogDBContext Log = new SystemLogDBContext();

        [HttpPost]
        public IActionResult Create([Bind("PatientId","Name")]Treatment treatment)
        {
            if (!isAuth("/Treatments/Create", out User user))
            {
                return NotFound();
            }
            DB.Add(treatment);
            Log.Add(new SystemLog
            {
                CreatedDate = DateTime.Now,
                OwnerId = user.Id,
                Content = "người dùng đã thêm pháp đồ điều trị cho bênh nhân có id là" +
                    "" + treatment.PatientId
            });
            return Redirect("/Patients/Details/" + treatment.PatientId);
        }
    }
}
