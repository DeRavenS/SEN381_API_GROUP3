using Microsoft.AspNetCore.Mvc.RazorPages;
using SEN381_API_GROUP3.Database;
using System.Data.SqlClient;

namespace SEN381_API_GROUP3.Services
{
    public class MedicalServiceProviderService
    {
        public static MedicalServiceProvider getProviderByID(int id)
        {
            List<MedicalServiceProvider> provider = new List<MedicalServiceProvider>();
            Connection con = new Connection();
            SqlConnection scon = con.ConnectDatabase();
            SqlCommand command = new SqlCommand("SELECT * FROM [dbo].[Client] where ClientID = " + id, scon);
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    provider.Add(new MedicalServiceProvider(reader.GetInt32(0).ToString(), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4)));
                }
            }
            if (provider.Count() == 0)
            {
                throw new HttpRequestException();
            }
            return provider[0];
        }

        public static List<MedicalServiceProvider> getProviderList(int page, int size)
        {
            int offset = (page - 1) * size;
            List<MedicalServiceProvider> modules = new List<MedicalServiceProvider>();
            Connection con = new Connection();
            SqlConnection scon = con.ConnectDatabase();

            SqlCommand command = new SqlCommand("SELECT * FROM [dbo].[MedicalServiceProvidor] ORDER BY MedicalServiceProvidorID OFFSET @offset ROWS FETCH NEXT @size ROWS ONLY;", scon);
            command.Parameters.AddWithValue("@offset", offset);
            command.Parameters.AddWithValue("@size", size);
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    modules.Add(new MedicalServiceProvider(reader.GetInt32(0).ToString(), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4)));
                }
            }


            return modules;
        }
    }
}
