using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DentalManagementSystem.Utils;

namespace DentalManagementSystem.Models
{
    public partial class Patient
    {
        public long Id { get; set; }

        [TwoWords]
        [StringLength(maximumLength: 250, ErrorMessage = "tên chỉ có tối đa 250 kí tự")]
        public string Name { get; set; } = null!;

        [ValidateBirthday]
        public DateTime Birthday { get; set; }

        public bool Gender { get; set; }

        [StringLength(maximumLength: 250, MinimumLength = 5, ErrorMessage = "Địa chỉ nên có ít nhất 5 kí tự và tối đa là 250 kí tự")]
        public string Address { get; set; } = null!;

        [RegularExpression(@"^\d{10,15}$", ErrorMessage = "Số điện thoại không hợp lệ")]
        public string Phone { get; set; } = null!;

        [StringLength(maximumLength: 250, MinimumLength = 10, ErrorMessage = "Độ dài email tổi thiểu là 10 kí tự và tối đa là 250 kí tự")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Email không hợp lệ")]
        public string Email { get; set; } = null!;

        [StringLength(maximumLength: 250, MinimumLength = 2, ErrorMessage = "bản ghi này phải có từ 2 kí tự đến tối đa 250 ki tự")]
        public string BodyPrehistory { get; set; } = null!;

        [StringLength(maximumLength: 250, MinimumLength = 2, ErrorMessage = "bản ghi này phải có từ 2 kí tự đến tối đa 250 ki tự")]
        public string TeethPrehistory { get; set; } = null!;

        public int Status { get; set; }

        public bool IsDeleted { get; set; }

        public virtual ICollection<PatientRecord> PatientRecords { get; } = new List<PatientRecord>();

        public virtual ICollection<Schedule> Schedules { get; } = new List<Schedule>();

        public virtual ICollection<Treatment> Treatments { get; } = new List<Treatment>();

        public void Trim()
        {
            Name = Name?.Trim();
            Address = Address?.Trim();
            BodyPrehistory = BodyPrehistory?.Trim();
            TeethPrehistory = TeethPrehistory?.Trim();
        }
    }
}
