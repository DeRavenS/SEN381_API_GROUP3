using System.Data.SqlClient;

namespace SEN381_API_GROUP3.Database
{
    public class Connection
    {
        public SqlConnection ConnectDatabase()
        {
            SqlConnection connection = new SqlConnection(@"Data Source = .; Initial Catalog = UkupholisaHealthcare; Integrated Security = True");

            return connection;
        }
    }
}
