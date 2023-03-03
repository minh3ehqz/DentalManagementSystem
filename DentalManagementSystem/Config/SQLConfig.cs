namespace DentalManagementSystem.Config
{
    public class SQLConfig
    {
        public static string ConnectionString = File.ReadAllText("ConnectString.txt");
    }
}
