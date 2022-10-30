using System.Data.SqlClient;

namespace SEN381_API_GROUP3.Database
{
    public class Connection
    {
        public SqlConnection ConnectDatabase()
        {
            try
            {
                SqlConnection connection = new SqlConnection(@"Data Source = DESKTOP-IHTCUHK\SQLEXPRESS; Initial Catalog = UkupholisaHealthcare; Integrated Security = True");
                connection.Open();
                Console.WriteLine("Connected");
                return connection;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }

            
        }
    }
}
