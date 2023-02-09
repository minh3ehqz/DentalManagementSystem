using DentalManagementSystem.DAL;
using DentalManagementSystem.Models;
using System.Net;
using System.Reflection;

namespace Test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            PatientDBContext DB = new PatientDBContext();
            Patient patient = new Patient()
            {
                Name = "Vippro3",
                Birthday = DateTime.Now,
                Gender = true,
                Address = "Ha noi",
                BodyPrehistory = "skljdfujsd",
                Email = "vip@pro.com",
                IsDeleted = false,
                Phone = "0987654321",
                Status = 0,
                TeethPrehistory = "Sau rang vc"
            };
            DB.Add(patient);
        }
    }
}