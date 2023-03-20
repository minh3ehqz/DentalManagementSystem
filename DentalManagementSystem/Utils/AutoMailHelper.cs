using DentalManagementSystem.DAL;
using DentalManagementSystem.Models;

namespace DentalManagementSystem.Utils
{
    public class AutoMailHelper
    {
        public static void ScanSchedules()
        {
            scheduleDBContext scheduleDB = new scheduleDBContext();
            List<Schedule> SentList = new List<Schedule>();
            var CurrentDateTime = DateTime.Now.Date;
            new Thread(() =>
            {
                while (true)
                {
                    var Schedules = scheduleDB.ListAll().Where(x => x.Date.AddDays(1) >= DateTime.Now && x.Status == 0).ToList();
                    foreach (var schedule in Schedules)
                    {
                        if (SentList.Any(x => x.Id == schedule.Id))
                        {
                            continue;
                        }
                        try
                        {
                            EmailHelper.Send(schedule.Patient.Email, "THÔNG BÁO LỊCH KHÁM TẠI PHÒNG KHÁM NHA KHOA ABC", "Xin kín chào anh/chị, phòng khám của em email để nhắc nhở anh chị đi khám, anh chị có lịch hẹn vào lúc " + schedule.Date.ToString("dd/MM/yyyy HH:mm"));
                            SentList.Add(schedule);
                            Thread.Sleep(1000);
                        }
                        catch (Exception)
                        {

                        }
                    }
                    Thread.Sleep(3000);
                    if (CurrentDateTime < DateTime.Now.Date)
                    {
                        SentList.Clear();
                    }
                }
            }).Start();
        }
    }
}
