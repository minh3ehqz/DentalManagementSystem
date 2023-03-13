namespace DentalManagementSystem.Utils
{
    public class RoleHelper
    {
        public static string GetRoleNameById(long RoleId)
        {
            switch (RoleId)
            {
                case 1:
                    return "Giám đốc";
                case 2:
                    return "Quản lý";
                case 3:
                    return "Quản trị viên hệ thống";
                case 4:
                    return "Y tá";
                case 5:
                    return "Bác sĩ";
                case 6:
                    return "Tiếp tân";
                case 7:
                    return "Kế toán";
                default:
                    return "";
            }
        }
    }
}
