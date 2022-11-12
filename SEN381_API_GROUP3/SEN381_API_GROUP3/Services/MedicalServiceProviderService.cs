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
            SqlCommand command = new SqlCommand("dbo.GetProviderFromID", scon);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@id",id);
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

            SqlCommand command = new SqlCommand("dbo.GetAllProviders", scon);
            command.CommandType = CommandType.StoredProcedure;

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
            string query = $@"dbo.InsertProvider";

            Connection con = new Connection();
            SqlConnection scon = con.ConnectDatabase();


            SqlCommand insertCommand = new SqlCommand(query, scon);
            insertCommand.CommandType = CommandType.StoredProcedure;

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
            string query = $@"dbo.UpdateProvider";

            Connection con = new Connection();
            SqlConnection scon = con.ConnectDatabase();

            SqlCommand insertCommand = new SqlCommand(query, scon);
            insertCommand.CommandType = CommandType.StoredProcedure;

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

            string query = "dbo.DeleteProvider";
            SqlCommand com = new SqlCommand(query, scon);
            com.CommandType = CommandType.StoredProcedure;

            com.Parameters.AddWithValue("@id",id);
            com.ExecuteNonQuery();

            scon.Close();
            return new MedicalServiceProvider();
        }
    }
}
