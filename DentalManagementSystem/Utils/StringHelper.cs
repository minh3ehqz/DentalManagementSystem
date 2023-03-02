namespace DentalManagementSystem.Utils
{
    public class StringHelper
    {
        public static string GenerateToken()
        {
            string data = "1234567890-qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM";
            string Token = "";
            Random rd = new Random();
            for (int i = 0; i < 100; i++)
            {
                Thread.Sleep(20);
                Token += data[rd.Next(0, data.Length)];
            }
            return Token;
        }
    }
}
