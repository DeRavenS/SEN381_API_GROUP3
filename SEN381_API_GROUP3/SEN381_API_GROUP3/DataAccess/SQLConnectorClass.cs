using System.Data.SqlClient;

namespace SEN381_API_GROUP3.DataAccess
{
    public class SQLConnectorClass
    {
        private static void CreateCommand(string queryString,string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(
                       connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
