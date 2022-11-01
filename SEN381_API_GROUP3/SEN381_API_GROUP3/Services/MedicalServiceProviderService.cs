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
            SqlCommand command = new SqlCommand("SELECT * FROM [dbo].[MedicalServiceProvidor] where MedicalServiceProvidorID = " + id, scon);
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
        public void addMedicalServiceProvider(string MedicalServiceProvidorName, string MedicalServiceProvidorAddress, string MedicalServiceProvidorEmail, string MedicalServiceProvidorPhone)
        {
            string query = $@"INSERT INTO MedicalServiceProvider (MedicalServiceProviderName, MedicalServiceProviderAddress,MedicalServiceProviderEmail,MedicalServiceProviderPhone)VALUES('{MedicalServiceProvidorName}', '{MedicalServiceProvidorAddress}', '{MedicalServiceProvidorEmail}', '{MedicalServiceProvidorPhone}')";

            SqlParameter MedicalServiceProvidorname = new SqlParameter("@MedicalServiceProviderName", SqlDbType.VarChar);
            SqlParameter MedicalServiceProvidoraddress = new SqlParameter("@MedicalServiceProviderAddress", SqlDbType.VarChar);
            SqlParameter MedicalServiceProvidoremail = new SqlParameter("@MedicalServiceProviderEmail", SqlDbType.VarChar);
            SqlParameter MedicalServiceProvidorphone = new SqlParameter("@MedicalServiceProviderPhone", SqlDbType.VarChar);

            MedicalServiceProvidorname.Value = MedicalServiceProvidorName.ToString();
            MedicalServiceProvidoraddress.Value = MedicalServiceProvidorAddress.ToString();
            MedicalServiceProvidoremail.Value = MedicalServiceProvidorEmail.ToString();
            MedicalServiceProvidorphone.Value = MedicalServiceProvidorPhone.ToString();

            Connection con = new Connection();
            SqlConnection scon = con.ConnectDatabase();


            SqlCommand insertCommand = new SqlCommand(query, scon);
            insertCommand.Parameters.Add(MedicalServiceProvidorname);
            insertCommand.Parameters.Add(MedicalServiceProvidoraddress);
            insertCommand.Parameters.Add(MedicalServiceProvidorphone);

            insertCommand.ExecuteNonQuery();
            scon.Close();
        }
        public void updateMedicalServiceProvider(int id, string MedicalServiceProvidorName, string MedicalServiceProvidorAddress, string MedicalServiceProvidorEmail, string MedicalServiceProvidorPhone)
        {
            string query = $@"Update MedicalServiceProvidor set MedicalServiceProvidorName = '{MedicalServiceProvidorName}', MedicalServiceProvidorAddress = '{MedicalServiceProvidorAddress}', MedicalServiceProvidorEmail = '{MedicalServiceProvidorEmail}', MedicalServiceProvidorPhone='{MedicalServiceProvidorPhone}' WHERE MedicalServiceProvidorID = '{id}'";

            SqlParameter MedicalServiceProvidorname = new SqlParameter("@MedicalServiceProvidorName", SqlDbType.VarChar);
            SqlParameter MedicalServiceProvidoraddress = new SqlParameter("@MedicalServiceProvidorAddress", SqlDbType.VarChar);
            SqlParameter MedicalServiceProvidoremail = new SqlParameter("@MedicalServiceProvidorEmail", SqlDbType.VarChar);
            SqlParameter MedicalServiceProvidorphone = new SqlParameter("@MedicalServiceProvidorPhone", SqlDbType.VarChar);

            MedicalServiceProvidorname.Value = MedicalServiceProvidorName.ToString();
            MedicalServiceProvidoraddress.Value = MedicalServiceProvidorAddress.ToString();
            MedicalServiceProvidoremail.Value = MedicalServiceProvidorEmail.ToString();
            MedicalServiceProvidorphone.Value = MedicalServiceProvidorPhone.ToString();

            Connection con = new Connection();
            SqlConnection scon = con.ConnectDatabase();


            SqlCommand insertCommand = new SqlCommand(query, scon);
            insertCommand.Parameters.Add(MedicalServiceProvidorname);
            insertCommand.Parameters.Add(MedicalServiceProvidoraddress);
            insertCommand.Parameters.Add(MedicalServiceProvidorphone);

            insertCommand.ExecuteNonQuery();
            scon.Close();
        }
        public void deleteMedicalServiceProvider(int id)
        {
            Connection con = new Connection();
            SqlConnection scon = con.ConnectDatabase();
            string query = $@"DELETE from MedicalServiceProvidor WHERE MedicalServiceProvidorID = '{id}'";
            SqlCommand com = new SqlCommand(query, scon);
            com.ExecuteNonQuery();
            scon.Close();
        }
    }
}
