using Microsoft.AspNetCore.Mvc.RazorPages;
using SEN381_API_GROUP3.Database;
using System.Data;
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
            SqlCommand command = new SqlCommand("SELECT * FROM [dbo].[MedicalServiceProvider] where MedicalServiceProviderID = " + id, scon);
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

            SqlCommand command = new SqlCommand("SELECT * FROM [dbo].[MedicalServiceProvider] ORDER BY MedicalServiceProviderID OFFSET @offset ROWS FETCH NEXT @size ROWS ONLY;", scon);
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
        public MedicalServiceProvider addMedicalServiceProvider(MedicalServiceProvider provider)
        {
            string query = $@"INSERT INTO MedicalServiceProvider (MedicalServiceProviderName, MedicalServiceProviderAddress,MedicalServiceProviderEmail,MedicalServiceProviderPhone)"+
                            " VALUES(@name, @address, @email, @phone)";

            Connection con = new Connection();
            SqlConnection scon = con.ConnectDatabase();


            SqlCommand insertCommand = new SqlCommand(query, scon);
            insertCommand.Parameters.AddWithValue("@name",provider.PolicyProviderName);
            insertCommand.Parameters.AddWithValue("@address", provider.PolicyProviderAddresses);
            insertCommand.Parameters.AddWithValue("@email", provider.PolicyProviderEmail);
            insertCommand.Parameters.AddWithValue("@phone", provider.PolicProviderPhone);

            insertCommand.ExecuteNonQuery();
            scon.Close();
            return provider;
        }
        public MedicalServiceProvider updateMedicalServiceProvider(MedicalServiceProvider provider)
        {
            string query = $@"Update MedicalServiceProvider set "+
                "             MedicalServiceProviderName = @name, MedicalServiceProviderAddress = @address, MedicalServiceProviderEmail = @email, MedicalServiceProviderPhone=@phone WHERE MedicalServiceProviderID = @id";

            Connection con = new Connection();
            SqlConnection scon = con.ConnectDatabase();


            SqlCommand insertCommand = new SqlCommand(query, scon);
            insertCommand.Parameters.AddWithValue("@name", provider.PolicyProviderName);
            insertCommand.Parameters.AddWithValue("@address", provider.PolicyProviderAddresses);
            insertCommand.Parameters.AddWithValue("@email", provider.PolicyProviderEmail);
            insertCommand.Parameters.AddWithValue("@phone", provider.PolicProviderPhone);
            insertCommand.Parameters.AddWithValue("@id", provider.PolicyProviderID);

            insertCommand.ExecuteNonQuery();
            scon.Close();
            return provider;
        }
        public MedicalServiceProvider deleteMedicalServiceProvider(int id)
        {
            Connection con = new Connection();
            SqlConnection scon = con.ConnectDatabase();
            SqlCommand com = new SqlCommand("", scon);
            string query = "DELETE from MedicalServiceProviderTreatment WHERE MedicalServiceProviderID = @id";
            com.Parameters.AddWithValue("@id",id);
            com.CommandText = query;
            com.ExecuteNonQuery();

            query = "DELETE from MedicalServiceProvider WHERE MedicalServiceProviderID = @id";
            com.CommandText= query;
            com.ExecuteNonQuery();
            scon.Close();
            return new MedicalServiceProvider();
        }
    }
}
