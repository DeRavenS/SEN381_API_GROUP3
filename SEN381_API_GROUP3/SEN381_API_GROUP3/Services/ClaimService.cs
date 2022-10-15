using SEN381_API_Group3.shared.models;
using SEN381_API_GROUP3.Database;
using System.Data.SqlClient;

namespace SEN381_API_GROUP3.Services
{
    public class ClaimService
    {

        public List<Claim> getClaims(int page, int size)
        {

            int offset = (page - 1) * size;
            List<Claim> modules = new List<Claim>();
            Connection con = new Connection();
            SqlConnection scon = con.ConnectDatabase();

            SqlCommand command = new SqlCommand("SELECT * FROM [dbo].[Claim] ORDER BY CLAIMID OFFSET @offset ROWS FETCH NEXT @size ROWS ONLY;", scon);
            command.Parameters.AddWithValue("@offset", offset);
            command.Parameters.AddWithValue("@size", size);
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    modules.Add(new Claim(reader.GetInt32(0).ToString(), reader.GetInt32(1).ToString(), reader.GetInt32(2).ToString(), reader.GetString(3), reader.GetInt32(4).ToString(), reader.GetString(5)));


                }
            }


            return modules;
        }

    }
}
