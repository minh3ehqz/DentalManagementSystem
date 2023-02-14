namespace DentalManagementSystem.Utils
{
    public class ValidateBirthday
    {
        public ValidateBirthday() {  }
        //kiểm tra năm sinh hợp lệ
        public bool checkBirthday(DateTime birthday)
        {
            if (DateTime.Now < birthday) return false;
            else if (DateTime.Now.Year - birthday.Year >= 150) return false;
            return true;
        }
    }
}
