﻿using DentalManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace DentalManagementSystem.DAL
{
    public class PatientDBContext : DBContext<Patient>
    {
        public override void Add(Patient entity)
        {
            Patients.Add(entity);
            SaveChanges();
        }
        
        public override void Delete(long Id)
        {
            Patients.Remove(Patients.FirstOrDefault(x => x.Id == Id));
            SaveChanges();
        }

        public override Patient Get(long id)
        {
            return Patients
                .Include(x => x.PatientRecords)
                .FirstOrDefault(x => x.Id == id);
        }

        public override List<Patient> ListAll()
        {
            return Patients
                .Include(x => x.PatientRecords)
                .Where(x => !x.IsDeleted).ToList();
        }

        public override List<Patient> ListAll(long OwnerId)
        {
            throw new NotImplementedException();
        }

        public override void Update(Patient entity)
        {
            throw new NotImplementedException();
        }
    }
}
